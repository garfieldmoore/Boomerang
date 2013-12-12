namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    using Rainbow.Testing.Boomerang.Host;

    public class Examples
    {
        #region Public Methods and Operators

        [Test]
        [Ignore]
        public void Get_register_multiple_responses()
        {
            Boomerang.Server(5100)
                     .Get("anaddress")
                     .Returns("response body", 200)
                     .Get("anotheraddress")
                     .Returns("another response body", 201);
        }

        [Test]
        [Ignore]
        public void Get_with_single_registration()
        {
            Boomerang.Server(5100).Get("myaddress").Returns("my response body", 200);
        }

        #endregion
    }
}