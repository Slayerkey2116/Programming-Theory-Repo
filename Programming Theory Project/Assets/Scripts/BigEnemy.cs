using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{

    public override void Health()
    {
        health = 10;
        pointValue = 5;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            gameManager.UpdateScore(pointValue);
        }
    }

    public override void Attack()
    {
        hp.Health -= 3;
    }

    public override void Move()
    {        
        speed = 1.5f;
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
        Health();
        Move();
    }
}
