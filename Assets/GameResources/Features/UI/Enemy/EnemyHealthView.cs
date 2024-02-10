namespace Tanks.Features.UI
{
    using Tanks.Features.Enemies;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Отображение здоровья врага
    /// </summary>
    public class EnemyHealthView : MonoBehaviour
    {
        [SerializeField]
        protected Image view = default;

        [SerializeField]
        protected GameObject healthBar = default;

        protected EnemyHealthController controller = default;

        protected virtual void Awake() => view.fillAmount = 1;

        protected virtual void OnDestroy() =>
            controller.onHealthValueChangedEvent -= OnHealthValueChanged;
       
        protected virtual void OnEnable() => healthBar.SetActive(false);

        protected virtual void OnDisable() => healthBar.SetActive(true);

        public virtual void SetHealthController(EnemyHealthController controller)
        {
            this.controller = controller;
            controller.onHealthValueChangedEvent += OnHealthValueChanged;
        }

        protected virtual void OnHealthValueChanged()
        {
            view.fillAmount = controller.CurrentHealthValue / controller.MaxHealth;

            healthBar.SetActive(controller.CurrentHealthValue < controller.MaxHealth);
        }
    }
}