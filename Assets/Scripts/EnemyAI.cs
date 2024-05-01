using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _maxDistance;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ChaseUpdate();
    }

    private void ChaseUpdate()
    {
        if (IsPlayerNoticed())
        {
            GoToPatrolPoint(_player.gameObject.transform.position);
        }
        else
        {
            GoToPatrolPoint(_patrolPoints[Random.Range(0, _patrolPoints.Length)].position);
        }
    }

    private void GoToPatrolPoint(Vector3 point)
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(point);
        }
    }

    private bool IsPlayerNoticed()
    {
        Vector3 direction = transform.position - _player.transform.position;

        float angle = Vector3.Angle(transform.forward, direction);
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (angle <= _viewAngle && distance <= _maxDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
