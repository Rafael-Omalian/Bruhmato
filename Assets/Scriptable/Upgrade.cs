using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 0)]
public class Upgrade : ScriptableObject
{
    [Header("Affichage")]
    public string upgradeName;
    public Sprite upgradeSprite;
    public string upgradeDesc;

    [Header("Statisiques à changer")]
    public float atkSpeed;
    public float vie;
    public float vieMax;
    public float damage;
    public float reach;
}