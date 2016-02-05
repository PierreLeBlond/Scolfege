using UnityEngine;
using System.Collections;

public class TutoNotePlayerController : MonoBehaviour {

	public PlayerAvatar playerAvatarPrefab;
	public Piano pianoPrefab;
	
	private PlayerAvatar _playerAvatar;
	
	private Piano _piano;
	
	private int _currentNoteId = 6;//La note ou se trouve le joueur
	
	// Use this for initialization
	void Start () {
		_playerAvatar = Instantiate(playerAvatarPrefab) as PlayerAvatar;
		_playerAvatar.transform.parent = transform;

		transform.localPosition = new Vector3 (-7f, -1f, 0f);
		
		_piano = Instantiate (pianoPrefab) as Piano;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow) && _currentNoteId < 14)
		{
			transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.5f);
			_currentNoteId++;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && _currentNoteId > 2)
		{
			transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.5f);
			_currentNoteId--;
		}
	}
	
	public PlayerAvatar getAvatar()
	{
		return _playerAvatar;
	}

	public int getCurrentNoteId(){
		return _currentNoteId;
	}
}
