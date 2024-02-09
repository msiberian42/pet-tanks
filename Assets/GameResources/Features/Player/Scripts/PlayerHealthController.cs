namespace Tanks.Features.Player
{
    using Tanks.Features.Explosion;
    using Tanks.Features.Shooting;

    /// <summary>
    /// Контроллер здоровья игрока
    /// </summary>
    public class PlayerHealthController : BaseHealthController
    {
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

            gameObject.SetActive(false);
        }
    }
}
