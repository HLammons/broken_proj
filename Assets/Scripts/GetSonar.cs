using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.Events;


public class GetSonar : MonoBehaviour
{
    string data;
    public float updateTime = 0.25f;
    public bool on = true;
    public float[] vals = new float[1];

    private string DataBaseIp = "http://98.48.61.104/lewis/getSensorData.php?apikey=123&name=RobotSonar&start=00000000000000&count=1";
    private StreamWriter writer;
    private StreamReader reader;
    private string returnMessage;
    private string cts;

    PostProcessedData postprocesseddata;
    public Vector3 headPos = new Vector3(-0.015f, -0.026f, 0);
    public int activeInterestPointsID = 1;

    private bool m_IsContinueInput = false;

    //private int i = 0;

    [Range(10, 100)]
    public int resolution = 100;

    void Awake()
    {

    }

#if (!UNITY_EDITOR)
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

    // Use this for initialization
    void Start()
    {

    }

    private IEnumerator GetRequest(string uri, System.Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            //byte[] results = www.downloadHandler.data;
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                callback("Failed to connect");
            }
            else
            {
                string data = webRequest.downloadHandler.text;
                callback(data);
            }
        }
    }

    public class PostProcessedData
    {
        public string status;
        public int id;
        public string datetime;
        public string x;
    }

    // function to connect to server using either UWP for HoloLens or nonUWP for Unity
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
#endif
#if (!UNITY_EDITOR)
    private async void ConnectUWP(string host, string port)
#endif
    {
#if (UNITY_EDITOR)
        Debug.Log("UWP TCP client used in Unity!");
#endif
#if (!UNITY_EDITOR)
        try
        {
            if (exchangeTask != null)
            {
                StopExchange();
            }

            // for HoloLens setup sockets
            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);

            // setup writer and reader for HoloLens
            Stream streamOut = socket.OutputStream.AsStreamForWrite();
            writer = new StreamWriter(streamOut) { AutoFlush = true };

            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn);

            // needed to trigger Arduino to start sending data
            RestartExchange();

        }
        catch (Exception e)
        {
            Debug.Log("error");
            Debug.Log(e.ToString());
        }
#endif
    }

    private void ConnectUnity(string host, string port)
    {
#if (!UNITY_EDITOR)
        Debug.Log("Unity TCP client used in UWP!");
#endif
#if (UNITY_EDITOR)
        try
        {
            if (exchangeThread != null) StopExchange();

            // setup sockets for Unity
            client = new System.Net.Sockets.TcpClient(host, Int32.Parse(port));
            stream = client.GetStream();
            // assign reader and writer to the streams
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };

            // needed to trigger Arduino to start sending data
            RestartExchange();
        }
        catch (Exception e)
        {
            Debug.Log("error");
            Debug.Log(e.ToString());
        }
#endif
    }

    public void RestartExchange()
    {
        if (_useUWP) cts = "UWP Connect";
        else cts = "Unity Connect";

#if (UNITY_EDITOR)
        if (exchangeThread != null) StopExchange();
        exchangeThread = new System.Threading.Thread(SendPackets);
        exchangeThread.Start();
#endif
#if (!UNITY_EDITOR)
        if (exchangeTask != null) StopExchange();
        exchangeTask = Task.Run(() => SendPackets());
#endif
    }

    public void SendPackets()
    {
        try
        {
            writer.Write(cts);
            Debug.Log(cts);
        }
        catch (Exception e)
        {
            Debug.Log("error");
            Debug.Log(e.ToString());
        }
        cts = "";
    }

    public void ReceiveDataLine()
    {
        try
        {
#if (UNITY_EDITOR)
            // setup byte array of bytes length set to what server is sending
            byte[] bytes = new byte[client.ReceiveBufferSize];
            // receive data into byte array
            stream.Read(bytes, 0, (int)client.ReceiveBufferSize);
            // decode array of bytes into string
            data = Encoding.UTF8.GetString(bytes);
#else
            // in HoloLens use ReadLine to get data from server
            {
            }
#endif
        }
        catch (Exception e)
        {
            Debug.Log("error");
            Debug.Log(e.ToString());
        }
    }

    public void StopExchange()
    {
        // if using Unity stop the Thread and if HoloLens stop the Task
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
#endif
#if (!UNITY_EDITOR)
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
        // clear data in writer and reader
        writer = null;
        reader = null;
    }

    // make sure to stop server when shutting down
    public void OnDestroy()
    {
        StopExchange();
    }

    void Update()
    {

    }

    public class CommandData
    {
        public int id;
        public string datetime;
        public string x;
        public string command;
        public string data;

    }

    private IEnumerator DataBaseObserver()
    {
        print("db Observer started");
        string uri = DataBaseIp;
        string commandUri = DataBaseIp;
        int prevID = 0;
        print(uri);
        while (true)
        {
            yield return GetRequest(uri, (string str) =>
            {
                print(str);
                if (str != "Failed to connect")
                {
                    postprocesseddata = JsonUtility.FromJson<PostProcessedData>(str);
                    string[] sonar = str.Split(new string[] { "\":\"" }, StringSplitOptions.None);
                    foreach (var value in sonar)
                    {
                        //print(value);
                    }
                    String DistanceString = sonar[2].Substring(0, 6);
                    print(DistanceString);
                    dist.text = DistanceString + " cm";
                    if (postprocesseddata.id != prevID)
                    {
                        //prevID = postprocesseddata.id;
                        //string[] sonar = str.Split(new string[] { "\":\"" }, StringSplitOptions.None);
                        //String DistanceString = sonar[2];
                        //float distance = float.parse(distanceString);
                    }
                }
                else
                {
                    Debug.LogError("Failed to connect to: " + uri);
                }
            });
        }
    }

    //IEnumerator DataBaseObserver()
    //{
    //    while (on)
    //    {
    //        UnityWebRequest www = UnityWebRequest.Get("http://98.48.61.104/lewis/getSensorData.php?apikey=123&name=RobotSonar&start=00000000000000&count=1");
    //        yield return www.SendWebRequest();

    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            // Show results as text
    //            Debug.Log(www.downloadHandler.text);
    //            data = www.downloadHandler.text;
    //            // Or retrieve results as binary data
    //            byte[] results = www.downloadHandler.data;

    //            ParseData();
    //        }
    //        yield return new WaitForSeconds(updateTime);
    //    }
    //}

    //private void ParseData()
    //{
    //    List<string> ls = new List<string>();
    //    char[] trimChars = { ':', ',','[',']' };

    //    //Parse data to floats
    //    int j = 0;
    //    vals[j] = float.Parse(ls[i].Trim(trimChars));
    //}

    private void ContinueInput(int i)
    {

        // check if you want to keep showing data
        if (false == m_IsContinueInput)
        {
            return;
        }

        // get data from server
        ReceiveDataLine();
    }

    [SerializeField]
    private TextMesh dist;

    // function to view feed when button pressed
    public void OnViewFeed()
    {
        Debug.Log("View Feed");

        if (!m_IsContinueInput)
        {
            m_IsContinueInput = true;
            StartCoroutine(DataBaseObserver());
        };
    }

    // function to stop feed when button pressed
    public void OnStopFeed()
    {
        Debug.Log("Stop Feed");
        m_IsContinueInput = false;
    }
}
