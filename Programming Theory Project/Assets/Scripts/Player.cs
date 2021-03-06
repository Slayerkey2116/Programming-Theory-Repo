using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player HP
    [SerializeField]
    private int MaxHealth = 10;
    public int Health { get; set; } // ENCAPSULATION

    public GameManager gameManager;

    [SerializeField] float speed = 10f;

    [SerializeField] LayerMask layerMask; // For Isometric or topdown mouse aim 
    private void Awake()
    {
        Health = MaxHealth;       
    }

    public void Start()
    {
        gameManager = GameObject.Find("Spawner").GetComponent<GameManager>();
    }
    public void Update()
    {
        AimTowardMouse();  // ABSTRACTION

        float horizontal = Input.GetAxis("Horizontal"); // Reads inout from WASD OR ARROWS KEYS
        float vertical = Input.GetAxis("Vertical"); // Reads inout from WASD OR ARROWS KEYS

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);

        // MOVEMENT
        if (inputDirection.magnitude > 0)
        {
            inputDirection.Normalize();
            inputDirection *= speed * Time.deltaTime;
            transform.Translate(inputDirection, Space.World);

        }

        void AimTowardMouse()  // For Isometric or topdown mouse aim
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
            {
                var direction = hitInfo.point - transform.position;
                direction.y = 0f;
                direction.Normalize();
                transform.forward = direction;
            }
        }

        // FIRE
        if (Input.GetKeyDown(KeyCode.Mouse0)) // When mouse is pressed fire
        {
            GameObject pooledProjectile = BulletPooler.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); // activate pooled object it
                pooledProjectile.transform.position = transform.position; // position it at player

                pooledProjectile.transform.rotation = transform.rotation;  // fires in direction the player is facing

                if (Health <= 0)
                {
                    pooledProjectile.SetActive(false);
                    
                }
            }
        }

        // Player Health, calls GameOver method from GameManager.cs
        if (Health <= 0)
        {
            gameManager.GameOver();
        }
        gameManager.UpdateHealth(0);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Wall") 
        {
            if (Health > 0)
            {
                Health = 0;
            }
        }
    }
}
