using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;
using System.IO.Packaging;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Data;

//hello github and modern
//just looking at modern's code to get an idea of what i'm doing

namespace BPSI2
{
    class GameFileInstall
    {
        
        public static void GrabAppFiles(string url, string appname)
        {
            SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            DirectorySecurity securityRules = new DirectorySecurity();
            FileSystemAccessRule fsRule =
                new FileSystemAccessRule(sid, FileSystemRights.FullControl,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None, AccessControlType.Allow);
            securityRules.SetAccessRule(fsRule);
            

            WebClient p = new WebClient();
            string folderforfilesorwhatever = Path.GetPathRoot(Environment.SystemDirectory);
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Blu");
            //check to see if the app already has a directory (thank you ModernEra for a good bit of this code!!
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)+ "\\Blu\\" + appname))
            {
                Uri appdown = new Uri(url);
                String filename = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Blu\\" + appname + ".zip";
                p.DownloadFile(appdown, filename);
                ZipFile.ExtractToDirectory(filename, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Blu\\" + appname);
                if(appname == "pavlov")
                {
                    
                    Directory.SetAccessControl(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Blu\\pavlov\\platform-tools", securityRules);
                    Directory.SetAccessControl(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Blu\\pavlov\\install.bat", securityRules);
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Blu\\pavlov\\platform-tools");
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Blu\\pavlov\\install.bat");
                    
                    
                }
            }

        }

    }
}
