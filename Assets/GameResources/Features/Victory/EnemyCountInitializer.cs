namespace Tanks.Features.Victory
{
    using System.Collections;
    using UnityEngine;
    using Tanks.Features.Enemies;

    /// <summary>
    /// Скрипт, добавляющий врага в счетчик врагов на OnEnable
    /// </summary>
    [RequireComponent(typeof(EnemyHealthController))]
    public class EnemyCountInitializer : MonoBehaviour
    {
        protected EnemyHealthController enemy = default;
        protected EnemyCounterVictoryController enemyCounter = default;

        protected virtual void Awake() => enemy = GetComponent<EnemyHealthController>();

        protected virtual void OnEnable() => AddEnemy();

        protected virtual void AddEnemy()
        {
            if (enemyCounter == null)
            {
                enemyCounter = FindAnyObjectByType<EnemyCounterVictoryController>();
            }

            enemyCounter.AddEnemy(enemy);
        }
    }
}
