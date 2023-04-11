using ArcFaceService.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArcFaceService.Service
{
    public class SocketClient
    {
        private static ASCIIEncoding encoding = new ASCIIEncoding();
        private TcpClient client;
        private Stream stream;
        private StreamReader streamReader;
        private static SocketClient _instance;
        private bool reconnect = false;
        public static SocketClient Instance
        {
            get
            {
                return _instance == null ? _instance = new SocketClient() : _instance;
            }
        }

        public void connectToServer()
        {
            client = new TcpClient();

            try
            {
                int servicePort = int.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["SOCKET_SERVICE_PORT"]);
                string serviceIp = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["SOCKET_SERVICE_IP"];
                client.Connect(serviceIp, servicePort);
                stream = client.GetStream();
                streamReader = new StreamReader(stream);
                Console.WriteLine("Connected to face engine server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not connect to Face engine service " + ex.Message);
            }
        }

        public void reconnectToServer()
        {
            client = new TcpClient();
            int servicePort = int.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["SOCKET_SERVICE_PORT"]);
            string serviceIp = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["SOCKET_SERVICE_IP"];
            client.Connect(serviceIp, servicePort);
            stream = client.GetStream();
            streamReader = new StreamReader(stream);
            reconnect = false;
            Console.WriteLine("Connected to face engine server.");
        }

        public async Task<string> sendRequest(string message)
        {
            string responseJson = "";

            //Check connection
            if (!isConnected())
            {
                if (!reconnect)
                {
                    try
                    {
                        reconnect = true;
                        reconnectToServer();
                        responseJson = await sendRequest(message);
                        return responseJson;
                    }
                    catch
                    {
                        reconnect = false;
                        throw new Exception("Can not connect to Face engine service, please check connection.");
                    }
                }
            }

            try
            {
                // 2. send
                byte[] data = encoding.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // 3. receive
                //byte[] result = new byte[2 * 1024 * 1024];

                //// String to store the response ASCII representation.
                //string responseData = string.Empty;

                //// Read the first batch of the TcpServer response bytes.
                //int bytes = await stream.ReadAsync(result, 0, result.Length);
                //responseData = Encoding.ASCII.GetString(result, 0, bytes);
                //return responseData;

                string response;
                while ((response = streamReader.ReadLine()) != null)
                {
                    responseJson = response;
                    Console.WriteLine(responseJson);
                    return responseJson;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("An existing connection was forcibly closed by the remote host"))
                {
                    if (!reconnect)
                    {
                        try
                        {
                            reconnect = true;
                            reconnectToServer();
                            responseJson = await sendRequest(message);
                        }
                        catch
                        {
                            reconnect = false;
                            throw new Exception("Can not connect to Face engine service, please check connection.");
                        }
                    }
                }
                else
                {
                    throw ex;
                }
            }

            return responseJson;
        }

        public bool isConnected()
        {
            return client.Connected;
        }
    }
}
