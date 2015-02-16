using System;
using Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace PHT
{
	internal class KeyValueRepository : IDisposable
	{
		Dictionary<int,IJsonSerializedEntity> _dictionary = new Dictionary<int, IJsonSerializedEntity>();

		public KeyValueRepository(){
		}

		public void Put(IJsonSerializedEntity Data){
			_dictionary.Add (Data.Id, Data);
		}

		public IJsonSerializedEntity Get(int Id){
			if( _dictionary.ContainsKey (Id)){
				return  _dictionary [Id];
			}
			throw new Exception("Could not find Item!");
		}

		public void Delete(IJsonSerializedEntity Data){
			_dictionary.Remove (Data.Id);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			// TODO
		}

		#endregion
	}

}

