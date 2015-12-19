using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    public GameObject _keyOut;
    public GameObject _keyIn;

    private AudioSource _audioSource;

	private bool _isRight = false;

    private Player _player;

	// Use this for initialization
	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setRight(bool b){
		_isRight = b;
	}

    public void setPlayer(Player player)
    {
        _player = player;
    }

    public void setKeyIn()
    {
        _keyIn.SetActive(true);
        _keyOut.SetActive(false);
    }

    public void setKeyOut()
    {
        _keyIn.SetActive(false);
        _keyOut.SetActive(true);
    }

	public void OnTriggerEnter2D (Collider2D intruder){
		if (intruder.CompareTag ("Player")) {
			if (_isRight) {
                _keyOut.GetComponent<SpriteRenderer> ().color = Color.green;
                _keyIn.GetComponent<SpriteRenderer>().color = Color.green;
                _player.win();
			} else {
                _keyOut.GetComponent<SpriteRenderer> ().color = Color.red;
                _keyIn.GetComponent<SpriteRenderer>().color = Color.red;
                _player.loose();
			}
		}
	}
}
