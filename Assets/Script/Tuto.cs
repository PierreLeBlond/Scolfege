using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

//TODO Animation for text and hide/show

public class Tuto : MonoBehaviour {
	public bool mobile = false;

	public GameManager gameManager;

	public Vector3[] positions;

	public GameObject chooseTutoPanel;

	public Button button;
	public BlinkingButton blinkingButton;
	public Text text;
	//public SpriteRenderer background;

	//public Button continueButton;
	//public Text continueButtonText;

	public KeyboardArrow keyboardArrow;

	public String[] messages;

	private int _state = 0;

	private int _playTuto = 0;

	public bool continueTuto = false;
	public Collider2D topCollider;
	public Collider2D bottomCollider;
	public Collider2D tutoCollider;

	//private bool _reverse = true;
	/*void Awake(){
		text.CrossFadeAlpha(0f, 0f, false);
	}*/

	// Use this for initialization
	void Start () {
		button.gameObject.SetActive(false);
		hideCommand();
		generateMessage();
		text.text = messages[0];

		wait();
	}

	public void setTutoEnabled(bool b){
		if(b)
			gameObject.SetActive(true);
		else
			StartCoroutine(disableTuto());
	}

	public IEnumerator disableTuto(){
		yield return StartCoroutine(hide());
		continueGame();
		gameObject.SetActive(false);
	}

	public void generateMessage(){
		//continueButtonText.text = mobile?"Touchez ici pour continuer.":"appuyez sur espace pour continuer.";

		messages = new String[19];
		messages[0] = mobile?"Vous voila à l'intérieur d'une partition ! Appuyez ici pour continuer.":"Vous voila à l'intérieur d'une partition ! Appuyez sur espace pour continuer.";
		messages[1] = "Atteignez la partie supérieure de la portée.";
		messages[2] = "Atteignez la partie inférieure.";
		messages[3] = mobile?"Touchez la portée pour jouer la note, et lancer un projectile.":"Appuyez sur la fleche directionnelle droite pour jouer la note, et lancer un projectile.";
		messages[4] = "Le nom des notes s'affiche en haut de l'écran, prenez le temps de les apprendre avant de continuer.";
		messages[5] = "Le nom affiché en haut de l'écran correspond à l'une de ces notes, à vous de trouver la bonne.";
		messages[6] = "Lancez un projectile ou attendez de rencontrer la note que vous souhaitez choisir.";
		messages[7] = "Si celle ci correspond au nom en haut de l'écran, vous gagnerez des points !";
		messages[8] = "Mais si vous vous trompez, ou si vous ne choisissez pas assez vite, vous perdrez de la vie...";
		messages[9] = "Prêt ?";
		messages[10] = "Parfois, des bonus peuvent apparaître : ils rapportent des points ou des multiplicateurs de score !";
		messages[11] = "Mais ils rendent aussi le jeu plus difficile ! A vous de voir si cela vaut la peine de les prendre.";
		messages[12] = "Ce bonus ci change la clé de sol en clé de fa.";
		messages[13] = "Vous feriez bien de vous mettre à l'abri à la note indiquée !";
		messages[14] = "Ce bonus ne permet plus de voir les trois notes possibles !";
		messages[15] = mobile?"Ce bonus ne permet plus de voir le nom de la note, touchez l'icone en bas pour jouer le son à la place !":"Ce bonus ne permet plus de voir le nom de la note, touchez l'icone en bas pour jouer le son à la place !";
		messages[16] = "Vous vous deplacez maintenant à l'aide d'un clavier de piano !";
		messages[17] = mobile?"Touchez le clavier pour vous deplacer sur la partition":"Appuyez sur les touches du clavier pour vous deplacer sur la partition, et sur la portée pour jouer une note.";
		messages[18] = "Ce bonus permet d'annuler le bonus actuel, par exemple si le jeu devient trop difficile.";
	}

	// Update is called once per frame
	void Update () {
		if(!mobile && Input.GetKeyDown(KeyCode.Space)){
			continueGame();
		}
	}

	public void wait(){
		continueTuto = false;
	}

	public void continueGame(){
			continueTuto = true;
		//continueButton.gameObject.SetActive(false);
	}

	public void nextState(){
		_state++;
	}

	public IEnumerator hide()
	{
		hideCommand();
		text.CrossFadeAlpha(0.0f, 0.25f, false);
		button.GetComponent<Image>().GetComponent<CanvasRenderer>().SetAlpha(1.0f);
		button.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.5f, false);
		yield return new WaitForSeconds(0.5f);
		button.gameObject.SetActive(false);
	}

	public IEnumerator show(){
		//text.enabled = true;
		button.gameObject.SetActive(true);
		button.GetComponent<Image>().GetComponent<CanvasRenderer>().SetAlpha(0.0f);
		//text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
		text.CrossFadeAlpha(0.0f, 0.0f, false);
		text.text = messages[_state];
		//background.enabled = true;
		yield return new WaitForSeconds(0.25f);
		text.CrossFadeAlpha(1.0f, 1.0f, false);
		button.GetComponent<Image>().CrossFadeAlpha(1.0f, 0.5f, false);
		blinkingButton.play();
	}

	public void showCommand(Direction direction){
		if(!mobile){
			keyboardArrow.gameObject.SetActive(true);
			keyboardArrow.reset();
			keyboardArrow.highlight((int)direction);
		}
	}

	public void hideCommand(){
		keyboardArrow.gameObject.SetActive(false);
	}

	/*public void reverse(bool b){
		_reverse = b;
		if(!b)
		{
			background.transform.localPosition = new Vector3(0f, -2f, 0f);
			background.transform.localScale = new Vector3(2.25f, 1f, 1f);
			text.transform.localPosition = new Vector3(-3.5f, -3.5f, 0f);
			//continueButtonText.color = Color.black;
			continueButton.transform.localPosition = new Vector3(-3.5f, -5f, 0f);
		}
		else{
			background.transform.localPosition = new Vector3(0f, 2f, 0f);
			background.transform.localScale = new Vector3(2.25f, -1f, 1f);
			text.transform.localPosition = new Vector3(0f, 4f, 0f);
			//continueButtonText.color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
			continueButton.transform.localPosition = new Vector3(0f, 2f, 0f);
		}
	}*/

	public void place(int i){
		if(i >= 0 && i < positions.Length)
			button.transform.localPosition = positions[i];
	}

	public IEnumerator chooseTuto(){
		chooseTutoPanel.SetActive(true);
		while(_playTuto == 0) yield return null;
		chooseTutoPanel.SetActive(false);
		if(_playTuto == 1)
		{
			yield return StartCoroutine(startTuto());
			launchTutoCoroutines();
		}
		else if(_playTuto == 2)
		{
			gameManager.player.playerController.gameObject.SetActive(true);
			gameManager.touchZoneUp.gameObject.SetActive(true);
			//gameManager.touchZoneUp.hideUberHelp();
			gameManager.touchZoneUp.showHelp();
			gameManager.touchZoneDown.gameObject.SetActive(true);
			//gameManager.touchZoneDown.hideUberHelp();
			gameManager.touchZoneDown.showHelp();
			gameManager.playerUI.showScore();
			gameManager.playerUI.showLife();
			gameManager.player.reload();
			gameManager.touchZoneShoot.gameObject.SetActive(true);
			gameManager.getGameplay().setBlindBonus(true);
			gameManager.getGameplay().setPianoBonus(true);
			gameManager.getGameplay().setSoundBonus(true);
			gameManager.setFlatAttaqueAvailable(true);
		}
	}

	public void playTuto(){
		if(_playTuto == 0)
			_playTuto = 1;
	}

	public void abortTuto(){
		if(_playTuto == 0)
			_playTuto = 2;
	}

	public IEnumerator startTuto(){
		place(4);
		yield return StartCoroutine(show());
		gameManager.player.playerController.gameObject.SetActive(true);
		wait();
		while(!continueTuto) yield return null;
		//1: Go up
		yield return StartCoroutine(hide());
		_state = 1;
		//place(2);
		yield return StartCoroutine(show());
		blinkingButton.stop();
		gameManager.touchZoneUp.gameObject.SetActive(true);
		gameManager.touchZoneUp.showUberHelp();
		//Time.timeScale = 1;
		while(!topCollider.IsTouching(gameManager.player.GetComponent<Collider2D>())) yield return null;
		//2: Go down
		yield return StartCoroutine(hide());
		_state = 2;
		//place(3);
		yield return StartCoroutine(show());
		blinkingButton.stop();
		gameManager.touchZoneDown.gameObject.SetActive(true);
		gameManager.touchZoneUp.hideUberHelp();
		gameManager.touchZoneUp.showHelp();
		gameManager.touchZoneDown.showUberHelp();
		showCommand(Direction.DOWN);
		while(!bottomCollider.IsTouching(gameManager.player.GetComponent<Collider2D>())) yield return null;
		//3: Shoot
		yield return StartCoroutine(hide());
		_state = 3;
		yield return StartCoroutine(show());
		blinkingButton.stop();
		gameManager.touchZoneShoot.gameObject.SetActive(true);
		gameManager.touchZoneDown.hideUberHelp();
		gameManager.touchZoneDown.showHelp();
		gameManager.touchZoneShoot.showUberHelp();
		showCommand(Direction.RIGHT);
		gameManager.player.reload();
		while(gameManager.player.isLoaded()) yield return null;
		//4: learn notes
		yield return StartCoroutine(hide());
		_state = 4;
		yield return StartCoroutine(show());
		gameManager.touchZoneShoot.hideUberHelp();
		gameManager.touchZoneShoot.showHelp();
		hideCommand();
		blinkingButton.play();
		//tuto.reverse(false);
		wait();
		gameManager.info.setGameplay(gameManager.getGameplay());
		while(!continueTuto)
		{
			gameManager.info.setNote(gameManager.player.playerController.CurrentPositionId);
			yield return null;
		}
		yield return StartCoroutine(hide());
		//end first part of tuto
	}

	public void launchTutoCoroutines(){
		StartCoroutine(explainNote());
		StartCoroutine(explainAttack());
		StartCoroutine(explainKeyBonus());
		StartCoroutine(explainBlindBonus());
		StartCoroutine(explainSoundBonus());
		StartCoroutine(explainPianoBonus());
		StartCoroutine(explainBecarreBonus());
	}

	public IEnumerator explainNote(){
		yield return new WaitUntil(() => gameManager.getChord() && tutoCollider.IsTouching(gameManager.getChord().GetComponent<Collider2D>()));
		//explain basic gameplay
		//5: show notes
		yield return null;
		gameManager.getChord().pause();
		wait();
		_state = 5;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		//6: explain projectile or touch
		wait();
		yield return StartCoroutine(hide());
		_state = 6;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		//7: explain score
		wait();
		yield return StartCoroutine(hide());
		_state = 7;
		yield return StartCoroutine(show());
		gameManager.playerUI.showScore();
		while(!continueTuto) yield return null;
		//8: explain life
		wait();
		yield return StartCoroutine(hide());
		_state = 8;
		yield return StartCoroutine(show());
		gameManager.playerUI.showLife();
		while(!continueTuto) yield return null;
		//9: ready ?
		wait();
		yield return StartCoroutine(hide());
		_state = 9;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		//Time.timeScale = 1;
		yield return StartCoroutine(hide());
		gameManager.getChord().play();
	}

	public IEnumerator explainAttack(){
		yield return new WaitUntil(() => gameManager.getFlatAttaque());
		yield return null;
		//explain flat attaque
		gameManager.getFlatAttaque().pause();
		wait();
		_state = 13;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		gameManager.getFlatAttaque().play();
		yield return StartCoroutine(hide());
	}

	public IEnumerator explainKeyBonus(){
		Bonus bonus = gameManager.getBonus();
		yield return new WaitUntil(() => ( bonus.gameObject.activeSelf && bonus.getBonusType() <= 1 && tutoCollider.IsTouching(bonus.GetComponent<Collider2D>())));
		yield return null;
		//10: enter bonus
		//Time.timeScale = 0;
		bonus.pause();
		wait();
		_state = 10;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		//11: explain key bonus type
		wait();
		yield return StartCoroutine(hide());
		_state = 11;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		//Time.timeScale = 1;
		wait();
		yield return StartCoroutine(hide());
		_state = 12;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		bonus.play();
		yield return StartCoroutine(hide());
		gameManager.setFlatAttaqueAvailable(true);
		gameManager.getGameplay().setBlindBonus(true);
	}

	public IEnumerator explainBlindBonus(){
		Bonus bonus = gameManager.getBonus();
		yield return new WaitUntil(() => bonus.gameObject.activeSelf && bonus.getBonusType() == 2 && bonus.isTaken());
		yield return null;
		bonus.pause();
		wait();
		_state = 14;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		bonus.play();
		yield return StartCoroutine(hide());
		gameManager.getGameplay().setSoundBonus(true);
		gameManager.getGameplay().setBlindBonus(false);
	}

	public IEnumerator explainSoundBonus(){
		Bonus bonus = gameManager.getBonus();
		yield return new WaitUntil(() => bonus.gameObject.activeSelf && bonus.getBonusType() == 3 && bonus.isTaken());
		yield return null;
		bonus.pause();
		wait();
		_state = 15;
		place(1);
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		bonus.play();
		yield return StartCoroutine(hide());
		gameManager.getGameplay().setPianoBonus(true);
		gameManager.getGameplay().setSoundBonus(false);
		place(4);
	}

	public IEnumerator explainPianoBonus(){
		Bonus bonus = gameManager.getBonus();
		yield return new WaitUntil(() => bonus.gameObject.activeSelf && bonus.getBonusType() == 4 && bonus.isTaken());
		yield return null;
		//Time.timeScale = 0;
		bonus.pause();
		//place(0);
		wait();
		_state = 16;
		place(1);
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		yield return StartCoroutine(hide());
		gameManager.player.playerController.movable = false;
		yield return new WaitForSeconds(2);
		wait();
		_state = 17;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		yield return StartCoroutine(hide());
		//Time.timeScale = 1;
		bonus.play();
		gameManager.getGameplay().setBlindBonus(true);
		gameManager.getGameplay().setPianoBonus(true);
		gameManager.getGameplay().setSoundBonus(true);
		place(4);
	}

	public IEnumerator explainBecarreBonus(){
		Bonus bonus = gameManager.getBonus();
		yield return new WaitUntil(() => bonus.gameObject.activeSelf && bonus.getBonusType() == 5 && bonus.isTaken());
		yield return null;
		bonus.pause();
		wait();
		_state = 18;
		yield return StartCoroutine(show());
		while(!continueTuto) yield return null;
		bonus.play();
		yield return StartCoroutine(hide());
	}
}
