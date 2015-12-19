using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chord : MonoBehaviour {

	public Key keyPrefab;

	public Vector2 speed = new Vector2 (3f, 0f);
	public Vector2 direction = new Vector2 (-1f, 0f);

	private List<Key> keys = new List<Key>();

	private int keyName;

	private Vector2 movement;

	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3(13f, 0f, 0f);
	}

    public void generateKeys()
    {
        Key key;
        int j = Random.Range(0, 8);
        int rightKey = Random.Range(0, 3);
        for (int i = 0; i < 3; i++)
        {
            key = Instantiate(keyPrefab) as Key;
            key.transform.localPosition = new Vector3(0f, -1.5f + j * 0.5f + i * 1f, 0.0f);
            if(j + 2*i < 0 || j + 2*i > 10 && (j + 2*i) % 2 != 0)
            {
                key.setKeyOut();
            }
            key.transform.parent = transform;
            keys.Add(key);
            if (i == rightKey)
            {
                keyName = (j + 2 * i + 1) % 7;
                key.setRight(true);
            }
        }
    }

	public int getRightKeyName(){
		return keyName;
	}

    public List<Key> getKeys()
    {
        return keys;
    }
	
	// Update is called once per frame
	void Update () {
		movement = new Vector2 (
			speed.x * direction.x,
			speed.y * direction.y);
	}

	void FixedUpdate () {
		GetComponent<Rigidbody2D>().velocity = movement;
	}
}
