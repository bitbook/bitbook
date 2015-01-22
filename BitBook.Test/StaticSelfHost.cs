using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using BitBook;
using BitBook.StaticSelfHost;

namespace BitBook.Test
{
	[TestFixture ()]
	public class Test
	{
		[Test]
		public void Index_Should_return_200()
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
		public void Serving_Static_Files_return_200()
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
	