using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] private EnemyAI _enemyAI;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;

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
        _navMeshAgent.enabled = false;
        _enemyAI.enabled = false;
        _animator.SetTrigger("Death");
        Destroy(gameObject, 10);
    }
}
