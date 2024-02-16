namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Tanks.Features.Player;
    using Zenject;

    /// <summary>
    /// Отображение здоровья игрока
    /// </summary>
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField]
        protected Image view = default;

        protected PlayerHealthController controller = default;

        [Inject]
        protected virtual void Construct(PlayerController player)
        {
            controller = player.HealthController;
            controller.onHealthValueChangedEvent += OnHealthValueChanged;
        }

        protected virtual void Awake() => view.fillAmount = 1;

        protected virtual void OnDestroy() => 
            controller.onHealthValueChangedEvent -= OnHealthValueChanged;

        protected virtual void OnHealthValueChanged() => 
            view.fillAmount = controller.CurrentHealthValue / controller.MaxHealth;
    }
}
