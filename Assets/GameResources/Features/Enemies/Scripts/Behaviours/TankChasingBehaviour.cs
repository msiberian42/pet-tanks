namespace Tanks.Features.Enemies
{
    using Tanks.Features.Interfaces;
    using UnityEngine;

    /// <summary>
    /// Поведение врага в преследовании игрока
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Chasing", fileName = "Chasing Enemy Behaviour")]
    public class TankChasingBehaviour : BaseBehaviour
    {
        [SerializeField]
        protected float rotationSpeed = 180f;
        [SerializeField]
        protected float attackingDistance = 12f;

        protected IBotTankController controller = default;
        protected EnemyBehaviourInitializer initializer = default;
        protected Transform target = default;

        protected Vector3 lastTargetPos = default;
        protected float distanceToTarget = 0f;

        protected Vector3 direction = default;
        protected float angle = 0f;
        protected Quaternion targetRotation = default;

        /// <summary>
        /// Инициализирует поведение
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="target"></param>
        public virtual void Init(IBotTankController controller, EnemyBehaviourInitializer initializer, Transform target)
        {
            this.controller = controller;
            this.initializer = initializer;
            this.target = target;
        }

        public override void OnStateEnter() => controller.Agent.isStopped = false;

        public override void OnStateExit() { }

        public override void OnUpdate()
        {
            if (controller.TargetIsVisible)
            {
                lastTargetPos = target.position;

                if (Vector3.Distance(controller.Transform.position, lastTargetPos) <= attackingDistance)
                {
                    initializer.SetAttackBehaviour();
                }
                else
                {
                    Move(lastTargetPos);
                    RotateTank(lastTargetPos);
                }
            }
            else if (lastTargetPos != default 
                && Vector3.Distance(controller.Transform.position, lastTargetPos) >= 1f)
            {
                Move(lastTargetPos);
                RotateTank(lastTargetPos);
            }
            else if (lastTargetPos != default)
            {
                lastTargetPos = target.position;
                Move(lastTargetPos);
                RotateTank(lastTargetPos);
            }
        }

        protected virtual void Move(Vector3 target) => controller.Agent.SetDestination(target);

        protected virtual void RotateTank(Vector3 target)
        {
            direction = new Vector2(-controller.Agent.velocity.x, controller.Agent.velocity.y).normalized;

            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            controller.Transform.rotation = Quaternion.RotateTowards(
                controller.Transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
