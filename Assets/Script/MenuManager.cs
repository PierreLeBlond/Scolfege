using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	static int exitState = Animator.StringToHash("Base Layer.Vortexing");

	public Animator animator;

	public GameObject title;

	public AudioSource orchestraSound;

	public GameObject avatar;

	public Button button;

	public Vortex vortex;
	public SpriteRenderer fondu;

	public Light light;
	public Light ambiantLight;

	public GameObject canvas;

	public GameObject creditCanvas;
	public Button creditButton;
	public Text creditText;
	public Image creditImage;

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
		//button.gameObject.SetActive(false);
		canvas.SetActive(false);
		animator.SetTrigger("Awake");
		StartCoroutine(launchGame());
	}

	public void quit(){
		Application.Quit();
	}

	public void showCredit() {
		creditCanvas.SetActive(true);
		canvas.SetActive(false);
		creditButton.GetComponent<Image>().GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		creditText.CrossFadeAlpha(0.0f, 0.0f, false);
		creditImage.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		creditText.CrossFadeAlpha(1.0f, 1.0f, false);
		creditButton.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, false);
		creditImage.CrossFadeAlpha(1.0f, 0.5f, false);
	}

	public void hideCredit() {
		creditText.CrossFadeAlpha(0.0f, 0.25f, false);
		creditButton.GetComponent<Image>().GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		creditButton.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.25f, false);
		creditImage.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		creditImage.CrossFadeAlpha(0.0f, 0.5f, false);
		StartCoroutine(hideCreditCoroutine());
	}

	public IEnumerator hideCreditCoroutine(){
		yield return new WaitForSeconds(0.5f);
		creditCanvas.SetActive(false);
		canvas.SetActive(true);
	}

	public void yawn(){
		Debug.Log("Yawn");
	}

	public IEnumerator launchGame(){
		while(orchestraSound.volume > 0){
			orchestraSound.volume -= 0.002f;
			yield return null;
		 }
		while(!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Vortexing")) yield return null;
		vortex.scaleMax = 3.0f;
		vortex.show();
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
