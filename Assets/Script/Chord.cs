using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Chord : MonoBehaviour {

	public Vector3 scoreTarget = new Vector3(3.39f, 4.12f, 0f);

	private Gameplay _gameplay;
	public void setGameplay(Gameplay gameplay){
		_gameplay = gameplay;
		_info.setGameplay(_gameplay);
	}

	private Note _ghostNote;
	public Note GhostNote {
		get {
			return _ghostNote;
		}
		set {
			_ghostNote = value;
		}
	}

	public enum Result { None, Win, Loose };

	public SpriteRenderer 		blindSprite;
	//Offsets for a right display
    public float 				staveOffset = -3.0f;
    public float 				staveInterval = 0.5f;
	//Number of notes in one chord
	public int 					_numberOfNote = 3;
	//Will notify the game manager that this chord has done her job
	public bool 				isDestroyed = false;
	public bool 				isDisabled = false;

	//Note prefab
	public Note 				notePrefab;

	private List<Note> 			_notes = new List<Note>();
	private int 				_rightNoteId;
	private Note 				_rightNote;
	private Info 				_info;

	// Use this for initialization
	void Start () {
		if(_gameplay.hint)
			blindSprite.enabled = false;
		else
			blindSprite.enabled = true;

		transform.localPosition = new Vector3(13f, -1f, 0f);
	}

    public void generateNotes(Level level)
    {
        Note note;
		bool todo;
		int noteId = 0, j = 0, rightNotePosition = Random.Range (0, _numberOfNote);

		j = Random.Range(level.minNote, level.maxNote);

		for (int i = 0; i < _numberOfNote; i++) {

			todo = true;
			while(todo)
			{
				if(level.regular)
				{
					todo = false;
					if(i == 0)
					{
						j = Random.Range (level.minNote, level.maxNote);
						if(!level.interLine){
							j = 4 + j*2;
						}
					}
					else
						j += 2;
				}
				else
				{
					j = Random.Range (level.minNote, level.maxNote);
					todo = false;
					foreach(Note previous_note in _notes){
						if((j%7) == (previous_note.getNoteId()%7))
							todo = true;
					}
				}
			}

			noteId = j;

			note = Instantiate (notePrefab) as Note;
			note.transform.localPosition = new Vector3 (0f, staveOffset + noteId * staveInterval, 0.0f);
			int k = 0;
			/*while (noteId < 3 - k || noteId > 13 + k) {
				NoteLinePrefab line = Instantiate (linePrefab) as NoteLinePrefab;
				if (noteId < 3 - k)
				line.transform.localPosition = new Vector3 (0f, staveOffset + (2 - k) * staveInterval, 0f);
				else
				line.transform.localPosition = new Vector3 (0f, staveOffset + (14 + k) * staveInterval, 0f);
				k += 2;
				line.transform.parent = transform;
			}*/

			if(noteId < 3 || noteId > 13)
			{
				note.setNoteOut();
			}

			note.transform.parent = transform;
			_notes.Add (note);
			note.setNoteId (noteId);

			if(!_gameplay.hint){
				note.sprite.enabled = false;
			}


			if (i == rightNotePosition) {
				_rightNoteId = noteId;
				note.setRight(true);
				_rightNote = note;
			}
		}

		_info.setNote(_rightNoteId);

		//generate ghost note //Deprecated
		//_ghostNote = Instantiate (notePrefab) as Note;
		//_ghostNote.transform.parent = transform;
		//_ghostNote.noteSprite.GetComponent<SpriteRenderer> ().color = Color.Lerp (Color.white, Color.clear, 0.5f);
		//_ghostNote.noteLight.gameObject.SetActive(true);

		//Move the chord
		Scrollable scroll = GetComponent("Scrollable") as Scrollable;
		scroll.speed = level.scrollSpeed;
    }

	public int getRightNoteId(){
		return _rightNoteId;
	}

    public List<Note> getNotes()
    {
        return _notes;
    }

	//Check if the chord has the note given by noteId
    public bool hasNote(int noteId)
    {
        bool b = false;
        foreach(Note note in _notes)
        {
            b = noteId == note.getNoteId() || b;
        }
        return b;
    }

	public Result checkPickedNote(int noteId){
		Result result = Result.None;

		if(_gameplay.hint)
		{
			foreach(Note note in _notes)
			{
				if(noteId == note.getNoteId()){
					if(noteId == _rightNoteId){
						note.paint (Color.green);
						note.transform.parent = null;
						note.GetComponent<Scrollable>().speed = new Vector2(0.5f, 0.5f);
						note.GetComponent<Scrollable>().direction = new Vector2(scoreTarget.x - note.transform.localPosition.x, scoreTarget.y - note.transform.localPosition.y);
						result = Result.Win;
						note.sprite.enabled = true;
					}else{
						note.paint (Color.red);
						note.GetComponent<Rigidbody2D>().isKinematic = false;
						result = Result.Loose;
						note.sprite.enabled = true;
						_rightNote.paint(Color.yellow);
						_rightNote.sprite.enabled = true;
					}
				}
			}

			if(result != Result.None)
			{
				fade();
				isDisabled = true;
				StartCoroutine(destroy());
			}
		}
		else
		{
			_rightNote.sprite.enabled = true;
			if(noteId%7 == _rightNoteId%7)
			{
				_rightNote.transform.localPosition = new Vector3 (0f, staveOffset + noteId * staveInterval, 0.0f);
				_rightNote.paint (Color.green);
				_rightNote.transform.parent = null;
				_rightNote.GetComponent<Scrollable>().speed = new Vector2(0.5f, 0.5f);
				_rightNote.GetComponent<Scrollable>().direction = new Vector2(scoreTarget.x - _rightNote.transform.localPosition.x, scoreTarget.y - _rightNote.transform.localPosition.y);
				result = Result.Win;
				_rightNote.sprite.enabled = true;
			}else{
				Note note = Instantiate (notePrefab) as Note;
				note.transform.parent = transform;
				note.transform.localPosition = new Vector3 (0f, staveOffset + noteId * staveInterval, 0.0f);
				note.paint (Color.red);
				note.GetComponent<Rigidbody2D>().isKinematic = false;
				note.GetComponent<FadableToDeath>().startFadingToDeath();
				result = Result.Loose;
				_rightNote.paint(Color.yellow);
				_rightNote.sprite.enabled = true;
			}

			blindSprite.transform.parent = null;
			blindSprite.GetComponent<FadableToDeath>().startFadingToDeath();
			fade();
			isDisabled = true;
			StartCoroutine(destroy());
		}

		return result;
	}

	public IEnumerator destroy()
	{
		if(_ghostNote)
			Destroy (_ghostNote.gameObject);
		yield return new WaitForSeconds(2);
		isDestroyed = true;
	}



    public void paint(int noteId, Color color)
    {
        foreach (Note note in _notes)
        {
            if (noteId == note.getNoteId())
            {
                note.paint(color);
            }
        }
    }

	public void fade(){
		foreach (Note note in _notes) {
				note.GetComponent<FadableToDeath>().startFadingToDeath();
		}
	}

	public void setInfo(Info info)
	{
		_info = info;
	}

	public void OnTriggerEnter2D(Collider2D intruder)
    {
		if (!isDisabled && intruder.CompareTag("LeftLimit"))
        {
			fade();
			isDisabled = true;
			StartCoroutine(destroy());
		}
	}

	public void pause()
	{
		GetComponent<Scrollable>().pause();
	}

	public void play()
	{
		GetComponent<Scrollable>().play();
	}
}
