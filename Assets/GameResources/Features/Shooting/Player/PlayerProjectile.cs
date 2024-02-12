namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using Tanks.Features.Interfaces;

    /// <summary>
    /// Снаряд игрока
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class PlayerProjectile : BaseProjectile
    {
        protected float speed = 10f;
        protected float damage = 1f;
        protected IPlayerProjectileTarget target = default;

        protected override void Awake()
        {
            base.Awake();
            pool = FindAnyObjectByType<PlayerProjectilePool>();
        }

        protected virtual void Update() => transform.Translate(Vector2.up * speed * Time.deltaTime);

        /// <summary>
        /// Задает скорость снаряда
        /// </summary>
        /// <param name="speed"></param>
        public virtual void SetSpeed(float speed) => this.speed = speed;

        /// <summary>
        /// Задает урон снаряда
        /// </summary>
        /// <param name="speed"></param>
        public virtual void SetDamage(float damage) => this.damage = damage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == WALLS_LAYER)
            {
                SpawnEffect();
                pool.ReleaseObject(this);
                return;
            }

            target = collision.gameObject.GetComponent<IPlayerProjectileTarget>();

            if (target != null)
            {
                SpawnEffect();
                target.GetProjectileDamage(damage);
                pool.ReleaseObject(this);
            }
        }
    }
}