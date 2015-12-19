using UnityEngine;
using System.Collections;

public enum KeysName {
	DO,
	RE,
	MI,
	FA,
	SOL,
	LA,
	SI
}

public static class Keys {

	public static KeysName getKeyName( int y ){
		return (KeysName) y;
	}

	public static string getString ( int keyId ){
		string name;
		switch((KeysName) keyId){
		case KeysName.DO :
			name = "Do";
			break;
		case KeysName.RE :
			name = "Re";
			break;
		case KeysName.MI :
			name = "Mi";
			break;
		case KeysName.FA :
			name = "Fa";
			break;
		case KeysName.SOL :
			name = "Sol";
			break;
		case KeysName.LA :
			name = "La";
			break;
		case KeysName.SI :
			name = "Si";
			break;
		default:
			name = "error";
			break;
		}
		return name;
	}
}
