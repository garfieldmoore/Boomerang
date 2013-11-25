namespace Rainbow.Testing.Boomerang.Host
{
    public interface ISendBy
    {
        IBoomerang Returns(string body, int i);

        void AddAddress(string address1);
    }
}