namespace Tanks.Features.Interfaces
{
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Танк, управляемый ИИ
    /// </summary>
    public interface IBotTankController : ITankController
    {
        /// <summary>
        /// Цель в зоне видимости
        /// </summary>
        bool TargetIsVisible { get; }

        /// <summary>
        /// Готов к стрельбе
        /// </summary>
        bool IsLoaded { get; }

        NavMeshAgent Agent { get; }

        Transform Transform { get; }

        /// <summary>
        /// Задает время перезарядки
        /// </summary>
        /// <param name="cooldown"></param>
        void SetReloadingCooldown(float cooldown);

        /// <summary>
        /// Начинает перезарядку
        /// </summary>
        void StartReloading();
    }
}