namespace Tanks.Features.Enemies
{
    using UnityEngine;

    /// <summary>
    /// Поведение врага в патрулировании
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Patrol", fileName = "Patrol Enemy Behaviour")]
    public class PatrolEnemyBehaviour : BaseEnemyBehaviour
    {
        protected EnemyBehaviourController controller = default;
        protected Transform target = default;
        protected float distanceToTarget = 0f;

        /// <summary>
        /// Инициализирует поведение
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="target"></param>
        public virtual void Init(EnemyBehaviourController controller, Transform target)
        {
            this.controller = controller;
            this.target = target;
        }

        public override void OnStateEnter() => controller.Agent.isStopped = false;

        public override void OnStateExit() { }

        public override void OnUpdate()
        {
            controller.Move(controller.transform.position);
        }
    }
}