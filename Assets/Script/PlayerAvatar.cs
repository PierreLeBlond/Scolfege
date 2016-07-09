using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlayerState { DEFAULT, WIN, LOOSE, ASS, GAMEOVER  }

public class PlayerAvatar : MonoBehaviour {

	public Animator playerAnimator;

	private PlayerState _playerState = PlayerState.DEFAULT;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		int blink = Random.Range (0, 100);
		if(blink == 1)
			playerAnimator.SetTrigger("blink");
    }

    public void setPlayerState(PlayerState state)
    {
		_playerState = state;
    }

    public void win()
    {
		playerAnimator.SetTrigger("win");
		_playerState = PlayerState.WIN;
        StartCoroutine(defaultState());
    }

	public void gameOver()
	{
		playerAnimator.SetTrigger("gameover");
		_playerState = PlayerState.GAMEOVER;
	}

    public void loose()
    {
		playerAnimator.SetTrigger("loose");
		_playerState = PlayerState.LOOSE;
        StartCoroutine(defaultState());
    }

    public void showAss()
    {
		playerAnimator.SetTrigger("ass");
		_playerState = PlayerState.ASS;
        StartCoroutine(defaultState());
    }

    public IEnumerator defaultState()
    {
        yield return new WaitForSeconds(2);
		_playerState = PlayerState.DEFAULT;
    }
}
