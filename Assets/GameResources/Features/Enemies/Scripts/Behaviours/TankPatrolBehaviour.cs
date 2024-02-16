namespace Tanks.Features.Enemies
{
    using System.Collections.Generic;
    using Tanks.Features.Interfaces;
    using UnityEngine;

    /// <summary>
    /// Поведение врага в патрулировании
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Patrol", fileName = "Patrol Enemy Behaviour")]
    public class TankPatrolBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Точки патрулирования
        /// </summary>
        public List<Transform> patrolPoints = new List<Transform>();
    
        [SerializeField]
        protected float rotationSpeed = 180f;

        protected IBotTankController controller = default;

        protected float distanceToTarget = 0f;
        protected int targetPatrolPoint = 0;
        protected float patrolDelta = 1f;

        protected Coroutine testRoutine = default;

        protected Vector3 direction = default;
        protected float angle = 0f;
        protected Quaternion targetRotation = default;

        /// <summary>
        /// Инициализирует поведение
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="target"></param>
        public virtual void Init(IBotTankController controller, List<Transform> patrolPoints = null)
        {
            this.controller = controller;
            this.patrolPoints = patrolPoints;
        }

        public override void OnStateEnter()
        {
            if (controller.Agent != null)
            {
                controller.Agent.isStopped = false;
            }
        }

        public override void OnStateExit() { }

        public override void OnUpdate()
        {
            if (patrolPoints == null)
            {
                Move(controller.Transform.position);
                return;
            }

            if (Vector3.Distance(controller.Transform.position, patrolPoints[targetPatrolPoint].position) <= patrolDelta)
            {
                if (targetPatrolPoint >= patrolPoints.Count - 1)
                {
                    targetPatrolPoint = 0;
                }
                else
                {
                    targetPatrolPoint++;
                }
            }

            Move(patrolPoints[targetPatrolPoint].position);
            RotateTank(patrolPoints[targetPatrolPoint].position);
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