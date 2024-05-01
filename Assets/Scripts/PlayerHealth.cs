using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100;

    public void DealDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}