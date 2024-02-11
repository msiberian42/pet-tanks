namespace Tanks.Features.Interfaces
{
    /// <summary>
    /// Взрываемый объект
    /// </summary>
    public interface IExplodable
    {
        /// <summary>
        /// Получает урон от взрыва
        /// </summary>
        /// <param name="damage"></param>
        public void GetExplosionDamage(float damage);
    }
}