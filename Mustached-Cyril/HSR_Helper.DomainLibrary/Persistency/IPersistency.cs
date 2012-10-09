using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Persistency
{
	public interface IPersistency
	{
		bool Save (IPersistentObject obj);
		bool Delete (Guid id);
		T Load<T> (Guid id) where T : IPersistentObject;
	}
}
