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
        protected float visionRange = 30f;

        [SerializeField]
        protected float rotationSpeed = 3f;

        [SerializeField]
        protected LayerMask wallsLayer = default;

        [SerializeField]
        protected BaseEnemyBehaviour startBehaviour = default;

        protected BaseEnemyBehaviour currentBehaviour = default;

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
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            player = FindAnyObjectByType<PlayerMovementController>();
        }

        protected virtual void OnEnable()
        {
            currentBehaviour = startBehaviour;
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

        protected virtual void Update() => RotateTank(target);

        /// <summary>
        /// Меняет поведение врага
        /// </summary>
        /// <param name="behaviour"></param>
        public virtual void SetCurrentBehaviour(BaseEnemyBehaviour behaviour) => 
            currentBehaviour = behaviour;

        protected virtual IEnumerator CheckPlayerPositionRoutine()
        {
            while (isActiveAndEnabled)
            {
                /*distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

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
                }*/

                Debug.Log(currentBehaviour);
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
            if (Vector3.Distance(transform.position, player.transform.position) > visionRange)
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

        protected virtual void RotateTank(Vector3 target)
        {
            direction = new Vector2(-agent.velocity.x, agent.velocity.y).normalized;

            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}