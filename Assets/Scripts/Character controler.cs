using UnityEngine;

public class Character_controller : MonoBehaviour
{
    [Header("Ship parameters")] [SerializeField]
    private float _speed = 5f;

    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 180f;
    [SerializeField] private float _ammoSpeed = 8f;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _hyperSpaceRate = 3f;

    [Header("Object references")] [SerializeField]
    private Transform _ammoSpawn;

    [SerializeField] private Rigidbody2D _ammoPrefab;
    [SerializeField] private ParticleSystem destroyedParticles;

    public float nextHyperSpaceTime;
    
    private bool _isAlive = true;
    private bool _isSpeed;
    private float _nextFireTime;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isAlive)
        {
            HandleSpeed();
            HandleRotation();
            HandleShooting();
            HandleHyperSpace();
        }
    }

    private void FixedUpdate()
    {
        if (_isAlive && _isSpeed)
        {
            _rigidbody.AddForce(_speed * transform.up);
            _rigidbody.linearVelocity = Vector2.ClampMagnitude(_rigidbody.linearVelocity, _maxSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            _isAlive = false;

            var gameManager = FindAnyObjectByType<GameManager>();

            gameManager.GameOver();

            Instantiate(destroyedParticles, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void HandleSpeed()
    {
        _isSpeed = Input.GetKey(KeyCode.W);
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(_rotationSpeed * Time.deltaTime * transform.forward);
        else if (Input.GetKey(KeyCode.D)) transform.Rotate(-_rotationSpeed * Time.deltaTime * transform.forward);
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextFireTime)
        {
            var ammo = Instantiate(_ammoPrefab, _ammoSpawn.position, Quaternion.identity);

            var shipVelocity = _rigidbody.linearVelocity;
            Vector2 shipDirection = transform.up;
            var shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            if (shipForwardSpeed > 0) shipForwardSpeed = 0;

            ammo.linearVelocity = shipDirection * shipForwardSpeed;

            ammo.AddForce(_ammoSpeed * transform.up, ForceMode2D.Impulse);

            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void HandleHyperSpace()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextHyperSpaceTime)
        {
            var chance = Random.value;

            /*if (chance < 0.15f)
            {
                Destroy(gameObject);
            }*/
            var x = Random.Range(-8f, 8f);
            var y = Random.Range(-4f, 4f);

            transform.position = new Vector2(x, y);

            nextHyperSpaceTime = Time.time + _hyperSpaceRate;
        }
    }
}