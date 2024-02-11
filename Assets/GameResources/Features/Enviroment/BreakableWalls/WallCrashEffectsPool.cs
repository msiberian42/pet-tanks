namespace Tanks.Features.Enviroment
{
    using UnityEngine;
    using UnityEngine.Pool;

    /// <summary>
    /// Пул эффекта ломания стены
    /// </summary>
    public class WallCrashEffectsPool : MonoBehaviour
    {
        [SerializeField]
        protected WallCrashEffect effectPrefab = default;

        protected ObjectPool<WallCrashEffect> pool = default;

        protected virtual void Awake()
        {
            pool = new ObjectPool<WallCrashEffect>(
                createFunc: () => Instantiate(effectPrefab, gameObject.transform),
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
        public virtual WallCrashEffect GetObject() => pool.Get();

        /// <summary>
        /// Возвращает эффект в пул
        /// </summary>
        public virtual void ReleaseObject(WallCrashEffect proj) => pool.Release(proj);
    }
}