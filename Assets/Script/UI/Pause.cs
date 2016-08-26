using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public GameObject canvas;
	public Image image;
	public Button[] buttons;

	// Use this for initialization
	void Start () {
		canvas.SetActive(false);
	}

	public void show(){
		canvas.SetActive(true);
		/*image.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		image.CrossFadeAlpha(1.0f, 0.25f, false);
		foreach(Button button in buttons)
		{
			button.GetComponent<Image>().GetComponent<CanvasRenderer>().SetAlpha(0.0f);
			button.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, false);
		}
		yield return new WaitForSeconds(1.0f);*/
		StartCoroutine(pause());
	}

	public IEnumerator pause(){
		yield return new WaitForEndOfFrame();
		Time.timeScale = 0.0f;
	}

	public void resume(){
		Time.timeScale = 1.0f;
		canvas.SetActive(false);
	}

	public void restart(){
		Time.timeScale = 1.0f;
		Application.LoadLevel("_NoteGame");
	}

	public void quitToMenu(){
		Time.timeScale = 1.0f;
		Application.LoadLevel("_Menu");
	}

	public void quit(){
		Application.Quit();
	}

	/*public IEnumerator hideCoroutine(){
		Time.timeScale = 1.0f;
		image.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		image.CrossFadeAlpha(0.0f, 0.5f, false);
		foreach(Button button in buttons)
		{
			button.GetComponent<Image>().GetComponent<CanvasRenderer>().SetAlpha(1.0f);
			button.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.25f, false);
		}
		yield return new WaitForSeconds(0.5f);
		canvas.SetActive(false);
	}*/

}
