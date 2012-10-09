using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Persistency
{
	public interface IPersistentObject
	{
		string Filename { get; set; }
	}
}
