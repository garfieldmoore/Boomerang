namespace Rainbow.Testing.Boomerang.Host
{
    using System;

    using Fiddler;

    public interface IMasqarade
    {
        void Start(string hostBaseAddress, int portNumber);
        event EventHandler BeforeRequest;
        void Stop();

        void SetResponse(Session session, Response response);
    }
}