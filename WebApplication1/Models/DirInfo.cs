using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DirInfo 
    {
        public string CurrentDirectory { get; set; }
        public string ParentDirectory { get; set; }
        public List<string> ListOfFiles { get; set; }
        public List<string> ListOfDirectories { get; set; }
        public long CountOfSmallFiles { get; set; }
        public long CountOfMediumFiles { get; set; }
        public long CountOfLargeFiles { get; set; }
    }
}