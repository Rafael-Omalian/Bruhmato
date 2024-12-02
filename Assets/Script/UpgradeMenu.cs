using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private UpgradeChoice choiceMenu;

    public void LevelUp()
    {
        choiceMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        choiceMenu.levels++;
    }
}
