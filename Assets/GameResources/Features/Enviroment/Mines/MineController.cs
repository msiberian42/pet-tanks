namespace Tanks.Features.Enviroment
{
    using System.Collections;
    using UnityEngine;
    using Tanks.Features.Interfaces;
    using Tanks.Features.Explosion;

    /// <summary>
    /// Контроллер мины
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class MineController : MonoBehaviour, IExplodable, IPlayerProjectileTarget, IEnemyProjectileTarget
    {
        [SerializeField]
        protected float explosionDamage = 80f;

        [SerializeField]
        protected float explosionRadius = 2f;

        [SerializeField]
        protected float explodeDelay = 0.5f;

        protected ExplosionsPool explosionsPool = default;
        protected ExplosionController explosionController = default;
        protected ITankController target = default;
        protected Coroutine explodeRoutine = default;

        protected virtual void Awake() => explosionsPool = FindAnyObjectByType<ExplosionsPool>();

        protected virtual void OnDisable()
        {
            if (explodeRoutine == null)
            {
                StopCoroutine(ExplodeRoutine());
                explodeRoutine = null;
            }
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            target = collision.gameObject.GetComponent<ITankController>();

            if (target != null)
            {
                StartExplode();
            }
        }

        protected virtual void Explode()
        {
            explosionController = explosionsPool.GetObject();
            explosionController.SetExplosionDamage(explosionDamage);
            explosionController.SetExplosionRadius(explosionRadius);
            explosionController.transform.position = transform.position;

            gameObject.SetActive(false);
        }

        public void GetExplosionDamage(float damage) => StartExplode();

        protected virtual IEnumerator ExplodeRoutine()
        {
            yield return new WaitForSeconds(explodeDelay);

            Explode();
        }

        protected virtual void StartExplode()
        {
            if (explodeRoutine == null)
            {
                explodeRoutine = StartCoroutine(ExplodeRoutine());
            }
        }

        public void GetProjectileDamage(float damage) => StartExplode();
    }
}
