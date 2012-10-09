using HSR_Helper.DomainLibrary.Persistency;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace HSR_Helper.DomainLibrary.iOS.Persistency
{
	public class IPhonePersistency : IPersistency
	{

		public static string SavePath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

		public IPhonePersistency ()
		{

		}

		public bool Save (IPersistentObject obj)
		{
			try {
				FileStream fs = new FileStream (Path.Combine (SavePath, obj.Filename), FileMode.OpenOrCreate, FileAccess.Write);
				IFormatter formatter = new BinaryFormatter ();
				formatter.Serialize (fs, obj);
				fs.Close ();
				return true;
			} catch (Exception) {
				return false;
			}
		}

		public bool Delete (System.Guid id)
		{
			throw new System.NotImplementedException ();
		}

		public T Load<T> (System.Guid id) where T : IPersistentObject
		{
			throw new System.NotImplementedException ();
		}

		public T Load<T> (string filename) where T : IPersistentObject, new()
		{
			if (File.Exists (Path.Combine (SavePath, filename))) {
				FileStream fs = new FileStream (Path.Combine (SavePath, filename), FileMode.Open, FileAccess.Read);
				IFormatter formatter = new BinaryFormatter ();
				T obj = (T)formatter.Deserialize (fs);
				fs.Close ();
				return obj;
			} else {
				return new T ();
			}
		}
	}
}