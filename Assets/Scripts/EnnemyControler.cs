using UnityEngine;

public class EnnemyControler : MonoBehaviour
{
    public float speed = 3f;

    private Transform _player;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameObject playerobj = GameObject.FindGameObjectWithTag("Player");

        if (playerobj != null)
        {
            _player = playerobj.transform;
        }
    }

    private void FixedUpdate()
    {
        if (_player == null) return;
       
        Vector2 direction = (_player.position - transform.position).normalized;
        _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
