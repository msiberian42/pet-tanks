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
        /// <summary>
        /// Текущее поведение врага
        /// </summary>
        public BaseEnemyBehaviour CurrentBehaviour => currentBehaviour;

        /// <summary>
        /// Игрок в зоне видимости врага
        /// </summary>
        public bool PlayerIsVisible {  get; protected set; }

        [SerializeField, Header("Частота обновления информации о позиции игрока")]
        protected float playerPosUpdateRate = 0.5f;

        [SerializeField]
        protected float visionRange = 30f;

        [SerializeField]
        protected float rotationSpeed = 3f;

        [SerializeField]
        protected LayerMask wallsLayer = default;

        protected BaseEnemyBehaviour currentBehaviour = default;

        protected NavMeshAgent agent = default;
        protected PlayerMovementController player = default;
        protected Coroutine checkPlayerRoutine = default;

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
            checkPlayerRoutine = StartCoroutine(CheckPlayerRoutine());
        }

        protected virtual void OnDisable()
        {
            if (checkPlayerRoutine != null)
            {
                StopCoroutine(checkPlayerRoutine);
                checkPlayerRoutine = null;
            }
        }

        protected virtual void Update()
        {
            RotateTank(target);

            Debug.Log(currentBehaviour);
            currentBehaviour.OnUpdate();
        }

        /// <summary>
        /// Меняет поведение врага
        /// </summary>
        /// <param name="behaviour"></param>
        public virtual void SetCurrentBehaviour(BaseEnemyBehaviour behaviour) => 
            currentBehaviour = behaviour;

        protected virtual IEnumerator CheckPlayerRoutine()
        {
            while (isActiveAndEnabled)
            {
                PlayerIsVisible = CheckPlayerVisible();

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

        protected virtual bool CheckPlayerVisible()
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