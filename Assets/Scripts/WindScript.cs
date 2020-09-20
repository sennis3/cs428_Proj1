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
    int speedLevelIndex = 0;


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
                double speedDouble = 0;

                try
                {
                    //WindSock.transform.rotation = Quaternion.Euler(0, 0, 0);
                    int windInt = Int32.Parse(windDirStr);
                    speedDouble = Convert.ToDouble(windSpeedStr);

                    if (dirSymbol.Equals("N")){
                        WindSock.transform.Rotate(0,0,0);
                    }
                    if (dirSymbol.Equals("E")){
                        WindSock.transform.Rotate(0,-90,0);
                    }
                    if (dirSymbol.Equals("S")){
                        WindSock.transform.Rotate(0,-180,0);
                    }
                    if (dirSymbol.Equals("W")){
                        WindSock.transform.Rotate(0,90,0);
                    }

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

                    float val;
                    for (int i = 1; i <= 6; i++){
                        if (speedLevelIndex == i){
                            val = (float) -0.01*i;
                            WindSock.transform.Translate(0,val,0);
                        }
                    }

                    val = (float) 0.01;
                    if (speedDouble > 0){
                        WindSock.transform.Translate(0,val,0);
                        speedLevelIndex = 1;
                    }
                    if (speedDouble > 3){
                        WindSock.transform.Translate(0,val,0);
                        speedLevelIndex = 2;
                    }
                    if (speedDouble > 6){
                        WindSock.transform.Translate(0,val,0);
                        speedLevelIndex = 3;
                    }
                    if (speedDouble > 9){
                        WindSock.transform.Translate(0,val,0);
                        speedLevelIndex = 4;
                    }
                    if (speedDouble > 12){
                        WindSock.transform.Translate(0,val,0);
                        speedLevelIndex = 5;
                    }
                    if (speedDouble > 15){
                        WindSock.transform.Translate(0,val,0);
                        speedLevelIndex = 6;
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
                //resultStr = windSpeedStr + "mph\n" + windDirStr + "°";
                Debug.Log("Result before: " + resultStr);

                //WindTextObject.GetComponent<TextMeshPro>().text = resultStr;
                //resultStr = windSpeedStr + " mph\n" + windDirStr + "° " + dirSymbol;
                resultStr = windSpeedStr + " mph " + dirSymbol + "\nN is Forward";
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
