namespace Tanks.Features.Enviroment
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Эффект разрушения стены
    /// </summary>
    public sealed class WallCrashEffect : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particles = default;

        [SerializeField]
        private AudioSource _sound = default;

        [SerializeField]
        private float _lifetime = 2f;

        private WallCrashEffectsPool _pool = default;
        private Coroutine _lifetimeRoutine = default;

        protected float minPitch = 0.95f;
        protected float maxPitch = 1.05f;

        private void Awake() => _pool = FindAnyObjectByType<WallCrashEffectsPool>();

        private void OnEnable()
        {
            _particles.Play();

            _sound.pitch = Random.Range(minPitch, maxPitch);
            _sound.Play();
            _lifetimeRoutine = StartCoroutine(LifetimeRoutine());
        }

        private void OnDisable()
        {
            if (_lifetimeRoutine != null)
            {
                StopCoroutine(_lifetimeRoutine);
                _lifetimeRoutine = null;
            }
        }

        private IEnumerator LifetimeRoutine()
        {
            yield return new WaitForSeconds(_lifetime);

            _pool.ReleaseObject(this);
        }
    }
}