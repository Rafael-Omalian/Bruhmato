using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    // Attributs de gestion de l'experience et du niveau du joueur
    private int level = 0;
    private float currExp = 0f;
    private float requiredToNextLevel = 10;

    // Attribut pour l'activation du choix d'amelioration
    [SerializeField] private UpgradeMenu upgradeMenu;

    public void GetExp(float expValue)
    {
        currExp += expValue;

        // Tant que l'experience depasse le palier pour le prochain niveau, le joueur monte de niveau
        while (currExp >= requiredToNextLevel)
        {
            currExp -= requiredToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        requiredToNextLevel = level * 10f *1.25f; // Calcule du prochain palier de niveau
        upgradeMenu.LevelUp();
    }
}
