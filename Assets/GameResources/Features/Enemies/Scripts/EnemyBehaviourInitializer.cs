namespace Tanks.Features.Enemies
{
    using Tanks.Features.Player;
    using Tanks.Features.Shooting;
    using UnityEngine;

    /// <summary>
    /// Инициализатор всех поведений врага
    /// </summary>
    public class EnemyBehaviourInitializer : MonoBehaviour
    {
        /// <summary>
        /// Патрулирование
        /// </summary>
        public PatrolEnemyBehaviour PatrolBehaviourInstance => patrolBehaviourInstance;

        /// <summary>
        /// Преследование
        /// </summary>
        public ChasingEnemyBehaviour ChasingBehaviourInstance => chasingBehaviourInstance;

        /// <summary>
        /// Атака
        /// </summary>
        public AttackEnemyBehaviour AttackBehaviourInstance => attackBehaviourInstance;

        [SerializeField]
        protected ChasingEnemyBehaviour chasingBehaviour = default;
        [SerializeField]
        protected PatrolEnemyBehaviour patrolBehaviour = default;
        [SerializeField]
        protected AttackEnemyBehaviour attackBehaviour = default;

        [SerializeField]
        protected Transform turret = default;
        [SerializeField]
        protected Transform shootingPoint = default;

        protected PatrolEnemyBehaviour patrolBehaviourInstance = default;
        protected ChasingEnemyBehaviour chasingBehaviourInstance = default;
        protected AttackEnemyBehaviour attackBehaviourInstance = default;

        [SerializeField]
        protected EnemyBehaviourController controller = default;

        protected PlayerMovementController player = default;
        protected EnemyProjectilePool projectilePool = default;

        protected virtual void Awake()
        {
            player = FindAnyObjectByType<PlayerMovementController>();
            projectilePool = FindAnyObjectByType<EnemyProjectilePool>();

            patrolBehaviourInstance = Instantiate(patrolBehaviour);
            chasingBehaviourInstance = Instantiate(chasingBehaviour);
            attackBehaviourInstance = Instantiate(attackBehaviour);

            patrolBehaviourInstance.Init(controller);

            if (player != null)
            {
                chasingBehaviourInstance.Init(controller, player.transform);
                attackBehaviourInstance.Init(controller: controller, initializer: this, projectilePool: projectilePool,
                    target: player.transform, turret: turret, shootingPoint: shootingPoint);
            }
        }

        protected virtual void OnEnable() => SetPatrolBehaviour();

        /// <summary>
        /// Переводит врага в патрулирование
        /// </summary>
        public virtual void SetPatrolBehaviour() => controller.SetCurrentBehaviour(patrolBehaviourInstance);

        /// <summary>
        /// Переводит врага в преследование
        /// </summary>
        public virtual void SetChasingBehaviour() => controller.SetCurrentBehaviour(chasingBehaviourInstance);

        /// <summary>
        /// Переводит врага в атаку
        /// </summary>
        public virtual void SetAttackBehaviour() => controller.SetCurrentBehaviour(attackBehaviourInstance);
    }
}