using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundGameManager : MonoBehaviour {

    public Line linePrefab;
	public Door doorPrefab;
	public Lamp lampPrefab;

	public SoundGamePlayerController _player;

    private List<Line> _lines = new List<Line>();
	private List<Door> _doors = new List<Door>();
	private List<Lamp> _lamps = new List<Lamp>();

	private int _firstNote;
	private int _secondNote;

	private Door _topDoor;
	private Door _bottomDoor;

	private Lamp _firstLamp;
	private Lamp _secondLamp;

	private int _score = 0;

	// Use this for initialization
	void Start () {
        Line rightLine = Instantiate(linePrefab) as Line;
		rightLine.transform.localPosition = new Vector3(-10.0f, 0.0f, 0.0f);
		rightLine.GetComponent<BoxCollider2D> ().isTrigger = false;
		_lines.Add (rightLine);

		Line upLeftLine = Instantiate (linePrefab) as Line;
		upLeftLine.transform.localPosition = new Vector3(15.0f, 2.0f, 0.0f);
		upLeftLine.GetComponent<BoxCollider2D> ().isTrigger = false;
		_lines.Add (upLeftLine);

		Line downLeftLine = Instantiate (linePrefab) as Line;
		downLeftLine.transform.localPosition = new Vector3(15.0f, -2.0f, 0.0f);
		downLeftLine.GetComponent<BoxCollider2D> ().isTrigger = false;
		_lines.Add (downLeftLine);

		Line topLine = Instantiate (linePrefab) as Line;
		topLine.transform.localPosition = new Vector3(0.0f, 4.9f, 0.0f);
		topLine.GetComponent<BoxCollider2D> ().isTrigger = false;
		_lines.Add (topLine);

		Line bottomLine = Instantiate (linePrefab) as Line;
		bottomLine.transform.localPosition = new Vector3(0.0f, -5.0f, 0.0f);
		bottomLine.GetComponent<BoxCollider2D> ().isTrigger = false;
		_lines.Add (bottomLine);

		Line leftSideLine = Instantiate (linePrefab) as Line;
		leftSideLine.transform.localPosition = new Vector3(-8.8f, 0.0f, 0.0f);
		leftSideLine.transform.localRotation = new Quaternion (0.0f, 0.0f, 1.0f, 1.0f);
		leftSideLine.GetComponent<BoxCollider2D> ().isTrigger = false;
		_lines.Add (leftSideLine);

		Line rightSideLine = Instantiate (linePrefab) as Line;
		rightSideLine.transform.localPosition = new Vector3(8.9f, 0.0f, 0.0f);
		rightSideLine.transform.localRotation = new Quaternion (0.0f, 0.0f, 1.0f, 1.0f);
		rightSideLine.GetComponent<BoxCollider2D> ().isTrigger = false;
		_lines.Add (rightSideLine);

		_bottomDoor = Instantiate (doorPrefab) as Door;
		_bottomDoor.transform.localPosition = new Vector3 (6.0f, -1.0f, 0.0f);
		_doors.Add (_bottomDoor);

		_topDoor = Instantiate (doorPrefab) as Door;
		_topDoor.transform.localPosition = new Vector3 (6.0f, 3.0f, 0.0f);
		_doors.Add (_topDoor);

		_firstLamp = Instantiate (lampPrefab) as Lamp;
		_firstLamp.transform.localPosition = new Vector3 (-6.0f, 0.72f, 0.0f);
		_lamps.Add (_firstLamp);

		_secondLamp = Instantiate (lampPrefab) as Lamp;
		_secondLamp.transform.localPosition = new Vector3 (0.6f, 0.72f, 0.0f);
		_lamps.Add (_secondLamp);

		_secondNote = Random.Range(1, 14);
		generateLevel ();

	}

	void generateLevel(){
		_firstNote = _secondNote;
		while (_secondNote == _firstNote) {
			_secondNote = Random.Range(0, 15);
		}
		if (_secondNote > _firstNote) {
			_topDoor.setIsRight (true);
			_bottomDoor.setIsRight (false);
		} else {
			_topDoor.setIsRight (false);
			_bottomDoor.setIsRight (true);
		}
		
		_firstLamp.playKey (_firstNote);
		_secondLamp.playKey (_secondNote);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			UserManager.Instance.addSoundGameScore(_score);
			Application.LoadLevel("_MainMenu");
		}
	}

	public void doorIsChosen(){
		if (_player.transform.position.y > 0) {
			if (_secondNote > _firstNote) {
				_score++;
			} else {
				_score--;
			}
		} else {
			if (_secondNote > _firstNote) {
				_score--;
			} else {
				_score++;
			}
		}
		generateLevel ();
	}


}
