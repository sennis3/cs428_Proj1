using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MainConditionScript : MonoBehaviour
{

    public GameObject MainCondTextObject;

    public GameObject ClearSkyIcon;
    public GameObject FewCloudsIcon;
    public GameObject ScatteredCloudsIcon;
    public GameObject BrokenCloudsIcon;
    public GameObject ShowerRainIcon;
    public GameObject RainIcon;
    public GameObject ThunderstormIcon;
    public GameObject SnowIcon;
    public GameObject MistIcon;
    List<GameObject> iconList = new List<GameObject>();

    string mainCondStr = "Loading...";
       string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=3e73c3cad5c6f14cbad12da1ff9856a1&units=imperial";
   
    void Start()
    {
        iconList.Add(ClearSkyIcon);
        iconList.Add(FewCloudsIcon);
        iconList.Add(ScatteredCloudsIcon);
        iconList.Add(BrokenCloudsIcon);
        iconList.Add(ShowerRainIcon);
        iconList.Add(RainIcon);
        iconList.Add(ThunderstormIcon);
        iconList.Add(SnowIcon);
        iconList.Add(MistIcon);

    // wait a couple seconds to start and then refresh every 5 seconds

       InvokeRepeating("GetDataFromWeb", 2f, 5f);
       
   }

   void GetDataFromWeb()
   {

       StartCoroutine(GetRequest(url));
       UpdateWind();
       //MainCondTextObject.GetComponent<TextMeshPro>().text = mainCondStr;
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

                int mainCondIndex = -1;
                for (int i = 0; i < strList.Length; i++){
                    Debug.Log(" strList: (" + i + ") " + strList[i]);
                    if (strList[i].Equals("description")){
                        mainCondIndex = i+3;
                        mainCondStr = strList[mainCondIndex];
                        break;
                    }
                }

                /*int dirIndex = -1;
                for (int i = 0; i < strList.Length; i++){
                    Debug.Log(" strList: (" + i + ") " + strList[i]);
                    if (strList[i].Equals("deg")){
                        dirIndex = i+2;
                        windDirStr = strList[dirIndex];
                        break;
                    }
                }*/


                /*Debug.Log("speedIndex: " + speedIndex);
                Debug.Log("WindSpeed: " + strList[speedIndex]);
                Debug.Log("WindSpeed: " + windSpeedStr);

                Debug.Log("dirIndex: " + dirIndex);
                Debug.Log("WindDirection: " + strList[dirIndex]);
                Debug.Log("WindDirection: " + windDirStr);
                mainCondStr = windSpeedStr + "mph\n" + windDirStr + "°";*/
                Debug.Log("Result before: " + mainCondStr);

                //WindTextObject.GetComponent<TextMeshPro>().text = resultStr;
                //mainCondStr = windSpeedStr + " mph\n" + windDirStr + "°";
                Debug.Log("Result after: " + mainCondStr);
            }
        }
    }
    void UpdateWind(){
        MainCondTextObject.GetComponent<TextMeshPro>().text = mainCondStr;
        //Debug.Log("Result: " + mainCondStr);

        for (int i = 0; i < iconList.Count; i++){
            iconList[i].SetActive(false);
        }

        if (mainCondStr.Equals("clear sky")){
            ClearSkyIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("few clouds")){
            FewCloudsIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("scattered clouds")){
            ScatteredCloudsIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("broken clouds")){
            BrokenCloudsIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("shower rain")){
            ShowerRainIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("rain")){
            RainIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("thunderstorm")){
            ThunderstormIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("snow")){
            SnowIcon.SetActive(true);
        }
        else if (mainCondStr.Equals("mist")){
            MistIcon.SetActive(true);
        }
    }
}
