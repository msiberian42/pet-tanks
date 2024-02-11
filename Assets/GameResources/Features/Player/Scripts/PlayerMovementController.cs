namespace Tanks.Features.Player
{
    using UnityEngine;
    using Tanks.Features.Interfaces;

    /// <summary>
    /// Контроллер движения игрока
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour, ITankController
    {
        [SerializeField]
        protected float rotationSpeed = 100f;

        [SerializeField]
        protected float movementSpeed = 1f;

        protected float horizontalInput = 0f;
        protected float verticalInput = 0f;

        protected const float DELTA = 0.2f;

        protected Rigidbody2D rb = default;
        protected Vector2 direction = default;
        protected Quaternion targetRotation = default;
        protected float angle = 0f;

        protected virtual void Awake() => rb = GetComponent<Rigidbody2D>();

        protected virtual void Update()
        {
            if (Mathf.Abs(horizontalInput) >= DELTA || Mathf.Abs(verticalInput) >= DELTA)
            {
                Rotate();
            }
        }

        protected virtual void FixedUpdate()
        {
            if ((Mathf.Abs(horizontalInput) >= DELTA || Mathf.Abs(verticalInput) >= DELTA)
                && (Mathf.Abs(targetRotation.eulerAngles.z - transform.eulerAngles.z) <= 90))
            {
                Move();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

        /// <summary>
        /// Задает значение инпута
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public void SetInputValue(float horizontal, float vertical)
        {
            horizontalInput = Mathf.Clamp(horizontal, -1f, 1f);
            verticalInput = Mathf.Clamp(vertical, -1f, 1f);
        }
        
        protected virtual void Rotate()
        {
            direction = new Vector2(-horizontalInput, verticalInput).normalized;

            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        protected virtual void Move()
        {
            rb.velocity = new Vector3(horizontalInput * movementSpeed,
                verticalInput * movementSpeed, 0);
        }
    }
}