using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HSR_Helper.DomainLibrary.Persistency;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HSR_Helper.DomainLibrary.Android.Persistency
{
    public class AndroidPersistency : IPersistency
    {
        public static string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public bool Save(PersistentObject obj)
        {
            try
            {
                Console.WriteLine("Saving: " + obj.ToString() + " to: " + Path.Combine(SavePath, obj.Id));
                FileStream fs;
                if (!File.Exists(Path.Combine(SavePath, obj.Id)))
                {
                    fs = new FileStream(Path.Combine(SavePath, obj.Id), FileMode.OpenOrCreate, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(Path.Combine(SavePath, obj.Id), FileMode.Truncate, FileAccess.Write);
                }
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                XmlWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
                serializer.Serialize(writer, obj);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Exists<T>() where T : PersistentObject, new()
        {
            return Exists(new T());
        }

        public bool Exists<T>(T prototype) where T : PersistentObject, new()
        {
            Console.WriteLine("Checking: " + prototype.ToString() + " is existing in : " + Path.Combine(SavePath, prototype.Id));
            return File.Exists(Path.Combine(SavePath, prototype.Id));
        }

        public bool Delete<T>() where T : PersistentObject, new()
        {
            return Delete(new T());
        }

        public bool Delete<T>(T prototype) where T : PersistentObject, new()
        {
            if (Exists(prototype))
            {
                try
                {
                    File.Delete(Path.Combine(SavePath, prototype.Id));
                    Console.WriteLine("deleted: " + prototype.ToString() + " from: " + Path.Combine(SavePath, prototype.Id));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public T Load<T>() where T : PersistentObject, new()
        {
            return Load(new T());
        }

        public T Load<T>(T prototype) where T : PersistentObject, new()
        {
            try
            {
                if (Exists<T>(prototype))
                {
                    Console.WriteLine("loading: " + prototype.ToString() + " from: " + Path.Combine(SavePath, prototype.Id));
                    FileStream fs = new FileStream(Path.Combine(SavePath, prototype.Id), FileMode.Open, FileAccess.Read);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    T obj = (T)serializer.Deserialize(fs);
                    fs.Close();
                    Console.WriteLine("loaded: " + obj.ToString());
                    return obj;
                }
                else
                {
                    return new T();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new T();
            }
        }

    }
}