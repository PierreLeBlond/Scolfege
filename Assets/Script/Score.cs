using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text numberOfChordText;
	
	public Text scoreText;

	private int _score = 0;
	private int _numberOfChord;
	private int _noteCombo = 0;
	private int _scoreValue = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateScore()
	{
		scoreText.text = _score.ToString();
		numberOfChordText.text = "combo : " + _noteCombo.ToString();
	}

	public void oneMoreChord(){
		_numberOfChord++;
	}

	public int getScore(){
		return _score;
	}

	public void setScore(int score){
		_score = score;
	}

	public void setNoteCombo(int combo){
		_noteCombo = combo;
	}

	public void setScoreValue(int value){
		_scoreValue = value;
	}

	public void OnTriggerEnter2D(Collider2D intruder)
	{
		if (intruder.CompareTag("Note"))
		{
			_score += _scoreValue;
			updateScore();
		}
	}

	 

}
