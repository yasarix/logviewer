using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LogViewer
{
	public class Configuration
	{
		string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/config.xml";

		public string this[string key]
		{
			get
			{
				var document = XDocument.Load(FilePath);
				var values = from n in document.Root.Elements()
						where n.Name == key
						select n.Value;

				if(values.Any()) {
					return values.First();
				}

				return null;
			}
			set
			{
				var document = XDocument.Load(FilePath);
				var values = from n in document.Root.Elements()
						where n.Name == key
						select n;

				if(values.Any()) {
					values.First().Value = value;
				}
				else {
					document.Root.Add(new XElement(key, value));
				}

				document.Save(FilePath);
			}
		}

		public Configuration ()
		{
		}
	}

}
