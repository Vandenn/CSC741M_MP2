using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSC741M_MP2.Model
{
    [XmlRoot("settings")]
    public class Settings
    {
        private static Settings _instance;

        private string defaultSearchPath;

        public string DefaultSearchPath
        {
            get { return defaultSearchPath; }
            set
            {
                if (Directory.Exists(value))
                    defaultSearchPath = value;
            }
        }

        protected Settings()
        {
            defaultSearchPath = @"C:\";
        }

        public static Settings getInstance()
        {
            if (_instance == null)
            {
                XmlSerializer s = new XmlSerializer(typeof(Settings));
                using (var stream = new FileStream("settings.xml", FileMode.Open))
                {
                    _instance = s.Deserialize(stream) as Settings;
                }
            }
            return _instance;
        }

        public void saveSettings()
        {
            if (_instance != null)
            {
                XmlSerializer s = new XmlSerializer(typeof(Settings));
                using (var stream = new FileStream("settings.xml", FileMode.Create))
                {
                    s.Serialize(stream, _instance);
                }
            }
        }
    }
}
