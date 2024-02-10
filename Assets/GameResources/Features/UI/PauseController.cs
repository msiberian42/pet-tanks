namespace Tanks.Features.UI
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Контроллер окна паузы
    /// </summary>
    public class PauseController : MonoBehaviour
    {
        /// <summary>
        /// Пауза включена
        /// </summary>
        public event Action onPauseEnabled = delegate { };

        /// <summary>
        /// Пауза выключена
        /// </summary>
        public event Action onPauseDisabled = delegate { };

        [SerializeField]
        protected GameObject pauseScreen = default;

        /// <summary>
        /// Ставит игру на паузу
        /// </summary>
        public virtual void EnablePause()
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            onPauseEnabled();
        }

        /// <summary>
        /// Выключает паузу
        /// </summary>
        public virtual void DisablePause()
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            onPauseDisabled();
        }
    }
}
