//-----------------------------------------------------------------------
// <copyright file="UdpUser.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace Msiexecutor.UDP
{
    using System.Text;
    
    /// <summary>
    /// UDP User
    /// </summary>
    public class UdpUser : UdpBase
    {
        /// <summary>
        /// Connect to host
        /// </summary>
        /// <param name="hostname">host name</param>
        /// <param name="port">port number</param>
        /// <returns>UDP User object</returns>
        public static UdpUser ConnectTo(string hostname, int port)
        {
            var connection = new UdpUser();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">message to send</param>
        public void Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }
    }
}
