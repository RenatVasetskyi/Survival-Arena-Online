namespace Business.Data.Interfaces
{
    public interface IGameSettings
    {
        AddressableAssets AddressableAssets { get; }
        float PlayerSpawnRange { get; }
    }
}