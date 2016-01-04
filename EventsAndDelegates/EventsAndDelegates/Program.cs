///
/// Author: Robert C. Martin and Micah Martin
/// Book: Agile Principles, Practices and Patterns in C#
/// 
/// Igor Octaviano
/// More? access: https://github.com/igoroctaviano/unclebob-potofcode
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EventsAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var video = new Video() { Title = "The Lonely Shepherd" };
            var videoEncoder = new VideoEncoder(); // Publisher
            var mailService = new MailService(); // Subscriber
            var messageService = new MessageService(); // Subscriber

            videoEncoder.VideoEnconded += mailService.OnVideoEnconded; // Add method *event reference
            videoEncoder.VideoEnconded += messageService.OnVideoEncoded; // Add another method *event reference

            /* This way, we have to make changes to VideoEncoder class?
               We do not net to change that class anymore, the only thing we have to do
               does not need recompilation and redeployment.
               We can add another class, nothing will be affected in the VideoEncoder class. */

            videoEncoder.Encode(video);
        }
    }

    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    public class MessageService
    {
        // Maybe                  (object source, VideoEventArgs e)
        public void OnVideoEncoded(object source, EventArgs e)
        {
            Console.WriteLine("MessageService: Sending a text message..." + " Additional data info: " + e.GetType().ToString());
        }
    }

    public class VideoEncoder
    {
        // 1- Define a delegate
        public delegate void VideoEncondedEventHandler(object source, EventArgs e);

        /* We can use EventHandler from C# instead of using event and personal event handler (delegate)
        by just assigning this: public event EventHandler<VideoEventArgs> VideoEncoded and its done, 
        nothing more to do, where EventHandler<VideoEventHandler> its a delegate. 
        We can also declare just: public event EventHandler VideoEncoded without the parameter <VideoEventHandler>
        used only if you need to pass some data or personal data *EventArgs in an event. */     

        // 2- Define an event based on that delegate
        public event VideoEncondedEventHandler VideoEnconded;

        public void Encode(Video video)
        {
            Console.WriteLine("Enconding Video...");
            Thread.Sleep(3000);

            // 3- Raise the event
            OnVideoEnconded(video);
        }

        protected virtual void OnVideoEnconded(Video video)
        {
            if (VideoEnconded != null)
                // VideoEnconded(this, EventArgs.Empty);
                VideoEnconded(this, new VideoEventArgs() { Video = video }); // If you want additional data *EventArgs
        }
    }

    public class MailService
    {
        public void OnVideoEnconded(object source, EventArgs e)
        {
            Console.WriteLine("MailService: Sending an Email...");
        }
    }

    public class Video
    {
        public string Title { get; set; }
    }
}
