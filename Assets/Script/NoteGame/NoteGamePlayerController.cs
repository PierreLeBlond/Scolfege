using UnityEngine;
using System.Collections;

public class NoteGamePlayerController : MonoBehaviour {

	//Public
    public PlayerAvatar playerAvatarPrefab;
	public Piano pianoPrefab;

	//Private
    private PlayerAvatar _playerAvatar;

	private Piano _piano;

    private int _currentNoteId = 6;//L'id de la note ou se trouve le joueur
    private int _chosenNoteId;//La dernière note choisit par le joueur

    private bool _hasChoosenANote = false;//Si le joueur à choisit une note
    private bool _hasTheRightNote = false;//Si le joueur à choisit la bonne note

	private float _autoFire;
    

	// Use this for initialization
	void Start () {
        _playerAvatar = Instantiate(playerAvatarPrefab) as PlayerAvatar;
        _playerAvatar.transform.parent = transform;

		_piano = Instantiate (pianoPrefab) as Piano;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow) && _currentNoteId < 14 && Time.time - _autoFire > 0.1)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.5f);
            _currentNoteId++;
			_autoFire = Time.time;
        }
		else if (Input.GetKey(KeyCode.DownArrow) && _currentNoteId > 2 && Time.time - _autoFire > 0.1)
		{
			transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.5f);
            _currentNoteId--;
			_autoFire = Time.time;
        }
	}

    public PlayerAvatar getAvatar()
    {
        return _playerAvatar;
    }

    public void setCurrentNoteId(int currentNoteId)
    {
        _currentNoteId = currentNoteId;
    }

    public int getChosenNoteId()
    {
        return _chosenNoteId;
    }

    public bool hasChoosenANote()
    {
        return _hasChoosenANote;
    }

    public void setHasChoosenNote(bool hasChoosenANote)
    {
        _hasChoosenANote = hasChoosenANote;
    }

    public void OnTriggerEnter2D(Collider2D intruder)
    {
        if (!_hasChoosenANote && intruder.CompareTag("Chord"))
        {
            _hasChoosenANote = true;
            _chosenNoteId = _currentNoteId;
        }
    }
}
