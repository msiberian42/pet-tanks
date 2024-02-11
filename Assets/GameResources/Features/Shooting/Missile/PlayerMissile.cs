namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using Tanks.Features.Explosion;
    using Tanks.Features.Enemies;

    /// <summary>
    /// Ракета, которая автоматически наводится на первого противника в радиусе ее триггера
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class PlayerMissile : BaseProjectile
    {
        [SerializeField]
        protected float explosionDamage = 70f;

        [SerializeField]
        protected float explosionRadius = 2f;

        [SerializeField]
        protected float missileSpeed = 1f;

        [SerializeField]
        protected ParticleSystem fireParticles = default;

        protected ExplosionsPool explosionsPool = default;
        protected ExplosionController explosionController = default;
        protected EnemyHealthController enemyHealthController = default;

        protected bool hasTarget = false;

        protected virtual void Awake()
        {
            explosionsPool = FindAnyObjectByType<ExplosionsPool>();
            pool = FindAnyObjectByType<PlayerMissilePool>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            hasTarget = false;
            fireParticles.Play();
        }

        protected virtual void Update()
        {
            if (!hasTarget)
            {
                transform.Translate(Vector2.up * missileSpeed * Time.deltaTime);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == WALLS_LAYER)
            {
                Explode();
                return;
            }

            enemyHealthController = collision.gameObject.GetComponent<EnemyHealthController>();

            if (enemyHealthController != null)
            {
                Explode();
            }
        }

        protected virtual void Explode()
        {
            fireParticles.Stop();

            explosionController = explosionsPool.GetObject();
            explosionController.SetExplosionDamage(explosionDamage);
            explosionController.SetExplosionRadius(explosionRadius);
            explosionController.transform.position = transform.position;

            pool.ReleaseObject(this);
        }
    }
}