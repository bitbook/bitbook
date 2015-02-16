using System;
using Interfaces;
using System.Threading.Tasks;
using System.Reactive;

namespace PHT
{
	public class KeyValueStore : IDisposable
	{
		KeyValueRepository _repository;

		public KeyValueStore (string appName)
		{
			_repository = new KeyValueRepository ();
		}

		public IJsonSerializedEntity Get(int Id){
			return _repository.Get (Id);
		}

		public void Put(IJsonSerializedEntity Data){
			_repository.Put (Data);
		}

		public void Remove(IJsonSerializedEntity Data){
			_repository.Delete (Data);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			_repository.Dispose();
		}

		#endregion
	}
}

