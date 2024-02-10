namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Управление паузой с ПК
    /// </summary>
    public class PCPauseInput : MonoBehaviour
    {
        [SerializeField]
        protected KeyCode pauseKey = KeyCode.Escape;

        [SerializeField]
        protected PauseController controller = default;

        [SerializeField]
        protected Text clueText = default;

        protected virtual void Awake()
        {
            if (clueText)
            {
                clueText.text = pauseKey.ToString() + " - pause";
            }
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(pauseKey))
            {
                if (Time.timeScale > 0)
                {
                    controller.EnablePause();
                }
                else
                {
                    controller.DisablePause();
                }
            }
        }
    }
}