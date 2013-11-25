namespace Rainbow.Testing.Boomerang.Host
{
    public interface IBoomerang
    {
        void AddAddress(string prefix);

        void Start(string localhost, int listeningOnPort);
    }
}