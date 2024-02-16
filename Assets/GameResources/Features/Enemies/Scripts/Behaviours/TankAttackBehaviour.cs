namespace Tanks.Features.Enemies
{
    using System;
    using Tanks.Features.Interfaces;
    using Tanks.Features.Shooting;
    using UnityEngine;

    /// <summary>
    /// Поведение врага в атаке
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Attack", fileName = "Attack Enemy Behaviour")]
    public class TankAttackBehaviour : BaseBehaviour
    {
        /// <summary>
        /// Выстрел совершен
        /// </summary>
        public event Action onShootEvent = delegate { };

        [SerializeField]
        protected float reloadCooldown = 1f;

        [SerializeField]
        protected float projectileSpeed = 10f;

        [SerializeField]
        protected float projectileDamage = 10f;

        protected IBotTankController controller = default;
        protected EnemyBehaviourInitializer initializer = default;
        protected EnemyProjectilePool projectilePool = default;
        protected Transform target = default;
        protected Transform turret = default;
        protected Transform shootingPoint = default;

        protected Vector3 rotateDirection = default;
        protected Vector3 shootDirection = default;
        protected float angle = 0f;
        protected EnemyProjectile projectile = default;

        public virtual void Init(IBotTankController controller, 
            EnemyBehaviourInitializer initializer, EnemyProjectilePool projectilePool,
            Transform target, Transform turret, Transform shootingPoint)
        {
            this.controller = controller;
            this.initializer = initializer;
            this.projectilePool = projectilePool;
            this.target = target;
            this.turret = turret;
            this.shootingPoint = shootingPoint;

            controller.SetReloadingCooldown(reloadCooldown);
        }

        public override void OnStateEnter()
        {
            if (controller.Agent != null && controller.Agent.isActiveAndEnabled)
            {
                controller.Agent.isStopped = true;
            }   
        }

        public override void OnStateExit() => controller.Agent.isStopped = false;

        public override void OnUpdate()
        {
            if (controller.TargetIsVisible)
            {
                RotateTurret(target.position);

                if (controller.IsLoaded)
                {
                    Shoot(target.position);
                }
            }
            else
            {
                initializer.SetChasingBehaviour();
            }
        }

        protected virtual void RotateTurret(Vector3 target)
        {
            rotateDirection = target - turret.transform.position;

            rotateDirection.Normalize();

            angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;

            turret.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        protected virtual void Shoot(Vector3 target)
        {
            shootDirection = target - shootingPoint.transform.position;

            shootDirection.Normalize();

            projectile = (EnemyProjectile)projectilePool.GetObject();

            projectile.transform.position = shootingPoint.position;
            projectile.transform.position = shootingPoint.position;
            projectile.transform.rotation = turret.transform.rotation;
            projectile.SetSpeed(projectileSpeed);
            projectile.SetDamage(projectileDamage);

            onShootEvent();
            controller.StartReloading();
        }
    }
}