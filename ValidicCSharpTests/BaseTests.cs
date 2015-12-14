namespace ValidicCSharpTests
{
    using System.Diagnostics;
    using System.Text;

    using NUnit.Framework;

    using ValidicCSharp;

    [TestFixture]
    public class BaseTests
    {
        private readonly StringBuilder _log = new StringBuilder();

        [TestFixtureSetUp]
        public void SetUp()
        {
            Client.EnableLogging = false;
            Client.AddLine += a =>
                {
                    this._log.AppendLine(a.Name);
                    this._log.AppendLine(a.Address);
                    this._log.AppendLine(a.Json);
                };
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Debug.WriteLine(this._log);
        }
    }
}