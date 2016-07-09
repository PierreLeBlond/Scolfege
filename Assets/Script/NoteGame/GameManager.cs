using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//Prefab
	public Line linePrefab;
	public Square squarePrefab;
	public Chord chordPrefab;
	public Life lifePrefab;
	public Key keyPrefab;
	public Bar barPrefab;

	//Public
	public NoteGamePlayerController player;

    public Text noteText;
	public bool pause = false;
	public Score score;

	//Private
	private int _lifeCombo = 0;
	private int _noteCombo = 0;
	private int _scoreValue = 50;
	private Life[] _lifes;
	private int _lifeId = 12;
	private int _level;

	private List<Line> _lines = new List<Line>();
	private Square _square;
	private Chord _chord;
	private Key _keyUI;
	private Bar _bar;

	private int _numberOfChord = 0;
    private int _rightNoteId;
	private KeyEnum _key = KeyEnum.GKey;



	// Use this for initialization
	void Start () {

		_lifes = new Life[3];
		for (int i = 0; i < 3; i++) {
			Life life = Instantiate(lifePrefab) as Life;
			life.transform.localPosition = new Vector3(5f + i, 4f, 0f);
			_lifes[i] = life;
		}

		_keyUI = Instantiate (keyPrefab) as Key;
		_keyUI.transform.localPosition = new Vector3 (-6f, -3.3f, 0f);

		Line line;
		for (int i = 0; i < 5; i++) {
			line = Instantiate (linePrefab) as Line;
			line.transform.localPosition = new Vector3(1.0f, (i-2)*1.0f, 0.0f);
            //line.transform.parent = forGround.parent;
			_lines.Add(line);
		}

        player.setCurrentNoteId(6);
        //player.transform.parent = playGround;
        player.transform.localPosition = new Vector3(-2, -1, 0);

		generateChord ();
	}

	void generateChord () {
        score.oneMoreChord();
        _chord = Instantiate (chordPrefab) as Chord;
		_bar = Instantiate (barPrefab) as Bar;
		//_chord.transform.parent = playGround.transform;
		switch (_level) {
		case 0:
			_chord.generateNotes(0);
			_chord.speed = new Vector2(2f, 0f);
			_bar.speed = new Vector2(2f, 0f);
			break;
		case 1:
			_chord.generateNotes(1);
			_chord.speed = new Vector2(2f, 0f);
			_bar.speed = new Vector2(2f, 0f);
			break;
		case 2:
			_chord.generateNotes(1);
			_chord.speed = new Vector2(4f, 0f);
			_bar.speed = new Vector2(2f, 0f);
			break;
		case 3:
			_chord.generateNotes(2);
			_chord.speed = new Vector2(4f, 0f);
			_bar.speed = new Vector2(2f, 0f);
			break;
		case 4:
			_chord.generateNotes(2);
			_chord.speed = new Vector2(5f, 0f);
			_bar.speed = new Vector2(2f, 0f);
			break;
		default:
			_chord.generateNotes(2);
			_chord.speed = new Vector2(5f + 0.2f*(6f-(float)_level), 0f);
			_bar.speed = new Vector2(2f, 0f);
			break;
		}
  
        _rightNoteId = _chord.getRightNoteId();
        noteText.text = Notes.getString(_rightNoteId, _key);
    }
	
	// Update is called once per frame
	void Update () {
		if (_lifeId <= 0) {
			//TODO GameOver
			quitGame ();
		}
        if (player.hasChoosenANote())
        {
            int chosenNoteId = player.getChosenNoteId();

			switch(_chord.checkPickedNote(chosenNoteId)){
			case (Chord.Result.None) :
				_lifeCombo = 0;
				_noteCombo = 0;
				_scoreValue = 50;
				player.getAvatar().showAss();
				removeLife();
				break;
			case (Chord.Result.Win) :
				_lifeCombo++;
				if(_lifeCombo >= 5){
					addPartLife();
					_lifeCombo = 0;
				}
				_noteCombo++;
				switch(_noteCombo){
				case 5 :
					_scoreValue = 100;
					break;
				case 10 :
					_level++;
					_scoreValue = 150;
					break;
				case 15:
					_scoreValue = 200;
					break;
				case 20:
					_scoreValue = 250;
					break;
				case 30:
					_scoreValue = 300;
					break;
				default:
					if(_noteCombo / 10 >= 4 && _noteCombo % 10 == 0){
						_scoreValue+=50;
					}
					break;
				}
				/*if (_score.getScore() + 1 == 0)
				{
					player.getAvatar().setIsWinning(true);
				}*/
				player.getAvatar().win();
				break;
			case (Chord.Result.Loose) :
				_lifeCombo = 0;
				_noteCombo = 0;
				_scoreValue = 50;
				removeLife();
				/*if (_score.getScore() < 0)
				{
					player.getAvatar().setIsWinning(false);
				}*/
				player.getAvatar().loose();
				_chord.speed = new Vector2(1.5f, 0f);
				break;
			default:
				break;
			}
            player.setHasChoosenNote(false);

			score.setScoreValue(_scoreValue);
			score.setNoteCombo(_noteCombo);
		}

		if (Input.GetKeyDown (KeyCode.R) || _chord.transform.position.x < -10f) {
			Destroy (_chord.gameObject);
			Destroy (_bar.gameObject);
			generateChord();    
		}

        score.updateScore();


		if(Input.GetKey(KeyCode.Escape)){
			quitGame();
		}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("coucou");
            if (pause)
            {
                pause = false;
                _chord.pause = false;
            }
            else
            {
                pause = true;
                _chord.pause = true;
            }
        }

	}

	public void changeKey(){
		if (_key == KeyEnum.FKey)
		{
			_key = KeyEnum.GKey;
			_keyUI.setGKey();
		}
		else
		{
			_key = KeyEnum.FKey;
			_keyUI.setFKey();
		}
		noteText.text = Notes.getString(_rightNoteId, _key);
	}

	public void addPartLife(){
		if (_lifeId < 12) {
			_lifeId++;
			setLife ();
		}
	}

	public void removeLife(){
		_lifeId -= 4;
		setLife ();
	}

	public void setLife(){
		for (int i = 0; i < 3; i++) {
			if(_lifeId >= (i+1)*4){
				_lifes[i].setPartId(4);
				_lifes[i].setParts();
			}else if( _lifeId > i*4 && _lifeId < (i+1)*4){
				_lifes[i].setPartId(_lifeId%4);
				_lifes[i].setParts();
			}else{
				_lifes[i].setPartId(0);
				_lifes[i].setParts();
			}
		}
	}


	public void quitGame()
	{
		UserManager.Instance.addNoteGameScore(score.getScore());
		Application.LoadLevel("_MainMenu");
	}
}
