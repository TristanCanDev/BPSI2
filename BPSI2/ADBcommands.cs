using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


public class ADBcommands
{
    public static string adblocation;
    public void PushMap(string mapDir, string mapname, System.Windows.Controls.TextBlock status)
    {
        startADB();
        Process map_push = new Process();
        map_push.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        map_push.StartInfo.CreateNoWindow = true;
        map_push.StartInfo.FileName = adblocation;
        map_push.StartInfo.Arguments = "-d push \"" + mapDir + "\"  /sdcard/pavlov/maps/" + mapname;
        map_push.Start();
        map_push.WaitForExit();
        status.Text = mapname + " Pushed!";
        
    }

    public void setperms(System.Windows.Controls.TextBlock status, string comOBJ)
    {
        startADB();
        
        Process audioperm = new Process();
        audioperm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        audioperm.StartInfo.CreateNoWindow = true;
        audioperm.StartInfo.FileName = adblocation;
        audioperm.StartInfo.Arguments = "-d shell pm grant " + comOBJ + " android.permission.RECORD_AUDIO";
        audioperm.Start();
        audioperm.WaitForExit();
        
        

        Process rstorageperm = new Process();
        rstorageperm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        rstorageperm.StartInfo.CreateNoWindow = true;
        rstorageperm.StartInfo.FileName = adblocation;
        rstorageperm.StartInfo.Arguments = "-d shell pm grant " + comOBJ + " android.permission.READ_EXTERNAL_STORAGE";
        rstorageperm.Start();
        rstorageperm.WaitForExit();
        
        

        Process wstorageperm = new Process();
        wstorageperm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        wstorageperm.StartInfo.CreateNoWindow = true;
        wstorageperm.StartInfo.FileName = adblocation;
        wstorageperm.StartInfo.Arguments = "-d shell pm grant " + comOBJ + " android.permission.WRITE_EXTERNAL_STORAGE";
        wstorageperm.Start();
        wstorageperm.WaitForExit();
        
        
    }

    public void pushAPK(System.Windows.Controls.TextBlock status, string folderPath, string gameName, string apkname)
    {
        startADB();
        
        Process apk = new Process();
        apk.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        apk.StartInfo.CreateNoWindow = true;
        apk.StartInfo.FileName = adblocation;
        apk.StartInfo.Arguments = "install \"" + folderPath + "\\" + gameName + "\\" + apkname + "\"";
        apk.Start();
        apk.WaitForExit();
        
        
    }

    public void pushOBB(System.Windows.Controls.TextBlock status, string path, string gameName, string obbName, string comOBJ)
    {
        startADB();
        
        Process obbpush = new Process();
        obbpush.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        obbpush.StartInfo.CreateNoWindow = true;
        obbpush.StartInfo.FileName = adblocation;
        obbpush.StartInfo.Arguments = "-d push \""+path+"\\"+gameName+"\\"+obbName+"\" /sdcard/Android/obb/com.vankrupt.pavlov/"+obbName;
        obbpush.Start();
        obbpush.WaitForExit();
        
        
    }

    public void pavSetName(string path)
    {
        startADB();
        Process processfinal = new Process();
        processfinal.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        processfinal.StartInfo.CreateNoWindow = true;
        processfinal.StartInfo.FileName = adblocation;
        processfinal.StartInfo.Arguments = "push \"" + path + "\\" + "name.txt\"" + " /sdcard/pavlov.name.txt";
        processfinal.Start();
        processfinal.WaitForExit();
    }

    public void uninstallGame(System.Windows.Controls.TextBlock status, string comOBJ)
    {
        startADB();
        
        Process uninstall = new Process();
        uninstall.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        uninstall.StartInfo.CreateNoWindow = true;
        uninstall.StartInfo.FileName = adblocation;
        uninstall.StartInfo.Arguments = "uninstall " + comOBJ;
        uninstall.Start();
        uninstall.WaitForExit();
        
        
    }

    public void startADB()
    {
        Process process = new Process();
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.FileName = adblocation;
        process.StartInfo.Arguments = "devices";
        process.Start();
        process.WaitForExit();
    }

    public void adbKill()
    {
        foreach(var yes in Process.GetProcessesByName("adb"))
        {
            yes.Kill();
        }
    }   
}
