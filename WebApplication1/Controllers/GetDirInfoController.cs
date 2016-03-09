using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using WebApplication1.Models;
using System.Web;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    [EnableCorsAttribute(origins: "http://localhost:51016", headers: "*", methods: "*")]
    public class GetDirInfoController : ApiController
    {
      
        public DirInfo Get(string path)
        {
            string currentDirectory = path;
            string parentDirectory;
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
            List<string> listOfDirectories = new List<string>();
            List<string> listOfFiles = new List<string>();
            List<long> listOfFilesLength = new List<long>();

            currentDirectory = directoryInfo.FullName;
            if (directoryInfo.FullName == directoryInfo.Root.FullName)
            {
                parentDirectory = directoryInfo.FullName;
            }
            else
            {
                parentDirectory = directoryInfo.Parent.FullName;
            }
            //Get list of folders
            foreach (DirectoryInfo dInf in directoryInfo.GetDirectories())
            {
                listOfDirectories.Add(dInf.Name);
            }
            //Get list of files
            foreach (FileInfo fInfo in directoryInfo.GetFiles())
            {
                listOfFiles.Add(fInfo.Name);
            }

            
            SearchDirectory(directoryInfo, listOfFilesLength);


            DirInfo dInfo = new DirInfo()
            {
                CurrentDirectory = currentDirectory,
                ParentDirectory = parentDirectory,
                ListOfDirectories = listOfDirectories,
                ListOfFiles = listOfFiles,
                CountOfLargeFiles = listOfFilesLength.Count( length => length > 104857600),
                CountOfMediumFiles = listOfFilesLength.Count(length => length> 10485760 && length < 104857600),
                CountOfSmallFiles = listOfFilesLength.Count(length => length < 10485760)
            };

            return dInfo;
        }

        //POST api/dirInfo
        public DirInfo Post([FromBody]string path)

        {
            if (path == "listOfDrives")
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                List<string> drives = new List<string>();

                foreach (DriveInfo d in allDrives)
                {
                    drives.Add(d.Name);
                }
                DirInfo dInfo = new DirInfo()
                {
                    ListOfDirectories = drives,
                    CurrentDirectory = ""
                };
                return dInfo;
            }
            

            return Get(path);
        }

        private void SearchDirectory(DirectoryInfo dir_info, List<long> file_list)
        {
            try
            {
                foreach (DirectoryInfo subdir_info in dir_info.GetDirectories())
                {
                    SearchDirectory(subdir_info, file_list);
                }
            }
            catch
            {
            }
            try
            {
                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    file_list.Add(file_info.Length);
                }
            }
            catch
            {
            }
        }


    }
}
