namespace Tanks.Features.Shooting
{
    using UnityEngine;

    /// <summary>
    /// Снаряд игрока
    /// </summary>
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class PlayerProjectile : BaseProjectile
    {
        protected float speed = 10f;

        protected override void Awake()
        {
            base.Awake();
            pool = FindAnyObjectByType<PlayerProjectilePool>();
        }

        protected virtual void Update()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        /// <summary>
        /// Задает скорость снаряда
        /// </summary>
        /// <param name="speed"></param>
        public virtual void SetSpeed(float speed) => this.speed = speed;
    }
}