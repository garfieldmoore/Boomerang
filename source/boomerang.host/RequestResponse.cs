namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    public class RegisteredResponses
    {
//        public string Address { get; set; }
//        public string Method { get; set; }
        public Queue<Response> Responses { get; set; }

        public RegisteredResponses()
        {
            this.Responses = new Queue<Response>();
        }

//        public override int GetHashCode()
//        {
//            return string.Format("{0}{1}", this.Method, this.Address).GetHashCode();
//        }
    }
}