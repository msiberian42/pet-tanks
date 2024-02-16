namespace Tanks.Features.Enemies
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Контроллер маршрута патрулирования
    /// </summary>
    public class PatrolRouteController : MonoBehaviour
    {
        [SerializeField]
        protected List<Transform> patrolPoints = new List<Transform>();

        [SerializeField]
        protected List<EnemyBehaviourInitializer> enemies = new List<EnemyBehaviourInitializer>();

        protected virtual void Start()
        {
            if (enemies != null)
            {
                SetPatrolRouteToEnemies(enemies);
            }
        }

        protected virtual void SetPatrolRouteToEnemies(List<EnemyBehaviourInitializer> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.PatrolBehaviourInstance.patrolPoints = patrolPoints;
            }
        }

        /// <summary>
        /// Добавляет врага на маршрут
        /// </summary>
        /// <param name="enemy"></param>
        public virtual void AddEnemy(EnemyBehaviourInitializer enemy)
        {
            enemies.Add(enemy);
            enemy.PatrolBehaviourInstance.patrolPoints = patrolPoints;
        }
    }
}