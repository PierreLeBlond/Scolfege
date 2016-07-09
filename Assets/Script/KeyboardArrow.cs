using UnityEngine;
using System.Collections;

public enum Direction {
	UP, DOWN, LEFT, RIGHT
}

public class KeyboardArrow : MonoBehaviour {

	public Color defaultColor = new Color(0.4f, 0.4f, 0.4f, 1.0f);
	public Color highlightColor = new Color(0.4f, 0.4f, 0.7f, 1.0f);

	public SpriteRenderer[] _sprites;

	public void Start(){
		reset();
	}

	public void reset()
	{
		foreach(SpriteRenderer sprite in _sprites)
		{
			sprite.color = defaultColor;
		}
	}

	public void highlight(int id)
	{
		_sprites[id].color = highlightColor;
	}
}
