//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace Msiexecutor
{
    using System;
    using System.IO;
    using System.Net;
    using Msiexecutor.UDP;

    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args">arguments to main method</param>
        public static void Main(string[] args)
        {
            try
            {
                // validate is required parameters received
                if (IsParametersValid(args))
                {
                    // create a new client
                    var client = UdpUser.ConnectTo(args[0], 32123);
                    string url = args[1];
                    string fileName = Path.GetFileName(url);
                    string destination = Path.Combine(Constants.Path, fileName);
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadFile(url, destination);
                        client.Send(fileName);
                        Console.WriteLine("Sent message:" + fileName);
                        Received replay = client.Receive().Result; 
                        Console.WriteLine("replay " + replay.Message + " from Host Machine");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// validate is required parameters received
        /// </summary>
        /// <param name="args">list of arguments</param>
        /// <returns>is valid or not</returns>
        private static bool IsParametersValid(string[] args)
        {
            // args[0] is udp connect IP address
            IPAddress address;
            if (args.Length == 0)
            {
                Console.WriteLine(Constants.NoArgumentsFound);
                return false;
            }

            if (!IPAddress.TryParse(args[0], out address))
            {
                Console.WriteLine(Constants.NoValidIPFound);
                return false;
            }

            //// args[1] is *.msi network URL
            if (args.Length == 1)
            {
                Console.WriteLine(Constants.NoURLFound);
                return false;
            }

            return true;
        }
    }
}
