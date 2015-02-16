using System;

namespace Interfaces
{
	internal interface IRepository<TEntity> : IDisposable where TEntity : class, IJsonSerializedEntity 
	{
		TEntity Get(int Id);
		TEntity Put(IJsonSerializedEntity Data);
		void Delete(IJsonSerializedEntity Data);
	}
}

