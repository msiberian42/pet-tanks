namespace Tanks.Features.Enemies
{
    using System.Collections;
    using Tanks.Features.Player;
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Контроллер передвижения врага
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyBehaviourController : MonoBehaviour
    {
        [SerializeField, Header("Частота обновления информации о позиции игрока")]
        protected float playerPosUpdateRate = 1f;

        [SerializeField]
        protected float attackingDistance = 12f;

        [SerializeField]
        protected float aggroRange = 25f;

        [SerializeField]
        protected float rotationSpeed = 3f;

        [SerializeField]
        protected LayerMask wallsLayer = default;

        [SerializeField]
        protected ChasingEnemyBehaviour chasingBehaviour = default;
        [SerializeField]
        protected PatrolEnemyBehaviour patrolBehaviour = default;
        [SerializeField]
        protected AttackEnemyBehaviour attackBehaviour = default;

        protected BaseEnemyBehaviour currentBehaviour = default;

        protected Rigidbody2D rb = default;
        protected NavMeshAgent agent = default;
        protected PlayerMovementController player = default;
        protected Coroutine checkPlayerPositionRoutine = default;
        protected float distanceToPlayer = default;
        protected Vector3 direction = default;
        protected float angle = 0f;
        protected Quaternion targetRotation = default;
        protected Vector3 target = default;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            rb = GetComponent<Rigidbody2D>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            player = FindAnyObjectByType<PlayerMovementController>();

            patrolBehaviour.Init();
            chasingBehaviour.Init(this, player.transform);
            attackBehaviour.Init();
        }

        protected virtual void OnEnable()
        {
            currentBehaviour = chasingBehaviour;
            checkPlayerPositionRoutine = StartCoroutine(CheckPlayerPositionRoutine());
        }

        protected virtual void OnDisable()
        {
            if (checkPlayerPositionRoutine != null)
            {
                StopCoroutine(checkPlayerPositionRoutine);
                checkPlayerPositionRoutine = null;
            }
        }

        protected virtual void Update()
        {
            Rotate(target);
        }

        protected virtual IEnumerator CheckPlayerPositionRoutine()
        {
            while (isActiveAndEnabled)
            {
                distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                if (distanceToPlayer > aggroRange)
                {
                    currentBehaviour = patrolBehaviour;
                }
                else
                {
                    currentBehaviour = chasingBehaviour;
                }

                if (distanceToPlayer <= attackingDistance && PlayerIsVisible())
                {
                    currentBehaviour = attackBehaviour;
                }

                currentBehaviour.OnUpdate();

                yield return new WaitForSeconds(playerPosUpdateRate);
            }
        }

        /// <summary>
        /// Двигает врага к указанной точке
        /// </summary>
        /// <param name="target"></param>
        public virtual void Move(Vector3 target)
        {
            this.target = target;
            agent.SetDestination(target);
        }

        /// <summary>
        /// Находится ли игрок в зоне видимости врага
        /// </summary>
        /// <returns></returns>
        public virtual bool PlayerIsVisible()
        {
            if (Vector3.Distance(transform.position, player.transform.position) > aggroRange)
            {
                return false;
            }

            RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, wallsLayer);

            if (hit.collider != null)
            {
                return false;
            }

            return true;
        }

        protected virtual void Rotate(Vector3 target)
        {
            direction = new Vector2(-agent.velocity.x, agent.velocity.y).normalized;

            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}