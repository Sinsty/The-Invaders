using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] private EnemyAI _enemyAI;

    public void DealDamage(float damage)
    {
        _enemyAI.StartResearch(true);
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
