using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyAI : MonoBehaviour
{
    public enum EnemyStates
    {
        patroling = 1,
        holding = 2,
        shooting = 3,
        chasing = 4,
        researching = 5,
    }

    public EnemyStates State { get; private set; }

    [Header("Targets")]
    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Transform _holdingPoint;
    [SerializeField] private Transform[] _patrolPoints;
    [Header("Distances")]
    [SerializeField] private float _maxShootTriggerDistance = 50;
    [SerializeField] private float _maxShootingDistance = 10;
    [SerializeField] private float _maxChasingDistance = 20;
    [SerializeField] private float _enemyTriggerRadius = 25;
    [Header("State settings")]
    [SerializeField] private float _viewAngle = 90;
    [SerializeField] private float _multiplySearchSensWhileHolding = 1f;
    [SerializeField] private float _multiplySearchSensWhileResearching = 1f;
    [SerializeField] private float _holdingTime = 10;
    [SerializeField] private float _chahsingSpeed = 7;
    [SerializeField] private float _patrolSpeed = 5;
    [Header("Shooting settings")]
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private float _rotatingToTargetSpeed = 1;
    [SerializeField] private float _fireRate = 1;
    [SerializeField] private float _damage = 5;
    [SerializeField, Range(0, 1)] private float _hitChanse = 1;
    [Header("Animations")]
    [SerializeField] private Animator _animator;

    private float _timer;
    private float _nextTimeToFire;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        Gun.OnHit.AddListener(HeardShots);
    }

    private void FixedUpdate()
    {
        SwitchStatesUpdate();
    }

    private void HeardShots(Vector3 hotPoint)
    {
        float maxHearDistance = _maxShootTriggerDistance;

        if (State == EnemyStates.holding)
            maxHearDistance *= _multiplySearchSensWhileHolding;
        else if (State == EnemyStates.researching)
            maxHearDistance *= _multiplySearchSensWhileResearching;

        float distance = Vector3.Distance(transform.position, hotPoint);

        if (distance < maxHearDistance)
        {
            print("HeardShots");
            if (State != EnemyStates.holding && State != EnemyStates.researching)
            {
                SwitchStateAllEnemyInRadius(_enemyTriggerRadius, EnemyStates.holding, true, 0.9f);
            }
        }
    }

    private void SwitchStatesUpdate()
    {
        if (State != EnemyStates.researching)
        {
            if (IsCanShooting()) // if player was noticed all enemyes will hold positions
            {
                SwitchStateAllEnemyInRadius(_enemyTriggerRadius, EnemyStates.holding, 1);

                State = EnemyStates.shooting;
            }
            else if (State == EnemyStates.shooting && IsCanShooting() == false && IsCanChasing())  // if enemy was shooting and then he lost sight of player he start chasing player
            {
                State = EnemyStates.chasing;
            }
            else if (State == EnemyStates.holding && _timer >= _holdingTime) // if enemy holding enought time he start patroling
            {
                State = EnemyStates.patroling;
            }
            else if (IsCanShooting() == false && IsCanChasing() == false && State != EnemyStates.holding)
            {
                State = EnemyStates.patroling;
            }

            if (State == EnemyStates.holding)
            {
                _timer += Time.fixedDeltaTime;
            }
            else
            {
                _timer = 0;
                _animator.ResetTrigger("Take a knee");
            }
        }

        if (State == EnemyStates.researching && (IsCanChasing() || IsCanShooting() || _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance))
        {
            if (IsCanChasing())
            {
                State = EnemyStates.chasing;
            }
            else if (IsCanShooting())
            {
                State = EnemyStates.shooting;
            }
            else
            {
                State = EnemyStates.patroling;
            }
        }

        UpdateState();
    }

    private void UpdateState()
    {
        switch (State)
        {
            case EnemyStates.patroling:
                //Debug.Log("Patroling");
                PatrolUpdate();
                break;

            case EnemyStates.holding:
                //Debug.Log("Holding");
                HoldUpdate();
                break;

            case EnemyStates.shooting:
                //Debug.Log("Shooting");
                ShootUpdate();
                break;

            case EnemyStates.chasing:
                //Debug.Log("Chasing");
                ChaseUpdate();
                break;
        }

        _animator.SetInteger("State", (int)State);
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
    }

    #region Research

    public void StartResearch(bool IsRunWhenResearching)
    {
        State = EnemyStates.researching;

        float velocity = _patrolSpeed;
        if (IsRunWhenResearching)
        {
            velocity = _chahsingSpeed;
        }

        _navMeshAgent.speed = velocity;
        _navMeshAgent.SetDestination(_player.transform.position);
        print("Researching " + _navMeshAgent.destination);
    }

    public void StartResearch(Vector3 point, bool IsRunWhenResearching)
    {
        State = EnemyStates.researching;

        float velocity = _patrolSpeed;
        if (IsRunWhenResearching)
        {
            velocity = _chahsingSpeed;
        }

        _navMeshAgent.speed = velocity;
        _navMeshAgent.SetDestination(point);
    }

    #endregion Research

    #region StateUpdates

    private void PatrolUpdate()
    {
        _navMeshAgent.speed = _patrolSpeed;
        GoToPoint(_patrolPoints[UnityEngine.Random.Range(0, _patrolPoints.Length)].position); //go to random patrol point
    }

    private void HoldUpdate()
    {
        _navMeshAgent.speed = _chahsingSpeed;
        _navMeshAgent.SetDestination(_holdingPoint.position);
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _animator.SetTrigger("Take a knee");
            RotateToTarget(_holdingPoint.position + _holdingPoint.forward);
        }
    }

    private void ShootUpdate()
    {
        _navMeshAgent.SetDestination(transform.position);
        RotateToTarget(_player.transform.position);
        Shoot();
    }

    private void ChaseUpdate()
    {
        _navMeshAgent.speed = _chahsingSpeed;
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    #endregion StateUpdates

    private void Shoot()
    {
        print("EnemyShoot");
        if (Time.time >= _nextTimeToFire)
        {
            _muzzleFlash.Play();

            if (UnityEngine.Random.Range(0, (int)(1 / _hitChanse)) == 0)
            {
                _player.DealDamage(_damage);
            }

            _nextTimeToFire = Time.time + 1f / _fireRate;
        }
    }

    private void GoToPoint(Vector3 point)
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(point);
        }
    }

    private bool IsCanChasing()
    {
        if (State == EnemyStates.holding)
            return IsCanSeePlayer(_viewAngle, _maxChasingDistance * _multiplySearchSensWhileHolding);
        else if (State == EnemyStates.researching)
            return IsCanSeePlayer(_viewAngle, _maxChasingDistance * _multiplySearchSensWhileResearching);
        else
            return IsCanSeePlayer(_viewAngle, _maxChasingDistance);
    }

    private bool IsCanShooting()
    {
        if (State == EnemyStates.holding)
            return IsCanSeePlayer(_viewAngle, _maxShootingDistance * _multiplySearchSensWhileHolding);
        else if (State == EnemyStates.researching)
            return IsCanSeePlayer(_viewAngle, _maxShootingDistance * _multiplySearchSensWhileResearching);
        else
            return IsCanSeePlayer(_viewAngle, _maxShootingDistance);
    }

    private bool IsCanSeePlayer(float viewAngle, float maxDistanceToSee)
    {
        Vector3 direction = _player.transform.position - transform.position;

        float angle = Vector3.Angle(transform.forward, direction);
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
        {
            if (hit.collider.gameObject == _player.gameObject)
            {
                if (angle <= viewAngle && distance <= maxDistanceToSee)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    #region SwitchStateAllEnemyInRadius

    private void SwitchStateAllEnemyInRadius(float radius, EnemyStates newState, float chance)
    {
        var objects = Physics.SphereCastAll(transform.position, radius, Vector3.up);

        foreach (var obj in objects)
        {
            EnemyAI enemyAI;
            if (obj.collider.gameObject.TryGetComponent<EnemyAI>(out enemyAI))
            {
                if (enemyAI.State != newState && enemyAI.State != EnemyStates.researching && UnityEngine.Random.Range(0, (int)(1 / chance)) == 0)
                {
                    enemyAI.State = newState;
                    enemyAI.SwitchStatesUpdate();
                }
            }
        }
    }

    private void SwitchStateAllEnemyInRadius(float radius, EnemyStates firstState, EnemyStates secondState, float chance)
    {
        var objects = Physics.SphereCastAll(transform.position, radius, Vector3.up);

        foreach (var obj in objects)
        {
            EnemyAI enemyAI;
            if (obj.collider.gameObject.TryGetComponent<EnemyAI>(out enemyAI))
            {
                if (enemyAI.State != firstState && enemyAI.State != EnemyStates.researching && UnityEngine.Random.Range(0, (int)(1 / chance)) == 0)
                {
                    enemyAI.State = firstState;
                }
                else
                {
                    enemyAI.State = secondState;
                }
                enemyAI.SwitchStatesUpdate();
            }
        }
    }

    private void SwitchStateAllEnemyInRadius(float radius, EnemyStates firstState, bool secondResearch, float chance)
    {
        var objects = Physics.SphereCastAll(transform.position, radius, Vector3.up);

        foreach (var obj in objects)
        {
            EnemyAI enemyAI;
            if (obj.collider.gameObject.TryGetComponent<EnemyAI>(out enemyAI))
            {
                if (enemyAI.State != firstState && enemyAI.State != EnemyStates.researching && UnityEngine.Random.Range(0, (int)(1 / chance)) == 0)
                {
                    enemyAI.State = firstState;
                }
                else if (secondResearch)
                {
                    enemyAI.StartResearch(true);
                }
                enemyAI.SwitchStatesUpdate();
            }
        }
    }

    #endregion SwitchStateAllEnemyInRadius

    private void RotateToTarget(Vector3 target)
    {
        var targetRotation = Quaternion.LookRotation(target - transform.position, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotatingToTargetSpeed);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void OnDisable()
    {
        Gun.OnHit.RemoveListener(HeardShots);
    }

}