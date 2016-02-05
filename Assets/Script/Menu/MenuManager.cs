using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadNoteGame()
    {
        Application.LoadLevel("_NoteGame");
    }

	public void LoadSoundGame()
	{
		Application.LoadLevel("_SoundGame");
	}

	public void OpenProfil()
	{
		Application.LoadLevel("_Profil");
	}

	public void LoadTuto()
	{
		Application.LoadLevel ("_Tuto");
	}
}
