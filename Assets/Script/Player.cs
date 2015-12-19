using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public GameObject defaultSkin;
    public GameObject winSkin;
    public GameObject looseSkin;
    public GameObject assSkin;
    public GameObject sickSkin;

    public Text _scoreText;

    private int _score = 0;

    private bool _hasTheChord = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void win ()
    {
        _score += 1;
        updateScore();
        _hasTheChord = true;
        defaultSkin.SetActive(false);
        sickSkin.SetActive(false);
        winSkin.SetActive(true);
        assSkin.SetActive(false);
        StartCoroutine(defaultState());
    }

    public void loose ()
    {
        _score -= 1;
        updateScore();
        _hasTheChord = true;
        defaultSkin.SetActive(false);
        sickSkin.SetActive(false);
        looseSkin.SetActive(true);
        assSkin.SetActive(false);
        StartCoroutine(defaultState());
    }

    public void showAss()
    {
        defaultSkin.SetActive(false);
        sickSkin.SetActive(false);
        looseSkin.SetActive(false);
        assSkin.SetActive(true);
        StartCoroutine(defaultState());
    }

    public bool HasTheChord()
    {
        return _hasTheChord;
    }

    public void setHasTheChord(bool b)
    {
        _hasTheChord = b;
    }

    private void updateScore()
    {
        _scoreText.text = _score.ToString();
        if(_score < 0)
        {
            _scoreText.color = Color.red;
        }
        else
        {
            _scoreText.color = Color.white;
        }
    }

    public IEnumerator defaultState ()
    {
        yield return new WaitForSeconds(2);
        winSkin.SetActive(false);
        looseSkin.SetActive(false);
        assSkin.SetActive(false);
        if(_score < 0)
        {
            defaultSkin.SetActive(false);
            sickSkin.SetActive(true);
        }
        else
        {
            defaultSkin.SetActive(true);
            sickSkin.SetActive(false);
        }
        
    }
}
