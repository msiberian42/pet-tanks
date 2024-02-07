using UnityEngine;

namespace Tanks.Features.Enemies
{
    /// <summary>
    /// Поведение врага в преследовании игрока
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Chasing", fileName = "Chasing Enemy Behaviour")]
    public class ChasingEnemyBehaviour : BaseEnemyBehaviour
    {
        protected EnemyBehaviourController controller = default;
        protected Transform target = default;

        protected Vector3 lastPlayerPos = default;
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

        public override void OnUpdate()
        {
            distanceToTarget = Vector3.Distance(controller.transform.position, target.position);

            if (controller.PlayerIsVisible())
            {
                lastPlayerPos = target.position;
                controller.Move(lastPlayerPos);
            }
            else if (lastPlayerPos != default 
                && Vector3.Distance(controller.transform.position, lastPlayerPos) >= 1f)
            {
                controller.Move(lastPlayerPos);
            }
            else if (lastPlayerPos != default)
            {
                controller.Move(target.position);
                lastPlayerPos = default;
            }
        }
    }
}
