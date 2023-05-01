// grab HLS stream audio/video by using VLC of other tools executable program in Windows CMD by passing a command and parameters also.
// remember to read a json file and catch values of URLs streams and names of files in output

using RestSharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using VideoAudioGrabberVLC;


public class Program
{


    public static void Main()
    {
        string _Output = null;
        string _Error = null;

        string projPath = "C:\\Users\\andy\\source\\repos\\VideoAudioGrabberVLC\\VideoAudioGrabberVLC\\";
        
        Console.WriteLine("Choose type of operation: \\n" +
                          "1- Download of videoAudio files to disk \\n" +
                          "2-Once downloaded source files, make video Files by merging audio video track");
        int choice= Convert.ToInt16( Console.ReadLine());
        var audioStreamList = VideoGrabberVLC.getVideoAudioStreamList(projPath);

        if (choice == 1 )
        {

            foreach (var item in audioStreamList)
            {
                if (item.videoUrl != null)
                {

                    VideoGrabberVLC.downloadLessonFileToLocalDisk(
                                            $"{projPath}{item.outputFileName}.ts",
                                            item.baseUrl,
                                            item.videoUrl
                                            );
                }
                if (item.audioUrl != null)
                {
                    VideoGrabberVLC.downloadLessonFileToLocalDisk(
                                           $"{projPath} {item.outputFileName}.acc",
                                           item.baseUrl,
                                           item.audioUrl
                                           );
                }
            }
        } 
        else if (choice == 2 )
        {
            foreach (var item in audioStreamList)
            {

                //  VideoGrabberVLC.ExecuteShellCommand("C:\\Users\\andy\\Downloads\\ffmpeg-master-latest-win64-gpl\\bin\\ffmpeg.exe ", String.Format(" -i C:\\Users\\andy\\source\repos\\VideoAudioGrabberVLC\\VideoAudioGrabberVLC\\{0}.ts -i C:\\Users\\andy\\source\repos\\VideoAudioGrabberVLC\\VideoAudioGrabberVLC\\{0}.acc -c:v copy -map 0:v -map 1:a -y C:\\Users\\andy\\source\repos\\VideoAudioGrabberVLC\\VideoAudioGrabberVLC\\{0}-FINAL.mp4", item.outputFileName), ref _Output, ref _Error);

                System.Diagnostics.Process.Start("ffmpeg.exe",
                    $" -i {projPath}{item.outputFileName}.ts -i {projPath}{item.outputFileName}.acc -c:v copy -map 0:v -map 1:a -y {projPath}{item.outputFileName}-FINAL.mp4"
                    );
            }
        }
        
      
    }

    
}