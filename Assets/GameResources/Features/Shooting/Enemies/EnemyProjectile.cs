namespace Tanks.Features.Shooting
{
    public class EnemyProjectile : BaseProjectile
    {
        /// <summary>
        /// Снаряд врага
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            pool = FindAnyObjectByType<EnemyProjectilePool>();
        }
    }
}
