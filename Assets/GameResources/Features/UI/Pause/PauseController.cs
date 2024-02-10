namespace Tanks.Features.UI
{
    using System;
    using System.Collections.Generic;
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

        [SerializeField]
        protected List<GameObject> objToEnableOnPause = new List<GameObject>();

        [SerializeField]
        protected List<GameObject> objToDisableOnPause = new List<GameObject>();

        protected virtual void Awake() => Time.timeScale = 1.0f;

        protected virtual void OnDestroy() => Time.timeScale = 1.0f;

        /// <summary>
        /// Ставит игру на паузу
        /// </summary>
        public virtual void EnablePause()
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            onPauseEnabled();

            foreach (var obj in objToEnableOnPause)
            {
                obj.SetActive(true);
            }

            foreach (var obj in objToDisableOnPause)
            {
                obj.SetActive(false);
            }
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
