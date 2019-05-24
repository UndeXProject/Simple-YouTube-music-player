using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;

namespace Simple_YouTube_Music_Player.Classes
{
    public class Discord
    {
        private static string AppDataDir = Functions.AppDataSoft;
        public static string DisctordAppID { get; } = "<YOUR_APP_ID>";
        public static LogLevel DiscordLogLevel { get; private set; }

        private static DiscordRpcClient client =  new DiscordRpcClient(DisctordAppID);
        private static bool isRunning = true;
        private static RichPresence presence = new RichPresence()
        {
            Details = "(v 2.0) © UndeX Project",
            State = "https://discord.gg/RyG8NUr",
            Assets = new Assets()
            {
                LargeImageKey = "logo",
#if DEBUG
                LargeImageText = "SYMP V2.0 [DEBUG MODE]",
#else
                LargeImageText = "SYMP V2.0",
#endif
            }
        };

#region Methods

#region Init
        public static void Init()
        {
            client.RegisterUriScheme();
            var logFile = Path.Combine(AppDataDir, "discord-rpc.log");
            //MessageBox.Show(logFile);
            //Set the logger. This way we can see the output of the client.
            //We can set it this way, but doing it directly in the constructor allows for the Register Uri Scheme to be logged too.
            System.IO.File.WriteAllBytes(logFile, new byte[0]);
            client.Logger = new FileLogger(logFile, DiscordLogLevel);

            //Register to the events we care about. We are registering to everyone just to show off the events

            client.OnReady += OnReady;                                      //Called when the client is ready to send presences
            client.OnClose += OnClose;                                      //Called when connection to discord is lost
            client.OnError += OnError;                                      //Called when discord has a error

            client.OnConnectionEstablished += OnConnectionEstablished;      //Called when a pipe connection is made, but not ready
            client.OnConnectionFailed += OnConnectionFailed;                //Called when a pipe connection failed.

            client.OnPresenceUpdate += OnPresenceUpdate;                    //Called when the presence is updated

            client.OnSubscribe += OnSubscribe;                              //Called when a event is subscribed too
            client.OnUnsubscribe += OnUnsubscribe;                          //Called when a event is unsubscribed from.

            client.OnJoin += OnJoin;                                        //Called when the client wishes to join someone else. Requires RegisterUriScheme to be called.
            client.OnSpectate += OnSpectate;                                //Called when the client wishes to spectate someone else. Requires RegisterUriScheme to be called.
            client.OnJoinRequested += OnJoinRequested;                      //Called when someone else has requested to join this client.
            presence.Secrets = new Secrets()
            {
                //These secrets should contain enough data for external clients to be able to know which
                // game to connect too. A simple approach would be just to use IP address, but this is highly discouraged
                // and can leave your players vulnerable!
                JoinSecret = "",
                SpectateSecret = ""
            };
            client.SetPresence(presence);

            //Initialize the connection. This must be called ONLY once.
            //It must be called before any updates are sent or received from the discord client.
            client.Initialize();
            isRunning = true;
        }
#endregion

#region SetTitle
        public static void SetTitle(string TrackName)
        {
            presence = new RichPresence()
            {
                Details = "Слушает:",
                State = TrackName,
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = TrackName,
                    SmallImageKey = "stop",
                    SmallImageText = "Стоп"
                },
                Party = new Party()
                {
                    ID = "",
                    Max = 100,
                    Size = 0
                }
            };
            client.SetPresence(presence);
        }
#endregion

#region SetText
        public static void SetText(string Line1, string Line2)
        {
            presence = new RichPresence()
            {
                Details = Line1,
                State = Line2,
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = Line1
                }
            };
            client.SetPresence(presence);
        }
#endregion

#region SetData
        public static void SetData(int Mode)
        {
            Regex reg = new Regex("([0-9]{1,2}):([0-9]{1,2})");
            Match m = reg.Match(Functions.playlist[Functions.PlaylistPosition][4]);
            int duration = 0;
            if (m.Groups[1].Length > 0)
            {
                int minuteToSec = Convert.ToInt32(m.Groups[1].Value) * 60;
                int seconds = Convert.ToInt32(m.Groups[2].Value);
                duration = minuteToSec + seconds;
            }
            presence = new RichPresence()
            {
                Details = "Слушает:",
                State = Functions.playlist[Functions.PlaylistPosition][0],
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = Functions.playlist[Functions.PlaylistPosition][0],
                    SmallImageKey = "stop",
                    SmallImageText = "Стоп"
                }
            };
            presence.Secrets = new Secrets()
            {
                SpectateSecret = Functions.playlist[Functions.PlaylistPosition][5]
            };
            presence.Party = new Party()
            {
                ID = @"https://youtu.be/"+Functions.playlist[Functions.PlaylistPosition][5],
                Max = 100,
                Size = 0
            };
            switch (Mode)
            {
                case 1:
                    presence.Assets.SmallImageKey = "play";
                    presence.Assets.SmallImageText = "Воспроизведение";
                    presence.Timestamps = new Timestamps()
                    {
                        Start = DateTime.UtcNow,
                        End = DateTime.UtcNow + TimeSpan.FromSeconds(duration)
                    };
                    break;
                case 2:
                    presence.Assets.SmallImageKey = "pause";
                    presence.Assets.SmallImageText = "Пауза";
                    presence.Timestamps = new Timestamps();
                    break;
                default:
                    presence.Assets.SmallImageKey = "stop";
                    presence.Assets.SmallImageText = "Стоп";
                    presence.Timestamps = new Timestamps();
                    break;
            }
            client.SetSubscription(EventType.Join | EventType.Spectate | EventType.JoinRequest);
            client.SetPresence(presence);
        }
#endregion
#endregion

#region Events

#region State Events
        private static void OnReady(object sender, ReadyMessage args)
        {
            //This is called when we are all ready to start receiving and sending discord events. 
            // It will give us some basic information about discord to use in the future.

            //It can be a good idea to send a inital presence update on this event too, just to setup the inital game state.
            //Console.WriteLine("On Ready. RPC Version: {0}", args.Version);
            //MessageBox.Show("On Ready. RPC Version: "+ args.Version.ToString());

        }
        private static void OnClose(object sender, CloseMessage args)
        {
            //This is called when our client has closed. The client can no longer send or receive events after this message.
            // Connection will automatically try to re-establish and another OnReady will be called (unless it was disposed).
            //Console.WriteLine("Lost Connection with client because of '{0}'", args.Reason);
            //MessageBox.Show("Lost Connection with client because of '" + args.Reason + "'");
        }
        private static void OnError(object sender, ErrorMessage args)
        {
            //Some error has occured from one of our messages. Could be a malformed presence for example.
            // Discord will give us one of these events and its upto us to handle it
            //Console.WriteLine("Error occured within discord. ({1}) {0}", args.Message, args.Code);
            //MessageBox.Show("Error occured within discord. (" + args.Code.ToString() + ") " + args.Message);
        }
#endregion

#region Pipe Connection Events
        private static void OnConnectionEstablished(object sender, ConnectionEstablishedMessage args)
        {
            //This is called when a pipe connection is established. The connection is not ready yet, but we have at least found a valid pipe.
            //Console.WriteLine("Pipe Connection Established. Valid on pipe #{0}", args.ConnectedPipe);
            //MessageBox.Show("Pipe Connection Established. Valid on pipe #" + args.ConnectedPipe.ToString());
        }
        private static void OnConnectionFailed(object sender, ConnectionFailedMessage args)
        {
            //This is called when the client fails to establish a connection to discord. 
            // It can be assumed that Discord is unavailable on the supplied pipe.
            //Console.WriteLine("Pipe Connection Failed. Could not connect to pipe #{0}", args.FailedPipe);
            //MessageBox.Show("Pipe Connection Failed. Could not connect to pipe #" + args.FailedPipe.ToString());
            isRunning = false;
        }
#endregion

        private static void OnPresenceUpdate(object sender, PresenceMessage args)
        {
            //This is called when the Rich Presence has been updated in the discord client.
            // Use this to keep track of the rich presence and validate that it has been sent correctly.
            //Console.WriteLine("Rich Presence Updated. Playing {0}", args.Presence == null ? "Nothing (NULL)" : args.Presence.State);
        }

#region Subscription Events
        private static void OnSubscribe(object sender, SubscribeMessage args)
        {
            //This is called when the subscription has been made succesfully. It will return the event you subscribed too.
            //Console.WriteLine("Subscribed: {0}", args.Event);
        }
        private static void OnUnsubscribe(object sender, UnsubscribeMessage args)
        {
            //This is called when the unsubscription has been made succesfully. It will return the event you unsubscribed from.
            //Console.WriteLine("Unsubscribed: {0}", args.Event);
        }
#endregion

#region Join / Spectate feature
        private static void OnJoin(object sender, JoinMessage args)
        {
            /*
			 * This is called when the Discord Client wants to join a online game to play.
			 * It can be triggered from a invite that your user has clicked on within discord or from an accepted invite.
			 * 
			 * The secret should be some sort of encrypted data that will give your game the nessary information to connect.
			 * For example, it could be the Game ID and the Game Password which will allow you to look up from the Master Server.
			 * Please avoid using IP addresses within these fields, its not secure and defeats the Discord security measures.
			 * 
			 * This feature requires the RegisterURI to be true on the client.
			*/
            //Console.WriteLine("Joining Game '{0}'", args.Secret);
        }

        private static void OnSpectate(object sender, SpectateMessage args)
        {   /*
			 * This is called when the Discord Client wants to join a online game to watch and spectate.
			 * It can be triggered from a invite that your user has clicked on within discord.
			 * 
			 * The secret should be some sort of encrypted data that will give your game the nessary information to connect.
			 * For example, it could be the Game ID and the Game Password which will allow you to look up from the Master Server.
			 * Please avoid using IP addresses within these fields, its not secure and defeats the Discord security measures.
			 * 
			 * This feature requires the RegisterURI to be true on the client.
			*/
            //Console.WriteLine("Spectating Game '{0}'", args.Secret);
            MessageBox.Show("[DiscordRPC] Получена ссылка на трек: '" + args.Secret + "'");
            Functions.JoinID = args.Secret;
        }

        private static void OnJoinRequested(object sender, JoinRequestMessage args)
        {
            /*
			 * This is called when the Discord Client has received a request from another external Discord User to join your game.
			 * You should trigger a UI prompt to your user sayings 'X wants to join your game' with a YES or NO button. You can also get
			 *  other information about the user such as their avatar (which this library will provide a useful link) and their nickname to
			 *  make it more personalised. You can combine this with more API if you wish. Check the Discord API documentation.
			 *  
			 *  Once a user clicks on a response, call the Respond function, passing the message, to respond to the request.
			 *  A example is provided below.
			 *  
			 * This feature requires the RegisterURI to be true on the client.
			*/

            //We have received a request, dump a bunch of information for the user
            //Console.WriteLine("'{0}' has requested to join our game.", args.User.Username);
            //Console.WriteLine(" - User's Avatar: {0}", args.User.GetAvatarURL(User.AvatarFormat.GIF, User.AvatarSize.x2048));
            //Console.WriteLine(" - User's Descrim: {0}", args.User.Discriminator);
            //Console.WriteLine(" - User's Snowflake: {0}", args.User.ID);
            //Console.WriteLine();

            //Ask the user if they wish to accept the join request.
            //Console.Write("Do you give this user permission to join? [Y / n]: ");
            //bool accept = Console.ReadKey().Key == ConsoleKey.Y; Console.WriteLine();

            //Tell the client if we accept or not.
            //DiscordRpcClient client = (DiscordRpcClient)sender;
            //client.Respond(args, accept);

            //All done.
            //Console.WriteLine(" - Sent a {0} invite to the client {1}", accept ? "ACCEPT" : "REJECT", args.User.Username);
        }
#endregion

#endregion
    }
}
