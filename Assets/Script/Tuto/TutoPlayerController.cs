using UnityEngine;
using System.Collections;

public class TutoPlayerController : MonoBehaviour {

    public PlayerAvatar playerAvatarPrefab;
    public float speed = 1.0f;
    public Piano pianoPrefab;

    public TutoManager _manager;

    private PlayerAvatar _playerAvatar;
    private Vector3 _force;
    private Rigidbody2D rb;

    private Piano _piano;

    private int _nbJump = 0;

    private bool _hasADoor = false;
    private bool _hasChosenADoor = false;
    private float _enteringDoorRatio = 0.0f;

    // Use this for initialization
    void Start()
    {
        _playerAvatar = Instantiate(playerAvatarPrefab) as PlayerAvatar;
        _playerAvatar.transform.parent = transform;
        transform.localPosition = new Vector3(-5.0f, 4.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();

        _piano = Instantiate(pianoPrefab) as Piano;

    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasChosenADoor && Input.GetKeyDown("space") && _nbJump < 1)
        {
            rb.AddForce(new Vector3(0.0f, speed * 30.0f, 0.0f));
            _nbJump++;
            //_piano.playKey(2*_nbJump);
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.y == 0.0f)
        {
            _nbJump = 0;
        }

        if (_hasChosenADoor)
        {
            _enteringDoorRatio += 0.002f;
            _playerAvatar.defaultSkin.GetComponent<SpriteRenderer>().color = Color.Lerp(_playerAvatar.defaultSkin.GetComponent<SpriteRenderer>().color, Color.clear, _enteringDoorRatio);
        }
        else
        {
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

            if (Input.GetKey(KeyCode.UpArrow) && _hasADoor)
            {
                _hasChosenADoor = true;
                StartCoroutine(entersDoor());
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D intruder)
    {
        if (intruder.CompareTag("Door"))
        {
            _hasADoor = true;
        }
    }

    public void OnTriggerExit2D(Collider2D intruder)
    {
        if (intruder.CompareTag("Door"))
        {
            _hasADoor = false;
        }
    }

    public PlayerAvatar getPlayerAvatar()
    {
        return _playerAvatar;
    }

    public bool hasADoor()
    {
        return _hasADoor;
    }

    public IEnumerator entersDoor()
    {
        yield return new WaitForSeconds(2);
        _playerAvatar.defaultSkin.GetComponent<SpriteRenderer>().color = Color.white;
        _enteringDoorRatio = 0.0f;
        _hasChosenADoor = false;
        _manager.doorIsChosen();
        transform.localPosition = new Vector3(-5.0f, 4.0f, 0.0f);
    }
}
