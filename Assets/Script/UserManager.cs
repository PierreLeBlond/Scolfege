using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserManager : Singleton<UserManager> {

	protected UserManager() {}

	private List<int> _noteGameScores = new List<int>();
	private int numberOfNoteGame;

	private List<int> _soundGameScores = new List<int>();
	private int numberOfSoundGame;

	public int getTopNoteGameScore(){
		int top = 0;
		foreach(int score in _noteGameScores){
			if(score > top){
				top = score;
			}
		}
		return top;
	}

	public int getTopSoundGameScore(){
		int top = 0;
		foreach(int score in _soundGameScores){
			if(score > top){
				top = score;
			}
		}
		return top;
	}

	public void addNoteGameScore(int score){
		_noteGameScores.Add(score);
	}

	public void addSoundGameScore(int score){
		_soundGameScores.Add(score);
	}

	public int getNumberOfNoteGame(){
		return _noteGameScores.Count;
	}

	public int getNumberOfSoundGame(){
		return _soundGameScores.Count;
	}

	public float getMeanNoteGameScore(){
		float mean = 0.0f;
		foreach(int score in _noteGameScores){
			mean += score;
		}
		return mean/(float)_noteGameScores.Count;
	}

	public float getMeanSoundGameScore(){
		float mean = 0.0f;
		foreach(int score in _soundGameScores){
			mean += score;
		}
		return mean/(float)_soundGameScores.Count;
	}
}
