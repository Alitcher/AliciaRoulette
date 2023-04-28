using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Roulette;

public class RouletteTcpListener
{
    public event EventHandler<RouletteData> OnDataReceived;

    public async void StartListening()
    {
        int port = 4948;

        if (!IsPortAvailable(port))
        {
            return;
        }

        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            StreamReader reader = new StreamReader(client.GetStream());
            string jsonString = await reader.ReadToEndAsync();

            RouletteData rouletteData = JsonConvert.DeserializeObject<RouletteData>(jsonString);

            OnDataReceived?.Invoke(this, rouletteData);
        }
    }

    public static bool IsPortAvailable(int port)
    {
        bool isAvailable = true;

        try
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, port);
            }
        }
        catch
        {
            isAvailable = false;
        }

        return isAvailable;
    }
}
