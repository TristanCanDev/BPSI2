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
        Process map_push = new Process();
        map_push.StartInfo.CreateNoWindow = true;
        map_push.StartInfo.FileName = adblocation;
        map_push.StartInfo.Arguments = "-d push \"" + mapDir + "\"  /sdcard/pavlov/maps/" + mapname;
        map_push.Start();
        map_push.WaitForExit();
        status.Text = mapname + " Pushed!";
    }

    public void setperms()
    {

    }

    public void pushAPK()
    {

    }

    public void pushOBB()
    {

    }

    public void pavSetName()
    {

    }

}
