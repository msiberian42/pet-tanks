namespace Tanks.Features.Enemies
{
    using UnityEngine;

    /// <summary>
    /// Поведение врага в патрулировании
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Patrol", fileName = "Patrol Enemy Behaviour")]
    public class PatrolEnemyBehaviour : BaseEnemyBehaviour
    {
        [SerializeField]
        protected ChasingEnemyBehaviour chasingBehaviour = default;

        [SerializeField, Header("Дистанция, на которой враг начинает преследование")]
        protected float chasingRange = 25;

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

        public override void OnUpdate()
        {
            distanceToTarget = Vector3.Distance(controller.transform.position, target.position);

            if (distanceToTarget <= chasingRange && controller.PlayerIsVisible())
            {
                controller.SetCurrentBehaviour(chasingBehaviour);
            }
        }
    }
}