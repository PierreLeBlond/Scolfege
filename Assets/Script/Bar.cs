using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {

	public Line firstLine;
	public Line secondLine;

	public Vector2 speed = new Vector2 (3f, 0f);
	public Vector2 direction = new Vector2 (-1f, 0f);
	
	public bool pause = false;

	private Vector2 _movement;

	// Use this for initialization
	void Start () {
		secondLine.gameObject.SetActive(false);
		transform.localPosition = new Vector3(8f, -1f, 0f);
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
}
