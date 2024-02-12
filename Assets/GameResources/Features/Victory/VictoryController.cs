namespace Tanks.Features.Victory
{
    using System;
    using UnityEngine;
    using Tanks.Features.UI;

    public abstract class VictoryController : MonoBehaviour
    {
        /// <summary>
        /// Игрок победил
        /// </summary>
        public static event Action onVictoryEvent = delegate { };

        [SerializeField]
        protected GameObject victoryScreen = default;

        protected GameOverController gameOverController = default;

        protected virtual void Awake()
        {
            gameOverController = FindAnyObjectByType<GameOverController>();
            gameOverController.onGameOverEvent += OnGameOver;
        }

        protected virtual void OnDestroy() => gameOverController.onGameOverEvent -= OnGameOver;

        protected virtual void EnableVictoryScreen() => victoryScreen?.SetActive(true);

        protected virtual void OnVictoryEvent() => onVictoryEvent();

        protected virtual void OnGameOver() => gameObject.SetActive(false);
    }
}