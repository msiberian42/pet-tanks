namespace Tanks.Features.Victory
{
    using System.Collections.Generic;
    using UnityEngine;
    using Tanks.Features.Enemies;

    /// <summary>
    /// Контроллер, включающий экран победы при уничтожении всех врагов
    /// </summary>
    public class EnemyCounterVictoryController : VictoryController
    {
        [SerializeField]
        protected List<EnemyHealthController> enemies = new List<EnemyHealthController>();

        protected virtual void Awake() => 
            EnemyHealthController.onEnemyDeathEvent += CheckEnemyCount;

        protected virtual void OnDestroy() => 
            EnemyHealthController.onEnemyDeathEvent -= CheckEnemyCount;

        public virtual void AddEnemy(EnemyHealthController enemyHealthController) => 
            enemies.Add(enemyHealthController);

        protected virtual void CheckEnemyCount(EnemyHealthController enemyHealth)
        {
            foreach (var enemy in enemies)
            {
                if (enemy == enemyHealth)
                {
                    enemies.Remove(enemyHealth);
                    break;
                }
            }

            if (enemies.Count == 0)
            {
                victoryScreen.SetActive(true);
                OnVictoryEvent();
            }
        }
    }
}