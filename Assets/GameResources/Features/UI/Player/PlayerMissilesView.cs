namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Tanks.Features.Player;

    /// <summary>
    /// Отображение количества ракет и перезарядки в интерфейсе
    /// </summary>
    public class PlayerMissilesView : MonoBehaviour
    {
        [SerializeField]
        protected Text countText = default;
        [SerializeField]
        protected Image icon = default;

        protected PlayerMissileLaunchController missileLaunchController = default;
        protected bool isLoading = false;

        protected virtual void Awake()
        {
            missileLaunchController = FindAnyObjectByType<PlayerMissileLaunchController>();
            missileLaunchController.onMissilesCountChangedEvent += ShowMissilesCount;
            missileLaunchController.onReloadingStartedEvent += OnReloadingStarted;
            missileLaunchController.onReloadingFinishedEvent += OnReloadingFinished;

            ShowMissilesCount();
        }

        protected virtual void OnDestroy()
        {
            missileLaunchController.onMissilesCountChangedEvent -= ShowMissilesCount;
            missileLaunchController.onReloadingStartedEvent -= OnReloadingStarted;
            missileLaunchController.onReloadingFinishedEvent -= OnReloadingFinished;
        }

        protected virtual void Update()
        {
            if (isLoading)
            {
                icon.fillAmount += Time.deltaTime / missileLaunchController.ReloadCooldown;
            }
        }

        protected virtual void ShowMissilesCount() => 
            countText.text = missileLaunchController.MissilesCount.ToString();

        protected virtual void OnReloadingStarted()
        {
            isLoading = true;
            icon.fillAmount = 0;
        }

        protected virtual void OnReloadingFinished()
        {
            isLoading = false;
            icon.fillAmount = 1;
        }
    }
}