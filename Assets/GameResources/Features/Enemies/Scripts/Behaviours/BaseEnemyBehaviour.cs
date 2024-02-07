namespace Tanks.Features.Enemies
{
    using UnityEngine;

    /// <summary>
    /// Базовое поведение врага
    /// </summary>
    public abstract class BaseEnemyBehaviour : ScriptableObject
    {
        /// <summary>
        /// Просчитывает логику поведения
        /// </summary>
        public abstract void OnUpdate();
    }
}