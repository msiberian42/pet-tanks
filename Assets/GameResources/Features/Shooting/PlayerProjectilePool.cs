namespace Tanks.Features.Shooting
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Пул снарядов игрока
    /// </summary>
    public class PlayerProjectilePool : MonoBehaviour
    {
        [SerializeField]
        protected PlayerProjectile prefab = default;

        protected ObjectPool<PlayerProjectile> projPool = default;

        protected virtual void Awake()
        {
            projPool = new ObjectPool<PlayerProjectile>(
                createFunc: () => Instantiate(prefab, gameObject.transform),
                actionOnGet: (obj) => obj.gameObject.SetActive(true),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 100);
        }

        /// <summary>
        /// Возвращает врага из пула
        /// </summary>
        public virtual PlayerProjectile GetProjectile() => projPool.Get();

        /// <summary>
        /// Возвращает врага в пул
        /// </summary>
        public virtual void ReleaseProjectile(PlayerProjectile proj) => projPool.Release(proj);
    }
}