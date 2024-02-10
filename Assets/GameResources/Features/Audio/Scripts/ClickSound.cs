namespace Tanks.Features.Audio
{
    using UnityEngine;

    /// <summary>
    /// Проигрывание звука при нажатии на кнопку
    /// </summary>
    public class ClickSound : MonoBehaviour
    {
        [SerializeField]
        protected AudioSource sound = default;

        /// <summary>
        /// Проиграть клик
        /// </summary>
        public virtual void PlayClickSound() => sound.Play();
    }
}
