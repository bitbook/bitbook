using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using BitBook.StaticSelfHost;

namespace BitBook.Tests
{
	[TestFixture]
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
		public void ServingCSSFilesReturn200()
		{
			// Given
			var browser = new Browser(new ApplicationBootstrapper());

			// When
			var result = browser.Get("/css/bower.css");

			// Then
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			Assert.That ("text/css", Is.EqualTo (result.ContentType));
		}
		[Test]
		public void ServingFontFilesReturn200()
		{
			// Given
			var browser = new Browser(new ApplicationBootstrapper());

			// When
			var result = browser.Get("/fonts/fontawesome-webfont.ttf");

			// Then
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			Assert.That ("application/x-font-ttf", Is.EqualTo (result.ContentType));
		}
		[Test]
		public void ServingJSFilesReturn200()
		{
			// Given
			var browser = new Browser(new ApplicationBootstrapper());

			// When
			var result = browser.Get("/js/bower.min.js");

			// Then
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
			Assert.That ("application/javascript", Is.EqualTo (result.ContentType));
		}
	}
}
