using UnityEngine;
using System.Collections;
using System;

class Repeater
{
	const float threshold = 0.5f;
	const float rate = 0.25f;
	float _next;
	bool _isHeld;
	string _axis;

	public Repeater (string axisName)
	{
		_axis = axisName;
	}

	public int Update ()
	{
		int retValue = 0;
		int value = Mathf.RoundToInt (Input.GetAxisRaw (_axis));

		if (value != 0) {
			if (Time.time > _next) {
				retValue = value;
				_next = Time.time + (_isHeld ? rate : threshold);
				_isHeld = true;
			}
		} else {
			_isHeld = false;
			_next = 0;
		}

		return retValue;
	}

}

public class InputController : MonoBehaviour
{
	Repeater _horizontal = new Repeater ("Horizontal");
	Repeater _vertical = new Repeater ("Vertical");
	public static event EventHandler<InfoEventArgs<Point>> moveEvent;
	public static event EventHandler<InfoEventArgs<int>> fireEvent;
	public static event EventHandler<InfoEventArgs<KeyCode>> keyEvent;
	string[] _buttons = new string[] {"Fire1","Fire2","Fire3"};
	KeyCode[] _buttonKey = new KeyCode[] {KeyCode.Q,KeyCode.E};

	void Update ()
	{
		int x = _horizontal.Update ();
		int y = _vertical.Update ();

		if (x != 0 || y != 0) {
			if (moveEvent != null) {
				moveEvent (this, new InfoEventArgs<Point> (new Point (x, y)));
			}
		}

		for (int i = 0; i < _buttons.Length; ++i) {
			if (Input.GetButtonUp (_buttons [i])) {
				if (fireEvent != null)
					fireEvent (this, new InfoEventArgs<int> (i));
			}
		}

		for (int k = 0; k < _buttonKey.Length; ++k) {
			if (Input.GetKeyDown (_buttonKey [k])) {
				if (keyEvent != null) {
					keyEvent (this, new InfoEventArgs<KeyCode> (_buttonKey [k]));
				}
			}
		}
	}

}
