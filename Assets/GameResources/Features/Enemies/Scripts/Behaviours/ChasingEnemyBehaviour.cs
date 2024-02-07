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
        /// <param name="player"></param>
        public virtual void Init(EnemyBehaviourController controller, Transform player)
        {
            this.controller = controller;
            target = player;
        }

        public override void OnUpdate()
        {
            if (controller.PlayerIsVisible)
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
                lastPlayerPos = target.position;
                controller.Move(lastPlayerPos);
            }
        }
    }
}
