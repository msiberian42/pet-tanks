namespace Tanks.Features.Player
{
    using System;
    using System.Collections;
    using Tanks.Features.Shooting;
    using UnityEngine;

    /// <summary>
    /// Контроллер пуска ракет
    /// </summary>
    public class PlayerMissileLaunchController : MonoBehaviour
    {
        /// <summary>
        /// Счетчик ракет
        /// </summary>
        public int MissilesCount => missilesCount;

        /// <summary>
        /// Количество ракет изменилось
        /// </summary>
        public event Action onMissilesCountChangedEvent = delegate { };

        /// <summary>
        /// Пушка перезаряжена
        /// </summary>
        public bool IsLoaded => isLoaded;

        /// <summary>
        /// Перезарядка началась
        /// </summary>
        public event Action onReloadingStartedEvent = delegate { };

        /// <summary>
        /// Перезарядка закончена
        /// </summary>
        public event Action onReloadingFinishedEvent = delegate { };

        /// <summary>
        /// Выстрел совершен
        /// </summary>
        public event Action onShootEvent = delegate { };

        /// <summary>
        /// Время перезарядки
        /// </summary>
        public float ReloadCooldown => reloadCooldown;

        [SerializeField]
        protected Transform turret = default;
        [SerializeField]
        protected Transform shootingPoint = default;

        [SerializeField]
        protected float reloadCooldown = 5f;
        [SerializeField, Min(0)]
        protected int missilesCount = 4;

        protected PlayerMissilePool missilePool = default;

        protected Vector3 rotateDirection = default;
        protected Vector3 shootDirection = default;
        protected float angle = 0f;
        protected bool isLoaded = true;
        protected PlayerMissile missile = default;

        protected virtual void Awake() => missilePool = FindAnyObjectByType<PlayerMissilePool>();

        /// <summary>
        /// Запускает ракету
        /// </summary>
        /// <param name="target"></param>
        public virtual void LaunchMissile(Vector3 target)
        {
            shootDirection = target - shootingPoint.transform.position;

            shootDirection.Normalize();

            missile = (PlayerMissile)missilePool.GetObject();

            missile.transform.position = shootingPoint.position;
            missile.transform.rotation = turret.transform.rotation;

            onShootEvent();
            isLoaded = false;
            StartCoroutine(LoadingRoutine());
            ChangeMissileCount(-1);
        }

        protected virtual IEnumerator LoadingRoutine()
        {
            onReloadingStartedEvent();

            yield return new WaitForSeconds(reloadCooldown);

            isLoaded = true;
            onReloadingFinishedEvent();
        }

        /// <summary>
        /// Меняет количество ракет на заданное число
        /// </summary>
        /// <param name="value"></param>
        public virtual void ChangeMissileCount(int value)
        {
            missilesCount += value;

            if (missilesCount < 0) missilesCount = 0;

            onMissilesCountChangedEvent();
        }

        /// <summary>
        /// Задает количество ракет
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetMissileCount(int value)
        {
            missilesCount = value;

            if (missilesCount < 0) missilesCount = 0;

            onMissilesCountChangedEvent();
        }
    }
}