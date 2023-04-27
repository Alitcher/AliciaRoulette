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
        int port = 5000;
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
}
