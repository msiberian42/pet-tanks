namespace Tanks.Features.Audio
{
    using UnityEngine;
    using UnityEngine.Audio;
    using Zenject;

    /// <summary>
    /// Контроллер громкости
    /// </summary>
    public class AudioVolumeController : MonoBehaviour
    {
        [SerializeField]
        protected AudioMixerGroup group = default;
        [SerializeField]
        protected string soundsParamName = "SoundsVolume";
        [SerializeField]
        protected string musicParamName = "MusicVolume";
        [SerializeField]
        protected float defaultValue = -20f;

        protected virtual void Start()
        {
            if (PlayerPrefs.HasKey(soundsParamName))
            {
                group.audioMixer.SetFloat(soundsParamName, PlayerPrefs.GetFloat(soundsParamName));
            }
            else
            {
                group.audioMixer.SetFloat(soundsParamName, defaultValue);
                PlayerPrefs.SetFloat(soundsParamName, defaultValue);
            }

            if (PlayerPrefs.HasKey(musicParamName))
            {
                group.audioMixer.SetFloat(musicParamName, PlayerPrefs.GetFloat(musicParamName));
            }
            else
            {
                group.audioMixer.SetFloat(musicParamName, defaultValue);
                PlayerPrefs.SetFloat(musicParamName, defaultValue);
            }
        }

        /// <summary>
        /// Меняет громкость и сохраняет в префсы
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="volume"></param>
        public virtual void ChangeVolume(string paramName, float volume)
        {
            group.audioMixer.SetFloat(paramName, volume);
            PlayerPrefs.SetFloat(paramName, volume);
        }
    }
}
