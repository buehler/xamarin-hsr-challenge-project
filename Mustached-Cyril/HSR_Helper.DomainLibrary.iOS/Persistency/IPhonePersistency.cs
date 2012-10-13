using System;
using System.IO;
using System.Xml.Serialization;
using HSR_Helper.DomainLibrary.Persistency;


namespace HSR_Helper.DomainLibrary.iOS.Persistency
{
    public class IPhonePersistency : IPersistency
    {
        public static string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public bool Save(PersistentObject obj)
        {
            try
            {
                FileStream fs = new FileStream(Path.Combine(SavePath, obj.Id), FileMode.OpenOrCreate, FileAccess.Write);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
                fs.Close();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool Exists<T>() where T : PersistentObject, new()
        {
            return Exists(new T());
        }

        public bool Exists<T>(T prototype) where T : PersistentObject, new()
        {
            return File.Exists(Path.Combine(SavePath, prototype.Id));
        }

        public bool Delete<T>() where T : PersistentObject, new()
        {
            try
            {
                T prototype = new T();
                File.Delete(Path.Combine(SavePath, prototype.Id));
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public T Load<T>() where T : PersistentObject, new()
        {
            return Load(new T());
        }

        public T Load<T>(T prototype) where T : PersistentObject, new()
        {
            try
            {
                if (Exists<T>())
                {
                    FileStream fs = new FileStream(Path.Combine(SavePath, prototype.Id), FileMode.Open, FileAccess.Read);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    T obj = (T)serializer.Deserialize(fs);
                    fs.Close();
                    return obj;
                } else
                {
                    return new T();
                }
            } catch (Exception)
            {
                return new T();
            }
        }

    }
}