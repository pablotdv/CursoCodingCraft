using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Exercicio05WebAPI.Models
{
    public class VideoStream
    {
        private readonly string videoName;

        public VideoStream(string videoName)
        {
            this.videoName = videoName;
        }

        public async Task WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            string videoFileName = "TestData\\Videos\\" + videoName + ".mp4";
            try
            {
                var buffer = new byte[65536];

                using (var video = File.Open(videoFileName, FileMode.Open, FileAccess.Read))
                {
                    var length = (int)video.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = video.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}