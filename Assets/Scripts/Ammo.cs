using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float _ammoLifetime = 1f;

    private void Awake()
    {
        Destroy(gameObject, _ammoLifetime);
    }
}
