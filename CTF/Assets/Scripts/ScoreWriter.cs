using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWriter : MonoBehaviour
{
    private int blueScore;
    private int redScore;

    public Text blueSc;
    public Text redSc;
    private void Start()
    {
        blueScore = PlayerPrefs.GetInt("BlueTeam");
        redScore = PlayerPrefs.GetInt("RedTeam");
        PlayerPrefs.DeleteAll();
        
        //Writing
        blueSc.text = blueScore.ToString();
        redSc.text = redScore.ToString();
    }
}
