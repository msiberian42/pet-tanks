namespace Tanks.Features.Interfaces
{
    /// <summary>
    /// Интерфейс получения урона от снарядов игрока
    /// </summary>
    public interface IPlayerProjectileTarget
    {
        /// <summary>
        /// Получает урон от снаряда
        /// </summary>
        public void GetProjectileDamage(float damage);
    }
}
