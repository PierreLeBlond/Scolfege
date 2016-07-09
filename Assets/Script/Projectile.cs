using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector2 speed = new Vector2 (6f, 0f);
	public Vector2 direction = new Vector2 (1f, 0f);

	public int id;

	public PlayerManager player;

	private Vector2 _movement;

	private float lifeTime = 1.3f;
	private float birthTime;

	// Use this for initialization
	void Start () {
		birthTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		_movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);

			if(Time.time - birthTime > lifeTime)
			{
				destroy();
			}
	}

	void FixedUpdate(){
		GetComponent<Rigidbody2D>().velocity = _movement;
	}

	public void destroy()
	{
		player.reload();
		Destroy(gameObject);
	}

	public void OnTriggerEnter2D(Collider2D intruder)
    {
		if (intruder.CompareTag("Chord"))
        {
			player.chordIntruder(intruder, id);
		}

		/*if (intruder.CompareTag("Bonus"))
        {
			player.OnTriggerEnter2D(intruder);
		}*/
			/*Chord chord = intruder.GetComponent<Collider2D>().GetComponent<Chord>();

				if(chord == null)
				{
					Debug.Log("Right tag Note, but component hasn't the according class");
				}
				else if(!chord.isDisabled)
				{
					if(player.getResult(chord.checkPickedNote(id)))
						destroy();
						//player.reload();
					//piano.playKey(_chosenNoteId);
				}
		}*/
	}
}
