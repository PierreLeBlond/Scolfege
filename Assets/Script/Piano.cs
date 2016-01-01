using UnityEngine;
using System.Collections;

public class Piano : MonoBehaviour {

    public AudioClip[] keys;

    public AudioSource _audioSource;

	// Use this for initialization
	void Start () {
		_audioSource.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playKey(int key)
    {
        _audioSource.clip = keys[key];
        _audioSource.Play();
    }
}
