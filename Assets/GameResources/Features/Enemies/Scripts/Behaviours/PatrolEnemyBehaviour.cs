namespace Tanks.Features.Enemies
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Поведение врага в патрулировании
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Patrol", fileName = "Patrol Enemy Behaviour")]
    public class PatrolEnemyBehaviour : BaseEnemyBehaviour
    {
        protected EnemyBehaviourController controller = default;
        protected List<Transform> patrolPoints = new List<Transform>();

        protected float distanceToTarget = 0f;
        protected int targetPatrolPoint = 0;
        protected float patrolDelta = 1f;

        /// <summary>
        /// Инициализирует поведение
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="target"></param>
        public virtual void Init(EnemyBehaviourController controller, List<Transform> patrolPoints = null)
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
                controller.Move(controller.transform.position);
                return;
            }

            if (Vector3.Distance(controller.transform.position, patrolPoints[targetPatrolPoint].position) <= patrolDelta)
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

            controller.Move(patrolPoints[targetPatrolPoint].position);
            controller.RotateTank(patrolPoints[targetPatrolPoint].position);
        }

        /// <summary>
        /// Задает точки для патрулирования
        /// </summary>
        /// <param name="points"></param>
        public virtual void SetPatrolPoints(List<Transform> points) => patrolPoints = points;
    }
}