using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameState
{
    public static Player player;
    public static int currWave;

    // Evolution de la partie
    public static float enemyHealthMult = 1.0f;

    public static void ResetStats()
    {
        currWave = 0;
        enemyHealthMult = 1.0f;
    }

    public static void UpgradeEnemies()
    {
        if (currWave % 5 == 0)
        {
            enemyHealthMult += 0.5f;
        }
    }
}
