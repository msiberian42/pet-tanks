namespace Tanks.Features.Shooting
{
    using Tanks.Features.Pool;
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Пул снарядов врага
    /// </summary>
    public class EnemyProjectilePool : BasePool
    {
        [SerializeField]
        protected EnemyProjectile prefab = default;

        protected ObjectPool<BaseProjectile> projPool = default;

        protected virtual void Awake()
        {
            projPool = new ObjectPool<BaseProjectile>(
                createFunc: () => Instantiate(prefab, gameObject.transform),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 100);
        }

        /// <summary>
        /// Возвращает снаряд из пула
        /// </summary>
        public override BaseProjectile GetObject() => projPool.Get();

        /// <summary>
        /// Возвращает снаряд в пул
        /// </summary>
        public override void ReleaseObject(BaseProjectile proj) => projPool.Release(proj);
    }
}
