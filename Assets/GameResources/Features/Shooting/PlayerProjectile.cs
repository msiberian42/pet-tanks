namespace Tanks.Features.Shooting
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Снаряд игрока
    /// </summary>
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField]
        protected float lifetime = 6f;

        protected PlayerProjectilePool pool = default;
        protected Coroutine lifetimeRoutine = default;

        /// <summary>
        /// RB снаряда
        /// </summary>
        public Rigidbody2D Rb => rb;

        protected Rigidbody2D rb = default;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            pool = FindAnyObjectByType<PlayerProjectilePool>();
        }

        protected virtual void OnEnable() => 
            lifetimeRoutine = StartCoroutine(LifetimeRoutine());

        protected virtual void OnDisable()
        {
            rb.velocity = Vector3.zero;

            if (lifetimeRoutine != null)
            {
                StopCoroutine(lifetimeRoutine);
                lifetimeRoutine = null;
            }
        }

        protected virtual IEnumerator LifetimeRoutine()
        {
            yield return new WaitForSeconds(lifetime);

            pool.ReleaseProjectile(this);
        }
    }
}