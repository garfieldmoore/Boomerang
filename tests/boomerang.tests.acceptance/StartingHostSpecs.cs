namespace boomerang.tests.acceptance
{
    using NUnit.Framework;

    public class StartingHostSpecs
    {
        #region Public Methods and Operators

        [Test]
        public void Should_start_host()
        {
            Spec.GivenADefaultServer();
        }

        #endregion
    }
}