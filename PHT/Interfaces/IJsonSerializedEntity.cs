using System;

namespace Interfaces
{
	public interface IJsonSerializedEntity
	{
		int Id{ get;}
		string ToJsonSerialized();
	}
}

