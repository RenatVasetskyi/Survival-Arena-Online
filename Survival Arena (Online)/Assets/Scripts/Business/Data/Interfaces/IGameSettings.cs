namespace Business.Data.Interfaces
{
    public interface IGameSettings
    {
        AddressableAssets AddressableAssets { get; }
        GameObjectHolder GameObjectHolder { get; }
        float PlayerMinSpawnRange { get; }
        float PlayerMaxSpawnRange { get; }
        float EnemyMinSpawnRange { get; }
        float EnemyMaxSpawnRange { get; }
    }
}