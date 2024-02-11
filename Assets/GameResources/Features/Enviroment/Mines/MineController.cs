namespace Tanks.Features.Enviroment
{
    using System.Collections;
    using UnityEngine;
    using Tanks.Features.Enemies;
    using Tanks.Features.Player;
    using Tanks.Features.Explosion;
    using Tanks.Features.Interfaces;

    /// <summary>
    /// Контроллер мины
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class MineController : MonoBehaviour, IExplodable
    {
        [SerializeField]
        protected float explosionDamage = 80f;

        [SerializeField]
        protected float explosionRadius = 2f;

        [SerializeField]
        protected float explodeDelay = 0.5f;

        protected ExplosionsPool explosionsPool = default;
        protected ExplosionController explosionController = default;
        protected PlayerMovementController player = default;
        protected EnemyBehaviourController enemy = default;
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
            player = collision.gameObject.GetComponent<PlayerMovementController>();

            if (player != null)
            {
                StartExplode();
                return;
            }

            enemy = collision.gameObject.GetComponent<EnemyBehaviourController>();

            if (enemy != null)
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
    }
}
