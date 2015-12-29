using UnityEngine;
using System.Collections;

public class SoundGamePlayerController : MonoBehaviour {

    public PlayerAvatar playerAvatarPrefab;
    public float speed = 1.0f;

    private PlayerAvatar _playerAvatar;
    private Vector3 _force;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        _playerAvatar = Instantiate(playerAvatarPrefab) as PlayerAvatar;
        _playerAvatar.transform.parent = transform;
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(new Vector3(0.0f, speed*30.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _force = new Vector3(-speed, 0.0f, 0.0f);
            rb.AddForce(_force);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _force = new Vector3(speed, 0.0f, 0.0f);
            rb.AddForce(_force);
            transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

    public PlayerAvatar getPlayerAvatar()
    {
        return _playerAvatar;
    }


}
