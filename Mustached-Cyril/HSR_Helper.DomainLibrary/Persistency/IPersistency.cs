
namespace HSR_Helper.DomainLibrary.Persistency
{
	public interface IPersistency
	{
		bool Save (IPersistentObject obj);
        bool Delete<T>() where T : IPersistentObject, new();
        T Load<T>() where T : IPersistentObject, new();
	}
}
