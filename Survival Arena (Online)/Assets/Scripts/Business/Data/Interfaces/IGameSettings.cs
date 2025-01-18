namespace Business.Data.Interfaces
{
    public interface IGameSettings
    {
        AddressableAssets AddressableAssets { get; }
        GameObjectHolder GameObjectHolder { get; }
        float PlayerSpawnRange { get; }
    }
}