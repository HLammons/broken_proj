using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataBaseAPIController : MonoBehaviour
{
    static public string website = "http://127.0.0.1";
    static public int activeInspector = 1;



    private void Start()
    {
        //test
        //StartCoroutine("Test");

    }
    public IEnumerator Test()
    {
        InspectionData inspectionData1 = new InspectionData { GazeTimes = new List<DateTime>(), GazePoints = new List<Vector3>() };
        InspectionData inspectionData2 = new InspectionData { GazeTimes = new List<DateTime>(), GazePoints = new List<Vector3>() };

        for (int i = 0; i < 100; i++)
        {
            Vector3 RandomOffset1 = new Vector3(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
            Vector3 RandomOffset2 = new Vector3(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
            Vector3 newPoint1 = (Vector3.forward * i + RandomOffset1) * 0.01f;
            Vector3 newPoint2 = (Vector3.forward * i + RandomOffset2) * 0.01f;
            inspectionData1.GazePoints.Add(newPoint1);
            inspectionData2.GazePoints.Add(newPoint2);
        }
        
        //yield return InsertInspection("Foo_Base", DateTime.Now, inspectionData1.ToString(), 1, null, 0.0f);
        //yield return InsertInspection("Foo_Routine", DateTime.Now, inspectionData2.ToString(), 1, 1, 0.0f);


        //yield return InsertInspector("Foo", "Bar");

        print("Test finished");

        //yield return GetRequest("http://192.168.1.22/EyeTracking/SelectInspector.php?id=1");

        //yield return GetRequest("http://192.168.1.22/EyeTracking/SelectInspection.php?id=1");
        yield return SelectInspection("1");
    }


    static public IEnumerator SelectInspection(string id)
    {
        string uri = string.Format("{0}/EyeTracking/{1}.php?id={2}", website, nameof(SelectInspection), id);

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
            }
            else
            {
                string data = webRequest.downloadHandler.text;
                print(data);
                JsonInspection jsonInspection = JsonUtility.FromJson<JsonInspection>(data);
                InspectionData inspectionData = JsonUtility.FromJson<InspectionData>(jsonInspection.Data);
                print(inspectionData.GazePoints);
            }
        }
    }

    static public IEnumerator GetRequest(string uri, System.Action<string> callback)
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

    static public IEnumerator GetRequest(string uri)
    {
        yield return GetRequest(uri, (string str) => { });
    }



    #region InsertMethods

    static public IEnumerator InsertInspection(Inspection inspection)
    {
        yield return InsertInspection(inspection.name, inspection.date, inspection.inspectionData.ToString(), inspection.inspector_id, inspection.reference_inspection_id, inspection.score);
    }

    static public IEnumerator InsertInspection(string name, DateTime date, string data, int inspector_id, int? reference_inspection_id, float score)
    {

        //url for API call
        string uri = string.Format("{0}/EyeTracking/{1}.php", website, nameof(InsertInspection));

        yield return Insert(
            uri,
            nameof(name), name,
            nameof(date), date.ToString("yyyy-MM-dd"), // HH:mm:ss.fff"),
            nameof(data), data,
            nameof(inspector_id), inspector_id,
            nameof(reference_inspection_id), reference_inspection_id,
            nameof(score), score
        );
    }

    static public IEnumerator InsertInspector(string first_name, string last_name)
    {
        //url for API call
        string uri = string.Format("{0}/EyeTracking/{1}.php", website, nameof(InsertInspector));

        yield return Insert(
            uri,
            nameof(first_name), first_name,
            nameof(last_name), last_name
            );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="args">arg1Name, arg1Value</param>
    /// <returns></returns>
    static public IEnumerator Insert(string uri, params object[] args)
    {
        //Create form
        WWWForm form = new WWWForm();

        //Add args to form
        for (int i = 0; i < args.Length - 1; i+=2)
        {
            string name = args[i].ToString();
            //If arg value is unassigned, use NULL
            string value = args[i + 1] == null ? "NULL" : args[i + 1].ToString();
            
            form.AddField(name, value);

            //print(string.Format("{0} form.Addfield({1}, {2})", uri, name, value));
        }

        //Send request
        yield return InsertRequest(uri, form);
    }

    static public IEnumerator InsertRequest(string uri, WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
                print(www.error);
            else
                print(www.downloadHandler.text);
        }
    }

    #endregion
}
