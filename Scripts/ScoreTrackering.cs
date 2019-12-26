using UnityEngine;
using System.Collections;

public class ScoreTrackering : MonoBehaviour {

    public static int scoreNum = 0;

    void OnGUI()
    {
        GUIStyle scorer = new GUIStyle(GUI.skin.GetStyle("label"));
        scorer.fontSize = 35;
        scorer.normal.textColor = Color.red;

        GUI.Label(new Rect(5, 5, 500, 100), "SCORE:  " + scoreNum, scorer);

    }
}
