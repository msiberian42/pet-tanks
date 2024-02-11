namespace Tanks.Features.Enviroment
{
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

        protected ExplosionsPool explosionsPool = default;
        protected ExplosionController explosionController = default;
        protected PlayerMovementController player = default;
        protected EnemyBehaviourController enemy = default;

        protected virtual void Awake() => explosionsPool = FindAnyObjectByType<ExplosionsPool>();

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            player = collision.gameObject.GetComponent<PlayerMovementController>();

            if (player != null)
            {
                Explode();
                return;
            }

            enemy = collision.gameObject.GetComponent<EnemyBehaviourController>();

            if (enemy != null)
            {
                Explode();
            }
        }

        public virtual void Explode()
        {
            explosionController = explosionsPool.GetObject();
            explosionController.SetExplosionDamage(explosionDamage);
            explosionController.SetExplosionRadius(explosionRadius);
            explosionController.transform.position = transform.position;

            gameObject.SetActive(false);
        }

        public void GetExplosionDamage(float damage) => Explode();
    }
}
