using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text numberOfChordText;
	
	public Text scoreText;

	private int _score = 0;
	private int _numberOfChord;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateScore()
	{
		scoreText.text = _score.ToString();
		if (_score < 0)
		{
			scoreText.color = Color.red;
		}
		else
		{
			scoreText.color = Color.white;
		}
		numberOfChordText.text = "/" + _numberOfChord.ToString();
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

	public void OnTriggerEnter2D(Collider2D intruder)
	{
		Debug.Log ("Ook !");
		if (intruder.CompareTag("Note"))
		{
			Debug.Log ("Ook ?");
			_score++;
			updateScore();
		}
	}

	 

}
