using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BPSI2
{
    class CoolKidClass
    {

        public static bool checkforinternetconnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;

            }
            catch
            {
                return false;
            }
        }


        

    }
}
