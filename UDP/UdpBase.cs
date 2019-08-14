//-----------------------------------------------------------------------
// <copyright file="UdpBase.cs" company="WEIR">
//    © 2019 WEIR All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------

namespace Msiexecutor.UDP
{
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// UDP Base
    /// </summary>
    public abstract class UdpBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UdpBase" /> class.
        /// </summary>
        protected UdpBase()
        {
            this.Client = new UdpClient();
        }

        /// <summary>
        /// Gets or sets client
        /// </summary>
        protected UdpClient Client { get; set; }

        /// <summary>
        /// Receive message from Sender
        /// </summary>
        /// <returns>Received Object</returns>
        public async Task<Received> Receive()
        {
            var result = await this.Client.ReceiveAsync();
            return new Received()
            {
                Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }
}
