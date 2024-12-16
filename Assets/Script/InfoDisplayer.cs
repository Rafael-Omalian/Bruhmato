using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoDisplayer : MonoBehaviour
{
    public TMP_Text waveDisplayer;
    public TMP_Text healthDisplayer;
    public PlayerStats playerStats;

    void Start()
    {
        waveDisplayer.text = "Vague : " + GameState.currWave;
        healthDisplayer.text = playerStats.GetHealth().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        waveDisplayer.text = "Vague : " + GameState.currWave;
        healthDisplayer.text = playerStats.GetHealth().ToString();
    }
}
