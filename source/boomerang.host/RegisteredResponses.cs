namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    public class RegisteredResponses
    {
        public Queue<Response> Responses { get; set; }

        public RegisteredResponses()
        {
            this.Responses = new Queue<Response>();
        }
    }
}