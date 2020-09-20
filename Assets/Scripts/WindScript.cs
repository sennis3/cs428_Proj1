using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System;

public class WindScript : MonoBehaviour
{

    public GameObject WindTextObject;
    string windSpeedStr = "";
    string windDirStr = "";
    string dirSymbol = "";


    public GameObject WindSock;

    string resultStr = "Loading...";
       string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=3e73c3cad5c6f14cbad12da1ff9856a1&units=imperial";
   
    void Start()
    {

    // wait a couple seconds to start and then refresh every 5 seconds

       InvokeRepeating("GetDataFromWeb", 2f, 5f);
       
   }

   void GetDataFromWeb()
   {

       StartCoroutine(GetRequest(url));
       WindTextObject.GetComponent<TextMeshPro>().text = resultStr;
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

                int speedIndex = -1;
                for (int i = 0; i < strList.Length; i++){
                    Debug.Log(" strList: (" + i + ") " + strList[i]);
                    if (strList[i].Equals("speed")){
                        speedIndex = i+2;
                        windSpeedStr = strList[speedIndex];
                        break;
                    }
                }

                int dirIndex = -1;
                for (int i = 0; i < strList.Length; i++){
                    Debug.Log(" strList: (" + i + ") " + strList[i]);
                    if (strList[i].Equals("deg")){
                        dirIndex = i+2;
                        windDirStr = strList[dirIndex];
                        break;
                    }
                }

                try
                {
                    WindSock.transform.rotation = Quaternion.Euler(0, 0, 0);
                    int windInt = Int32.Parse(windDirStr);
                    if (windInt > 315 || windInt <= 45){
                        dirSymbol = "N";
                        //WindSock.transform.rotation = Quaternion.Euler(0, -90, 0);
                        WindSock.transform.Rotate(0,0,0);
                    }
                    else if (windInt > 45 && windInt <= 135){
                        dirSymbol = "E";
                        //WindSock.transform.rotation = Quaternion.Euler(0, 0, 0);
                        WindSock.transform.Rotate(0,90,0);
                    }
                    else if (windInt > 135 && windInt <= 225){
                        dirSymbol = "S";
                        //WindSock.transform.rotation = Quaternion.Euler(0, 90, 0);
                        WindSock.transform.Rotate(0,180,0);
                    }
                    else if (windInt > 225 && windInt <= 315){
                        dirSymbol = "W";
                        //WindSock.transform.rotation = Quaternion.Euler(0, 180, 0);
                        WindSock.transform.Rotate(0,-90,0);
                    }
                }
                catch (FormatException)
                {
                    Debug.Log("Error: Unable to parse windDirStr");
                }

                Debug.Log("speedIndex: " + speedIndex);
                Debug.Log("WindSpeed: " + strList[speedIndex]);
                Debug.Log("WindSpeed: " + windSpeedStr);

                Debug.Log("dirIndex: " + dirIndex);
                Debug.Log("WindDirection: " + strList[dirIndex]);
                Debug.Log("WindDirection: " + windDirStr);
                resultStr = windSpeedStr + "mph\n" + windDirStr + "°";
                Debug.Log("Result before: " + resultStr);

                //WindTextObject.GetComponent<TextMeshPro>().text = resultStr;
                resultStr = windSpeedStr + " mph\n" + windDirStr + "° " + dirSymbol;
                Debug.Log("Result after: " + resultStr);
            }
        }
    }
    void UpdateWind(){
        WindTextObject.GetComponent<TextMeshPro>().text = resultStr;
        //Debug.Log("WindSpeed: " + windSpeedStr);
        //Debug.Log("WindDirection: " + windDirStr);
        //Debug.Log("Result: " + resultStr);
    }
}
