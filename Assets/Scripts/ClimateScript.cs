using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System;

public class ClimateScript : MonoBehaviour
{

    public GameObject ClimateTextObject;
    string temperatureStr = "";
    string humidityStr = "";

    public GameObject firstLevel;
    public GameObject secondLevel;
    public GameObject thirdLevel;
    public GameObject fourthLevel;
    public GameObject fifthLevel;

    public GameObject humidFirstLevel;
    public GameObject humidSecondLevel;
    public GameObject humidThirdLevel;
    public GameObject humidFourthLevel;
    public GameObject humidFifthLevel;

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
                resultStr = temperatureStr + "°F        " + humidityStr + "%";
                Debug.Log("Result before: " + resultStr);

                //ClimateTextObject.GetComponent<TextMeshPro>().text = resultStr;
                resultStr = temperatureStr + "°F        " + humidityStr + "%";
                Debug.Log("Result after: " + resultStr);


                firstLevel.SetActive(false);
                secondLevel.SetActive(false);
                thirdLevel.SetActive(false);
                fourthLevel.SetActive(false);
                fifthLevel.SetActive(false);

                humidFirstLevel.SetActive(false);
                humidSecondLevel.SetActive(false);
                humidThirdLevel.SetActive(false);
                humidFourthLevel.SetActive(false);
                humidFifthLevel.SetActive(false);

                double tempDouble = 0;
                double humidDouble = 0;
                try {
                    tempDouble = Convert.ToDouble(temperatureStr);
                    humidDouble = Convert.ToDouble(humidityStr);
                }
                catch (FormatException)
                {
                    Debug.Log("Error: Unable to parse temperatureStr or humidityStr");
                }

                Debug.Log("TempDouble: " + tempDouble);
                if (tempDouble > 0){
                    firstLevel.SetActive(true);
                }
                if (tempDouble > 25){
                    secondLevel.SetActive(true);
                }
                if (tempDouble > 50){
                    thirdLevel.SetActive(true);
                }
                if (tempDouble > 75){
                    fourthLevel.SetActive(true);
                }
                if (tempDouble > 100){
                    fifthLevel.SetActive(true);
                }

                Debug.Log("TempDouble: " + tempDouble);
                if (tempDouble > 0){
                    humidFirstLevel.SetActive(true);
                }
                if (tempDouble > 20){
                    humidSecondLevel.SetActive(true);
                }
                if (tempDouble > 40){
                    humidThirdLevel.SetActive(true);
                }
                if (tempDouble > 60){
                    humidFourthLevel.SetActive(true);
                }
                if (tempDouble > 80){
                    humidFifthLevel.SetActive(true);
                }
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
