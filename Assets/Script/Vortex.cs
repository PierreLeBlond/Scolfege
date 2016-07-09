using UnityEngine;
using System.Collections;

public class Vortex : MonoBehaviour {

	public SpriteRenderer vortex;

	public AudioSource audio;

	public float scaleMax = 3.0f;

	public bool showing = false;
	public bool hiding = false;

	// Use this for initialization
	void Start () {
		vortex.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		vortex.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -(float)((Time.time*100)%360));
		if(showing)
		{
			if(vortex.transform.localScale.x < scaleMax)
			{
				vortex.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
				audio.volume += 0.05f/(scaleMax);
			}
		}else if(hiding)
		{
			if(vortex.transform.localScale.x > 0)
			{
				vortex.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
				audio.volume -= 0.05f/(scaleMax);
			}
			else{
				vortex.enabled = false;
				audio.Stop();
			}
		}
	}

	public void show(){
		vortex.enabled = true;
		hiding = false;
		showing = true;
		audio.Play();
	}

	public void hide(){
		showing = false;
		hiding = true;
	}
}
