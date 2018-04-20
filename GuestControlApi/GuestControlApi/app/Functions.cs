using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestControlApi.app
{
    public static class Functions
    {
        public static String GetHash(string value)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(value);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
