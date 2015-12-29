using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chord : MonoBehaviour {

    public float staveOffset = -3.0f;
    public float staveInterval = 0.5f;

	public Note notePrefab;

	public Vector2 speed = new Vector2 (3f, 0f);
	public Vector2 direction = new Vector2 (-1f, 0f);

	private List<Note> _notes = new List<Note>();

	private int _rightNoteId;

	private Vector2 _movement;

	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3(13f, 0f, 0f);
	}

    public void generateNotes()
    {
        Note note;
        int noteId = 0;
        int j = Random.Range(0, 11); //The first note goes from lower A to middle D
        int rightNotePosition = Random.Range(0, 3);
        for (int i = 0; i < 3; i++)
        {
            note = Instantiate(notePrefab) as Note;
            note.transform.localPosition = new Vector3(0f, staveOffset + j * staveInterval + i * 2 * staveInterval, 0.0f);
            if(j + 2*i < 3 || j + 2*i > 13 && (j + 2*i) % 2 == 0)
            {
                note.setNoteOut();
            }
            note.transform.parent = transform;
            _notes.Add(note);

            noteId = j + 2 * i;

            note.setNoteId(noteId);

            if (i == rightNotePosition)
            {
                _rightNoteId = noteId;
                note.setRight(true);
            }
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
		_movement = new Vector2 (
			speed.x * direction.x,
			speed.y * direction.y);
	}

	void FixedUpdate () {
		GetComponent<Rigidbody2D>().velocity = _movement;
	}

    public bool hasNote(int noteId)
    {
        bool b = false;
        foreach(Note note in _notes)
        {
            b = noteId == note.getNoteId() || b;
        }
        return b;
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
}
