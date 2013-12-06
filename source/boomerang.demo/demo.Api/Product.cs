namespace coffee.demo
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            var newobj = obj as Product;
            if (newobj == null) return false;

            return newobj.Description == Description && Name == newobj.Name;
        }
    }
}