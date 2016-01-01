using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundGameManager : MonoBehaviour {

    public Line linePrefab;
	public Door doorPrefab;
	public Lamp lampPrefab;

    private List<Line> _lines = new List<Line>();
	private List<Door> _doors = new List<Door>();
	private List<Lamp> _lamps = new List<Lamp>();

	private int _firstNote;
	private int _secondNote;

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

		Door bottomDoor = Instantiate (doorPrefab) as Door;
		bottomDoor.transform.localPosition = new Vector3 (6.0f, -1.0f, 0.0f);
		_doors.Add (bottomDoor);

		Door topDoor = Instantiate (doorPrefab) as Door;
		topDoor.transform.localPosition = new Vector3 (6.0f, 3.0f, 0.0f);
		_doors.Add (topDoor);

		Lamp firstLamp = Instantiate (lampPrefab) as Lamp;
		firstLamp.transform.localPosition = new Vector3 (-6.0f, 0.72f, 0.0f);
		_lamps.Add (firstLamp);

		Lamp secondLamp = Instantiate (lampPrefab) as Lamp;
		secondLamp.transform.localPosition = new Vector3 (0.6f, 0.72f, 0.0f);
		_lamps.Add (secondLamp);

		_firstNote = Random.Range(1, 14);
		_secondNote = _firstNote;
		while (_secondNote == _firstNote) {
			_secondNote = Random.Range(0, 15);
		}
		if (_secondNote > _firstNote) {
			topDoor.setIsRight (true);
		} else {
			bottomDoor.setIsRight (true);
		}

		firstLamp.playKey (_firstNote);
		secondLamp.playKey (_secondNote);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
