namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using Tanks.Features.Explosion;

    /// <summary>
    /// Ракета, которая автоматически наводится на первого противника в радиусе ее триггера
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class PlayerMissile : PlayerProjectile
    {
        protected ExplosionsPool explosionsPool = default;
        protected ExplosionController explosionController = default;

        protected float explosionRadius = 2f;

        protected bool hasTarget = false;

        protected override void Awake()
        {
            explosionsPool = FindAnyObjectByType<ExplosionsPool>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            hasTarget = false;
        }

        protected override void Update()
        {
            if (!hasTarget)
            {
                base.Update();
            }
        }

        /// <summary>
        /// Задает радиус взрыва
        /// </summary>
        /// <param name="radius"></param>
        public virtual void SetExplosionRadius(float radius) => explosionRadius = radius;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            explosionController = explosionsPool.GetObject();
            explosionController.SetExplosionDamage(damage);
            explosionController.SetExplosionRadius(explosionRadius);
            explosionController.transform.position = transform.position;

            pool.ReleaseObject(this);
        }
    }
}