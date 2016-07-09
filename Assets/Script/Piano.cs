using UnityEngine;
using System.Collections;

public class Piano : MonoBehaviour {

    public AudioSource _audioSource;

    public int pitchOffset = -5;

	// Use this for initialization
	void Start () {
		_audioSource.transform.parent = transform;
	}

    public void playNote(int note, KeyEnum key)
    {
        note += (int)key;
        //offset in white notes between audio source and wanted note
        int offset = note - pitchOffset;

        //Number of full octave from audio source to wanted note
        int octave = offset/7;

        //Start of remaining notes to get to wanted note
        int start = pitchOffset + 7*octave;

        int restnote = 0;

        for( int j = start; j < note; j++)
        {
            if(j%7 == 0 ||
            j%7 == 2 ||
            j%7 == 3 ||
            j%7 == 5 ||
            j%7 == 6)
            restnote++;
        }

        //_audioSource.clip = notes[note];
        _audioSource.pitch = Mathf.Pow(1.05946f, (float)(restnote + note - start)) * Mathf.Pow(2, octave);
        _audioSource.Play();
    }
}
