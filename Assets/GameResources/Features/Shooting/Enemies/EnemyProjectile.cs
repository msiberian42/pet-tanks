namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using Tanks.Features.Player;

    /// <summary>
    /// Снаряд врага
    /// </summary>
    public class EnemyProjectile : BaseProjectile
    {
        protected PlayerHealthController player = default;

        protected float speed = 10f;
        protected float damage = 1f;

        protected override void Awake()
        {
            base.Awake();
            pool = FindAnyObjectByType<EnemyProjectilePool>();
            player = FindAnyObjectByType<PlayerHealthController>();
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

        /// <summary>
        /// Задает урон снаряда
        /// </summary>
        /// <param name="speed"></param>
        public virtual void SetDamage(float damage) => this.damage = damage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == WALLS_LAYER)
            {
                pool.ReleaseObject(this);
            }
            else if (collision.gameObject == player.gameObject)
            {
                player.ChangeHealthValue(-damage);
                pool.ReleaseObject(this);
            }
        }
    }
}
