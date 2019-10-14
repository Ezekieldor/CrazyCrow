using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class Highscore : MonoBehaviour
{
	Text highScore;
    
    void Start()
    {
        highScore = GetComponent <Text> ();
		highScore.text = "HighScore: " + PlayerPrefs.GetInt ("HighScore").ToString ();
    }
}
