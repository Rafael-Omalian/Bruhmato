using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq.Expressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Joueurs")]
    public Player[] players = new Player[2];

    [Header("UI")]
    public TMP_Text[] names = new TMP_Text[2];
    public Image[] sprites = new Image[2];
    public TMP_Text[] descriptions = new TMP_Text[2];

    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            names[i].text = players[i].playerName;
            sprites[i].sprite = players[i].playerSprite;
            descriptions[i].text = players[i].playerDesc;
        }
    }

    // Choix du joueur
    public void ChoosePlayer(Player player)
    {
        GameState.player = player;
        GameState.ResetStats();
        SceneManager.LoadScene(1);
    }
}
