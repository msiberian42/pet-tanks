namespace Tanks.Features.Audio
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Проигрываель музыки
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public sealed class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer Instance => _instance;

        [SerializeField]
        private List<AudioClip> _tracks = new List<AudioClip>();

        private static MusicPlayer _instance = default;
        private AudioSource _audioSource = default;
        private int _currentTrack = 0;
        private int _prevTrack = 0;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();

            _currentTrack = Random.Range(0, _tracks.Count);
            _audioSource.clip = _tracks[_currentTrack];
            _audioSource.Play();

            StartCoroutine(SwitchTracksRoutine());
        }

        private void PlayRandomTrack()
        {
            _prevTrack = _currentTrack;
            _currentTrack = Random.Range(0, _tracks.Count);

            if (_currentTrack == _prevTrack)
            {
                _currentTrack++;
                if (_currentTrack > _tracks.Count - 1)
                {
                    _currentTrack = 0;
                }
            }

            _audioSource.clip = _tracks[_currentTrack];
            _audioSource.Play();

            StartCoroutine(SwitchTracksRoutine());
        }

        private IEnumerator SwitchTracksRoutine()
        {
            yield return new WaitForSecondsRealtime(_audioSource.clip.length);

            PlayRandomTrack();
        }
    }
}