namespace Tanks.Features.Enemies
{
    using System;
    using UnityEngine;
    using Tanks.Features.Shooting;
    using Tanks.Features.Explosion;

    /// <summary>
    /// Контроллер здоровья врага
    /// </summary>
    public class EnemyHealthController : BaseHealthController
    {
        /// <summary>
        /// Враг уничтожен
        /// </summary>
        public static event Action<EnemyHealthController> onEnemyDeathEvent = delegate { };

        [SerializeField]
        protected GameObject deathPrefab = default;

        protected ExplosionsPool explosionsPool = default;
        protected ExplosionController explosionController = default;

        protected override void Awake()
        {
            base.Awake();

            explosionsPool = FindAnyObjectByType<ExplosionsPool>();
        }

        public override void ChangeHealthValue(float value)
        {
            CurrentHealthValue += value;

            if (value < 0)
            {
                OnDamageReceived();
            }

            if (CurrentHealthValue <= 0)
            {
                KillEnemy();
            }
        }

        protected virtual void KillEnemy()
        {
            explosionController = explosionsPool.GetObject();
            explosionController.SetExplosionDamage(0);
            explosionController.transform.position = transform.position;

            onEnemyDeathEvent(this);

            Instantiate(deathPrefab, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }
    }
}