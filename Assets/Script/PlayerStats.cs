using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float atkSpeed = 1.0f;

    public float GetAtkSpeed()
    {
        return atkSpeed;
    }
}
