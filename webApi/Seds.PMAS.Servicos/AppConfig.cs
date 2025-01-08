using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace Seds.PMAS.Servicos
{
	public class AppConfig
	{
		public String FILENAME = "QuadroFinanceiro.config.xml";
		public Dictionary<String, SettingsDictionary> Settings { get; set; }
		public String FullFilename
		{
			get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILENAME); }
		}

		#region constructors
		public AppConfig() : this(false) { }
		public AppConfig(Boolean autoLoad)
		{
			Settings = new Dictionary<String, SettingsDictionary>();
			if (autoLoad)
				Load();
		}
		public AppConfig(Boolean load, String fileName)
		{
			Settings = new Dictionary<String, SettingsDictionary>();
			FILENAME = fileName;
			if (load)
				Load();
		}
		#endregion

		#region public methods
		public void Load()
		{
			Load(FullFilename);
		}

		public void Save()
		{
			SaveAs(FullFilename);
		}

		public void Load(String filename)
		{
			Settings = new Dictionary<String, SettingsDictionary>();
			if (!File.Exists(filename))
				return;

			var doc = new XmlDocument();
			doc.Load(filename);

			var lstNodes = doc.GetElementsByTagName("appSettings");
			foreach (var appSettingsNode in lstNodes)
			{
				var n1 = appSettingsNode as XmlElement;
				var obj = new SettingsDictionary();
				foreach (XmlNode n2 in n1.SelectNodes("add"))
					obj.Add(n2.Attributes["key"].Value, n2.Attributes["value"].Value);
				var k = n1.Attributes["key"] == null ? "default" + (Settings.Count + 1) : n1.Attributes["key"].Value;
				Settings[String.IsNullOrEmpty(k) ? "default" + (Settings.Count + 1) : k] = obj;
			}            
		}

		public void SaveAs(String filename)
		{
			var xml = new XmlTextWriter(filename, Encoding.Unicode);
			xml.WriteStartDocument();
			xml.Formatting = Formatting.Indented;
			xml.WriteStartElement("configuration");
			xml.WriteEndElement();
			xml.WriteEndDocument();
			xml.Close();

			var doc = new XmlDocument();
			doc.Load(filename);
			foreach (var k1 in Settings.Keys)
			{
				XmlElement appSettingsNode = doc.CreateElement("appSettings");
				appSettingsNode.SetAttribute("key", k1);
				doc.GetElementsByTagName("configuration")[0].AppendChild(appSettingsNode);

				foreach (String k2 in Settings[k1].Keys)
				{
					XmlElement n = doc.CreateElement("add");
					n.SetAttribute("key", k2);
					n.SetAttribute("value", Settings[k1][k2]);
					appSettingsNode.AppendChild(n);
				}
			}
			doc.Save(filename);
		}
		#endregion
	}

	#region SettingsDictionary class
	public class SettingsDictionary : Dictionary<String, String>
	{
		public new String this[String key]
		{
			get { return base.ContainsKey(key) ? base[key] : null; }
			set { base[key] = value; }
		}
	}
	#endregion
}
