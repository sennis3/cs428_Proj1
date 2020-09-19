using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class ClimateScript : MonoBehaviour
{

    public GameObject ClimateTextObject;
    string temperatureStr = "";
    string humidityStr = "";

    string resultStr = "Loading...";

    //public GameObject weatherTextObject;
       string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=3e73c3cad5c6f14cbad12da1ff9856a1&units=imperial";
       //string weatherData = "";
       //string temperature = "";
       //string humidity = "";
   
    void Start()
    {

    // wait a couple seconds to start and then refresh every 5 seconds

       InvokeRepeating("GetDataFromWeb", 2f, 5f);
       //GetDataFromWeb();
       //InvokeRepeating("UpdateClimate", 0f, 5f);
       //ClimateTextObject.GetComponent<TextMeshPro>().text = resultStr;
   }

   void GetDataFromWeb()
   {

       StartCoroutine(GetRequest(url));
       ClimateTextObject.GetComponent<TextMeshPro>().text = resultStr;
   }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                string weatherData = webRequest.downloadHandler.text;
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                Debug.Log(":\nReceived: " + weatherData);

                string[] strList = weatherData.Split('"', ':', ',', '}');
                //Debug.Log("strList: " + strList[0]);
                //strList = strList.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                /*for (int i = 0; i < strList.Length; i++){
                    if (strList[i].Equals("")){
                        strList[i].Remove();
                    }
                }*/

                int tempIndex = -1;
                for (int i = 0; i < strList.Length; i++){
                    Debug.Log(" strList: (" + i + ") " + strList[i]);
                    if (strList[i].Equals("temp")){
                        tempIndex = i+2;
                        temperatureStr = strList[tempIndex];
                        //resultStr = temperatureStr + "\n" + humidityStr;
                        //ClimateTextObject.GetComponent<TextMeshPro>().text = temperatureStr + "\n" + humidityStr;
                        break;
                    }
                }

                int humidityIndex = -1;
                for (int i = 0; i < strList.Length; i++){
                    Debug.Log(" strList: (" + i + ") " + strList[i]);
                    if (strList[i].Equals("humidity")){
                        humidityIndex = i+2;
                        humidityStr = strList[humidityIndex];
                        //resultStr = temperatureStr + "\n" + humidityStr;
                        //ClimateTextObject.GetComponent<TextMeshPro>().text = temperatureStr + "\n" + humidityStr;
                        break;
                    }
                }


                Debug.Log("tempIndex: " + tempIndex);
                Debug.Log("Temperature: " + strList[tempIndex]);
                Debug.Log("Temperature: " + temperatureStr);

                Debug.Log("humidityIndex: " + humidityIndex);
                Debug.Log("Humidity: " + strList[humidityIndex]);
                Debug.Log("Humidity: " + humidityStr);
                resultStr = temperatureStr + "°F\n" + humidityStr + "%";
                Debug.Log("Result before: " + resultStr);

                //ClimateTextObject.GetComponent<TextMeshPro>().text = resultStr;
                resultStr = temperatureStr + "°F\n" + humidityStr + "%";
                Debug.Log("Result after: " + resultStr);
            }
        }
    }
    void UpdateClimate(){
        ClimateTextObject.GetComponent<TextMeshPro>().text = resultStr;
        //Debug.Log("Temperature: " + temperatureStr);
        //Debug.Log("Humidity: " + humidityStr);
        //Debug.Log("Result: " + resultStr);
    }
}
