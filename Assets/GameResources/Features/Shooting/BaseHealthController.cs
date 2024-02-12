namespace Tanks.Features.Shooting
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Базовый контроллер здоровья
    /// </summary>
    public class BaseHealthController : MonoBehaviour
    {
        /// <summary>
        /// Максимальное значение здоровья
        /// </summary>
        public float MaxHealth => maxHealth;

        /// <summary>
        /// Количество здоровья изменено
        /// </summary>
        public event Action onHealthValueChangedEvent = delegate { };

        /// <summary>
        /// Получен урон
        /// </summary>
        public event Action onDamageReceivedEvent = delegate { };

        /// <summary>
        /// Текущее значение здоровья
        /// </summary>
        public virtual float CurrentHealthValue
        {
            get => currentHealthValue;
            protected set
            {
                currentHealthValue = Mathf.Clamp(value, 0, MaxHealth);

                onHealthValueChangedEvent();
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

            if (value < 0)
            {
                onDamageReceivedEvent();
            }

            if (CurrentHealthValue <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Восстанавливает ХП
        /// </summary>
        public virtual void SetMaxHP() => CurrentHealthValue = MaxHealth;

        protected virtual void OnDamageReceived() => onDamageReceivedEvent();
    }
}