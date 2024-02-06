namespace Tanks.Features.Player
{
    using UnityEngine;

    /// <summary>
    /// Обработчик инпута движения для ПК
    /// </summary>
    public class PCMovementInput : MonoBehaviour
    {
        [SerializeField]
        protected PlayerMovementController controller = default;

        protected virtual void Update() => 
            controller.SetInputValue(
                Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}