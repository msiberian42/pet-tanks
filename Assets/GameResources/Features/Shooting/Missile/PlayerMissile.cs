namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using Tanks.Features.Explosion;

    /// <summary>
    /// Ракета, которая автоматически наводится на первого противника в радиусе ее триггера
    /// </summary>
    public class PlayerMissile : BaseProjectile
    {
        /// <summary>
        /// Слой со стенами
        /// </summary>
        public int WallsLayer => WALLS_LAYER;

        [SerializeField]
        protected float explosionDamage = 70f;

        [SerializeField]
        protected float explosionRadius = 2f;

        [SerializeField]
        protected float missileSpeed = 5f;

        [SerializeField]
        protected ParticleSystem fireParticles = default;

        protected ExplosionsPool explosionsPool = default;
        protected ExplosionController explosionController = default;

        protected Transform target = default;
        protected Vector3 rotateDirection = default;
        protected float angle = 0f;

        protected override void Awake()
        {
            explosionsPool = FindAnyObjectByType<ExplosionsPool>();
            pool = FindAnyObjectByType<PlayerMissilePool>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            target = null;
            fireParticles.Play();
        }

        protected virtual void Update()
        {
            if (target == null)
            {
                transform.Translate(Vector2.up * missileSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate((target.position - transform.position).normalized * missileSpeed * Time.deltaTime, Space.World);
                Rotate(target.position);
            }
        }

        /// <summary>
        /// Взрывает ракету
        /// </summary>
        public virtual void Explode()
        {
            fireParticles.Stop();

            explosionController = explosionsPool.GetObject();
            explosionController.SetExplosionDamage(explosionDamage);
            explosionController.SetExplosionRadius(explosionRadius);
            explosionController.transform.position = transform.position;

            target = null;
            pool.ReleaseObject(this);
        }

        /// <summary>
        /// Задает цель ракете
        /// </summary>
        /// <param name="target"></param>
        public virtual void SetTarget(Transform target) => this.target = target;

        protected virtual void Rotate(Vector3 target)
        {
            rotateDirection = target - transform.position;

            rotateDirection.Normalize();

            angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}