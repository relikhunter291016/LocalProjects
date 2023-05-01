using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VideoAudioGrabberVLC
{
    public class VideoAudioStreamItem
    {
        public string baseUrl { get; set; }
        public string videoUrl { get; set; }
        public string audioUrl { get; set; }
        public string outputFileName { get; set; }


    }

    public class VideoGrabberVLC
    {

        public VideoGrabberVLC()
        {

        }

     public static   void ExecuteShellCommand(string _FileToExecute, string _CommandLine, ref string _outputMessage, ref string _errorMessage)
     {
            /**
             * 
             * set process variable
             * provides acccess to local and remote processses and enables yout to start and stop local system processes.
            */

            System.Diagnostics.Process _Process = null;

            try
            {
                _Process = new System.Diagnostics.Process();

                // invokes the cmd processs specifying the command to be executed.
                string _CMDProcess = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"{0}\cmd.exe", new object[] { Environment.SystemDirectory });

                // pass executing file to cmd ( windows command interpreter ) as a argument
                // /C tells cmd that we want it to execute the command that follows, and then exit.
                string _Arguments = string.Format(System.Globalization.CultureInfo.InvariantCulture, "/C {0}", new object[] { _FileToExecute });
                // pass any command line parameters for execution

                if (_CommandLine != null && _CommandLine.Length > 0)
                {
                    _Arguments += string.Format(System.Globalization.CultureInfo.InvariantCulture, " {0}", new object[] { _CommandLine, System.Globalization.CultureInfo.InvariantCulture });
                }
                // specify a set of values used when starting a process.
                System.Diagnostics.ProcessStartInfo _ProcessStartInfo = new System.Diagnostics.ProcessStartInfo(_CMDProcess, _Arguments);
                // sets a vlaue indicating not to start the process in a new window.
                _ProcessStartInfo.CreateNoWindow = true;
                // set a value indicating not to use the operating system shell to start the process.
                _ProcessStartInfo.UseShellExecute = false;
                // sets a value that indicates the output/input/error of an application is written to the Process.
                _ProcessStartInfo.RedirectStandardOutput = true;
                _ProcessStartInfo.RedirectStandardInput = true;
                _ProcessStartInfo.RedirectStandardError = true;

               
                _Process.StartInfo = _ProcessStartInfo;

                // Starts a process resource and associates it with a process component.
                _Process.Start();

                
                // instructs the Process component to wait indefinitely for the associated process to exit.
                _errorMessage = _Process.StandardError.ReadToEnd();
                _Process.WaitForExit();

                // Instructs the Process component to wait indefinitely for the associated process to exit.
                _outputMessage = _Process.StandardOutput.ReadToEnd();
                _Process.WaitForExit();

               

            }
            catch (Win32Exception _Win32Exception)
            {
                // Error
                Console.WriteLine("Win32 Exception caught in process: {0}", _Win32Exception.ToString());
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            finally
            {


                if (!_Process.HasExited)
                {
                    Console.WriteLine("{0} process is still running", _Process.ProcessName);
                }

                // close process and do cleanup

                _Process.Close();

                    _Process.Dispose();

                _Process = null;

            }
      }
        public static IList<VideoAudioStreamItem> getVideoAudioStreamList(string sourceFileList)
        {
            /*
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
            */
            string file = $"{sourceFileList}";
            var jsonDeserialized = File.ReadAllText(file);
            var list = JsonSerializer.Deserialize<IList<VideoAudioStreamItem>>(jsonDeserialized);
            return list;
        }
        public static byte[] GetFileFromLesson(string uriBase, string uriDestPath)
        {
            var client = new RestClient(uriBase);
            var request = new RestRequest(uriDestPath);
            byte[]? responseFileDownloaded = client.DownloadData(request);
            return responseFileDownloaded!;

        }
        public static void downloadLessonFileToLocalDisk(string destFilePath, string uriBase, string uriDestPath)
        {
            var responseFileDownloaded = GetFileFromLesson(uriBase, uriDestPath);
            FileInfo fileInfo = new FileInfo(destFilePath);
            if (!fileInfo.Exists)
            {

                FileStream fs = new FileStream(destFilePath, FileMode.OpenOrCreate);

                fs.Write(responseFileDownloaded);
                fs.Close();
            }
            else
                return;
        }
    }

}
