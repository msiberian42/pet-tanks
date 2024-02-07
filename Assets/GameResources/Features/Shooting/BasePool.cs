namespace Tanks.Features.Pool
{
    using Tanks.Features.Shooting;
    using UnityEngine;

    /// <summary>
    /// Базовый менеджер пула объектов
    /// </summary>
    public abstract class BasePool : MonoBehaviour
    {
        /// <summary>
        /// Возвращает врага из пула
        /// </summary>
        public abstract BaseProjectile GetObject();

        /// <summary>
        /// Возвращает врага в пул
        /// </summary>
        public abstract void ReleaseObject(BaseProjectile proj);
    }
}