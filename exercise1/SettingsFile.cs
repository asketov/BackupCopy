using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace exercise1
{
    public class SettingsFile
    {
        public string[] SourceFolders  { get; set; }

        public string DestFolder { get; set; }
    }
}
