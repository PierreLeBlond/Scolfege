using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchZone : MonoBehaviour {

	public Pointer pointer;
	public Button button;

	public GameObject defaultSprite;

	private BlinkingButton _animation;

	public void Start() {
		pointer.setButton(button);
		pointer.gameObject.SetActive(false);
		_animation = button.GetComponent<BlinkingButton>();
		gameObject.SetActive(false);
		defaultSprite.SetActive(false);
		defaultSprite.transform.position = button.transform.position;
	}

	public void showHelp() {
		defaultSprite.SetActive(true);
	}

	public void hideHelp() {
		defaultSprite.SetActive(false);
	}

	public void showUberHelp() {
		pointer.gameObject.SetActive(true);
		pointer.reload();
		//_animation.play();
	}

	public void hideUberHelp() {
		pointer.gameObject.SetActive(false);
		_animation.stop();
		_animation.hide();
	}
}
