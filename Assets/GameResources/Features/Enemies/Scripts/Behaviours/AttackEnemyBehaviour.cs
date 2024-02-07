﻿namespace Tanks.Features.Enemies
{
    using Tanks.Features.Shooting;
    using UnityEngine;

    /// <summary>
    /// Поведение врага в атаке
    /// </summary>
    [CreateAssetMenu(menuName = "Enemies/Behaviours/Attack", fileName = "Attack Enemy Behaviour")]
    public class AttackEnemyBehaviour : BaseEnemyBehaviour
    {
        [SerializeField]
        protected float reloadCooldown = 1f;

        [SerializeField]
        protected float shootingForce = 600f;

        protected EnemyBehaviourController controller = default;
        protected EnemyBehaviourInitializer initializer = default;
        protected EnemyProjectilePool projectilePool = default;
        protected Transform target = default;
        protected Transform turret = default;
        protected Transform shootingPoint = default;

        protected Vector3 rotateDirection = default;
        protected Vector3 shootDirection = default;
        protected float angle = 0f;
        protected BaseProjectile projectile = default;
        protected Vector3 prevRotation = default;

        public virtual void Init(EnemyBehaviourController controller, 
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
            prevRotation = controller.transform.eulerAngles;
            controller.Agent.isStopped = true;
        }

        public override void OnStateExit() => controller.Agent.isStopped = false;

        public override void OnUpdate()
        {
            if (controller.PlayerIsVisible)
            {
                RotateTurret(target.position);

                if (controller.IsLoaded)
                {
                    Shoot(target.position);
                }

                if (controller.transform.eulerAngles != prevRotation)
                {
                    controller.transform.eulerAngles = prevRotation;
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

            projectile = projectilePool.GetProjectile();

            projectile.transform.position = shootingPoint.position;
            projectile.Rb.velocity = Vector2.zero;
            projectile.Rb.AddForce(shootDirection * shootingForce);

            controller.StartReloading();
        }
    }
}