using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponChoice : MonoBehaviour
{
    // Toutes les ameliorations possibles
    [SerializeField] private WeaponSO[] weapons;

    // Gestion de l'augmentation
    private WeaponSO[] weaponsAvailable = new WeaponSO[2];
    [SerializeField] private WeaponManager weaponManager;

    // Gestion de l'affichage
    [SerializeField] private TMP_Text[] displayTexts = new TMP_Text[2];

    void OnEnable()
    {
        Time.timeScale = 0;
        RandomChoices();
    }

    // Choix de l'option
    public void ChooseOption(int i)
    {
        weaponManager.GetWeapon(weaponsAvailable[i]);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void RandomChoices()
    {
        for (int i = 0; i < weaponsAvailable.Length; i++)
        {
            weaponsAvailable[i] = weapons[Random.Range(0, weapons.Length)];
            displayTexts[i].text = weaponsAvailable[i].weaponName;
        }
    }
}
