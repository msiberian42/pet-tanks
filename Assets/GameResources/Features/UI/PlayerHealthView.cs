namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Tanks.Features.Player;

    /// <summary>
    /// Отображение здоровья игрока
    /// </summary>
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField]
        protected Image view = default;

        protected PlayerHealthController controller = default;

        protected virtual void Awake()
        {
            controller = FindAnyObjectByType<PlayerHealthController>();
            view.fillAmount = 1;
            controller.OnHealthValueChangedEvent += OnHealthValueChanged;
        }

        protected virtual void OnDestroy() => 
            controller.OnHealthValueChangedEvent -= OnHealthValueChanged;

        protected virtual void OnHealthValueChanged() => 
            view.fillAmount = controller.CurrentHealthValue / controller.MaxHealth;
    }
}
