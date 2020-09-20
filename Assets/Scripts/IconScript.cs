using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour
{
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
    int iconIndex = 0;

    // Start is called before the first frame update
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

        //ClearSkyIcon.SetActive(true);
        //FewCloudsIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //iconList[iconIndex].SetActive(false);
            setAllInactive();
            iconIndex++;
            if (iconIndex < 0)
                iconIndex = iconList.Count-1;
            else if (iconIndex >= iconList.Count)
                iconIndex = 0;
            iconList[iconIndex].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //iconList[iconIndex].SetActive(false);
            setAllInactive();
            iconIndex--;
            if (iconIndex < 0)
                iconIndex = iconList.Count-1;
            else if (iconIndex >= iconList.Count)
                iconIndex = 0;
            iconList[iconIndex].SetActive(true);
        }
        

        //iconList[iconIndex].SetActive(true);
    }

    void setAllInactive(){
        for (int i = 0; i < iconList.Count; i++){
            iconList[i].SetActive(false);
        }
    }
}
