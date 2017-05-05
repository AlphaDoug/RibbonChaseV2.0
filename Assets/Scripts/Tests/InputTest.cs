using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnFireEvent (object sender, InfoEventArgs<int> e)
	{
		Debug.Log ("Fire: " + e.info);
	}

	void OnMoveEvent (object sender, InfoEventArgs<Point> e)
	{
		Debug.Log ("Move " + e.info.ToString ());
	}

	void OnKeyEvent (object sender, InfoEventArgs<KeyCode> e)
	{
		Debug.Log ("key " + e.info);
	}

	void OnEnable ()
	{
		InputController.fireEvent += OnFireEvent;
		InputController.moveEvent += OnMoveEvent;
		InputController.keyEvent += OnKeyEvent;

	}

	void OnDisable ()
	{
		InputController.fireEvent -= OnFireEvent;
		InputController.moveEvent -= OnMoveEvent;
		InputController.keyEvent -= OnKeyEvent;
	}
}
