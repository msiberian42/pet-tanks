namespace Tanks.Features.Shooting
{
    using System.Collections;
    using UnityEngine;
    using Tanks.Features.Pool;

    /// <summary>
    /// Базовый снаряд
    /// </summary>
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField]
        protected float lifetime = 6f;

        protected BasePool pool = default;
        protected Coroutine lifetimeRoutine = default;
        protected const int WALLS_LAYER = 6;

        /// <summary>
        /// RB снаряда
        /// </summary>
        public Rigidbody2D Rb => rb;

        protected Rigidbody2D rb = default;

        protected virtual void Awake() => rb = GetComponent<Rigidbody2D>();

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

            pool.ReleaseObject(this);
        }
    }
}