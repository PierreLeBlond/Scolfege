using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundGameManager : MonoBehaviour {

    public Line linePrefab;

    private List<Line> _lines = new List<Line>();

	// Use this for initialization
	void Start () {
        Line leftLine = Instantiate(linePrefab) as Line;
        leftLine.transform.localPosition = new Vector3(10.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
