namespace Tanks.Features.Enemies
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.AI;
    using Tanks.Features.Player;
    using Tanks.Features.Interfaces;

    /// <summary>
    /// Контроллер состояния врага
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyTankStateController : MonoBehaviour, IPlayerMissileTarget, IBotTankController
    {
        /// <summary>
        /// Текущее поведение врага
        /// </summary>
        public BaseBehaviour CurrentBehaviour { get; protected set; } = default;

        public NavMeshAgent Agent { get; protected set; } = default;
        public Transform Transform => transform;
        public bool TargetIsVisible { get; protected set; } = false;
        public bool IsLoaded {  get; protected set; } = true;

        [SerializeField, Header("Частота обновления информации о позиции игрока")]
        protected float playerPosUpdateRate = 0.5f;

        [SerializeField]
        protected float visionRange = 30f;

        [SerializeField]
        protected LayerMask wallsLayer = default;

        protected PlayerMovementController player = default;
        protected Coroutine checkPlayerRoutine = default;
        protected float reloadCooldown = 1f;

        protected virtual void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;

            player = FindAnyObjectByType<PlayerMovementController>();
        }

        protected virtual void OnEnable()
        {
            checkPlayerRoutine = StartCoroutine(CheckPlayerRoutine());
            IsLoaded = true;
        }

        protected virtual void OnDisable()
        {
            if (checkPlayerRoutine != null)
            {
                StopCoroutine(checkPlayerRoutine);
                checkPlayerRoutine = null;
            }
        }

        protected virtual void Update() => CurrentBehaviour.OnUpdate();

        /// <summary>
        /// Меняет поведение врага
        /// </summary>
        /// <param name="behaviour"></param>
        public virtual void SetCurrentBehaviour(BaseBehaviour behaviour)
        {
            CurrentBehaviour?.OnStateExit();
            CurrentBehaviour = behaviour;
            CurrentBehaviour.OnStateEnter();
        }

        protected virtual IEnumerator CheckPlayerRoutine()
        {
            while (isActiveAndEnabled)
            {
                TargetIsVisible = CheckPlayerVisible();

                yield return new WaitForSeconds(playerPosUpdateRate);
            }
        }

        protected virtual bool CheckPlayerVisible()
        {
            if (player == null || Vector3.Distance(transform.position, player.transform.position) > visionRange)
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

        public virtual void SetReloadingCooldown(float cooldown) => reloadCooldown = cooldown;

        public virtual void StartReloading() => StartCoroutine(LoadingRoutine());

        protected virtual IEnumerator LoadingRoutine()
        {
            IsLoaded = false;

            yield return new WaitForSeconds(reloadCooldown);

            IsLoaded = true;
        }
    }
}