using UnityEngine;
using System.Collections;

public class Scrollable : MonoBehaviour {

	public Vector2 speed = new Vector2 (0f, 0f);
	public Vector2 direction = new Vector2 (-1f, 0f);

	private Vector2 _movement;

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
            GetComponent<Rigidbody2D>().velocity = _movement;
	}
}
