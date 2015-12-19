using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Line linePrefab;
	public Square squarePrefab;
	public Chord chordPrefab;

	public Transform backGround;
	public Transform playGround;
    public Transform forGround;
	public Player player;

	public Text keyText;
    public Text numberOfchordText;

	private List<Line> lines = new List<Line>();
	private Square square;
	private Chord chord;

    private int _numberOfChord = 0;

	// Use this for initialization
	void Start () {
		Line line;
		for (int i = 0; i < 5; i++) {
			line = Instantiate (linePrefab) as Line;
			line.transform.localPosition = new Vector3(1.0f, (i-1)*1.0f, 0.0f);
            line.transform.parent = forGround.parent;
			lines.Add(line);
		}

		/*square = Instantiate (squarePrefab) as Square;
		square.transform.parent = backGround.transform;*/

		generateChord ();
	}

	void generateChord () {
        _numberOfChord++;
        numberOfchordText.text = "/" + _numberOfChord.ToString();
        chord = Instantiate (chordPrefab) as Chord;
		chord.transform.parent = playGround.transform;
        chord.generateKeys();
        foreach (Key key in chord.getKeys())
        {
            key.setPlayer(player);
        }

        keyText.text = Keys.getString(chord.getRightKeyName());
    }
	
	// Update is called once per frame
	void Update () {

        foreach (Key key in chord.getKeys())
        {
            key.setPlayer(player);
        }

        keyText.text = Keys.getString (chord.getRightKeyName ());

		if (Input.GetKeyDown (KeyCode.R) || chord.transform.position.x < -10f) {
            if (!player.HasTheChord())
            {
                player.showAss();
            }
            
			Destroy (chord.gameObject);
			generateChord();
            player.setHasTheChord(false);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			player.transform.localPosition = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y + 0.5f);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			player.transform.localPosition = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y - 0.5f);
		}
	}
}
