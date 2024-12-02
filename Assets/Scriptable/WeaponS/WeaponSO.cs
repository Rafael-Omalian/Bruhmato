using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "WeaponSO", order = 0)]
public class WeaponSO : ScriptableObject
{
    [Header("Affichage")]
    public string weaponName;
    public Sprite weaponSprite;
    public string weaponDesc;

    [Header("Statisiques à changer")]
    public string weaponFileName;
    public float baseReloadTime;
    public bool isExplosive = false;
    public bool isPoison = false;
    public bool bounceIsRandom = false;
    public float baseDamage;
    public float baseReach;
    public int bounce;
    public int pierce;
}