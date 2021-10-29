using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;

namespace UniAlumni.Business.Services.PushNotification
{
    public static class PushNotification
    {
        public static async Task SendMessage(string uid, string title, string content)
        {
            try
            {
                var topic = uid;

                var message = new Message
                {
                    Notification = new Notification
                    {
                        
                        Title = title,
                        Body = content
                    },
                    Android = new AndroidConfig()
                    {
                        Notification = new AndroidNotification
                        {
                            Icon = "stock_ticker_update",
                            Color = "#f45342",
                            Sound = "default"
                        }
                    },
                    Data = new Dictionary<string, string>()
                    {
                        {"title", title},
                        {"content", content}
                    },
                    Topic = topic
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

          
        }
    }
}