namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Tanks.Features.Player;

    /// <summary>
    /// Отображение перезарядки основного оружия игрока
    /// </summary>
    public class PlayerReloadView : MonoBehaviour
    {
        [SerializeField]
        protected Image view = default;

        protected PlayerShootingController shootingController = default;
        protected bool isLoading = false;

        protected virtual void Awake()
        {
            shootingController = FindAnyObjectByType<PlayerShootingController>();
            shootingController.onReloadingStartedEvent += OnReloadingStarted;
            shootingController.onReloadingFinishedEvent += OnReloadingFinished;
        }

        protected virtual void OnDestroy()
        {
            shootingController.onReloadingStartedEvent -= OnReloadingStarted;
            shootingController.onReloadingFinishedEvent -= OnReloadingFinished;
        }

        protected virtual void Update()
        {
            if (isLoading)
            {
                view.fillAmount += Time.deltaTime / shootingController.ReloadCooldown;
            }
        }

        protected virtual void OnReloadingStarted()
        {
            isLoading = true;
            view.fillAmount = 0;
        }

        protected virtual void OnReloadingFinished()
        {
            isLoading = false;
            view.fillAmount = 1;
        }
    }
}
