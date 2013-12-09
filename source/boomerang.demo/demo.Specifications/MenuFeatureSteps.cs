using TechTalk.SpecFlow;

namespace CoffeTime.Specifications
{
    using System.Collections.Generic;
    using System.Linq;
    using coffee.demo;
    using CoffeTime.Api;
    using Rainbow.Testing.Boomerang.Host;
    using Shouldly;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class MenuFeatureSteps
    {
        [Given(@"A cafe only serves '(.*)' with description '(.*)'")]
        public void GivenACafeOnlyServesWithDescription(string productName, string productDescription)
        {
            var products = new List<Product>();
            products.Add(new Product() { Name = productName, Description = productDescription, Id = 1 });

            var dictionary = new Dictionary<string, string>() { { "content-type", "application/json" } };
            Boomerang.Server(5100).Get("/api/menu").Returns(products.SerialiseToJsonString(), 200, dictionary);
            dictionary.Remove("content-type");
        }

        [When(@"bobbie asks for the menu")]
        public void WhenBobbieAsksForTheMenu()
        {
            var service = new MenuService();
            var menuItems = service.GetMenu("http://localhost:5100/api/menu");
            ScenarioContext.Current.Add("menu", menuItems);
        }

        [Then(@"bobbie sees the menu")]
        public void ThenBobbieSeesTheMenu(Table table)
        {
            var expectedProducts = table.CreateSet<Product>().ToList();
            var actualProducts = ScenarioContext.Current.Get<IList<Product>>("menu");

            foreach (var expectedProduct in expectedProducts)
            {
                actualProducts.ShouldContain(expectedProduct);
            }

            actualProducts.Count.ShouldBe(expectedProducts.Count);
        }
    }
}
