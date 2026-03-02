using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int size = 3;
    
    private void Start()
    {
        transform.localScale = 0.5f * size * Vector3.one;
    
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float speed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }
}
