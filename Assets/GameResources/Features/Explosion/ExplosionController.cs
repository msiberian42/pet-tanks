namespace Tanks.Features.Explosion
{
    using System;
    using System.Collections;
    using UnityEngine;
    using Tanks.Features.Player;
    using Tanks.Features.Enemies;

    /// <summary>
    /// Контроллер взрыва
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class ExplosionController : MonoBehaviour
    {
        /// <summary>
        /// Произошел взрыв
        /// </summary>
        public static event Action onExplosionEvent = delegate { };

        [SerializeField]
        protected ParticleSystem fireParticles = default;
        [SerializeField]
        protected ParticleSystem smokeParticles = default;
        [SerializeField]
        protected AudioSource explosionSound = default;

        protected CircleCollider2D damageTrigger = default;
        protected ExplosionsPool pool = default;
        protected Coroutine lifetimeRoutine = default;
        protected PlayerHealthController player = default;
        protected EnemyHealthController enemyHealthController = default;

        protected float explosionDamage = 40f;
        protected float lifetime = 4f;
        protected float minPitch = 0.95f;
        protected float maxPitch = 1.05f;

        protected virtual void Awake()
        {
            damageTrigger = GetComponent<CircleCollider2D>();
            damageTrigger.isTrigger = true;
            player = FindAnyObjectByType<PlayerHealthController>();
            pool = FindAnyObjectByType<ExplosionsPool>();
        }

        protected virtual void OnEnable()
        {
            lifetimeRoutine = StartCoroutine(LifetimeRoutine());

            fireParticles.Play();
            smokeParticles.Play();

            explosionSound.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            explosionSound.Play();
        }

        protected virtual void OnDisable()
        {
            fireParticles.Stop();
            smokeParticles.Stop();

            if (lifetimeRoutine != null)
            {
                StopCoroutine(lifetimeRoutine);
                lifetimeRoutine = null;
            }
        }

        /// <summary>
        /// Задает радиус взрыва
        /// </summary>
        /// <param name="radius"></param>
        public virtual void SetExplosionRadius(float radius) => damageTrigger.radius = radius;

        /// <summary>
        /// Задает урон от взрыва
        /// </summary>
        /// <param name="damage"></param>
        public virtual void SetExplosionDamage(float damage) => explosionDamage = damage;

        protected virtual IEnumerator LifetimeRoutine()
        {
            yield return new WaitForSeconds(lifetime);

            pool.ReleaseObject(this);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player.gameObject)
            {
                player.ChangeHealthValue(-explosionDamage);
                pool.ReleaseObject(this);
                return;
            }

            enemyHealthController = collision.gameObject.GetComponent<EnemyHealthController>();

            if (enemyHealthController != null)
            {
                enemyHealthController.ChangeHealthValue(-explosionDamage);
                pool.ReleaseObject(this);
            }
        }
    }
}