using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using BitBook.StaticSelfHost;

namespace BitBook.Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test]
		public void IndexShouldReturn200()
		{
			// Given
			var browser = new Browser(with => with.Module(new IndexModule()));

			// When
			var result = browser.Get("/", with => with.HttpRequest ());

			// Then
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			Assert.That ("text/html", Is.EqualTo (result.ContentType));
		}
		[Test]
		public void ServingStaticFilesReturn200()
		{
			// Given
			var browser = new Browser(new ApplicationBootstrapper());

			// When
			var result = browser.Get("/test.css");

			// Then
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
		}
	}
}
	