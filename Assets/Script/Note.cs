using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
    public GameObject noteSprite;

    public Light noteLight;

    private int _noteId;
	private bool _isRight = false;

    private Color _color = Color.white;

	// Use this for initialization
	void Start () {
		noteSprite.GetComponent<SpriteRenderer> ().sortingOrder = 5;
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

    public void paint(Color color)
    {
        noteSprite.GetComponent<SpriteRenderer>().color = color;
        _color = color;
    }

	public void OnTriggerEnter2D(Collider2D intruder)
	{
		if(intruder.CompareTag("Score")){
			Destroy (gameObject);
		}
	}
}
