using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

	public GameObject[] part;
	public GameObject back;

	public int partId = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPartId(int id){
		partId = id;
	}

	public int getPartId(){
		return partId;
	}

	public void setParts(){
		for (int i = 1; i < 5; i++) {
			if(i != partId)
				part[i].SetActive(false);
			else
				part[i].SetActive(true);
		}
	}
}
