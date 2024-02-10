namespace Tanks.Features.Victory
{
    using System;
    using UnityEngine;

    public abstract class VictoryController : MonoBehaviour
    {
        /// <summary>
        /// Игрок победил
        /// </summary>
        public static event Action onVictoryEvent = delegate { };

        [SerializeField]
        protected GameObject victoryScreen = default;

        protected virtual void EnableVictoryScreen() => victoryScreen?.SetActive(true);

        protected virtual void OnVictoryEvent() => onVictoryEvent();
    }
}