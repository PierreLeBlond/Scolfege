using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{

    public GameObject noteOut;
    public GameObject noteIn;

    private int _noteId;
	private bool _isRight = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setRight(bool b){
		_isRight = b;
	}

    public void setNoteId(int noteId)
    {
        _noteId = noteId;
    }

    public int getNoteId()
    {
        return _noteId;
    }

    public void setNoteIn()
    {
        noteIn.SetActive(true);
        noteOut.SetActive(false);
    }

    public void setNoteOut()
    {
        noteIn.SetActive(false);
        noteOut.SetActive(true);
    }

    public void paint(Color color)
    {
        noteOut.GetComponent<SpriteRenderer>().color = color;
        noteIn.GetComponent<SpriteRenderer>().color = color;
    }
}
