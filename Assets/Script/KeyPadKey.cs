using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyPadKey : MonoBehaviour {

	public Button button;
	public int id = 9;

	public PlayerController player;

	void Start() {
		button.onClick.AddListener(delegate { player.CurrentPositionId = id;
			StartCoroutine(push()); });
	}

	IEnumerator push(){
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.localPosition.z);
		yield return new WaitForSeconds(0.2f);
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f, transform.localPosition.z);
	}

}
