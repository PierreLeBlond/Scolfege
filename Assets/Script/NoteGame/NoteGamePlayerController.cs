using UnityEngine;
using System.Collections;

public class NoteGamePlayerController : MonoBehaviour {

    public PlayerAvatar playerAvatarPrefab;
	public Piano pianoPrefab;

    private PlayerAvatar _playerAvatar;

	private Piano _piano;

    private int _currentNoteId = 6;//La note ou se trouve le joueur
    private int _chosenNoteId;//La dernière note choisit par le joueur

    private bool _hasANote = false;//Si le joueur à choisit une note
    private bool _hasTheRightNote = false;
    

	// Use this for initialization
	void Start () {
        _playerAvatar = Instantiate(playerAvatarPrefab) as PlayerAvatar;
        _playerAvatar.transform.parent = transform;

		_piano = Instantiate (pianoPrefab) as Piano;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _currentNoteId < 14)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.5f);
            _currentNoteId++;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && _currentNoteId > 0)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.5f);
            _currentNoteId--;
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

    public bool hasANote()
    {
        return _hasANote;
    }

    public void setHasANote(bool hasANote)
    {
        _hasANote = hasANote;
    }

    public void OnTriggerEnter2D(Collider2D intruder)
    {
        if (!_hasANote && intruder.CompareTag("Chord"))
        {
            _hasANote = true;
            _chosenNoteId = _currentNoteId;
        }
    }
}
