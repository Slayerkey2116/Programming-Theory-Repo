using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // INHERITANCE
{
    public int health = 1;
    public float speed = 10;
    public int damage = 1;

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
        gameManager = GameObject.Find("Spawner").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        hp = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Update()
    {
        //Looks at player
        transform.LookAt(player.transform);
        Move();
        //Checks player Health
        if (hp.Health <= 0)
        {
            pointValue = 0;
        }
        Health();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet")
        {
            health -= damage;
           
        }

        if (collider.gameObject.CompareTag("Player"))
        {
            Attack();
        }

    }

    public void Health()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
            gameManager.UpdateScore(pointValue);

        }
    }
    
    public virtual void Attack() 
    {
        hp.Health -= damage;
        Destroy(gameObject);
        print("Player HP -1");
    }
    
    public void Move()
    {
        //Moves toward player
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
