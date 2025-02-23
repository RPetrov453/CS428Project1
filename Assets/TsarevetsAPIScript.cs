using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class TsarevetsAPIScript : MonoBehaviour
{
    public GameObject TsarevetsWeatherTextObject;
    // add your personal API key after APPID= and before &units=
    // 43.0839� N, 25.6525� E
        string url = "http://api.openweathermap.org/data/2.5/weather?lat=43.08&lon=25.65&APPID=91ee6dd706d3595c8e1180e6e9e086fb&units=metric";


    void Start()
    {

        // wait a couple seconds to start and then refresh every 900 seconds

        InvokeRepeating("GetDataFromWeb", 2f, 900f);
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
                //Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                // this code will NOT fail gracefully, so make sure you have
                // your API key before running or you will get an error

                // grab the current temperature and simplify it if needed
                int startTemp = webRequest.downloadHandler.text.IndexOf("temp", 0);
                int endTemp = webRequest.downloadHandler.text.IndexOf(",", startTemp);
                double tempF = float.Parse(webRequest.downloadHandler.text.Substring(startTemp + 6, (endTemp - startTemp - 6)));
                int easyTempF = Mathf.RoundToInt((float)tempF);
                //Debug.Log ("integer temperature is " + easyTempF.ToString());
                int startConditions = webRequest.downloadHandler.text.IndexOf("main", 0);
                int endConditions = webRequest.downloadHandler.text.IndexOf(",", startConditions);
                string conditions = webRequest.downloadHandler.text.Substring(startConditions + 7, (endConditions - startConditions - 8));
                //Debug.Log(conditions);

                TsarevetsWeatherTextObject.GetComponent<TextMeshPro>().text = "" + easyTempF.ToString() + "�C\n" + conditions;
            }
        }
    }
}