namespace Tanks.Features.Enemies
{
    using UnityEngine;

    /// <summary>
    /// Анимация смерти танка
    /// </summary>
    public class TankDeathAnimation : MonoBehaviour
    {
        [SerializeField]
        protected Rigidbody2D turret = default;

        [SerializeField]
        protected float minForce = 500f;
        [SerializeField]
        protected float maxForce = 2000f;

        protected Vector3 turretPos = default;

        protected virtual void Awake()
        {
            turretPos = turret.transform.localPosition;
        }

        protected virtual void OnEnable()
        {
            turret.transform.localPosition = turretPos;
            turret.AddForce(new Vector2(Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f)) * Random.Range(minForce, maxForce));
        }
    }
}
