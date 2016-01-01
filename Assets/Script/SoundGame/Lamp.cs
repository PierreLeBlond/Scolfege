using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour {

	public Light light;

	public Piano pianoPrefab;

	private Piano _piano;

	private int _note = 0;

	// Use this for initialization
	void Start () {
		_piano = Instantiate (pianoPrefab) as Piano;
		_piano.transform.parent = transform;
		_piano.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);

		AudioSource src = _piano.GetComponent<AudioSource> ();
		src.loop = true;
		_piano.playKey (_note);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playKey(int note){
		_note = note;
	}

	public void OnTriggerEnter2D(Collider2D intruder)
	{
		if (intruder.CompareTag("Player"))
		{
			light.enabled = true;
		}
	}

	public void OnTriggerExit2D(Collider2D intruder)
	{
		if (intruder.CompareTag("Player"))
		{
			light.enabled = false;
		}
	}
}
