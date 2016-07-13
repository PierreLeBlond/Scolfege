using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

//TODO Animation for text and hide/show

public class Tuto : MonoBehaviour {
	public bool mobile = false;

	public Text text;
	public SpriteRenderer background;

	public Button continueButton;
	public Text continueButtonText;

	public KeyboardArrow keyboardArrow;
	public GameObject 	 pointer;

	public String[] messages;

	public int state = 0;

	public bool continueTuto = false;

	private bool _reverse = true;

	// Use this for initialization
	void Start () {
		generateMessage();
		text.text = messages[0];
		wait();
	}

	public void generateMessage(){

		continueButtonText.text = mobile?"Touchez ici pour continuer.":"appuyez sur espace pour continuer.";

		messages = new String[17];
		messages[0] = mobile?"Vous voila à l'intérieur d'une partition !":"Vous voila à l'intérieur d'une partition ! Appuyez sur espace pour continuer.";
		messages[1] = "Déplacez vous vers le haut pour atteindre la partie supérieur de la portée.";
		messages[2] = "Déplacez vous maintenant vers le bas pour atteindre la partie inférieur.";
		messages[3] = mobile?"Touchez la portée pour jouer la note, et lancer un projectile.":"Appuyez sur la fleche directionnelle droite pour jouer la note, et lancer un projectile.";
		messages[4] = "Le nom des notes s'affichent en haut de l'écran, prenez le temps de les apprendre avant de continuer.";
		messages[5] = "Le nom affiché en haut de l'écran correspond à l'une de ces notes, à vous de trouver la bonne.";
		messages[6] = "Lancez un projectile ou attendez de rencontrer la note que vous souhaitez choisir.";
		messages[7] = "Si celle ci correspond au nom en haut de l'écran, vous gagnerez des points !";
		messages[8] = "Mais si vous vous trompez, ou si vous ne choisissez pas assez vite, vous perdrez de la vie...";
		messages[9] = "Prêt ?";
		messages[10] = "Parfois, des bonus peuvent apparaître : ils rapportent des points ou des multiplicateurs de score !";
		messages[11] = "Mais ils rendent aussi le jeu plus difficile ! Ce bonus change la clé de sol en clé de fa.";
		messages[12] = "Vous feriez bien de vous mettre à l'abri à la note indiqué !";
		messages[13] = "Ce bonus ne permet plus de voir les choix de notes, il faut trouver du premier coup !";
		messages[14] = mobile?"Ce bonus ne permet plus de voir le nom de la note, touchez l'icone en haut pour jouer le son à la place !":"Ce bonus ne permet plus de voir le nom de la note, touchez l'icone en haut pour jouer le son à la place !";
		messages[15] = "Vous vous deplacez maintenant à l'aide d'un clavier de piano !";
		messages[16] = mobile?"Touchez le clavier pour vous deplacer sur la partition":"Appuyez sur les touches du clavier pour vous deplacer sur la partition, et sur la portée pour jouer une note.";
	}

	// Update is called once per frame
	void Update () {
		if(!mobile && Input.GetKeyDown(KeyCode.Space)){
			continueGame();
		}
	}

	public void wait(){
		continueTuto = false;
		continueButton.gameObject.SetActive(true);
	}

	public void continueGame(){
		continueTuto = true;
		continueButton.gameObject.SetActive(false);
	}

	public void nextState(){
		state++;
		text.text = messages[state];
	}

	public void show(){
		text.enabled = true;
		background.enabled = true;
	}

	public void showCommand(Direction direction){
		if(mobile){
			pointer.SetActive(true);
			switch(direction){
			case Direction.UP:
				pointer.transform.localPosition = new Vector3(-5.53f, 3.61f, 0f);
				break;
			case Direction.DOWN:
				pointer.transform.localPosition = new Vector3(5.38f, -3.66f, 0f);
				break;
			case Direction.RIGHT:
				pointer.transform.localPosition = new Vector3(0f, 0f, 0f);
				break;
			}
		}
		else{
			keyboardArrow.gameObject.SetActive(true);
			keyboardArrow.reset();
			keyboardArrow.highlight((int)direction);
		}
	}

	public void hideCommand(){
			pointer.SetActive(false);
			keyboardArrow.gameObject.SetActive(false);
	}

	public void hide(){
		text.enabled = false;
		background.enabled = false;
		hideCommand();
	}

	public void reverse(bool b){
		_reverse = b;
		if(!b)
		{
			background.transform.localPosition = new Vector3(0f, -2f, 0f);
			background.transform.localScale = new Vector3(2.25f, 1f, 1f);
			text.transform.localPosition = new Vector3(-3.5f, -3.5f, 0f);
			continueButtonText.color = Color.black;
			//continueButton.transform.localPosition = new Vector3(0f, -4f, 0f);
		}
		else{
			background.transform.localPosition = new Vector3(0f, 2f, 0f);
			background.transform.localScale = new Vector3(2.25f, -1f, 1f);
			text.transform.localPosition = new Vector3(0f, 4f, 0f);
			continueButtonText.color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
			//continueButton.transform.localPosition = new Vector3(0f, 2.6f, 0f);
		}
	}
}
