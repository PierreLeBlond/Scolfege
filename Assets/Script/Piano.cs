using UnityEngine;
using System.Collections;

public class Piano : MonoBehaviour {

    public AudioClip[] keys;

    private AudioSource _audioSource;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playKey(int key)
    {
        _audioSource.clip = keys[key];
        _audioSource.Play();
        //Debug.Log(keys[key]);
    }
}
