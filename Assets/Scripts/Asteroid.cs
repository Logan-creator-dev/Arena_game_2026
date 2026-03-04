using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyedParticles;
    public int size = 3;

    public int pointValue;
    public ScoreManager scoreManager;
    
    public GameManager gameManager;
    private void Start()
    {
        transform.localScale = 0.5f * size * Vector3.one;
    
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float speed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * speed, ForceMode2D.Impulse);

        gameManager._asteroidCount++;
        
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            if (size == 3)
                pointValue = 10;
            else if (size == 2)
                pointValue = 20;
            else if (size == 1)
                pointValue = 10;
            
            scoreManager.ChangeScore(pointValue);
            
            gameManager._asteroidCount--;
            
            Destroy(collision.gameObject);

            if (size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Asteroid newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                    newAsteroid.size = size - 1;
                    newAsteroid.gameManager = gameManager;
                }
            }
            
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
