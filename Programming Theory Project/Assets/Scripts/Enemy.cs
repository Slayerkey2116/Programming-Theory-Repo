using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed = 1;

    // Calls GameManager script
    public GameManager gameManager;
    // Finds Player object
    public GameObject player;
    // Gives object a point value of 1
    public int pointValue = 1;
    // Calls Player script
    public Player hp;

    void Start()
    {
     
    }

    private void Awake()
    {
        gameManager = GameObject.Find("Spawner").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        hp = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update()
    {
        //Looks at player
        this.transform.LookAt(player.transform);
        Move();
        //Checks player Health
        if (hp.Health <= 0)
        {
            pointValue = 0;
        }

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
        {
            health -= 1;            
        }

        if (collider.gameObject.CompareTag("Player"))
        {
            Attack();
        }

    }

    public virtual void Health()
    {
        health = 3;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            gameManager.UpdateScore(pointValue);

        }
    }

    public virtual void Attack()
    {
        hp.Health -= 1;
    }
    public virtual void Move()
    {
        //Moves toward player
       // transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
