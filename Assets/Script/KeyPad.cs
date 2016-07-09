using UnityEngine;
using System.Collections;

public class KeyPad : MonoBehaviour {

	public KeyPadKey whiteKeyPrefab;
	public GameObject blackKeyPrefab;

	public PlayerManager player;

	private KeyPadKey[] _whiteKeys;
	private GameObject[] _blackKeys;

	// Use this for initialization
	void Start () {
		_whiteKeys = new KeyPadKey[13];
		_blackKeys = new GameObject[9];

		int k = 0;

		for(int i = 2; i < 15; ++i)
		{
			_whiteKeys[i-2] = Instantiate (whiteKeyPrefab) as KeyPadKey;
			_whiteKeys[i-2].id = i;
			_whiteKeys[i-2].player = player.playerController;
			_whiteKeys[i-2].transform.localPosition = new Vector3((float)(i) - 8f, 0f, 0f);
			_whiteKeys[i-2].transform.parent = transform;

			if(i%7 != 1 && i%7 != 4 && i < 14)
			{
				_blackKeys[k] = Instantiate (blackKeyPrefab) as GameObject;
				_blackKeys[k].transform.localPosition = new Vector3((float)(i) - 7.5f, 0f, -0.5f);
				_blackKeys[k].transform.parent = transform;
				k++;
			}
		}
		transform.position = new Vector3(0f, -10f, 3f);
		transform.rotation = Quaternion.Euler(25f, 0f, 0f);
	}

	public IEnumerator show(){
		for(int i = 0; i < 250; ++i)
		{
			 transform.position = transform.position + new Vector3(0f, 0.02f, 0f);
			 yield return new WaitForSeconds(0.01f);
		}
	}

	public IEnumerator hide(){
		for(int i = 0; i < 250; ++i)
		{
			 transform.position = transform.position + new Vector3(0f, -0.02f, 0f);
			 yield return new WaitForSeconds(0.01f);
		}
	}
}
