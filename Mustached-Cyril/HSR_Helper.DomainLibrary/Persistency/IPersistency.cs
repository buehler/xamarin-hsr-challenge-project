
namespace HSR_Helper.DomainLibrary.Persistency
{
    public interface IPersistency
    {
        bool Save(PersistentObject obj);
        bool Exists<T>() where T : PersistentObject, new();
        bool Exists<T>(T prototype) where T : PersistentObject, new();
        bool Delete<T>() where T : PersistentObject, new();
        T Load<T>() where T : PersistentObject, new();
        T Load<T>(T prototype) where T : PersistentObject, new();
    }
}
