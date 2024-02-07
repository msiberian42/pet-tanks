namespace Tanks.Features.Shooting
{
    using UnityEngine;

    /// <summary>
    /// Снаряд игрока
    /// </summary>
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class PlayerProjectile : BaseProjectile
    {
        protected override void Awake()
        {
            base.Awake();
            pool = FindAnyObjectByType<PlayerProjectilePool>();
        }
    }
}