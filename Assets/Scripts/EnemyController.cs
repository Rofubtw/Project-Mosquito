using System;
using Fusion;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _investigateWaitTime = 3f;

    private NavMeshAgent _agent;

    private enum State
    {
        Patrol,
        Investigate
    }

    private State _currentState;
    private float _stateTimer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public override void Spawned()
    {
        // Sadece yetkili (Host/Server) yapay zekayı yönetir
        if (Object.HasStateAuthority)
        {
            ActionManager.OnEnemyTriggered += OnEnemyTriggeredHandler;
            _currentState = State.Patrol;
            SetNextWaypoint();
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        ActionManager.OnEnemyTriggered -= OnEnemyTriggeredHandler;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;

        switch (_currentState)
        {
            case State.Patrol:
                UpdatePatrolState();
                break;
            case State.Investigate:
                UpdateInvestigateState();
                break;
        }
    }

    private void UpdatePatrolState()
    {
        // Hedefe vardı mı kontrol et
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            SetNextWaypoint();
        }
    }

    private void UpdateInvestigateState()
    {
        // Olay yerine vardı mı?
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            // Bekleme süresini say
            _stateTimer += Runner.DeltaTime;
            if (_stateTimer >= _investigateWaitTime)
            {
                // Bekleme bitti, devriyeye dön
                _currentState = State.Patrol;
                SetNextWaypoint();
            }
        }
    }

    private void SetNextWaypoint()
    {
        if (_waypoints == null || _waypoints.Length == 0) return;

        Transform target = _waypoints[UnityEngine.Random.Range(0, _waypoints.Length)];
        _agent.SetDestination(target.position);
    }

    private void OnEnemyTriggeredHandler(Vector3 position)
    {
        if (!Object.HasStateAuthority) return;

        Debug.Log($"Enemy: Triggered at {position}");
        _currentState = State.Investigate;
        _agent.SetDestination(position);
        _stateTimer = 0f;
    }
}

public static partial class ActionManager
{
    public static Action<Vector3> OnEnemyTriggered;
}