using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	static int exitState = Animator.StringToHash("Base Layer.Vortexing");

	public Animator animator;

	public GameObject title;

	public AudioSource audio;

	public GameObject avatar;

	public Button button;

	public Vortex vortex;
	public SpriteRenderer fondu;

	public Light light;
	public Light ambiantLight;

	private float _scaleFactor = 0f;

	// Use this for initialization
	void Start () {
		fondu.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if(fondu.enabled)
		{
			if(fondu.transform.localScale.x > 0.05)
			{
				_scaleFactor += 0.01f;
				float scale = 1f/(_scaleFactor + 0.05f);
				fondu.transform.localScale = new Vector3(scale, scale, scale);
			}

			if(light.intensity > 0)
				light.intensity -= 0.05f;

			if(ambiantLight.intensity > 0)
				ambiantLight.intensity -= 0.05f;
		}
	}

	public void play() {
		button.gameObject.SetActive(false);
		animator.SetTrigger("Awake");
		StartCoroutine(launchGame());
	}

	public IEnumerator launchGame(){
		while(audio.volume > 0){
			audio.volume -= 0.002f;
			Debug.Log("coucou");
			yield return null;
		 }
		yield return new WaitForSeconds(6);
		Debug.Log("hello");
		vortex.scaleMax = 3.0f;
		vortex.show();
		while(!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Vortexing")) yield return null;
		yield return new WaitForSeconds(6);
		fondu.enabled = true;
		yield return new WaitForSeconds(1);
		avatar.SetActive(false);
		vortex.hide();
		title.SetActive(false);
		yield return new WaitForSeconds(1);
		Application.LoadLevel("_NoteGame");
	}
}
