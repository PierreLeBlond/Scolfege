using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfilManager : MonoBehaviour {

	public Text noteGameNumber;
	public Text noteGameTop;
	public Text noteGameMean;

	public Text soundGameNumber;
	public Text soundGameTop;
	public Text soundGameMean;

	// Use this for initialization
	void Start () {
		noteGameNumber.text = UserManager.Instance.getNumberOfNoteGame ().ToString ();
		noteGameTop.text = UserManager.Instance.getTopNoteGameScore().ToString ();
		noteGameMean.text = UserManager.Instance.getMeanNoteGameScore().ToString ();

		soundGameNumber.text = UserManager.Instance.getNumberOfSoundGame ().ToString ();
		soundGameTop.text = UserManager.Instance.getTopSoundGameScore().ToString ();
		soundGameMean.text = UserManager.Instance.getMeanSoundGameScore().ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			quitGame();
		}
	}

	public void quitGame()
	{
		Application.LoadLevel("_MainMenu");
	}
}
