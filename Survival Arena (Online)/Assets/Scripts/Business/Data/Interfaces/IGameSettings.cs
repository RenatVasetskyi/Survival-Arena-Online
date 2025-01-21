namespace Business.Data.Interfaces
{
    public interface IGameSettings
    {
        AddressableAssets AddressableAssets { get; }
        GameObjectHolder GameObjectHolder { get; }
        float EnemyMinSpawnRange { get; }
        float EnemyMaxSpawnRange { get; }
        int MaxEnemies { get; }
    }
}