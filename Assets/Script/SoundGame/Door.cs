using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	private bool _isRight = false;
	public SpriteRenderer _sprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setIsRight(bool b){
		_isRight = b;
	}

	public void OnTriggerEnter2D(Collider2D intruder)
	{
		if (intruder.CompareTag("Player"))
		{
			if(_isRight){
				_sprite.color = new Color(0.0f, 1.0f, 0.25f);
			}else{
				_sprite.color = new Color(1.0f, 0.0f, 0.1f);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D intruder)
	{
		if (intruder.CompareTag("Player"))
		{
			_sprite.color = new Color(255, 255, 255);
		}
	}
}
