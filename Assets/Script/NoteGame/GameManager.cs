using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Line linePrefab;
	public Square squarePrefab;
	public Chord chordPrefab;
    public NoteGamePlayerController player;

	public Key keyPrefab;

	public Transform backGround;
	public Transform playGround;
    public Transform forGround;

    public Text noteText;
    public Text numberOfChordText;
	
    public Text scoreText;

    private int _score = 0;

	private List<Line> _lines = new List<Line>();
	private Square _square;
	private Chord _chord;
	private Key _key;

    private int _numberOfChord = 0;

    private int _rightNoteId;

	// Use this for initialization
	void Start () {

		_key = Instantiate (keyPrefab) as Key;

		Line line;
		for (int i = 0; i < 5; i++) {
			line = Instantiate (linePrefab) as Line;
			line.transform.localPosition = new Vector3(1.0f, (i-1)*1.0f, 0.0f);
            line.transform.parent = forGround.parent;
			_lines.Add(line);
		}

        player.setCurrentNoteId(6);
        player.transform.parent = playGround;
        player.transform.localPosition = new Vector3(-4, 0, 0);

		generateChord ();
	}

	void generateChord () {
        _numberOfChord++;
        _chord = Instantiate (chordPrefab) as Chord;
		_chord.transform.parent = playGround.transform;
        _chord.generateNotes();
        _rightNoteId = _chord.getRightNoteId();
        noteText.text = Notes.getString(_rightNoteId);
    }
	
	// Update is called once per frame
	void Update () {

        if (player.hasANote())
        {
            int chosenNoteId = player.getChosenNoteId();

            if (!_chord.hasNote(chosenNoteId))
            {
                player.getAvatar().showAss();
            }
            else
            {
                if (chosenNoteId == _rightNoteId)
                {
                    _score++;
                    if (_score == 0)
                    {
                        player.getAvatar().setIsWinning(true);
                    }
                    player.getAvatar().win();
                    _chord.paint(_rightNoteId, Color.green);
                }
                else
                {
                    _score--;
                    if (_score < 0)
                    {
                        player.getAvatar().setIsWinning(false);
                    }
                    player.getAvatar().loose();
                    _chord.paint(_rightNoteId, Color.blue);
                    _chord.paint(chosenNoteId, Color.red);
                }   
            }
            player.setHasANote(false);
        }

		if (Input.GetKeyDown (KeyCode.R) || _chord.transform.position.x < -10f) {
			Destroy (_chord.gameObject);
			generateChord();    
		}

        updateScore();


		if(Input.GetKey(KeyCode.Escape)){
			quitGame();
		}
	}

    private void updateScore()
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

	public void quitGame()
	{
		UserManager.Instance.addNoteGameScore(_score);
		Application.LoadLevel("_MainMenu");
	}
}
