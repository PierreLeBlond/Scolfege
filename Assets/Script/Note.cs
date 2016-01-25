using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour
{

    public GameObject noteOut;
    public GameObject noteIn;
	public Vector2 speed = new Vector2(0f, 0f);
	public Vector2 direction = new Vector2(0f, -1f);

    private int _noteId;
	private bool _isRight = false;
	private bool _isPicked = false;

	private float _time;
	private Vector3 _position;

	private Vector2 _movement;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		_movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
	}

	void FixedUpdate(){
		if (_isPicked) {
			float time = Time.time - _time;
			if (_isRight) {
				if(time < 2.0f){
					transform.position = new Vector3 (_position.x + (time / (2.0f)) * (-7f - _position.x), _position.y + (time / (2.0f)) * (4f - _position.y), 0f);
				}
				noteIn.GetComponent<SpriteRenderer>().color = Color.Lerp (Color.green, Color.clear, time / 4.0f);
			}else{
				transform.position = new Vector3 (_position.x, _position.y + (time / (2.0f)) * (-8f - _position.y), 0f);
			}
		}
		//GetComponent<Rigidbody2D>().velocity = _movement;
	}

	public void setRight(bool b){
		_isRight = b;
	}

	public void setPicked(bool b){
		_isPicked = b;
		_time = Time.time;
		_position = transform.position;
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
