namespace Tanks.Features.Shooting
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Эффект при попадании снаряда
    /// </summary>
    public class ProjectileHitEffect : MonoBehaviour
    {
        [SerializeField]
        protected ParticleSystem particles = default;
        [SerializeField]
        protected float lifetime = 1f;

        protected ProjectileHitEffectPool pool = default;
        protected Coroutine lifetimeRoutine = default;

        protected virtual void Awake() => pool = FindAnyObjectByType<ProjectileHitEffectPool>();

        protected virtual void OnEnable()
        {
            particles.Play();
            lifetimeRoutine = StartCoroutine(LifetimeRoutine());
        }

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
    }
}