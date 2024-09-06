using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WebData : MonoBehaviour
{
    private int nextSceneToLoad;


    public int numPoints = 0;

    void Start()
    {
        //StartCoroutine(GetRequest("http://127.0.0.1/EyeTracking/GetData.php"));
        //StartCoroutine(GetRequest("http://127.0.0.1/EyeTracking/GetUsers.php"));
        //StartCoroutine(Login("testuser", "123456", "http://127.0.0.1/EyeTracking/Login.php"));
        //StartCoroutine(RegisterUser("testuser3", "123456", 1,"http://127.0.0.1/EyeTracking/RegisterUser.php"));
    }

    void Update()
    {
        //StartCoroutine(GetRequest("http://127.0.0.1/EyeTracking/GetData.php"));
        //StartCoroutine(GetRequest("http://127.0.0.1/EyeTracking/GetUsers.php"));
    }

    public IEnumerator GetRequest(string uri)
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
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator Login(string usernamecheck, string passwordcheck, string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", usernamecheck);
        form.AddField("loginPass", passwordcheck);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextSceneToLoad);
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password, int ProblemNum, string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginProbNum", ProblemNum);

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextSceneToLoad);
            }
        }
    }

    public IEnumerator InsertData(float xt, float yt, float zt, string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("xt", xt.ToString());
        form.AddField("yt", yt.ToString());
        form.AddField("zt", zt.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                numPoints++;
            }
        }
    }

    public IEnumerator DeleteData(float xt, float yt, float zt, string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("xt", xt.ToString());
        form.AddField("yt", yt.ToString());
        form.AddField("zt", zt.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
