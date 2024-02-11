namespace Tanks.Features.Interfaces
{
    /// <summary>
    /// Интерфейс получения урона от снарядов врагов
    /// </summary>
    public interface IEnemyProjectileTarget
    {
        /// <summary>
        /// Получает урон от снаряда
        /// </summary>
        public void GetProjectileDamage(float damage);
    }
}
