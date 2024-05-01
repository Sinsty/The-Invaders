using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private float _health = 100;

    public void DealDamage(float damage)
    {
        Debug.Log("Ouch");
        _health -= damage;
        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Death");
        Destroy(gameObject);
    }
}
