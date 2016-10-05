using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlayerState { DEFAULT, WIN, LOOSE, ASS, GAMEOVER  }

public class PlayerAvatar : MonoBehaviour {

	public Animator playerAnimator;
	public Renderer renderer;

	private PlayerState _playerState = PlayerState.DEFAULT;

    // Use this for initialization
    void Start()
    {
		mute();
    }

    // Update is called once per frame
    void Update()
    {
		int blink = Random.Range (0, 100);
		if(_playerState == PlayerState.DEFAULT && blink == 1)
		{
			StartCoroutine(blinke());
		}

		int lookAround = Random.Range (0, 500);
		if(lookAround == 1)
			playerAnimator.SetTrigger("LookAround");
    }

    public void setPlayerState(PlayerState state)
    {
		_playerState = state;
    }

    public void win()
    {
		switch(Random.Range(0, 2))
		{ 
			case 0 :
			playerAnimator.SetTrigger("Dab");
			break;
			case 1 :
			playerAnimator.SetTrigger("Win");
			break;
			default:
			playerAnimator.SetTrigger("Win");
			break;
		}
		_playerState = PlayerState.WIN;
		smile();
        StartCoroutine(defaultState());
    }

	public void shoot()
	{
		playerAnimator.SetTrigger("Shoot");
	}

	public void gameOver()
	{
		playerAnimator.SetTrigger("Gameover");
		_playerState = PlayerState.GAMEOVER;
		invertedSmile();
	}

    public void loose()
    {
		playerAnimator.SetTrigger("Loose");
		_playerState = PlayerState.LOOSE;
		invertedSmile();
        StartCoroutine(defaultState());
    }

    public void showAss()
    {
		playerAnimator.SetTrigger("Dodge");
		_playerState = PlayerState.ASS;
        StartCoroutine(defaultState());
    }

    public IEnumerator defaultState()
    {
        yield return new WaitForSeconds(2);
		_playerState = PlayerState.DEFAULT;
		mute();
    }

    public IEnumerator blinke()
    {
		closeEye();
        yield return new WaitForSeconds(0.5f);
		mute();
	}

	public void closeEye()
	{
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.5f));
	}

	public void mute()
	{
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0.0f, 0.5f));
	}

	public void smile()
	{
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0.0f, 0.0f));
	}

	public void invertedSmile()
	{
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.0f));
	}
}
