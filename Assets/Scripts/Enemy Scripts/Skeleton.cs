using UnityEngine;
using UnityEngine.AI;


public enum SkeletonState
{
    Idle,
    Patrol,
    Chase,
    Attack
}

public class Skeleton : MonoBehaviour
{
    [SerializeField]
    private Transform[] patrolPoints;
    private int patrolPointIndex;

    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private Animator animator;

    private SkeletonState skeletonState;


    [SerializeField]
    private float waitAtPoint = 1f;

    [SerializeField]
    private float chasingRange = 5f;

    [SerializeField]
    private float attackRange = 0.7f;

    [SerializeField]
    private float attackDelay = 2f;

    [SerializeField]
    private float rotationSpeed = 3f;

    private float waitAtPointTimer, attackDelayTimer;
    private Transform playerTarget;

    [SerializeField]
    private GameObject deathFx;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            AudioManager.instance.PlaySound(SFX.EnemyDeath);
            Instantiate(deathFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameplayManager.instance.gameState == GameState.GameOver)
        {
            navMeshAgent.isStopped = true;
            Idle(10000);
            return;
        }

        float distanceToPlayer = Vector3.Distance(playerTarget.position, navMeshAgent.transform.position);

        switch (skeletonState)
        {
            case SkeletonState.Idle:
                Idle(distanceToPlayer);
                break;

            case SkeletonState.Patrol:
                Patrol(distanceToPlayer);
                break;

            case SkeletonState.Chase:
                Chase(distanceToPlayer);
                break;

            case SkeletonState.Attack:
                Attack(distanceToPlayer);
                break;
        }
    }

    private void LookAtSlerp(Transform target)
    {
        navMeshAgent.transform.rotation = Quaternion.Slerp(navMeshAgent.transform.rotation, Quaternion.LookRotation(target.transform.position - navMeshAgent.transform.position), rotationSpeed * Time.deltaTime);

        navMeshAgent.transform.rotation = Quaternion.Euler(0f, navMeshAgent.transform.rotation.eulerAngles.y, 0f);
    }

    private void Idle(float distanceToPlayer)
    {
        if (distanceToPlayer <= chasingRange)
        {
            skeletonState = SkeletonState.Chase;
        }
        else
        {
            animator.SetBool(TagManager.MOVE_ANIMATION_PARAMETER, false);

            if (waitAtPointTimer > 0f)
            {
                waitAtPointTimer -= Time.deltaTime;

            }
            else
            {
                skeletonState = SkeletonState.Patrol;
                navMeshAgent.SetDestination(patrolPoints[patrolPointIndex].position);
            }
        }
    }
    private void Patrol(float distanceToPlayer)
    {
        if (distanceToPlayer <= chasingRange)
        {
            skeletonState = SkeletonState.Chase;
        }
        else
        {
            LookAtSlerp(patrolPoints[patrolPointIndex]);

            bool isMoving = true;

            if (navMeshAgent.remainingDistance <= 0.2)
            {
                patrolPointIndex += 1;

                if (patrolPointIndex == patrolPoints.Length)
                {
                    patrolPointIndex = 0;

                }
                skeletonState = SkeletonState.Idle;
                waitAtPointTimer += waitAtPoint;

                isMoving = false;

            }

            animator.SetBool(TagManager.MOVE_ANIMATION_PARAMETER, isMoving);
        }
    }

    private void Chase(float distanceToPlayer)
    {
        LookAtSlerp(playerTarget);

        navMeshAgent.SetDestination(playerTarget.position);

        bool isMoving = true;

        if (distanceToPlayer <= attackRange)
        {
            skeletonState = SkeletonState.Attack;

            isMoving = false;
            animator.SetTrigger(TagManager.ATTACK_ANIMATION_PARAMETER);

            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;

            attackDelayTimer = attackDelay;
        }
        else if (distanceToPlayer > chasingRange)
        {
            skeletonState = SkeletonState.Patrol;

            waitAtPointTimer = waitAtPoint;

            navMeshAgent.velocity = Vector3.zero;
        }

        animator.SetBool(TagManager.MOVE_ANIMATION_PARAMETER, isMoving);
    }

    private void Attack(float distanceToPlayer)
    {
        LookAtSlerp(playerTarget);

        attackDelayTimer -= Time.deltaTime;

        if (attackDelayTimer <= 0f)
        {
            if (distanceToPlayer <= attackRange)
            {

                animator.SetTrigger(TagManager.ATTACK_ANIMATION_PARAMETER);
                attackDelayTimer = attackDelay;
            }
            else
            {
                skeletonState = SkeletonState.Idle;
                navMeshAgent.isStopped = false;
            }
        }
    }
}
