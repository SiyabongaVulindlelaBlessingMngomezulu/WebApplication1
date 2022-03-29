using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace ConsoleApp1
{
    public class Utility
    {

        public Utility(IFormFile file, string path) 
        { 
           this.file = file;
           isComplete = false;
           semaphoreSlim = new SemaphoreSlim(1);
            filePath = path;
        }

        private System.Timers.Timer? aTimer;
        public  AudioMetadata? amd;
        public volatile bool isComplete;
        public string filePath;
        public IFormFile? file;
        public  SemaphoreSlim semaphoreSlim;

        public static string Test() {
            return "It works !";
        }
        /*
        public void SetTimer()
        {
            aTimer = new System.Timers.Timer(100); 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        */

        /*
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Utility.GetFileProperties(filePath);
            if (File.Exists(filePath))
            {
                try 
                {
                    GetFileProperties();                
                    semaphoreSlim.Wait();
                    isComplete = true;
                    semaphoreSlim.Release();
                    aTimer.Stop();
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine("Here is the exception on the timed action event: " + ex);
                }
                

            }
            else
            {

            }
        }
        */
        public  async Task writeFile() {
            try
            {
                    filePath = Path.Combine(@"C:\Users\username\Music\fyveAudio\", file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                       await file.CopyToAsync(fileStream);
                    }

            }
            catch (Exception ex)
            {


                //return new AudioMetadata() { MediaType = "Exception" };//"Oops an exception has occured, here are some details:\n\n\n" + ex;
            }

        }

        
        public async Task GetFileProperties() {
            //GetMetadata(1).Result;
            //await Task.Delay(100);
            while (true) {
                if (File.Exists(filePath)) {
                    Console.WriteLine("We made it.");
                    await GetMetadata();
                    break;
                }
            }
        }
        


        public  async Task GetMetadata() {
            try
            {

                int check = 0;

                while (check < 100000000) {
                    Thread.Sleep(40);
                    await Task.Delay(40);
                    check++;
                    if (File.Exists(filePath)) {
                        break;
                    }
                    else { continue; }
                }
                
                // var fileTest 
                var tagLibFile = TagLib.File.Create(filePath);
                var title = tagLibFile.Tag.Title;
                var album = tagLibFile.Tag.Album;
                var albumArtists = tagLibFile.Tag.AlbumArtists;
                var genres = tagLibFile.Tag.JoinedGenres;
                var length = tagLibFile.Properties.Duration;
                var bitRate = tagLibFile.Properties.AudioBitrate;
                var sampleRate = tagLibFile.Properties.AudioSampleRate;
                var bitDepth = tagLibFile.Properties.BitsPerSample;
                var channels = tagLibFile.Properties.AudioChannels;
                var types = tagLibFile.Properties.MediaTypes.ToString();


                string ext = Path.GetExtension(aFile.FullName);

                amd = new AudioMetadata()
                {
                    BitRate = bitRate,
                    SampleRate = sampleRate,
                    BitDepth = bitDepth,
                    MediaType = types,
                    Title = title,
                    Extension = ext
                };
            }
            catch (IOException ioe) {
                Console.WriteLine("Oops ! an i/o exception has occured, here are dome details: \n" + ioe);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops ! an exception has occured, here are dome details: \n" + e);
               
            }
            
        }
    }

}

    

