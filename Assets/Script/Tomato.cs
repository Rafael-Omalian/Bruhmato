using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : Weapons
{
    public override void Attack()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
