using UnityEngine;

namespace Tanks.Features.Enemies
{
    /// <summary>
    /// Поведение врага в преследовании игрока
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Chasing", fileName = "Chasing Enemy Behaviour")]
    public class ChasingEnemyBehaviour : BaseEnemyBehaviour
    {
        [SerializeField]
        protected PatrolEnemyBehaviour patrolBehaviour = default;
        [SerializeField]
        protected AttackEnemyBehaviour attackBehaviour = default;

        [SerializeField, Header("Дистанция, на которой враг возвращается к патрулированию")]
        protected float patrolRange = 30f;

        [SerializeField, Header("Дистанция, на которой враг переходит в атаку")]
        protected float attackRange = 12f;

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

            if (distanceToTarget >= patrolRange)
            {
                controller.SetCurrentBehaviour(patrolBehaviour);
                return;
            }

            if (distanceToTarget <= attackRange && controller.PlayerIsVisible())
            {
                controller.SetCurrentBehaviour(attackBehaviour);
                return;
            }

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
                lastPlayerPos = target.position;
                controller.Move(lastPlayerPos);
            }
        }
    }
}
