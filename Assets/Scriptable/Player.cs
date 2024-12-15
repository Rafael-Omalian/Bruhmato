using UnityEngine;

[CreateAssetMenu(fileName = "newPlayer", menuName = "Player", order = 0)]
public class Player : ScriptableObject
{
    [Header("Affichage")]
    public string playerName;
    public Sprite playerSprite;
    public string playerDesc;

    [Header("Statisiques")]
    public float vieMax;
    public float atkSpeed;
    public float damage;
    public float reach;

    [Header("Arme de depart")]
    public WeaponSO startWeapon;
}