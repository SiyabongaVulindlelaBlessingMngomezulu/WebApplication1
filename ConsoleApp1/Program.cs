using System;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            
            try
            {
                var file = new FileInfo("Nobody.61feb1fbee2d72c8bd1c91d5.wav");
                var fileTest = File.Create("https://s3.eu-west-1.amazonaws.com/co.za.fyve.uploads.prod/briansambo.nb%40gmail.com/artists/6135b3558afb29761aa08db7/releases/6225c4abdb61e688e7e7392f/tracks/Hell%20of%20a%20night.6225d788db61e688e7e73990.wav");
                var tagLibFile = TagLib.File.Create(file.Name);
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


                string ext = Path.GetExtension(file.FullName);


                Console.WriteLine("  Name : " + title + "\n");
                Console.WriteLine(" album : " + album + "\n");

                foreach (var artist in albumArtists)
                {
                    Console.WriteLine(" Artist : " + artist + "\n");
                }

                Console.WriteLine(" Genre : " + genres + "\n");
                Console.WriteLine(" duration : " + length + "\n");
                Console.WriteLine(" bit Rate : " + bitRate + "\n");

                Console.WriteLine(" Sample Rate : " + sampleRate + "\n");
                Console.WriteLine(" bit depth : " + bitDepth + "\n");
                Console.WriteLine(" channels : " + channels + "\n");

                Console.WriteLine(" types : " + types + "\n");

            }
            catch (Exception e)
            {
                Console.WriteLine("Oops ! an exception has occured, here are dome details: \n" + e);
            }
        }
    }
}