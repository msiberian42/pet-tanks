namespace Tanks.Features.Player
{
    using System;
    using UnityEngine;
    using Tanks.Features.Explosion;
    using Tanks.Features.Shooting;
    using Tanks.Features.Interfaces;

    /// <summary>
    /// Контроллер здоровья игрока
    /// </summary>
    public class PlayerHealthController : BaseHealthController, IExplodable, IEnemyProjectileTarget
    {
        /// <summary>
        /// У игрока кончилось здоровье
        /// </summary>
        public event Action onPlayerDeathEvent = delegate { };

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
                KillPlayer();
            }
        }

        protected virtual void KillPlayer()
        {
            explosionController = explosionsPool.GetObject();
            explosionController.SetExplosionDamage(0);
            explosionController.transform.position = transform.position;

            onPlayerDeathEvent();

            Instantiate(deathPrefab, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }

        public void GetExplosionDamage(float damage) => ChangeHealthValue(-damage);

        public void GetProjectileDamage(float damage) => ChangeHealthValue(-damage);
    }
}
