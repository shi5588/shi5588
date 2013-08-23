using System;
using System.IO;

namespace org.in2bits.MyXls.Tests
{
    public static class TestsConfig
    {
        /// <summary>
        /// Gets the Path to the Folder containing the Reference files for Unit Tests.
        /// </summary>
        public static string ReferenceFileFolder
        {
            get
            {
                return GetPath("ReferenceFiles");
            }
        }

        /// <summary>
        /// Gets the Path to the Folder containing Scripts for Unit Tests.
        /// </summary>
        public static string ScriptsFolder
        {
            get
            {
                return GetPath("Scripts");
                
            }
        }

        private static string GetPath(string folderName)
        {
            //TRYING to remaing platform agnostic for the mono people in the house
            string separator = Path.DirectorySeparatorChar.ToString();

            var upTargetFile = "MyXls.sln";
            var folderInfo = new DirectoryInfo(Environment.CurrentDirectory);
            while (0 == folderInfo.GetFiles(upTargetFile, SearchOption.TopDirectoryOnly).Length && !folderInfo.FullName.Equals(folderInfo.Root.FullName))
                folderInfo = folderInfo.Parent;
            if (0 == folderInfo.GetFiles(upTargetFile, SearchOption.TopDirectoryOnly).Length)
                throw new Exception(string.Format("Unable to GetPath({0}) - couldn't find MyXls.sln folder", folderName));
            var folderMatches = folderInfo.GetDirectories("MyXls Tests");
            if (0 == folderMatches.Length)
                throw new Exception(string.Format("Unable to GetPath({0}) - couldn't find MyXls Tests folder", folderName));

            string path = Path.Combine(folderMatches[0].FullName, folderName);
            if (!Directory.Exists(path))
            {
                throw new ApplicationException(string.Format("{0} Folder not found!", folderName));
            }
            if (!path.EndsWith(separator))
                path += separator;
            return path;
        }
    }
}
