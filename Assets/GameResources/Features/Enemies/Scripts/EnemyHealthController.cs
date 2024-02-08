namespace Tanks.Features.Enemies
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Контроллер здоровья врага
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class EnemyHealthController : MonoBehaviour
    {
        /// <summary>
        /// Максимальное значение здоровья
        /// </summary>
        public float MaxHealth => maxHealth;

        /// <summary>
        /// Количество здоровья изменено
        /// </summary>
        public event Action OnHealthValueChangedEvent = delegate { };

        /// <summary>
        /// Текущее значение здоровья
        /// </summary>
        public float CurrentHealthValue
        {
            get => currentHealthValue;
            protected set
            {
                currentHealthValue = Mathf.Clamp(value, 0, MaxHealth);
                OnHealthValueChangedEvent();
            }
        }

        [SerializeField, Min(5)]
        protected float maxHealth = 100f;

        protected float currentHealthValue = 100f;

        protected virtual void Awake() => CurrentHealthValue = MaxHealth;

        /// <summary>
        /// Меняет значение здоровья на заданную величину
        /// </summary>
        /// <param name="value"></param>
        public virtual void ChangeHealthValue(float value)
        {
            CurrentHealthValue += value;

            if (CurrentHealthValue <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}