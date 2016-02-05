using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chord : MonoBehaviour {

	public enum Result { None, Win, Loose };

    public float staveOffset = -3.0f;
    public float staveInterval = 0.5f;

	public int _numberOfNote = 3;

	public Note notePrefab;

	public NoteLinePrefab linePrefab;

	public Vector2 speed = new Vector2 (3f, 0f);
	public Vector2 direction = new Vector2 (-1f, 0f);

    public bool pause = false;

	private List<Note> _notes = new List<Note>();

	private int _rightNoteId;
	private Note _rightNote;

	private Vector2 _movement;

	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3(13f, -1f, 0f);
	}

    public void generateNotes(int level)
    {
        Note note;
		int noteId = 0, j = 0, rightNotePosition = Random.Range (0, 3);
		switch (level) {
		case 0:
			j = Random.Range (0, 2);
			for (int i = 0; i < _numberOfNote; i++) {
				note = Instantiate (notePrefab) as Note;

				noteId = 4 + j*2;

				note.transform.localPosition = new Vector3 (0f, staveOffset + noteId * staveInterval, 0.0f);

				note.transform.parent = transform;
				_notes.Add (note);

				note.setNoteId (noteId);
				
				if (i == rightNotePosition) {
					_rightNoteId = noteId;
					note.setRight (true);
					_rightNote = note;
				}
				
				j = Random.Range (j + 1, 4+i);
			}
			break;
		case 1:
			j = Random.Range (3, 6);
			for (int i = 0; i < _numberOfNote; i++) {
				note = Instantiate (notePrefab) as Note;
				
				noteId = j;
				
				note.transform.localPosition = new Vector3 (0f, staveOffset + noteId * staveInterval, 0.0f);
				
				note.transform.parent = transform;
				_notes.Add (note);
				
				note.setNoteId (noteId);
				
				if (i == rightNotePosition) {
					_rightNoteId = noteId;
					note.setRight (true);
					_rightNote = note;
				}
				
				j = Random.Range (j + 1, 13);
				foreach(Note previous_note in _notes){
					while(j%8 == previous_note.getNoteId()%8)
						j = Random.Range (j + 1, 13);
				}
			}
			break;
		case 2:
			j = Random.Range (2, 6); //The first note goes from lower A to middle F
			for (int i = 0; i < _numberOfNote; i++) {
				note = Instantiate (notePrefab) as Note;
				note.transform.localPosition = new Vector3 (0f, staveOffset + j * staveInterval, 0.0f);
				int k = 0;
				while (j < 3 - k || j > 13 + k) {
					NoteLinePrefab line = Instantiate (linePrefab) as NoteLinePrefab;
					if (j < 3 - k)
						line.transform.localPosition = new Vector3 (0f, staveOffset + (2 - k) * staveInterval, 0f);
					else
						line.transform.localPosition = new Vector3 (0f, staveOffset + (14 + k) * staveInterval, 0f);
					k += 2;
					line.transform.parent = transform;
				}
				note.transform.parent = transform;
				_notes.Add (note);

				noteId = j;

				note.setNoteId (noteId);

				if (i == rightNotePosition) {
					_rightNoteId = noteId;
					note.setRight (true);
					_rightNote = note;
				}

				j = Random.Range (j + 1, 14);
				foreach(Note previous_note in _notes){
					while(j%8 == previous_note.getNoteId()%8)
						j = Random.Range (j + 1, 13);
				}
			}
			break;
		}
    }

	public int getRightNoteId(){
		return _rightNoteId;
	}

    public List<Note> getNotes()
    {
        return _notes;
    }
	
	// Update is called once per frame
	void Update () {
        if (!pause)
        {
            _movement = new Vector2(
                speed.x * direction.x,
                speed.y * direction.y);
        }
	}

	void FixedUpdate () {
        if (!pause)
        {
            GetComponent<Rigidbody2D>().velocity = _movement;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
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
		foreach(Note note in _notes)
		{
			if(noteId == note.getNoteId()){
				note.setPicked(true);
				if(noteId == _rightNoteId){
					note.paint (Color.green);
					note.speed = new Vector2(0.0f, -1.0f);
					note.transform.parent = null;
					result = Result.Win;
				}else{
					note.paint (Color.red);
					_rightNote.paint (Color.gray);
					note.speed = new Vector2(0.0f, -1.0f);
					note.transform.parent = null;
					result = Result.Loose;
				}
			}
		}
		return result;
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

	public void OnDestroy(){
		if(_rightNote != null)
			Destroy (_rightNote.gameObject);
	}
}
