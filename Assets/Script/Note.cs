using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{
    public SpriteRenderer noteIn;
    public SpriteRenderer noteOut;

    public SpriteRenderer sprite;

    public Light noteLight;

    private int _noteId;
	private bool _isRight = false;

    private Color _color = Color.white;

	// Use this for initialization
	void Awake () {
        sprite = noteIn;
        noteOut.enabled = false;
        GetComponent<FadableToDeath>().sprite = sprite;
		sprite.sortingOrder = 5;
	}

	public void setRight(bool b){
		_isRight = b;
	}

    public void setNoteId(int noteId)
    {
        _noteId = noteId;
    }

    public void setNoteIn()
    {
        if(sprite)
            sprite.enabled = false;
        sprite = noteIn;
        GetComponent<FadableToDeath>().sprite = sprite;
        sprite.enabled = true;
    }

    public void setNoteOut()
    {
        if(sprite)
            sprite.enabled = false;
        sprite = noteOut;
        GetComponent<FadableToDeath>().sprite = sprite;
        sprite.enabled = true;
    }

    public int getNoteId()
    {
        return _noteId;
    }

    public void paint(Color color)
    {
        sprite.color = color;
        _color = color;
    }

	public void OnTriggerEnter2D(Collider2D intruder)
	{
		if(intruder.CompareTag("Score")){
			Destroy (gameObject);
		}
	}
}
