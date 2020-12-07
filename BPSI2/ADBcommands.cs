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
        map_push.StartInfo.CreateNoWindow = false;
        map_push.StartInfo.FileName = adblocation;
        map_push.StartInfo.Arguments = "-d push \"" + mapDir + "\"  /sdcard/pavlov/maps/" + mapname;
        map_push.Start();
        map_push.WaitForExit();
        status.Text = mapname + " Pushed!";
        
    }

    public void setperms(System.Windows.Controls.TextBlock status, string comOBJ)
    {
        startADB();
        status.Text = "Setting Permissions";
        Process audioperm = new Process();
        audioperm.StartInfo.CreateNoWindow = false;
        audioperm.StartInfo.FileName = adblocation;
        audioperm.StartInfo.Arguments = "-d shell pm grant " + comOBJ + " android.permission.RECORD_AUDIO";
        audioperm.Start();
        audioperm.WaitForExit();
        status.Text = "Audio Perms Set";
        

        Process rstorageperm = new Process();
        rstorageperm.StartInfo.CreateNoWindow = false;
        rstorageperm.StartInfo.FileName = adblocation;
        rstorageperm.StartInfo.Arguments = "-d shell pm grant " + comOBJ + " android.permission.READ_EXTERNAL_STORAGE";
        rstorageperm.Start();
        rstorageperm.WaitForExit();
        status.Text = "Read Storage Perms Set";
        

        Process wstorageperm = new Process();
        wstorageperm.StartInfo.CreateNoWindow = false;
        wstorageperm.StartInfo.FileName = adblocation;
        wstorageperm.StartInfo.Arguments = "-d shell pm grant " + comOBJ + " android.permission.WRITE_EXTERNAL_STORAGE";
        wstorageperm.Start();
        wstorageperm.WaitForExit();
        status.Text = "Write Storage Perms Set";
        
    }

    public void pushAPK(System.Windows.Controls.TextBlock status, string path, string name, string apkname)
    {
        startADB();
        status.Text = "Pushing APK";
        Process apk = new Process();
        apk.StartInfo.CreateNoWindow = false;
        apk.StartInfo.FileName = adblocation;
        apk.StartInfo.Arguments = "install \""+path+"\\"+name+"\\"+apkname+"\"";
        apk.Start();
        apk.WaitForExit();
        status.Text = "Apk Pushed";
        
    }

    public void pushOBB(System.Windows.Controls.TextBlock status, string path, string gameName, string obbName, string comOBJ)
    {
        startADB();
        status.Text = "Pushing OBB";
        Process obbpush = new Process();
        obbpush.StartInfo.CreateNoWindow = false;
        obbpush.StartInfo.FileName = adblocation;
        obbpush.StartInfo.Arguments = "-d push \""+path+"\\"+gameName+"\\"+obbName+"\" /sdcard/Android/obb/"+comOBJ+"/"+obbName;
        obbpush.Start();
        obbpush.WaitForExit();
        status.Text = "Obb Pushed";
        
    }

    public void pavSetName(string uName, System.Windows.Controls.TextBlock status)
    {
        status.Text = "Setting Username To: " + uName;
    }

    public void uninstallGame(System.Windows.Controls.TextBlock status, string comOBJ)
    {
        startADB();
        status.Text = "Uninstalling App From Quest";
        Process uninstall = new Process();
        uninstall.StartInfo.CreateNoWindow = false;
        uninstall.StartInfo.FileName = adblocation;
        uninstall.StartInfo.Arguments = "uninstall " + comOBJ;
        uninstall.Start();
        uninstall.WaitForExit();
        status.Text = comOBJ + " Uninstalled";
        
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
