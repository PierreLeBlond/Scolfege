using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public GameObject GKey;
	public GameObject FKey;

	// Use this for initialization
	void Start () {
		setGKey ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setFKey(){
		GKey.SetActive (false);
		FKey.SetActive (true);
	}

	public void setGKey(){
		FKey.SetActive (false);
		GKey.SetActive (true);
	}
}
