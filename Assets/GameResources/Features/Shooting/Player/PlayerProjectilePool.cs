namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using UnityEngine.Pool;
    using Tanks.Features.Pool;

    /// <summary>
    /// Пул снарядов игрока
    /// </summary>
    public class PlayerProjectilePool : BasePool
    {
        [SerializeField]
        protected PlayerProjectile prefab = default;

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
        public override BaseProjectile GetProjectile() => projPool.Get();

        /// <summary>
        /// Возвращает снаряд в пул
        /// </summary>
        public override void ReleaseProjectile(BaseProjectile proj) => projPool.Release(proj);
    }
}