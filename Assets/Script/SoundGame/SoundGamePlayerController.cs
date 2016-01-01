using UnityEngine;
using System.Collections;

public class SoundGamePlayerController : MonoBehaviour {

    public PlayerAvatar playerAvatarPrefab;
    public float speed = 1.0f;
	public Piano pianoPrefab;

    private PlayerAvatar _playerAvatar;
    private Vector3 _force;
    private Rigidbody2D rb;

	private Piano _piano;

	private int _nbJump = 0;

	// Use this for initialization
	void Start () {
        _playerAvatar = Instantiate(playerAvatarPrefab) as PlayerAvatar;
        _playerAvatar.transform.parent = transform;
		transform.localPosition = new Vector3 (-5.0f, 4.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();

		_piano = Instantiate (pianoPrefab) as Piano;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space") && _nbJump < 1)
		{
			rb.AddForce(new Vector3(0.0f, speed*30.0f, 0.0f));
			_nbJump++;
			//_piano.playKey(2*_nbJump);
		}

		if(Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel("_MainMenu");
		}
	}

    void FixedUpdate()
    {
		if (rb.velocity.y == 0.0f) {
			_nbJump = 0;
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
