using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Player HP
    [SerializeField]
    private int MaxHealth = 10;
    public int Health { get; set; }
    public int damage = 1;

    // Sound effects
    private AudioSource playerAudio;
    public AudioClip fire;

    public GameManager gameManager;


    private void Awake()
    {
        Health = MaxHealth;       
    }
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // FIRE
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAudio.PlayOneShot(fire, 1.0f);

            GameObject pooledProjectile = BulletPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.position = transform.position; // position it at player

                pooledProjectile.transform.rotation = transform.rotation;  // fires in direction the player is facing

                if (Health <= 0)
                {
                    pooledProjectile.SetActive(false);
                    gameManager.GameOver();
                }
            }
        }

    }
}
