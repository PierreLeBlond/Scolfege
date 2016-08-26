using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	//Offsets for a right display
    public float staveOffset = -3.0f;
    public float staveInterval = 0.5f;

	public bool isDestroyed = false;

	public Gameplay gameplay;

	public SpriteRenderer[] _bonusSprite;

	private int _bonusType;

	private int _lastBonusType = -1;

	private Scrollable _scroll;
	private FadableToDeath _fadableToDeath;

	private bool _hasBeenTaken = false;
	public void take()
	{
		_hasBeenTaken = true;
	}
	public bool isTaken()
	{
		return _hasBeenTaken;
	}

	//0 : GKey
	//1 : FKey
	//2 : Blind
	//3 : Sound
	//4 : Piano
	//5 : Default


	// Use this for initialization
	void Start () {
		_scroll = GetComponent("Scrollable") as Scrollable;
		_scroll.speed = new Vector2 (3f, 0f);

		gameObject.SetActive(false);


		_fadableToDeath = GetComponent<FadableToDeath>();
	}

	public void reset(){
		_hasBeenTaken = false;
		gameObject.SetActive(true);
		transform.localPosition = new Vector3(13f, staveOffset + Random.Range(2, 12)*staveInterval, 0f);
		_scroll.speed = new Vector2(3f, 0f);
		_scroll.direction = new Vector2(-1.0f, 0.0f);
		if(gameplay.defaultMode)
		{
			_bonusType = Random.Range(1, _bonusSprite.Length - 1);
			while((!gameplay.soundBonus && _bonusType == 3) ||
			(!gameplay.blindBonus && _bonusType == 2) ||
			(!gameplay.pianoBonus && _bonusType == 4) ||
			_bonusType == _lastBonusType)
			{
				_bonusType = Random.Range(1, _bonusSprite.Length - 1);
			}
		}else{
			int res = Random.Range(0, 2);
			if(res == 1)
				_bonusType = 1;
			else
				_bonusType = 5;
		}

		_lastBonusType = _bonusType;

		if( _bonusType == 0 && gameplay.key == KeyEnum.GKey){
			_bonusType = 1;
		}else if( _bonusType == 1 && gameplay.key == KeyEnum.FKey){
			_bonusType = 0;
		}

		for(int i = 0; i < _bonusSprite.Length; ++i){
			_bonusSprite[i].enabled = false;
		}
		_bonusSprite[_bonusType].enabled = true;
		_fadableToDeath.sprite = _bonusSprite[_bonusType];
	}

	public void OnTriggerEnter2D(Collider2D intruder)
    {
		if(intruder.CompareTag("LeftLimit") || intruder.CompareTag("Score"))
		{
			gameObject.SetActive(false);
			isDestroyed = true;
		}
	}

	public int getBonusType(){
		return _bonusType;
	}

	public void pause()
	{
		GetComponent<Scrollable>().pause();
	}

	public void play()
	{
		GetComponent<Scrollable>().play();
	}
}
