using UnityEngine;
using System.Collections;

public struct Gameplay{
	public bool defaultMode;
	public bool sound;
	public bool text;
	public bool hint;
	public bool piano;

	public bool soundBonus;
	public bool blindBonus;
	public bool pianoBonus;

	public KeyEnum key;

	public Gameplay(bool p_defaultMode, bool p_sound, bool p_text, bool p_hint, bool p_piano, KeyEnum p_key){
		defaultMode = p_defaultMode;
		sound = p_sound;
		text = p_text;
		hint = p_hint;
		piano = p_piano;
		key = p_key;
		soundBonus = false;
		blindBonus = false;
		pianoBonus = false;
	}
}
