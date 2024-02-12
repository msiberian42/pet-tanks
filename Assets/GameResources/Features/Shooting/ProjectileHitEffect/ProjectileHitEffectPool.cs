namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Пул эффектов снаряда
    /// </summary>
    public class ProjectileHitEffectPool : MonoBehaviour
    {
        [SerializeField]
        protected ProjectileHitEffect prefab = default;

        protected ObjectPool<ProjectileHitEffect> projPool = default;

        protected virtual void Awake()
        {
            projPool = new ObjectPool<ProjectileHitEffect>(
                createFunc: () => Instantiate(prefab, gameObject.transform),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100);
        }

        /// <summary>
        /// Возвращает эффект из пула
        /// </summary>
        public virtual ProjectileHitEffect GetObject() => projPool.Get();

        /// <summary>
        /// Возвращает эффект в пул
        /// </summary>
        public virtual void ReleaseObject(ProjectileHitEffect proj) => projPool.Release(proj);
    }
}
