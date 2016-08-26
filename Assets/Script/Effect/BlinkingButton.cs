using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour {
	public float timing = 0.5f;

	public Color sourceColor;
	public Color targetColor;

	private Image _image;
	private bool _fading = true;
	private float _time;

	private bool _play;

	// Use this for initialization
	void Start () {
		_image = GetComponent<Image>();
		if(!_image)
			Debug.Log("No image component found");
		stop();
		hide();
	}

	public void play () {
		_time = Time.time;
		_play = true;
	}

	public void stop(){
		_play = false;
		_image.color = sourceColor;
	}

	public void hide(){
		_image.color = sourceColor;
	}

	public void show(){
		_image.color = targetColor;
	}

		// Update is called once per frame
	void Update () {
		if(_play && _image){
			float time = Time.time - _time;
			if(_fading)
			{
				if(time < timing)
					_image.color = Color.Lerp(sourceColor, targetColor, 1 - time/timing);
				else
				{
					_fading = false;
					_time = Time.time;
				}
			}
			else
			{
				if(time < timing)
					_image.color = Color.Lerp(sourceColor, targetColor, time/timing);
				else
				{
					_fading = true;
					_time = Time.time;
				}
			 }
		}
	}
}
