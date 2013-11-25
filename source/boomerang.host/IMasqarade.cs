namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    public interface IMasqarade
    {
        void Start(string hostBaseAddress, int portNumber);
        event EventHandler BeforeRequest;
    }
}