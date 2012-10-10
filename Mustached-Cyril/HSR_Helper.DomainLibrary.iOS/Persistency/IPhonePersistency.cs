using System;
using System.IO;
using System.Xml.Serialization;
using HSR_Helper.DomainLibrary.Persistency;


namespace HSR_Helper.DomainLibrary.iOS.Persistency
{
	public class IPhonePersistency : IPersistency
	{
		public static string SavePath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

		public bool Save (IPersistentObject obj)
		{
			try {
				FileStream fs = new FileStream (Path.Combine (SavePath, obj.Id), FileMode.OpenOrCreate, FileAccess.Write);
				XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
				fs.Close ();
				return true;
			} catch (Exception) {
				return false;
			}
		}

        public bool Exists<T>() where T : IPersistentObject, new()
        {
            T prototype = new T();
            return File.Exists(Path.Combine(SavePath, prototype.Id));
        }

		public bool Delete<T> () where T : IPersistentObject, new()
		{
			try {
                T prototype = new T();
				File.Delete (Path.Combine (SavePath, prototype.Id));
				return true;
			} catch (Exception) {
				return false;
			}
		}

		public T Load<T> () where T : IPersistentObject, new()
		{
            T prototype = new T();
			if (File.Exists (Path.Combine (SavePath, prototype.Id))) {
                FileStream fs = new FileStream(Path.Combine(SavePath, prototype.Id), FileMode.Open, FileAccess.Read);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T obj = (T)serializer.Deserialize(fs);
				fs.Close ();
				return obj;
			} else {
				return new T ();
			}
		}

    }
}