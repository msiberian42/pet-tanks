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

        protected IBotTankController controller = default;
        protected Transform target = default;

        protected Vector3 lastPlayerPos = default;
        protected float distanceToTarget = 0f;

        protected Vector3 direction = default;
        protected float angle = 0f;
        protected Quaternion targetRotation = default;

        /// <summary>
        /// Инициализирует поведение
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="player"></param>
        public virtual void Init(IBotTankController controller, Transform player)
        {
            this.controller = controller;
            target = player;
        }

        public override void OnStateEnter() => controller.Agent.isStopped = false;

        public override void OnStateExit() { }

        public override void OnUpdate()
        {
            if (controller.TargetIsVisible)
            {
                lastPlayerPos = target.position;
                Move(lastPlayerPos);
                RotateTank(lastPlayerPos);
            }
            else if (lastPlayerPos != default 
                && Vector3.Distance(controller.Transform.position, lastPlayerPos) >= 1f)
            {
                Move(lastPlayerPos);
                RotateTank(lastPlayerPos);
            }
            else if (lastPlayerPos != default)
            {
                lastPlayerPos = target.position;
                Move(lastPlayerPos);
                RotateTank(lastPlayerPos);
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
