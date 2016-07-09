using UnityEngine;
using System.Collections;

public enum ChoiceState {
	NONE, CHOSEN, HASCHOSEN
}

public class PlayerManager : MonoBehaviour {

	//Public
	public GameManager gameManager;
    public PlayerAvatar playerAvatar;//Sprite and animation
	public PlayerController playerController;//User input and position in the game
	public PlayerUI playerUI;

	public Vector3 scoreTarget = new Vector3(3.39f, 4.12f, 0f);

	private Gameplay _gameplay;
	public void setGameplay(Gameplay gameplay){
		_gameplay = gameplay;
	}

	public Piano piano;//Le piano pour jouer les notes

	public Projectile projectilePrefab;

	//Private
    private int _chosenNoteId;//Last note chosen by the player
	public int ChosenNoteId {
		get {
			return _chosenNoteId;
		}
	}

    private ChoiceState _hasChosenANote = ChoiceState.NONE;
	public ChoiceState HasChosenANote {
		get {
			return _hasChosenANote;
		}
		set {
			_hasChosenANote = value;
		}
	}

	private Projectile _projectile;

	private bool _loaded = false;

	private int _lifeWinningStreak = 0;
	private int _levelWinningStreak = 0;

	private int _life = 12;
	public int Life {
		get {
			return _life;
		}
	}

	private int _level;
	public int Level {
		get {
			return _level;
		}
	}


	// Use this for initialization
	void Start () {
		if(playerAvatar)
        	playerAvatar.transform.parent = transform;
		if (playerController)
			playerController.Transform = transform;

	}

	// Update is called once per frame
	void Update () {
		playerUI.score.updateScore ();
		playerUI.updateLife (_life);

		if(Input.GetKey(KeyCode.RightArrow))
		{
			shoot();
		}
	}

	public void shoot(){
		if (_loaded){
			_projectile = Instantiate (projectilePrefab) as Projectile;
			_projectile.id = playerController.CurrentPositionId;
			_projectile.player = this;
			_projectile.transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
			piano.playNote(playerController.CurrentPositionId, _gameplay.key);
			_loaded = false;
		}
	}

	//Reset projectile
	public void reload(){
		//Destroy (_projectile.gameObject);
		_loaded = true;
	}

	public bool isLoaded(){
		return _loaded;
	}

	public void hasDodge(){
		_lifeWinningStreak = 0;
		_levelWinningStreak = 0;

		playerUI.score.resetCombo();
		gameManager.enterDefaultMode();

		removeLife(1);

		if(_life <= 0)
		{
			playerAvatar.gameOver();
		}
		else{
			playerAvatar.showAss();
		}
	}

	public void hasWon(){
		_lifeWinningStreak++;
		_levelWinningStreak++;

		if(_lifeWinningStreak >= 5){
			addLife(1);
			_lifeWinningStreak = 0;
		}

		if(_levelWinningStreak >= 5){
			_level++;
			_levelWinningStreak = 0;
		}

		playerUI.score.addCombo();
		playerAvatar.win();
	}

	public void hasLost(){
		_lifeWinningStreak = 0;
		_levelWinningStreak = 0;
		removeLife(4);
		playerUI.score.resetCombo();
		gameManager.enterDefaultMode();

		if(_life <= 0)
		{
			playerAvatar.gameOver();
		}
		else{
			playerAvatar.loose();
		}
	}

	public void OnTriggerEnter2D(Collider2D intruder)
    {
        if (intruder.CompareTag("Chord"))
        {
			if(!chordIntruder(intruder, playerController.CurrentPositionId))
				hasDodge();
		}

		if (intruder.CompareTag("FlatAttaque"))
        {
			FlatAttaque attaque = intruder.GetComponent<FlatAttaque>();
			int id = attaque.safe;
			if(playerController.CurrentPositionId%7 != id)
			{
				hasLost();
			}
		}

		if(intruder.CompareTag("Bonus"))
		{
			Bonus bonus = intruder.GetComponent<Bonus>();
			int type = bonus.getBonusType();
			FadableToDeath fadableToDeath = bonus.GetComponent<FadableToDeath>();
			fadableToDeath.startFadingToDeath();
			if(type == 0 || type == 1){
				gameManager.changeKey();
			}
			else if(type == 2){
				gameManager.enterBlindMode();
			}
			else if(type == 3){
				gameManager.enterSoundMode();
			}
			else if(type == 4){
				gameManager.enterPianoMode();
			}
			bonus.GetComponent<Scrollable>().speed = new Vector2(0.5f, 0.5f);
			bonus.GetComponent<Scrollable>().direction = new Vector2(scoreTarget.x - bonus.transform.localPosition.x, scoreTarget.y - bonus.transform.localPosition.y);
		}
    }

	//return if player hasn't dodge
	public bool chordIntruder(Collider2D intruder, int noteId)
	{
		Chord chord = intruder.GetComponent<Collider2D>().GetComponent<Chord>();
		if(chord == null)
		{
			Debug.Log("Right tag Note, but component hasn't the according class");
		}
		else if(!chord.isDisabled)
		{

			if(!getResult(chord.checkPickedNote(noteId)))
				return false;
			//piano.playKey(_chosenNoteId);
		}
		return true;
	}

	//Return whether a note has been met or not
	public bool getResult(Chord.Result result)
	{
		bool b = false;
		switch(result){
			case (Chord.Result.None) :
			//hasDodge();
			break;
			case (Chord.Result.Win) :
			hasWon();
			b = true;
			break;
			case (Chord.Result.Loose) :
			hasLost();
			b = true;
			break;
			default:
			break;
		}
		return b;
	}

	public void addLife(int life){
		_life += life;
		if(_life > 12)
			_life = 12;
	}

	public void removeLife(int life){
		_life -= life;
		if (_life < 0)
			_life = 0;
	}
}
