using HSR_Helper.DomainLibrary.Persistency;
namespace HSR_Helper.DomainLibrary.iOS.Persistency
{
	public class IPhonePersistency : IPersistency
	{

		public bool Save (IPersistentObject obj)
		{
			throw new System.NotImplementedException ();
		}

		public bool Delete (System.Guid id)
		{
			throw new System.NotImplementedException ();
		}

		public T Load<T> (System.Guid id) where T : IPersistentObject
		{
			throw new System.NotImplementedException ();
		}
	}
}