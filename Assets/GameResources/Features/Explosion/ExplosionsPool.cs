namespace Tanks.Features.Explosion
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Пул взрывов
    /// </summary>
    public class ExplosionsPool : MonoBehaviour
    {
        [SerializeField]
        protected ExplosionController explosionPrefab = default;

        protected ObjectPool<ExplosionController> projPool = default;

        protected virtual void Awake()
        {
            projPool = new ObjectPool<ExplosionController>(
                createFunc: () => Instantiate(explosionPrefab, gameObject.transform),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100);
        }

        /// <summary>
        /// Возвращает снаряд из пула
        /// </summary>
        public virtual ExplosionController GetObject() => projPool.Get();

        /// <summary>
        /// Возвращает снаряд в пул
        /// </summary>
        public virtual void ReleaseObject(ExplosionController proj) => projPool.Release(proj);
    }
}
