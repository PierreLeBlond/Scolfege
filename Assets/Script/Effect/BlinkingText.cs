using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour {

	public float timing = 1.0f;

	private Text _text;
	private bool _fading = true;
	private float _time;

	// Use this for initialization
	void Start () {
		_text = GetComponent<Text>();
		if(!_text)
			Debug.Log("No text component found");
		_time = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if(_text){
			float time = Time.time - _time;
			if(_fading)
			{
				if(time < timing)
					_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1 - time/timing);
				else
				{
					_fading = false;
					_time = Time.time;
				}
			}
			else
			{
				if(time < timing)
					_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, time/timing);
				else
				{
					_fading = true;
					_time = Time.time;
				}
			 }
		}
	}
}
