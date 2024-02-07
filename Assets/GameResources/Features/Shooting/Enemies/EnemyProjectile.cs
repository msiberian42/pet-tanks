namespace Tanks.Features.Shooting
{
    using UnityEngine;

    /// <summary>
    /// Снаряд врага
    /// </summary>
    public class EnemyProjectile : BaseProjectile
    {
        protected float speed = 10f;

        protected override void Awake()
        {
            base.Awake();
            pool = FindAnyObjectByType<EnemyProjectilePool>();
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
