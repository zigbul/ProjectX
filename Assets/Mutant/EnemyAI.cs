using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float _visionRange = 10f;

    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackDamage = 20f;
    [SerializeField] private float _attackDelay = 2f;
    [SerializeField] private float _lastAttackTime = 0f;

    [SerializeField] private float _patrolRange = 5f;
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 4f;

    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip[] _stepSounds;
    [SerializeField] private float _maxHearingDistance = 25f;

    private AudioSource _audioSource;

    private const string IS_WALKING = "IsWalking";
    private const string IS_ATTACKING = "IsAttacking";

    private NavMeshAgent _agent;
    private Animator _animator;

    private Vector3 _startPosition;
    private Vector3 _patrolDestination;

    private enum State { Patrol, Chase, Attack };
    private State _currentState;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _startPosition = transform.position;
        _currentState = State.Patrol;

        SetRandomPatrolDestination();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        float volume = Mathf.Clamp01(1 - (distanceToPlayer / _maxHearingDistance));
        _audioSource.volume = volume;

        switch(_currentState)
        {
            case State.Patrol:
                Patrol(distanceToPlayer);
                break;

            case State.Chase:
                Chase(distanceToPlayer);
                break;

            case State.Attack:
                Attack(distanceToPlayer);
                break;
        }
    }

    private void Patrol(float distanceToPlayer)
    {
        if (distanceToPlayer <= _visionRange)
        {
            _currentState = State.Chase;
            _animator.SetBool(IS_WALKING, true);
            _agent.speed = _chaseSpeed;
            return;
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            SetRandomPatrolDestination();
        }

        _animator.SetBool(IS_WALKING, _agent.velocity.magnitude > 0.1f);
    }

    private void Chase(float distanceToPlayer)
    {
        if (distanceToPlayer <= _attackRange)
        {
            _currentState= State.Attack;
            _animator.SetBool(IS_WALKING, false);
            _animator.SetBool(IS_ATTACKING, true);
            _agent.isStopped = true;
            return;
        }

        if (distanceToPlayer > _visionRange)
        {
            _currentState = State.Patrol;
            _animator.SetBool(IS_WALKING, true);
            _agent.speed = _patrolSpeed;
            SetRandomPatrolDestination();
            return;
        }

        _agent.SetDestination(_player.position);
        _animator.SetBool(IS_WALKING, true);
    }

    private void Attack(float distanceToPlayer)
    {
        if (distanceToPlayer > _attackRange)
        {
            _currentState = State.Chase;
            _animator.SetBool(IS_ATTACKING, false);
            _animator.SetBool(IS_WALKING, true);
            _agent.isStopped= false;
            return;
        }

        if (Time.time - _lastAttackTime >= _attackDelay)
        {
            _player.GetComponent<Status>().ChangeHealth(-_attackDamage);

            _lastAttackTime = Time.time;

            if (_attackSound != null && _audioSource != null)
            {
                _audioSource.PlayOneShot(_attackSound);
            }
        }
    }

    private void SetRandomPatrolDestination()
    {
        Vector3 randomPoint = _startPosition + Random.insideUnitSphere * _patrolRange;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, _patrolRange, NavMesh.AllAreas);
        _patrolDestination = hit.position;
        _agent.SetDestination(_patrolDestination);
    }

    public void PlayStepSound()
    {
        if (_stepSounds.Length > 0 && _audioSource != null)
        {
            AudioClip clip = _stepSounds[Random.Range(0, _stepSounds.Length)];
            _audioSource.PlayOneShot(clip);
        }
    }
}
