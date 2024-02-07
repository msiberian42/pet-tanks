namespace Tanks.Features.Player
{
    using UnityEngine;

    /// <summary>
    /// Контроллер здоровья игрока
    /// </summary>
    public class PlayerHealthController : MonoBehaviour
    {
        /// <summary>
        /// Максимальное значение здоровья
        /// </summary>
        public float MaxHealth => maxHealth;

        /// <summary>
        /// Текущее значение здоровья
        /// </summary>
        public float CurrentHealthValue 
        { 
            get => currentHealthValue;
            protected set 
            {
                currentHealthValue = Mathf.Clamp(value, 0, MaxHealth);
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
            Debug.Log(CurrentHealthValue);

            if (CurrentHealthValue <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
