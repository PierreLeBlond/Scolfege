using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text scoreAddPrefab;

	public Text comboText;
	public Text scoreText;
	public Text multiplicatorText;

	public int comboThreshold = 2;

	private int _score = 0;
	private int _numberOfChord;
	private int _noteCombo = 0;
	private int _scoreValue = 50;
	private int _scoreMultiplicator = 1;
	private int _comboMultiplicator = 1;

	private int _defaultSize = 50;

	private bool _blob = false;
	private int _blobTarget = 50;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(_blob)
		{
			if(multiplicatorText.fontSize < _blobTarget + 5)
				multiplicatorText.fontSize++;
			else
				_blob = false;
		}
		else if(multiplicatorText.fontSize > _blobTarget)
				multiplicatorText.fontSize--;

	}

	public void updateScore()
	{
		if(_score == 0)
		{
			scoreText.text = "0000000";
		}
		else
		{
			int score = _score;
			scoreText.text = "";
			while(score < 1000000)
			{
				scoreText.text += "0";
				score *= 10;
			}
			scoreText.text += _score.ToString();
		}
		comboText.text = "combo : " + _noteCombo.ToString();
		multiplicatorText.text = "X" + _scoreMultiplicator * _comboMultiplicator;
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

	public void addCombo(){
		_noteCombo++;
		if(_noteCombo > 0 && _noteCombo%comboThreshold == 0)
		{
			addComboMultiplicator();
		}
	}

	public void addComboMultiplicator(){
		_comboMultiplicator++;
		Color color;
		switch(_comboMultiplicator)
		{
			case 1 :
			color = Color.white;
			break;
			case 2 :
			color = Color.cyan;
			break;
			case 3 :
			color = Color.blue;
			break;
			case 4 :
			color = Color.yellow;
			break;
			case 5 :
			color = Color.magenta;
			break;
			default :
			color = Color.red;
			break;
		}
		multiplicatorText.color = color;
		_blobTarget = _defaultSize + _comboMultiplicator*5;
		_blob = true;
	}

	public void resetComboMultiplicator(){
		_comboMultiplicator = 1;
		_scoreMultiplicator = 1;
		multiplicatorText.color = Color.white;
		multiplicatorText.fontSize = _defaultSize;
	}

	public void resetCombo(){
		_noteCombo = 0;
		resetComboMultiplicator();
	}

	public void setScoreValue(int value){
		_scoreValue = value;
	}

	public void setScoreMultiplicator(int value){
		_scoreMultiplicator = value;
	}

	public void generateAddScore(int addValue){
		Text text = Instantiate (scoreAddPrefab) as Text;
		text.text = "+" + addValue;
		text.GetComponent<FadableToDeath>().startFadingToDeath();
		text.transform.SetParent(transform, false);
	}

	public void OnTriggerEnter2D(Collider2D intruder)
	{
		if (intruder.CompareTag("Note"))
		{
			int addValue = _scoreValue*_scoreMultiplicator*_comboMultiplicator;
			_score += addValue;
			generateAddScore(addValue);
			updateScore();
		}
		if(intruder.CompareTag("Bonus"))
		{
			Bonus bonus = intruder.GetComponent<Bonus>();
			int type = bonus.getBonusType();
			if(type == 0 || type == 1){
				_score += 200;
				generateAddScore(200);
			}
			else if(type == 2){
				_scoreMultiplicator = 2;
			}
			else if(type == 3){
				_scoreMultiplicator = 3;
			}
			else if(type == 4){
				_scoreMultiplicator = 4;
			}
			updateScore();
		}
	}
}
