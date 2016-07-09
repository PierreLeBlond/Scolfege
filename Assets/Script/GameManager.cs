using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//The chord prefab, the game manager is in charge of spaming these
	public Chord chordPrefab;
	public Bonus bonusPrefab;
	public FlatAttaque flatAttaquePrefab;

	public KeyPad keyPad;

	public GameOver gameOver;
	public AudioSource sountrack;
	//the player manager
	public PlayerManager player;
	public PlayerUI playerUI;
	public Key key;
	public Info info;

	public Vortex vortex;

	public Tuto tuto;

	public Collider2D topCollider;
	public Collider2D bottomCollider;
	public Collider2D tutoCollider;
	public Collider2D leftCollider;

	//The current chord
	private Chord _chord;
	private Level[] _levels;
	private Gameplay _gameplay = new Gameplay(true, false, true, true, false, KeyEnum.GKey);

	private int _numberOfChord = 0;
	private bool _gameIsOver = false;

	private bool _generate = false;

	private Bonus _bonus;
	private FlatAttaque _flatAttaque;
	private bool _flatAttaqueAvailable = false;

	// Use this for initialization
	void Start () {

		if (player) {
			player.playerController.CurrentPositionId = 6;
			player.transform.localPosition = new Vector3 (-2, -1, 0);
		}

		//_gameplay.pianoBonus = true;

		generateLevels();
		info.setGameplay(_gameplay);
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

	public IEnumerator generateBonus() {
		info.gameObject.SetActive(false);
		_bonus = Instantiate (bonusPrefab) as Bonus;
		_bonus.gameplay = _gameplay;
		while(_bonus) yield return null;
		_generate = true;
	}

	public IEnumerator generateFlatAttaque() {
		_flatAttaque = Instantiate (flatAttaquePrefab) as FlatAttaque;
		_flatAttaque.limit = leftCollider;
		_flatAttaque.info = info;
		while(_flatAttaque) yield return null;
		_generate = true;
	}

	void generateChord () {

		if(_numberOfChord > 3 && Random.Range(0, 3) == 1)
		{
			if(Random.Range(0, 2) == 0 || !_flatAttaqueAvailable)
				StartCoroutine(generateBonus());
			else
				StartCoroutine(generateFlatAttaque());
		}else{

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

	// Update is called once per frame
	void Update () {
		if (player.Life <= 0 && !_gameIsOver) {
			gameOver.show();
			_gameIsOver = true;
			sountrack.Stop();
			//player.playerAvatar.gameOver();
		}else if(!_gameIsOver)
		{
			if (_chord && _chord.isDestroyed) {
				Destroy (_chord.gameObject);
				_generate = true;

			}

			if(_generate)
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
		tuto.hide();
		tuto.continueGame();
		playerUI.hide();
		tuto.hideCommand();
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
		StartCoroutine(startTuto());
		//StartCoroutine(generateFlatAttaque());
		//enterPianoMode();
		//generateChord();
		player.playerController.movable = true;
	}

	public IEnumerator startTuto(){
		sountrack.Play();
		Time.timeScale = 0;
		tuto.reverse(true);
		tuto.show();
		player.playerController.gameObject.SetActive(true);
		tuto.wait();
		while(!tuto.continueTuto) yield return null;
		//1: Go up
		tuto.nextState();
		tuto.showCommand(Direction.UP);
		Time.timeScale = 1;
		while(!topCollider.IsTouching(player.GetComponent<Collider2D>())) yield return null;
		//2: Go down
		tuto.nextState();
		tuto.showCommand(Direction.DOWN);
		while(!bottomCollider.IsTouching(player.GetComponent<Collider2D>())) yield return null;
		//3: Shoot
		tuto.nextState();
		tuto.showCommand(Direction.RIGHT);
		player.reload();
		while(player.isLoaded()) yield return null;
		//4: learn notes
		tuto.nextState();
		tuto.hideCommand();
		tuto.reverse(false);
		tuto.wait();
		info.setGameplay(_gameplay);
		while(!tuto.continueTuto)
		{
			info.setNote(player.playerController.CurrentPositionId);
			yield return null;
		}
		tuto.hide();
		generateChord();
		//end first part of tuto
		while(!tutoCollider.IsTouching(_chord.GetComponent<Collider2D>())) yield return null;
		//explain basic gameplay
		//5: show notes
		yield return null;
		Time.timeScale = 0;
		tuto.show();
		tuto.hideCommand();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		//6: explain projectile or touch
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		//7: explain score
		playerUI.showScore();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		//8: explain life
		playerUI.showLife();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		//9: ready ?
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		Time.timeScale = 1;
		tuto.hide();
		//end notes tuto
		while(!_bonus || !tutoCollider.IsTouching(_bonus.GetComponent<Collider2D>())) yield return null;
		yield return null;
		//10: enter bonus
		Time.timeScale = 0;
		tuto.show();
		tuto.hideCommand();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		//11: explain key bonus type
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		Time.timeScale = 1;
		tuto.hide();
		_flatAttaqueAvailable = true;
		//end first bonus explain
		while(!_flatAttaque) yield return null;
		yield return null;
		//explain flat attaque
		Time.timeScale = 0;
		tuto.show();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		Time.timeScale = 1;
		tuto.hide();
		_gameplay.blindBonus = true;
		while(!_bonus || _bonus.getBonusType() != 2 || !_bonus.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>())) yield return null;
		yield return null;
		//explain second bonus
		Time.timeScale = 0;
		tuto.show();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		Time.timeScale = 1;
		tuto.hide();
		_gameplay.soundBonus = true;
		_gameplay.blindBonus = false;
		//end second bonus explain
		while(!_bonus || _bonus.getBonusType() != 3 || !_bonus.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>())) yield return null;
		yield return null;
		Time.timeScale = 0;
		tuto.show();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		Time.timeScale = 1;
		tuto.hide();
		_gameplay.pianoBonus = true;
		_gameplay.soundBonus = false;
		//end third bonus explain
		while(!_bonus || _bonus.getBonusType() != 4 || !_bonus.GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>())) yield return null;
		yield return null;
		Time.timeScale = 0;
		tuto.show();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		tuto.hide();
		player.playerController.movable = false;
		yield return new WaitForSeconds(2);
		tuto.show();
		tuto.wait();
		tuto.nextState();
		while(!tuto.continueTuto) yield return null;
		tuto.hide();
		Time.timeScale = 1;
		_gameplay.blindBonus = true;
		_gameplay.pianoBonus = true;
		_gameplay.soundBonus = true;
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

	public void enterPianoMode(){
		player.playerController.movable = false;
		_gameplay.piano = true;
		StartCoroutine(keyPad.show());
		_gameplay.defaultMode = false;
	}

	public void enterDefaultMode(){
		player.playerController.movable = true;
		_gameplay.defaultMode = true;
		_gameplay.hint = true;
		_gameplay.sound = false;
		_gameplay.text = true;
		if(_gameplay.piano){
			_gameplay.piano = false;
			StartCoroutine(keyPad.hide());
		}
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
