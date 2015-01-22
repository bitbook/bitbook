namespace BitBook.StaticSelfHost
{
	public class IndexModule : Nancy.NancyModule
	{
		public IndexModule ()
		{
			Get [@"/"] = _ => {
				return new Nancy.Responses.GenericFileResponse("static/index.html");
			};
		}
	}
}