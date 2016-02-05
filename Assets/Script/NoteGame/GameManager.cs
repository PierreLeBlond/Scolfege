using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Line linePrefab;
	public Square squarePrefab;
	public Chord chordPrefab;
	public Life lifePrefab;
    public NoteGamePlayerController player;

	public Key keyPrefab;

	public Bar barPrefab;

    public Text noteText;

	public bool pause = false;

	public Score _score;

	private int lifeCombo = 0;

	private int noteCombo = 0;

	private int scoreValue = 50;

	private Life[] _life;

	private int life = 12;

	private int level;

	private List<Line> _lines = new List<Line>();
	private Square _square;
	private Chord _chord;
	private Key _key;
	private Bar _bar;

    private int _numberOfChord = 0;

    private int _rightNoteId;

	private KeyEnum key = KeyEnum.GKey;



	// Use this for initialization
	void Start () {

		_life = new Life[3];
		for (int i = 0; i < 3; i++) {
			Life life = Instantiate(lifePrefab) as Life;
			life.transform.localPosition = new Vector3(5f + i, 4f, 0f);
			_life[i] = life;
		}

		_key = Instantiate (keyPrefab) as Key;
		_key.transform.localPosition = new Vector3 (-6f, -3.3f, 0f);

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
        _score.oneMoreChord();
        _chord = Instantiate (chordPrefab) as Chord;
		_bar = Instantiate (barPrefab) as Bar;
		//_chord.transform.parent = playGround.transform;
		switch (level) {
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
			_chord.speed = new Vector2(5f + 0.2f*(6f-(float)level), 0f);
			_bar.speed = new Vector2(2f, 0f);
			break;
		}
  
        _rightNoteId = _chord.getRightNoteId();
        noteText.text = Notes.getString(_rightNoteId, key);
    }
	
	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			quitGame ();
		}
        if (player.hasANote())
        {
            int chosenNoteId = player.getChosenNoteId();

			switch(_chord.checkPickedNote(chosenNoteId)){
			case (Chord.Result.None) :
				lifeCombo = 0;
				noteCombo = 0;
				scoreValue = 50;
				player.getAvatar().showAss();
				removeLife();
				break;
			case (Chord.Result.Win) :
				lifeCombo++;
				if(lifeCombo >= 5){
					addPartLife();
					lifeCombo = 0;
				}
				noteCombo++;
				switch(noteCombo){
				case 5 :
					scoreValue = 100;
					break;
				case 10 :
					level++;
					scoreValue = 150;
					break;
				case 15:
					scoreValue = 200;
					break;
				case 20:
					scoreValue = 250;
					break;
				case 30:
					scoreValue = 300;
					break;
				default:
					if(noteCombo / 10 >= 4 && noteCombo % 10 == 0){
						scoreValue+=50;
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
				lifeCombo = 0;
				noteCombo = 0;
				scoreValue = 50;
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
            player.setHasANote(false);

			_score.setScoreValue(scoreValue);
			_score.setNoteCombo(noteCombo);
		}

		if (Input.GetKeyDown (KeyCode.R) || _chord.transform.position.x < -10f) {
			Destroy (_chord.gameObject);
			Destroy (_bar.gameObject);
			generateChord();    
		}

        _score.updateScore();


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
		noteText.text = Notes.getString(_rightNoteId, key);
	}

	public void addPartLife(){
		if (life < 12) {
			life++;
			setLife ();
		}
	}

	public void removeLife(){
		life -= 4;
		setLife ();
	}

	public void setLife(){
		for (int i = 0; i < 3; i++) {
			if(life >= (i+1)*4){
				_life[i].setPartId(4);
				_life[i].setParts();
			}else if( life > i*4 && life < (i+1)*4){
				_life[i].setPartId(life%4);
				_life[i].setParts();
			}else{
				_life[i].setPartId(0);
				_life[i].setParts();
			}
		}
	}


	public void quitGame()
	{
		UserManager.Instance.addNoteGameScore(_score.getScore());
		Application.LoadLevel("_MainMenu");
	}
}
