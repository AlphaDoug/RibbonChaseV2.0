using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private UnityEngine.AI.NavMeshAgent _agent;
	private Ray _ray;
	private RaycastHit _hit;
	[SerializeField]
	private Vector3
		_target;
	private bool _isTargetSet;

	// Use this for initialization
	void Start ()
	{
		_agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		_isTargetSet = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		MoveAgent ();
	
	}

	void MoveAgent ()
	{
		var distanceLeft = Vector3.Distance (transform.position, _target);
		if (transform.position != _target && distanceLeft > _agent.stoppingDistance && _isTargetSet) {
			_agent.enabled = true;
			_agent.SetDestination (_target);
		} else {
			_isTargetSet = false;
			_agent.enabled = false;
		}
	}

	void OnEnable ()
	{
		InputController.fireEvent += HandlefireEvent;
	}

	void HandlefireEvent (object sender, InfoEventArgs<int> e)
	{
		switch (e.info) {
		case(0):
			_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (_ray, out _hit)) {
				Debug.Log ("Hit");
				if (_hit.transform.tag == "Ground") {
					_isTargetSet = true;
					_target = _hit.point;
				}
			}
			break;
		
		}

		
	}
	void OnDisable ()
	{
		InputController.fireEvent -= HandlefireEvent;
	}
}
