
namespace HSR_Helper.DomainLibrary.Persistency
{
	public interface IPersistency
	{
		bool Save (IPersistentObject obj);
	    bool Exists<T>() where T : IPersistentObject, new();
        bool Delete<T>() where T : IPersistentObject, new();
        T Load<T>() where T : IPersistentObject, new();
	}
}
