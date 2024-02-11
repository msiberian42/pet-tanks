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

        protected BaseProjectilePool pool = default;
        protected ProjectileHitEffectPool effectPool = default;

        protected Coroutine lifetimeRoutine = default;
        protected ProjectileHitEffect effect = default;

        protected const int WALLS_LAYER = 6;

        protected virtual void Awake() => effectPool = FindAnyObjectByType<ProjectileHitEffectPool>();

        protected virtual void OnEnable() =>
            lifetimeRoutine = StartCoroutine(LifetimeRoutine());

        protected virtual void OnDisable()
        {
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

        protected virtual void SpawnEffect()
        {
            effect = effectPool.GetObject();
            effect.transform.position = transform.position;
            effect.transform.rotation = transform.rotation;
        }
    }
}