using System.Collections.Generic;

namespace CoffeTime.Api
{
    using coffee.demo;
    using RestSharp;

    public class MenuService
    {
        public IEnumerable<Product> GetMenu(string address)
        {
            var request = new RestRequest(address, Method.GET);
            request.AddHeader("ContentType", "Application/json");
            var client = new RestClient();
            
            var response = client.Execute<List<Product>>(request).Data;

            return response;
        }

    }
}
