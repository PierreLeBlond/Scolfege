using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Info : MonoBehaviour {

	public Text noteName;
	public Button noteButton;
	public Image noteSprite;

	private Gameplay _gameplay;

	public Piano piano;

	private int _note = 0;



	// Use this for initialization
	void Start () {
		noteName.enabled = false;
		noteSprite.enabled = false;
		noteButton.interactable = false;
	}

	// Update is called once per frame
	void Update () {
		if(_gameplay.sound && Input.GetKeyDown(KeyCode.Space)){
			playNote();
		}
	}

	public void setNote(int note){
		_note = note;
		refresh();
	}

	public void refresh(){
		noteName.text = Notes.getString(_note, _gameplay.key);
		noteName.enabled = false;
		noteSprite.enabled = false;
		noteButton.interactable = false;
		if(_gameplay.text)
			noteName.enabled = true;
		else if(_gameplay.sound){
			noteSprite.enabled = true;
			noteButton.interactable = true;
		}
	}

	public void playNote(){
		piano.playNote(_note, _gameplay.key);
	}

	public void setGameplay(Gameplay gameplay){
		_gameplay = gameplay;
		refresh();
	}
}
