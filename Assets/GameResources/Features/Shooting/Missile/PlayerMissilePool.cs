namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using UnityEngine.Pool;
    using Tanks.Features.Pool;

    /// <summary>
    /// Пул ракет
    /// </summary>
    public class PlayerMissilePool : BaseProjectilePool
    {
        [SerializeField]
        protected PlayerMissile prefab = default;

        protected ObjectPool<BaseProjectile> projPool = default;

        protected virtual void Awake()
        {
            projPool = new ObjectPool<BaseProjectile>(
                createFunc: () => Instantiate(prefab, gameObject.transform),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100);
        }

        /// <summary>
        /// Возвращает ракету из пула
        /// </summary>
        public override BaseProjectile GetObject() => projPool.Get();

        /// <summary>
        /// Возвращает ракету в пул
        /// </summary>
        public override void ReleaseObject(BaseProjectile proj) => projPool.Release(proj);
    }
}
