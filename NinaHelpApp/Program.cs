using System;
using System.Net;
using System.Net.Sockets;
using System.Text;  
namespace Nina
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Conectar();
            Conectar2();
        }
        public static void Conectar2()
        {
            UdpClient udpClient = new UdpClient(6969);
            try{
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint); 
                string returnData = Encoding.ASCII.GetString(receiveBytes);        
                // Uses the IPEndPoint object to determine which of these two hosts responded.
                Console.WriteLine("This is the message you received " +
                                            returnData.ToString());
                Console.WriteLine("This message was sent from " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());

                udpClient.Close();                
            }  
            catch (Exception e ) {
                    Console.WriteLine(e.ToString());
            }

        }
        public static void Conectar() 
        {
            IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddress = host.AddressList[0];  
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 6969);
            try
            {
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Console.WriteLine("Escuchando...");
                Socket handler = listener.Accept();
                Console.WriteLine("Conectado con exito");
                byte [] bytes = new byte[1024];
                string data = null;
                int bytesRec = handler.Receive(bytes);  
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine(data);
                listener.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
            Console.WriteLine("Presione cualquier tecla para terminar");
            Console.ReadLine();
        }
    }

}
