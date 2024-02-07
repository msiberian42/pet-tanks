namespace Tanks.Features.Enemies
{
    using UnityEngine;

    /// <summary>
    /// Поведение врага в атаке
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Attack", fileName = "Attack Enemy Behaviour")]
    public class AttackEnemyBehaviour : BaseEnemyBehaviour
    {
        [SerializeField]
        protected float reloadCooldown = 1f;

        protected EnemyBehaviourController controller = default;
        protected EnemyBehaviourInitializer initializer = default;
        protected Transform target = default;
        protected Transform turret = default;
        protected Transform shootingPoint = default;

        public virtual void Init(EnemyBehaviourController controller, 
            EnemyBehaviourInitializer initializer, Transform target, Transform turret, Transform shootingPoint)
        {
            this.controller = controller;
            this.initializer = initializer;
            this.target = target;
            this.turret = turret;
            this.shootingPoint = shootingPoint;
        }

        public override void OnUpdate()
        {
            if (controller.PlayerIsVisible)
            {
                //shoot
            }
            else
            {
                initializer.SetChasingBehaviour();
            }
        }

        protected virtual void RotateTurret()
        {

        }

        protected virtual void Shoot()
        {

        }
    }
}