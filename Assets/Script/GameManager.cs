using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//The chord prefab, the game manager is in charge of spaming these
	public Chord chordPrefab;
	public Bonus bonusPrefab;
	public FlatAttaque flatAttaquePrefab;

	public KeyPad gKeyPad;
	public KeyPad fKeyPad;

	public GameOver gameOver;
	public AudioSource sountrack;
	//the player manager
	public PlayerManager player;
	public PlayerUI playerUI;
	public Key key;
	public Info info;

	public Vortex vortex;
	public Tuto tuto;

	public Collider2D leftCollider;

	public TouchZone touchZoneShoot;
	public TouchZone touchZoneUp;
	public TouchZone touchZoneDown;

	//The current chord
	private Chord _chord;
	public Chord getChord(){
		return _chord;
	}

	private Level[] _levels;
	private Gameplay _gameplay = new Gameplay(true, false, true, true, false, KeyEnum.GKey);
	public Gameplay getGameplay()
	{
		return _gameplay;
	}

	private int _numberOfChord = 0;
	private bool _gameIsOver = false;

	private bool _generate = false;
	private bool _commandReady = true;

	private Bonus _bonus;
	public Bonus getBonus()
	{
		return _bonus;
	}

	private FlatAttaque _flatAttaque;
	public FlatAttaque getFlatAttaque(){
		return _flatAttaque;
	}
	private bool _flatAttaqueAvailable = false;
	public void setFlatAttaqueAvailable(bool b)
	{
		_flatAttaqueAvailable = b;
	}

	private int _probaBonus = 0;

	// Use this for initialization
	void Start () {
		_bonus = Instantiate (bonusPrefab) as Bonus;

		if (player) {
			player.playerController.CurrentPositionId = 6;
			player.transform.localPosition = new Vector3 (-2, -1, 0);
		}

		gKeyPad.create(KeyEnum.GKey);
		fKeyPad.create(KeyEnum.FKey);

		//_gameplay.pianoBonus = true;

		//tuto.mobile = true;
		generateLevels();
		info.setGameplay(_gameplay);
		player.setGameplay(_gameplay);
		StartCoroutine(startGame());
	}

	void generateLevels(){
		_levels = new Level[6];
		_levels[0] = new Level(new Vector2(2f, 0f), false, true, 0, 2);
		_levels[1] = new Level(new Vector2(2f, 0f), false, false, 4, 10);
		_levels[2] = new Level(new Vector2(2f, 0f), true, false, 2, 12);
		_levels[3] = new Level(new Vector2(2f, 0f), true, false, 2, 12);
		_levels[4] = new Level(new Vector2(3f, 0f), true, false, 2, 14);
		_levels[5] = new Level(new Vector2(3f, 0f), true, false, 2, 14);
	}

	public void generateBonus() {
		info.gameObject.SetActive(false);
		_bonus.gameplay = _gameplay;
		_bonus.reset();
		//yield return new WaitUntil(() => !_bonus.gameObject.activeSelf);
		//_generate = true;
	}

	public IEnumerator generateFlatAttaque() {
		_flatAttaque = Instantiate (flatAttaquePrefab) as FlatAttaque;
		_flatAttaque.limit = leftCollider;
		_flatAttaque.info = info;
		while(_flatAttaque) yield return null;
		_generate = true;
	}

	void generateChord () {
		if(Random.Range(0, 4 + _probaBonus) >= 4)
		{
			_probaBonus = 0;
			if(Random.Range(0, 3) > 0 || !_flatAttaqueAvailable || !_gameplay.defaultMode)
			{
				_flatAttaqueAvailable = true;
				generateBonus();
			}
			else
			{
				_flatAttaqueAvailable = false;
				StartCoroutine(generateFlatAttaque());
			}
		}else{
			_probaBonus++;

			_numberOfChord++;
			player.playerUI.score.oneMoreChord();

			_chord = Instantiate (chordPrefab) as Chord;

			Scrollable chordScroll = _chord.GetComponent<Scrollable>();

			_chord.setInfo(info);
			_chord.setGameplay(_gameplay);
			player.setGameplay(_gameplay);

			if(player.Level < 6)
			_chord.generateNotes(_levels[player.Level]);
			else{
				_levels[5].scrollSpeed = new Vector2(3f + (player.Level - 5)*0.5f, 0.0f);
				_chord.generateNotes(_levels[5]);
			}
		}

		//player.playerController.GhostNote = _chord.GhostNote;
    }

	public IEnumerator endGame(){
		_gameIsOver = true;
		sountrack.Stop();
		player.playerController.movable = false;
		playerUI.hide();
		yield return new WaitForSeconds(1);
		vortex.transform.localPosition = player.transform.localPosition;
		vortex.scaleMax = 1.0f;
		vortex.show();
		yield return new WaitForSeconds(1);
		player.playerAvatar.gameObject.SetActive(false);
		yield return new WaitForSeconds(1);
		vortex.hide();
		yield return new WaitForSeconds(1);
		gameOver.show();
	}

	// Update is called once per frame
	void Update () {
		if (player.Life <= 0 && !_gameIsOver) {
			StartCoroutine(endGame());
			//player.playerAvatar.gameOver();
		}else if(!_gameIsOver)
		{
			if (_chord && _chord.isDestroyed) {
				Destroy (_chord.gameObject);
				_generate = true;
			}

			if(_bonus.isDestroyed){
				_bonus.isDestroyed = false;
				_generate = true;
			}

			if(_generate && _commandReady)
			{
				info.gameObject.SetActive(true);
				generateChord();
				_generate = false;
			}

			if(Input.GetKey(KeyCode.Escape)){
				quitGame();
			}
		}
	}

	public IEnumerator startGame(){
		player.playerController.movable = false;
		playerUI.hide();
		player.playerAvatar.gameObject.SetActive(false);
		yield return new WaitForSeconds(1);
		vortex.scaleMax = 1.0f;
		vortex.show();
		yield return new WaitForSeconds(1);
		player.playerAvatar.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		vortex.hide();
		while(vortex.vortex.enabled) yield return null;
		yield return new WaitForSeconds(1);
		sountrack.Play();
		player.playerController.movable = true;
		yield return StartCoroutine(tuto.chooseTuto());
		generateChord();
	}

	public void changeKey(){
		if (_gameplay.key == KeyEnum.FKey)
		{
			key.setGKey();
			_gameplay.key = KeyEnum.GKey;
		}
		else
		{
			key.setFKey();
			_gameplay.key = KeyEnum.FKey;
		}

		if(_gameplay.piano)
		{
			StartCoroutine(switchPiano());
		}

		_chord.setGameplay(_gameplay);
		player.setGameplay(_gameplay);
	}

	public void enterBlindMode(){
		_gameplay.hint = false;
		_gameplay.defaultMode = false;
	}

	public void enterSoundMode(){
		_gameplay.sound = true;
		_gameplay.text = false;
		_gameplay.defaultMode = false;
	}

	public IEnumerator enterPianoMode(){
		_commandReady = false;
		player.playerController.movable = false;
		touchZoneUp.gameObject.SetActive(false);
		touchZoneDown.gameObject.SetActive(false);
		_gameplay.piano = true;
		_gameplay.defaultMode = false;
		if(_gameplay.key == KeyEnum.FKey)
			yield return StartCoroutine(fKeyPad.show());
		else if(_gameplay.key == KeyEnum.GKey)
			yield return StartCoroutine(gKeyPad.show());
		_commandReady = true;
	}

	public IEnumerator switchPiano(){
		if(_gameplay.key == KeyEnum.FKey)
		{
			while(!_chord) yield return null;
			_chord.pause();
			StartCoroutine(gKeyPad.hide());
			yield return StartCoroutine(fKeyPad.show());
			_chord.play();
		}else if(_gameplay.key == KeyEnum.GKey)
		{
			while(!_chord) yield return null;
			_chord.pause();
			StartCoroutine(fKeyPad.hide());
			yield return StartCoroutine(gKeyPad.show());
			_chord.play();
		}
	}

	public void enterDefaultMode(){
		player.playerController.movable = true;
		_gameplay.hint = true;
		_gameplay.sound = false;
		_gameplay.text = true;
		if(_gameplay.piano){
			_gameplay.piano = false;
			if(_gameplay.key == KeyEnum.FKey)
				StartCoroutine(fKeyPad.hide());
			else if(_gameplay.key == KeyEnum.GKey)
				StartCoroutine(gKeyPad.hide());
			touchZoneUp.gameObject.SetActive(true);
			touchZoneDown.gameObject.SetActive(true);
		}
		_gameplay.defaultMode = true;
	}

	public void quitGame()
	{
		UserManager.Instance.addNoteGameScore(player.playerUI.score.getScore());
		Application.LoadLevel("_NoteGame");
	}

	public void restartGame()
	{
		UserManager.Instance.addNoteGameScore(player.playerUI.score.getScore());
		Application.LoadLevel("_NoteGame");
	}

	public void backToMenu()
	{
		UserManager.Instance.addNoteGameScore(player.playerUI.score.getScore());
		Application.LoadLevel("_Menu");
	}
}
