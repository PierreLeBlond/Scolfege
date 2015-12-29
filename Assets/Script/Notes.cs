using UnityEngine;
using System.Collections;

public enum NotesName {
	LA,
	SI,
    DO,
    RE,
    MI,
    FA,
    SOL
}

public static class Notes
{

    public static NotesName getNoteName(int noteId)
    {
        return (NotesName) noteId;
	}

    public static string getString(int NoteId)
    {
		string name;
        switch ((NotesName) (NoteId % 7))
        {
            case NotesName.DO:
			name = "Do";
			break;
            case NotesName.RE:
			name = "Re";
			break;
            case NotesName.MI:
			name = "Mi";
			break;
            case NotesName.FA:
			name = "Fa";
			break;
            case NotesName.SOL:
			name = "Sol";
			break;
            case NotesName.LA:
			name = "La";
			break;
            case NotesName.SI:
			name = "Si";
			break;
		default:
			name = "error";
			break;
		}
		return name;
	}
}
