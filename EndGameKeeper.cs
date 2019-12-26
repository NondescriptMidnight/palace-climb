using UnityEngine;
using System.Collections;

public class EndGameKeeper : MonoBehaviour {

    void OnGUI()
    {
        GUIStyle scorer = new GUIStyle(GUI.skin.GetStyle("label"));
        scorer.fontSize = 35;
        scorer.normal.textColor = Color.red;

        GUI.Label(new Rect(360, 150 , 500, 100), "SCORE:  " + ScoreTrackering.scoreNum, scorer);

    }
}

