namespace Tanks.Features.Audio
{
    using Tanks.Features.UI;

    /// <summary>
    /// Проигрывание звука при клике кнопки
    /// </summary>
    public class PlayClickSoundButton : BaseButton
    {
        protected ClickSound clickSound = default;

        protected override void Awake()
        {
            base.Awake();

            clickSound = FindAnyObjectByType<ClickSound>();
        }

        protected override void OnButtonClicked() => clickSound?.PlayClickSound();
    }
}
