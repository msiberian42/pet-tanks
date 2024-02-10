namespace Tanks.Features.UI
{
    using System;
    using UnityEngine;
    using Tanks.Features.Player;

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
            playerHealthController.onPlayerDeathEvent += EnableGameOverScreen;
        }

        protected virtual void OnDestroy() => 
            playerHealthController.onPlayerDeathEvent -= EnableGameOverScreen;

        protected virtual void EnableGameOverScreen() => gameOverScreen.SetActive(true);
    }
}