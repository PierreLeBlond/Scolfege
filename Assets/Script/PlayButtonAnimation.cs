using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButtonAnimation : MonoBehaviour {

	public Text text;
	// Use this for initialization
	void Start () {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Play("TextFall");
	}

        public void fall() {
            GetComponent<Animation>().Stop("TextFall");
			text.gameObject.SetActive(false);
            //GetComponent<Animation>().Play("TextBlink");
        }

}
