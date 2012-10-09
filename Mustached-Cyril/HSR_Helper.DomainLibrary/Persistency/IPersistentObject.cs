using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSR_Helper.DomainLibrary.Persistency
{
	public interface IPersistentObject
	{
		Guid Id { get; set; }
		string Filename { get; set; }
	}
}
