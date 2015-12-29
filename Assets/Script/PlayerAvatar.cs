using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour {

    public GameObject defaultSkin;
    public GameObject winSkin;
    public GameObject looseSkin;
    public GameObject assSkin;
    public GameObject sickSkin;

    public Piano pianoPrefab;

    private Piano _piano;//Le piano pour jouer les notes
    private bool _isWinning = true;
    private bool _isLoosing = false;

    // Use this for initialization
    void Start()
    {
        _piano = Instantiate(pianoPrefab) as Piano;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void setIsWinning(bool isWinning)
    {
        _isWinning = isWinning;
        _isLoosing = !isWinning;
    }

    public void win()
    {
        defaultSkin.SetActive(false);
        sickSkin.SetActive(false);
        winSkin.SetActive(true);
        assSkin.SetActive(false);
        StartCoroutine(defaultState());
    }

    public void loose()
    {
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

    public IEnumerator defaultState()
    {
        yield return new WaitForSeconds(2);
        winSkin.SetActive(false);
        looseSkin.SetActive(false);
        assSkin.SetActive(false);
        if (_isLoosing)
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
