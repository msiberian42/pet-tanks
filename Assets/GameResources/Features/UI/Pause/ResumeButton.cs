namespace Tanks.Features.UI
{
    using UnityEngine;

    /// <summary>
    /// Кнопка выключения паузы
    /// </summary>
    public class ResumeButton : BaseButton
    {
        [SerializeField]
        protected PauseController pauseController = default;

        protected override void OnButtonClicked() => pauseController.DisablePause();
    }
}
