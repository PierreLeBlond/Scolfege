using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutoNoteGameManager : MonoBehaviour {

	public TutoNotePlayerController player;

	public Text noteText;

	public Key keyPrefab;
	private Key _key;

	private int _currentNote;

	private KeyEnum key = KeyEnum.GKey;
	
	// Use this for initialization
	void Start () {

		_key = Instantiate (keyPrefab) as Key;
		_key.transform.localPosition = new Vector3 (-8f, -3.3f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		_currentNote = player.getCurrentNoteId ();
		noteText.text = Notes.getString(_currentNote, key);
	}

	public void changeKey(){
		if (key == KeyEnum.FKey)
		{
			key = KeyEnum.GKey;
			_key.setGKey();
		}
		else
		{
			key = KeyEnum.FKey;
			_key.setFKey();
		}
		noteText.text = Notes.getString(_currentNote, key);
	}

	public void quitGame()
	{
		Application.LoadLevel("_MainMenu");
	}
}
