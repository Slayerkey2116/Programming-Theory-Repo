using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{

    public override void Attack()
    {
        hp.Health -= damage;
        Destroy(gameObject);
        print("Player HP -3");
    }
}
