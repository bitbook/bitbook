using Nancy;
using Nancy.Conventions;

namespace BitBook.StaticSelfHost
{
	public class ApplicationBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions (NancyConventions nancyConventions)
		{
			nancyConventions.StaticContentsConventions.Add (StaticContentConventionBuilder.AddDirectory ("", @"static"));
			base.ConfigureConventions (nancyConventions);
		}
	}
}
