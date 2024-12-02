using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeChoice : MonoBehaviour
{
    // Toutes les ameliorations possibles
    [SerializeField] private Upgrade[] upgrades;

    // Gestion de l'augmentation
    private Upgrade[] upgradesAvailable = new Upgrade[3];
    [SerializeField] private PlayerStats playerStats;
    public int levels = 0;

    // Gestion de l'affichage
    [SerializeField] private TMP_Text[] displayTexts = new TMP_Text[3];

    void OnEnable()
    {
        RandomChoices();
    }

    // Choix de l'option
    public void ChooseOption(int i)
    {
        playerStats.StatUpgrade(upgradesAvailable[i]);
        levels--;
        if (levels <= 0)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        } else {
            RandomChoices();
        }
    }

    private void RandomChoices()
    {
        for (int i = 0; i < upgradesAvailable.Length; i++)
        {
            upgradesAvailable[i] = upgrades[Random.Range(0, upgrades.Length)];
            displayTexts[i].text = upgradesAvailable[i].upgradeName;
        }
    }
}
