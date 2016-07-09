using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {


	public Score score;

	public Life[] lifes;

	public void hide(){
		score.gameObject.SetActive(false);
		foreach(Life life in lifes){
			life.gameObject.SetActive(false);
		}
	}

	public void showAll(){
		showScore();
		showLife();
	}

	public void showScore(){
		score.gameObject.SetActive(true);
	}

	public void showLife(){
		foreach(Life life in lifes){
			life.gameObject.SetActive(true);
		}
	}

	public void updateLife(int life){
		for (int i = 0; i < lifes.Length; i++) {
			if(life >= (i+1)*4){
				lifes[i].setPartId(4);
				lifes[i].setParts();
			}else if( life > i*4 && life < (i+1)*4){
				lifes[i].setPartId(life%4);
				lifes[i].setParts();
			}else{
				lifes[i].setPartId(0);
				lifes[i].setParts();
			}
		}
	}
}
