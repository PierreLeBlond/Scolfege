using UnityEngine;
using System.Collections;

public class Scrollable : MonoBehaviour {

	public Vector2 speed = new Vector2 (0f, 0f);
	public Vector2 direction = new Vector2 (-1f, 0f);

	private Vector2 _movement;

	private bool _pause = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		_movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
	}

	void FixedUpdate() {
		if(!_pause)
            GetComponent<Rigidbody2D>().velocity = _movement;
		else
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

	}

	public void pause(){
		_pause = true;
	}

	public void play(){
		_pause = false;
	}
}
