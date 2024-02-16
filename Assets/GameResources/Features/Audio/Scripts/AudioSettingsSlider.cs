namespace Tanks.Features.Audio
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Audio;
    using Zenject;

    /// <summary>
    /// Слайдер настройки звука
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class AudioSettingsSlider : MonoBehaviour
    {
        [SerializeField]
        protected AudioMixerGroup group = default;
        [SerializeField]
        protected string paramName = default;

        protected AudioVolumeController volumeController = default;
        protected Slider slider = default;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();

            group.audioMixer.GetFloat(paramName, out float volume);
            slider.value = volume;

            slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected virtual void OnDestroy() => slider.onValueChanged.RemoveListener(OnValueChanged);

        [Inject]
        protected virtual void Construct(AudioVolumeController audioVolumeController) => 
            volumeController = audioVolumeController;

        protected virtual void OnValueChanged(float volume) => 
            volumeController.ChangeVolume(paramName, volume);
    }
}