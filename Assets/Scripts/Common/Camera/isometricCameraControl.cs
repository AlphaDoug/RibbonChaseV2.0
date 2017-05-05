/*********************************
* isoCameraControl.cs by Deozaan
* http://deozaan.com/
* http://twitter.com/Deozaan
 * 
 * Just attach this script to the Camera and it follows focusTarget around.
 * It provides functions to rotate the camera in 45 or 90 degree increments as
 * well as providing different levels of zoom.
 * 
 * Note that this requires the camera to be set to Orthographic mode and will
 * automatically change it to Orthographic mode.
*********************************/

using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Isometric Camera")]

public class isometricCameraControl : MonoBehaviour
{



	public Transform focusTarget; // we look at and follow this
	[HideInInspector]
	public float
		distance = 100.0f; // not even sure if this should be customizable...
	public bool invertRotation = false; // quick way to make the "rotate left" button rotate to the right, and vice versa.
	public bool invertZoom = false; // quick way to make the "zoom in" button zoom out, and vice versa.
	public bool rotateInstantly = false; // if true then instantly rotate to position
	public bool zoomInstantly = false; // if true then instantly set zoom amount
	public bool rotateBy45 = false;
	public float[] zoomSizes = new float[] { 12.21f};
	public float rotationDamping = 6.0f; // a higher value will rotate the camera faster
	public float zoomDamping = 6.0f; // a higher value will zoom the camera faster
	public bool offsetStartingRotationBy45 = true; // this defaults to the standard isometric viewpoint
	public bool moveToPositionInstantly = true; // if true then instantly move to position

	// private variables
	private float angle = 45.0f; // angle which the camera looks down on the scene. 45 degrees is true isometric.
	private float rotationOffset = 315.0f;
	private Quaternion targetRotation;
	private float targetZoom;
	private int zoomSizesIndex = 0;
	private Transform camContainer;

	void Start ()
	{
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 30;
		// create a new container object to make camera rotation/movement easier
		//camContainer = GameObject.Find("camContainer").transform;
		camContainer = new GameObject ("camContainer").transform;
		camContainer.position = new Vector3 (0, 0, 0);
		transform.parent = camContainer;

		// check to make sure we have something to focus on
		if (!focusTarget) {
			Debug.LogWarning ("WARNING: Nothing specified to look at. Setting focustTarget to camContainer.");
			focusTarget = camContainer;
		}

		// make sure we're using an orthographic camera
		if (Camera.main.orthographic == false) {
			Camera.main.orthographic = true;
		}

		// initialize starting rotation, zoom, and position
		transform.rotation = Quaternion.Euler (angle, rotationOffset, 0f);
		targetRotation = camContainer.rotation;
		targetZoom = zoomSizes [zoomSizesIndex];
		setCamPos ();

		setContainerPos (true);
		rotateCam (true);
		zoomCam (true);

		// this next part runs if we don't want to default to a 45 degree angle offset
		if (!offsetStartingRotationBy45) {

			targetRotation.eulerAngles = new Vector3 (targetRotation.eulerAngles.x, targetRotation.eulerAngles.y - 45, targetRotation.eulerAngles.z);
			rotateCam (true);
		}


	}



	void LateUpdate ()
	{
		setContainerPos (moveToPositionInstantly);
		rotateCam (rotateInstantly);
		zoomCam (zoomInstantly);
	}

	void rotateCam (bool instant)
	{
		// this is where the rotation actually takes place
		if (targetRotation != camContainer.rotation) {
			if (instant) { // do it instantly
				camContainer.rotation = targetRotation;
			} else {
				// I don't understand this, but I know it rotates the camera smoothly over time. :)
				camContainer.rotation = Quaternion.Slerp (camContainer.rotation, targetRotation, Time.deltaTime * rotationDamping);
				// check for when we're really close to our target rotation and if so, just finish the rotation
				if (Mathf.Abs (camContainer.rotation.eulerAngles.y - targetRotation.eulerAngles.y) < 0.1) {
					targetRotation.eulerAngles = new Vector3 (targetRotation.eulerAngles.x, Mathf.RoundToInt (targetRotation.eulerAngles.y), targetRotation.eulerAngles.z);
					camContainer.eulerAngles = new Vector3 (targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
				}
			}
		}
	}

	// This is where we update the desired rotation based upon player input
	void setRotationTarget (bool rotateRight = true)
	{
		int sign;
		int amount = -90;

		if (rotateRight) {
			sign = 1;
		} else {
			sign = -1;
		}

		if (rotateBy45) {
			amount = -45;
		}
		if (invertRotation) {
			amount *= -1;
		}

		targetRotation.eulerAngles = new Vector3 (targetRotation.eulerAngles.x, targetRotation.eulerAngles.y + amount * sign, targetRotation.eulerAngles.z);
	}

	void rotateLeft ()
	{
		setRotationTarget (false);
	}

	void rotateRight ()
	{
		setRotationTarget (true);
	}


	void zoomCam (bool instant)
	{
		float camZoom = Camera.main.orthographicSize;
		float diff;

		if (!instant) { // change the zoom over time smoothly.
			if (targetZoom != camZoom) {
				diff = targetZoom - camZoom;
				Camera.main.orthographicSize += diff * Time.deltaTime * zoomDamping;
				if (Mathf.Abs (diff) < 0.01) {
					Camera.main.orthographicSize = targetZoom;
				}
			}
		} else { // instantly change the "zoom"
			Camera.main.orthographicSize = targetZoom;
		}
	}

	void setZoomTarget (bool zoomIn)
	{
		if (zoomIn) {
			zoomSizesIndex -= 1;
		} else {
			zoomSizesIndex += 1;
		}

		// Mathf.Repeat is similar to the modulo operator, but it works with values below 0.
		zoomSizesIndex = (int)Mathf.Repeat (zoomSizesIndex, zoomSizes.Length);
		targetZoom = zoomSizes [zoomSizesIndex];
	}

	void zoomCamIn ()
	{
		if (invertZoom) {
			setZoomTarget (false);
		} else {
			setZoomTarget (true);
		}
	}

	void zoomCamOut ()
	{
		if (invertZoom) {
			setZoomTarget (true);
		} else {
			setZoomTarget (false);
		}
	}

	void setContainerPos (bool instant)
	{
		if (camContainer.position != focusTarget.position) {
			if (instant) {
				camContainer.position = focusTarget.position;
			} else {
				Debug.Log ("We haven't yet set up a smooth cam position so we're not doing it smoothly.");
				camContainer.position = focusTarget.position;
			}
		}
	}

	void setCamPos ()
	{
		float dist = FindDist ();
		float y = FindHeight ();
		float diag = Mathf.Sqrt ((dist * dist) * 2) / 2;

		var pos = new Vector3 (diag, y, -diag) + camContainer.position;
		transform.position = pos;
		//focusLastPos = camContainer.position; // No longer needed?
	}

	// x (on 2D xy plane)
	float FindDist ()
	{
		// Don't be a fool like me and forget that math functions use radians.
		var rads = angle * Mathf.Deg2Rad;
		var dist = Mathf.Sin (rads) * distance;
		return dist;
	}

	// y (on 2D xy plane)
	float FindHeight ()
	{
		var rads = angle * Mathf.Deg2Rad;
		var height = Mathf.Cos (rads) * distance;
		return height;
	}



	void HandlekeyEvent (object sender, InfoEventArgs<KeyCode> e)
	{
		switch (e.info) {
		case(KeyCode.Q):
			rotateLeft ();
			break;

		case (KeyCode.E):
			rotateRight ();
			break;
			
		}
	}

	void OnEnable ()
	{
		InputController.keyEvent += HandlekeyEvent;
	}

	void OnDisable ()
	{
		InputController.keyEvent -= HandlekeyEvent;
	}
}
