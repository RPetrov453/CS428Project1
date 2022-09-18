using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Text.RegularExpressions;

public class TsarevetsTimeScript : MonoBehaviour
{

    public GameObject TsarevetsTimeObject;
    // Start is called before the first frame update
        string url = "http://worldtimeapi.org/api/timezone/Europe/Sofia";
    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 2f, 10f);
    }

    void GetDataFromWeb()
    {

        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                //Debug.Log(":\nThe Date/Time: " + webRequest.downloadHandler.text.IndexOf("datetime"));
                int indexDT = webRequest.downloadHandler.text.IndexOf("datetime");
                int indexDW = webRequest.downloadHandler.text.IndexOf("day_of_week");
                string datetime = webRequest.downloadHandler.text[indexDT..indexDW];


                //Debug.Log(":\nCropped to Date/Time: " + datetime);
                int indexTime = datetime.IndexOf("T");
                //Debug.Log(":\nCropped Time: " + datetime[(indexTime+1)..(indexTime+6)]);
                string time = datetime[(indexTime + 1)..(indexTime + 6)];
                string Testhours = time[0..2];
                //Debug.Log(":\nTest Hours: " + Testhours);
                int hours = int.Parse(time[0..2]);

                //Debug.Log(":\nCropped Hours: " + hours);
                //Debug.Log(":\nCropped String: " + webRequest.downloadHandler.text[(indexDT+10)..]);

                //string date = Regex.Match(webRequest.downloadHandler.text, @"^\d{4}-\d{2}-\d{2}").Value;


                //Debug.Log(":\nReceived: " + webRequest.downloadHandler['datetime']);

                // this code will NOT fail gracefully, so make sure you have
                // your API key before running or you will get an error

                // grab the current temperature and simplify it if needed
                //int startTemp = webRequest.downloadHandler.text.IndexOf("temp", 0);
                //int endTemp = webRequest.downloadHandler.text.IndexOf(",", startTemp);
                //double tempF = float.Parse(webRequest.downloadHandler.text.Substring(startTemp + 6, (endTemp - startTemp - 6)));
                //int easyTempF = Mathf.RoundToInt((float)tempF);
                //Debug.Log(conditions);

                TsarevetsTimeObject.GetComponent<TextMeshPro>().text = "" + time;
            }
        }
    }
}
