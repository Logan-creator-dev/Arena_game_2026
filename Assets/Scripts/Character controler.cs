
using System.Collections;
using UnityEngine;

public class Character_controller : MonoBehaviour
{
    [Header("Ship parameters")]
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private float _ammoSpeed = 8f;
    [SerializeField] private float _fireRate = 0.25f;
    
    [Header("Object references")]
    
    [SerializeField] private Transform _ammoSpawn;
    [SerializeField] private Rigidbody2D _ammoPrefab;
    [SerializeField] private ParticleSystem destroyedParticles;
    
    float nextFireTime = 0f;
    private Rigidbody2D Rigidbody;
    private bool isAlive = true;
    private bool isSpeed;
    
    private void Start()
    {
        Rigidbody  = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAlive)
        {
            HandleSpeed();
            HandleRotation();
            HandleShooting();
            HandleHyperSpace();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive && isSpeed)
        {
            Rigidbody.AddForce(_speed * transform.up);
            Rigidbody.linearVelocity = Vector2.ClampMagnitude(Rigidbody.linearVelocity, _maxSpeed);
            
        }
    }
    
    private void HandleSpeed()
    {
        isSpeed = Input.GetKey(KeyCode.W);
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
           transform.Rotate(_rotationSpeed * Time.deltaTime *  transform.forward); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-_rotationSpeed * Time.deltaTime *  transform.forward);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Rigidbody2D ammo = Instantiate(_ammoPrefab, _ammoSpawn.position, Quaternion.identity);

            Vector2 shipVelocity = Rigidbody.linearVelocity;
            Vector2 shipDirection = transform.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            if (shipForwardSpeed > 0)
            {
                shipForwardSpeed = 0;
            }

            ammo.linearVelocity = shipDirection * shipForwardSpeed;
            
            ammo.AddForce(_ammoSpeed * transform.up, ForceMode2D.Impulse);
            
            nextFireTime = Time.time + _fireRate;
        }
    }

    private void HandleHyperSpace()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float chance = Random.value;

            /*if (chance < 0.15f)
            {
                Destroy(gameObject);
            }*/
                float x = Random.Range(-8f, 8f);
                float y = Random.Range(-4f, 4f);

                transform.position = new Vector2(x, y);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            isAlive = false;
            
            GameManager gameManager = FindAnyObjectByType<GameManager>();

            gameManager.GameOver();
            
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            
            Destroy(gameObject);


        }
    }
}
