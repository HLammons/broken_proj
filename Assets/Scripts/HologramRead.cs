// code is a modification of https://foxypanda.me/tcp-client-in-a-uwp-unity-app-on-hololens/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using System;
using System.IO;
using System.Text;
using System.Threading;


#if !UNITY_EDITOR
using System.Threading.Tasks;
using System.Net;
#endif

public class HologramRead : MonoBehaviour
{   
    MyTcpClient tcpClient;

    //String variable that will be sent to the server
    [SerializeField]
    public string DriveMessage;
    public string SteerMessage;
    public string PanTiltMessage;

    //string sendMessage;

#if !UNITY_EDITOR
    private bool _useUWP = true;
    private Windows.Networking.Sockets.StreamSocket socket;
    private Task exchangeTask;
    #endif

#if (UNITY_EDITOR)
    private bool _useUWP = false;
    System.Net.Sockets.TcpClient client;
    System.Net.Sockets.NetworkStream stream;
    private Thread exchangeThread;
#endif

    private Byte[] bytes = new Byte[256];
    private StreamWriter writer;
    private StreamReader reader;

    public void Start()
    {
        //Server ip address and port
        Connect("192.168.1.9", "8000");
    }

    public void Connect(string host, string port)
    {
        if (_useUWP)
        {
            ConnectUWP(host, port);
        }
        else
        {
            ConnectUnity(host, port);
        }
    }

#if (UNITY_EDITOR)
    private void ConnectUWP(string host, string port)
#else
    private async void ConnectUWP(string host, string port)
#endif
    {
#if (UNITY_EDITOR)
        errorStatus = "UWP TCP client used in Unity!";
#else
        try
        {
            if (exchangeTask != null) StopExchange();

            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);

            Stream streamOut = socket.OutputStream.AsStreamForWrite();
            writer = new StreamWriter(streamOut) { AutoFlush = true };

            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn);

            successStatus = "Connected!";
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private void ConnectUnity(string host, string port)
    {
#if !UNITY_EDITOR
        errorStatus = "Unity TCP client used in UWP!";
#else
        try
        {
            if (exchangeThread != null) StopExchange();

            client = new System.Net.Sockets.TcpClient(host, Int32.Parse(port));
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };

            RestartExchange();
            successStatus = "Connected!";
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private bool exchanging = false;
    private bool exchangeStopRequested = false;
    private string lastPacket = null;

    private string errorStatus = null;
    private string successStatus = null;

    public void RestartExchange()
    {
#if UNITY_EDITOR
        if (exchangeThread != null) StopExchange();
        exchangeStopRequested = false;
        exchangeThread = new System.Threading.Thread(SendPackets);
        exchangeThread.Start();
#else
        if (exchangeTask != null) StopExchange();
        exchangeStopRequested = false;
        exchangeTask = Task.Run(() => SendPackets());
#endif
    }


    public void SendPackets()
    {
        try
        {
            writer.Write(DriveMessage);
            writer.Write(SteerMessage);
            writer.Write(PanTiltMessage);
            
            
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

//    public void ReceivePackets()
//    {
//        while (returnMessage != "completed")
//        {
//#if (!UNITY_EDITOR)
//            byte[] bytes = new byte[client.SendBufferSize];
//            int recv = 0;
//            while (true)
//            {
//                recv = stream.Read(bytes, 0, client.SendBufferSize);
//                returnMessage += Encoding.UTF8.GetString(bytes, 0, recv);
//                if (returnMessage.EndsWith("\n")) break;
//            }
//#else
//            returnMessage = reader.ReadLine();
//#endif
//        }

//        Debug.Log(returnMessage);
//        returnMessage = "";

//    }

    public void StopExchange()
    {
        exchangeStopRequested = true;

#if UNITY_EDITOR
        if (exchangeThread != null)
        {
            exchangeThread.Abort();
            stream.Close();
            client.Close();
            writer.Close();
            reader.Close();

            stream = null;
            exchangeThread = null;
        }
#else
        if (exchangeTask != null)
        {
            exchangeTask.Wait();
            socket.Dispose();
            writer.Dispose();
            reader.Dispose();

            socket = null;
            exchangeTask = null;
        }
#endif
        writer = null;
        reader = null;
    }

    public void OnDestroy()
    {
        StopExchange();
    }    
    

    void Update()
    {
        SteerMessage = GameObject.Find("Steer").GetComponent<SteerMove>().steerMessage;
        PanTiltMessage = GameObject.Find("PanTilt").GetComponent<PanTiltMove>().panMessage;
        DriveMessage = GameObject.Find("Drive").GetComponent<DriveMove>().driveMessage;

        SendPackets();
    }

}
