using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Note _ghostNote;
	public Note GhostNote {
		get {
			return _ghostNote;
		}
		set {
			_ghostNote = value;
			_ghostNote.transform.localPosition = new Vector2(_ghostNote.transform.localPosition.x, _transform.localPosition.y + 1f);
		}
	}

	private int _currentPositionId;
	public int CurrentPositionId {
		get {
			return _currentPositionId;
		}
		set {
			if(value < 15 && value > 1)
				_currentPositionId = value;
			else
				_currentPositionId = 6;
			_transform.localPosition = new Vector2(_transform.localPosition.x, (_currentPositionId - 8)*0.5f);
		}
	}

	private Transform _transform;
	public Transform Transform {
		get {
			return _transform;
		}
		set {
			_transform = value;
		}
	}

	private float _autoFire;
	public float _autoFireSpeed = 0.1f;

	public bool movable = true;

	// Update is called once per frame
	void Update () {
		if(movable){
			if (Input.GetKey(KeyCode.UpArrow) && _currentPositionId < 14 && Time.time - _autoFire > _autoFireSpeed)
			{
				_transform.localPosition = new Vector2(_transform.localPosition.x, _transform.localPosition.y + 0.5f);
				if(_ghostNote)
					_ghostNote.transform.localPosition = new Vector2(_ghostNote.transform.localPosition.x, _transform.localPosition.y + 1f);
				_currentPositionId++;
				_autoFire = Time.time;
			}
			else if (Input.GetKey(KeyCode.DownArrow) && _currentPositionId > 2 && Time.time - _autoFire > _autoFireSpeed)
			{
				_transform.localPosition = new Vector2(_transform.localPosition.x, _transform.localPosition.y - 0.5f);
				if(_ghostNote)
					_ghostNote.transform.localPosition = new Vector2(_ghostNote.transform.localPosition.x, _transform.localPosition.y + 1f);
				_currentPositionId--;
				_autoFire = Time.time;
			}
		}
	}
}
