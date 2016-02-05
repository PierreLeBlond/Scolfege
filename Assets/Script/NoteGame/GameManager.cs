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

	public int noteCombo = 0;

	private Life[] _life;

	private int life = 12;

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

		Line line;
		for (int i = 0; i < 5; i++) {
			line = Instantiate (linePrefab) as Line;
			line.transform.localPosition = new Vector3(1.0f, (i-1)*1.0f, 0.0f);
            //line.transform.parent = forGround.parent;
			_lines.Add(line);
		}

        player.setCurrentNoteId(6);
        //player.transform.parent = playGround;
        player.transform.localPosition = new Vector3(-2, 0, 0);

		generateChord ();
	}

	void generateChord () {
        _score.oneMoreChord();
        _chord = Instantiate (chordPrefab) as Chord;
		_bar = Instantiate (barPrefab) as Bar;
		//_chord.transform.parent = playGround.transform;
        _chord.generateNotes();
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
				noteCombo = 0;
				player.getAvatar().showAss();
				removeLife();
				break;
			case (Chord.Result.Win) :
				noteCombo++;
				if(noteCombo >= 2){
					addPartLife();
					noteCombo = 0;
				}
				if (_score.getScore() + 1 == 0)
				{
					player.getAvatar().setIsWinning(true);
				}
				player.getAvatar().win();
				break;
			case (Chord.Result.Loose) :
				noteCombo = 0;
				removeLife();
				_score.setScore(_score.getScore()-1);
				if (_score.getScore() < 0)
				{
					player.getAvatar().setIsWinning(false);
				}
				player.getAvatar().loose();
				break;
			default:
				break;
			}
			_chord.speed = new Vector2(1.5f, 0f);
            player.setHasANote(false);
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
