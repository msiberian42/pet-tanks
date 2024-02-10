namespace Tanks.Features.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Контроллер кнопки
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        protected Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClicked);
        }

        protected virtual void OnDestroy() => button.onClick.RemoveListener(OnButtonClicked);

        protected abstract void OnButtonClicked();
    }
}
