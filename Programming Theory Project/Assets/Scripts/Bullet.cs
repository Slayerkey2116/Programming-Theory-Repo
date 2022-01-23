using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet : MonoBehaviour
{

    private float speed = 25;

    private GameManager gameManager;
    public void Start()
    {
        gameManager = GameObject.Find("Spawner").GetComponent<GameManager>();
    }



    public void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed); // propels bullet forward            
    }

    public void OnTriggerEnter(Collider collider)
    {        
        if (collider.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }

        if (collider.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }
}
