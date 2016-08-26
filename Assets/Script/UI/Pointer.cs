using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pointer : MonoBehaviour {

	public GameObject finger;
	public GameObject direction;

	private Button _button;
	private RectTransform _buttonRect;

	public void setButton(Button button)
	{
		_button = button;
		_buttonRect = _button.GetComponent<RectTransform>();
	}

	public void reload()
	{
		transform.position = _button.transform.position;
	}
}
