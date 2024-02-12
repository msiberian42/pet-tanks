﻿namespace Tanks.Features.UI
{
    using System;
    using UnityEngine;
    using Tanks.Features.Player;
    using Tanks.Features.Victory;

    /// <summary>
    /// Контроллер, включающий экран гейм овера при смерти игрока
    /// </summary>
    public class GameOverController : MonoBehaviour
    {
        /// <summary>
        /// Игрок проиграл
        /// </summary>
        public event Action onGameOverEvent = delegate { };

        [SerializeField]
        protected GameObject gameOverScreen = default;

        protected PlayerHealthController playerHealthController = default;

        protected virtual void Awake()
        {
            playerHealthController = FindAnyObjectByType<PlayerHealthController>();
            playerHealthController.onPlayerDeathEvent += OnPlayerDeath;
            VictoryController.onVictoryEvent += OnVictory;
        }

        protected virtual void OnDestroy()
        {
            playerHealthController.onPlayerDeathEvent -= OnPlayerDeath;
            VictoryController.onVictoryEvent -= OnVictory;
        }

        protected virtual void OnPlayerDeath()
        {
            onGameOverEvent();
            gameOverScreen.SetActive(true);
        }

        protected virtual void OnVictory() => gameObject.SetActive(false);
    }
}