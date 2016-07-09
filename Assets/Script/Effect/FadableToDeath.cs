using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadableToDeath : MonoBehaviour {
	public float lifeTime = 2.0f;
	public SpriteRenderer sprite;
	public Text text;

	private Color _spriteColor = Color.white;
	private Color _textColor = Color.white;


	public void startFadingToDeath(){
			if(!sprite && !text)//If no given sprite or text, search in gameobject components
			{
				sprite = GetComponent<SpriteRenderer>();
				text = GetComponent<Text>();
			}
			if(sprite)
				_spriteColor = sprite.color;
			if(text){
				_textColor = text.color;
			}
			StartCoroutine(fadeToDeath());
	}

	IEnumerator fadeToDeath(){
		float deathTime = Time.time + lifeTime;
		while(Time.time < deathTime){
			if(sprite)
				sprite.color = Color.Lerp (Color.clear, _spriteColor, (deathTime - Time.time) / lifeTime);
			if(text)
				text.color = Color.Lerp (Color.clear, _textColor, (deathTime - Time.time) / lifeTime);
			yield return null;
		}
		Destroy(gameObject);
	}
}
