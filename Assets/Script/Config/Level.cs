using UnityEngine;
using System.Collections;

public struct Level {
	public Vector2 			scrollSpeed;

	//Are interline notes are allowed ?
	public bool 			interLine;
	//Should we take the regular chord regarding the first note ?
	public bool 			regular;

	public int 				minNote;
	public int 				maxNote;

	public Level(Vector2 p_scrollSpeed,
	bool p_interLine, bool p_regular,
	int p_minNote, int p_maxNote){
		scrollSpeed = p_scrollSpeed;
		interLine = p_interLine;
		regular = p_regular;
		minNote = p_minNote;
		maxNote = p_maxNote;
	}
}
