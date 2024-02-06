namespace Tanks.Features.Player
{
    using UnityEngine;
    using Tanks.Features.Shooting;

    /// <summary>
    /// Контроллер башни игрока
    /// </summary>
    public class PlayerShootingController : MonoBehaviour
    {
        [SerializeField]
        protected Transform turret = default;
        [SerializeField]
        protected Transform shootingPoint = default;
        [SerializeField]
        protected float shootingForce = 10f;

        protected PlayerProjectilePool projPool = default;

        protected Vector3 rotateDirection = default;
        protected Vector3 shootDirection = default;
        protected float angle = 0f;

        protected virtual void Awake() => 
            projPool = FindAnyObjectByType<PlayerProjectilePool>();

        /// <summary>
        /// Вращает башню
        /// </summary>
        /// <param name="target"></param>
        public virtual void RotateTurret(Vector3 target)
        {
            rotateDirection = target - turret.transform.position;

            rotateDirection.Normalize();

            angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;

            turret.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        /// <summary>
        /// Стреляет снарядом в указанную точку
        /// </summary>
        /// <param name="target"></param>
        public virtual void Shoot(Vector3 target)
        {
            shootDirection = target - shootingPoint.transform.position;

            shootDirection.Normalize();

            PlayerProjectile proj = projPool.GetProjectile();

            proj.transform.position = shootingPoint.position;
            proj.Rb.AddForce(shootDirection * shootingForce);
        }
    }
}