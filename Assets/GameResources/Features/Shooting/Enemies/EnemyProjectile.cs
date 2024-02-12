namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using Tanks.Features.Interfaces;

    /// <summary>
    /// Снаряд врага
    /// </summary>
    public class EnemyProjectile : BaseProjectile
    {
        protected IEnemyProjectileTarget target = default;

        protected float speed = 10f;
        protected float damage = 1f;

        protected override void Awake()
        {
            base.Awake();
            pool = FindAnyObjectByType<EnemyProjectilePool>();
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

            target = collision.GetComponent<IEnemyProjectileTarget>();

            if (target != null)
            {
                SpawnEffect();
                target.GetProjectileDamage(damage);
                pool.ReleaseObject(this);
            }
        }
    }
}
