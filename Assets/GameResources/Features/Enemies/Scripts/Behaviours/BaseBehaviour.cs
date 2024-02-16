namespace Tanks.Features.Enemies
{
    using UnityEngine;

    /// <summary>
    /// Базовое поведение врага
    /// </summary>
    public abstract class BaseBehaviour : ScriptableObject
    {
        /// <summary>
        /// Просчитывает логику при входе в поведение
        /// </summary>
        public abstract void OnStateEnter();

        /// <summary>
        /// Просчитывает логику поведения в Update
        /// </summary>
        public abstract void OnUpdate();

        /// <summary>
        /// Просчитывает логику при выходе из поведения
        /// </summary>
        public abstract void OnStateExit();
    }
}