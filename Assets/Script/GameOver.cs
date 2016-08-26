using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Text tryAgainText;
	public Button tryAgainButton;
	public SpriteRenderer panel;
	public AudioSource audioSource;

	public GameObject canvas;

	private bool _show;
	private float _showTime;

	// Use this for initialization
	void Start () {
		canvas.SetActive(false);
	}

	// Update is called once per frame
	/*void FixedUpdate () {
		if(_show)
		{
			float time = Time.time - _showTime;
			panel.color = new Color(1.0f, 1.0f, 1.0f, time / 3.0f) ;
			tryAgainText.color = new Color(1.0f, 1.0f, 1.0f, time / 3.0f) ;
			if(time > 3.0f)
			{
				_show = false;
			}
		}
	}*/

	public void show () {
		/*_show = true;
		_showTime = Time.time;
		tryAgainButton.interactable = true;*/
		canvas.SetActive(true);
		audioSource.Play();
	}

	public void restart(){
		Application.LoadLevel("_NoteGame");
	}

	public void returnToMenu(){
		Application.LoadLevel("_Menu");
	}

	public void quit(){
		Application.Quit();
	}
}
