using UnityEngine;
using System.Collections;

public class PlayButtonAnimation : MonoBehaviour {
	// Use this for initialization
	void Start () {
            GetComponent<Animation>().Stop("TextBlink");
            GetComponent<Animation>().Play("TextFall");
	}

        public void fall() {
            GetComponent<Animation>().Stop("TextFall");
            GetComponent<Animation>().Play("TextBlink");
        }
	
}
