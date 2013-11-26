namespace Rainbow.Testing.Boomerang.Host
{
    using System.Collections.Generic;

    public interface IBoomerang
    {
    }

    public class RequestResponse
    {
        public string Address { get; set; }
        public string Method { get; set; }
        public int Instance { get; set; }
        public Response Response { get; set; }
        public Queue<Response> Responses { get; set; }

        public RequestResponse()
        {
            Responses=new Queue<Response>();
        }

        public override int GetHashCode()
        {
            return string.Format("{0}{1}", Method, Address).GetHashCode();
        }
    }

}