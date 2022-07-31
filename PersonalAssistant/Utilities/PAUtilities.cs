///This file is not neccessary for the program to run. It is used to open apps and websites. You may use it, but this didn't have very much thought put into it.
/// you can delete this file, it will only affect the pre-programmed methods in 'commands.cs'.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PersonalAssistant
{
    public class PAUtilities
    {
        
        /// <summary>
        /// makes your pc open an url in the default browser
        /// </summary>
        /// <param name="url">the string of the url: https://google.com</param>
        public void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) {UseShellExecute = true});
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        
        
        /// <summary>
        /// closes an application by its name
        /// </summary>
        /// <param name="applicationName">the name of the application</param>
        /// <param name="forceClose">hide it = false/blank, close/kill it = true</param>
        public void CloseApplication(string applicationName, bool forceClose = false)
        {
            Process[] procs = null;

            try
            {
                procs = Process.GetProcessesByName(applicationName);

                Process appProc = procs[0];

                if (!appProc.HasExited)
                {
                    if (forceClose)
                        appProc.Kill();
                    else
                        appProc.CloseMainWindow();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }
    }
}