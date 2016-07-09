using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	//Offsets for a right display
    public float staveOffset = -3.0f;
    public float staveInterval = 0.5f;

	public Gameplay gameplay;

	public SpriteRenderer[] _bonusSprite;

	private int _bonusType;
	//0 : GKey
	//1 : FKey
	//2 : Blind
	//3 : Sound
	//4 : Piano


	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3(13f, staveOffset + Random.Range(2, 12)*staveInterval, 0f);
		Scrollable scroll = GetComponent("Scrollable") as Scrollable;
		scroll.speed = new Vector2 (3f, 0f);

		if(gameplay.defaultMode)
		{
			_bonusType = Random.Range(1, _bonusSprite.Length);
			while((!gameplay.soundBonus && _bonusType == 3) ||
			(!gameplay.blindBonus && _bonusType == 2) ||
			(!gameplay.pianoBonus && _bonusType == 4))
				_bonusType = Random.Range(0, _bonusSprite.Length);
		}else{
			_bonusType = Random.Range(0, 2);
		}

		if( _bonusType == 0 && gameplay.key == KeyEnum.GKey){
			_bonusType = 1;
		}else if( _bonusType == 1 && gameplay.key == KeyEnum.FKey){
			_bonusType = 0;
		}



		for(int i = 0; i < _bonusSprite.Length; ++i){
			_bonusSprite[i].enabled = false;
		}
		_bonusSprite[_bonusType].enabled = true;
		FadableToDeath fadableToDeath = GetComponent<FadableToDeath>();
		fadableToDeath.sprite = _bonusSprite[_bonusType];
	}

	public void OnTriggerEnter2D(Collider2D intruder)
    {
		if (intruder.CompareTag("LeftLimit"))
        {
			FadableToDeath fadableToDeath = GetComponent<FadableToDeath>();
			fadableToDeath.startFadingToDeath();
		}
	}

	public int getBonusType(){
		return _bonusType;
	}
}
