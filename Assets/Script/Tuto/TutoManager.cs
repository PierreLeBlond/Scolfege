using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutoManager : MonoBehaviour {

    public Line linePrefab;
    public Door doorPrefab;
    public Lamp lampPrefab;

    public TutoPlayerController _player;

    private List<Line> _lines = new List<Line>();
    private List<Door> _doors = new List<Door>();
    private List<Lamp> _lamps = new List<Lamp>();

    private Door _tutoDoor;

    // Use this for initialization
    void Start()
    {
        Line upLine = Instantiate(linePrefab) as Line;
        upLine.transform.localPosition = new Vector3(-5.0f, 2.0f, 0.0f);
        upLine.GetComponent<BoxCollider2D>().isTrigger = false;
        _lines.Add(upLine);

        Line middleLine = Instantiate(linePrefab) as Line;
        middleLine.transform.localPosition = new Vector3(10.0f, 0.0f, 0.0f);
        middleLine.GetComponent<BoxCollider2D>().isTrigger = false;
        _lines.Add(middleLine);

        Line downLine = Instantiate(linePrefab) as Line;
        downLine.transform.localPosition = new Vector3(-5.0f, -2.0f, 0.0f);
        downLine.GetComponent<BoxCollider2D>().isTrigger = false;
        _lines.Add(downLine);

        Line topLine = Instantiate(linePrefab) as Line;
        topLine.transform.localPosition = new Vector3(0.0f, 4.9f, 0.0f);
        topLine.GetComponent<BoxCollider2D>().isTrigger = false;
        _lines.Add(topLine);

        Line bottomLine = Instantiate(linePrefab) as Line;
        bottomLine.transform.localPosition = new Vector3(0.0f, -5.0f, 0.0f);
        bottomLine.GetComponent<BoxCollider2D>().isTrigger = false;
        _lines.Add(bottomLine);

        Line leftSideLine = Instantiate(linePrefab) as Line;
        leftSideLine.transform.localPosition = new Vector3(-8.8f, 0.0f, 0.0f);
        leftSideLine.transform.localRotation = new Quaternion(0.0f, 0.0f, 1.0f, 1.0f);
        leftSideLine.GetComponent<BoxCollider2D>().isTrigger = false;
        _lines.Add(leftSideLine);

        Line rightSideLine = Instantiate(linePrefab) as Line;
        rightSideLine.transform.localPosition = new Vector3(8.9f, 0.0f, 0.0f);
        rightSideLine.transform.localRotation = new Quaternion(0.0f, 0.0f, 1.0f, 1.0f);
        rightSideLine.GetComponent<BoxCollider2D>().isTrigger = false;
        _lines.Add(rightSideLine);

        _tutoDoor = Instantiate(doorPrefab) as Door;
        _tutoDoor.transform.localPosition = new Vector3(0.0f, -4.0f, 0.0f);
        _doors.Add(_tutoDoor);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            quitGame();
        }
    }

    public void doorIsChosen()
    {
        Application.LoadLevel("_MainMenu");
    }

    public void quitGame()
    {
        Application.LoadLevel("_MainMenu");
    }
}
