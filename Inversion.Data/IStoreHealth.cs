namespace Inversion.Data
{
    public interface IStoreHealth : IStore
    {
        bool GetHealth(out string result);
    }
}