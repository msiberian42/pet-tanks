namespace Tanks.Features.Enemies
{
    using Tanks.Features.Player;
    using Tanks.Features.Shooting;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Инициализатор всех поведений врага
    /// </summary>
    public class EnemyBehaviourInitializer : MonoBehaviour
    {
        /// <summary>
        /// Патрулирование
        /// </summary>
        public TankPatrolBehaviour PatrolBehaviourInstance => patrolBehaviourInstance;

        /// <summary>
        /// Преследование
        /// </summary>
        public TankChasingBehaviour ChasingBehaviourInstance => chasingBehaviourInstance;

        /// <summary>
        /// Атака
        /// </summary>
        public TankAttackBehaviour AttackBehaviourInstance => attackBehaviourInstance;

        [SerializeField]
        protected TankChasingBehaviour chasingBehaviour = default;
        [SerializeField]
        protected TankPatrolBehaviour patrolBehaviour = default;
        [SerializeField]
        protected TankAttackBehaviour attackBehaviour = default;

        [SerializeField]
        protected Transform turret = default;
        [SerializeField]
        protected Transform shootingPoint = default;

        protected TankPatrolBehaviour patrolBehaviourInstance = default;
        protected TankChasingBehaviour chasingBehaviourInstance = default;
        protected TankAttackBehaviour attackBehaviourInstance = default;

        [SerializeField]
        protected EnemyTankStateController controller = default;

        protected PlayerMovementController player = default;
        protected EnemyProjectilePool projectilePool = default;

        [Inject]
        protected virtual void Construct(PlayerController player, EnemyProjectilePool projectilePool)
        {
            this.player = player.MovementController;
            this.projectilePool = projectilePool;
        }

        protected virtual void Awake()
        {
            patrolBehaviourInstance = Instantiate(patrolBehaviour);
            chasingBehaviourInstance = Instantiate(chasingBehaviour);
            attackBehaviourInstance = Instantiate(attackBehaviour);

            patrolBehaviourInstance.Init(controller);

            if (player != null)
            {
                chasingBehaviourInstance.Init(controller, this, player.transform);
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