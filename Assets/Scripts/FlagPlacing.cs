using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class FlagPlacing : MonoBehaviour
{
    [SerializeField] GameObject GreenFlag;
    [SerializeField] GameObject RedFlag;
    [SerializeField] GameObject BlueFlag;
    [SerializeField] GameObject YellowFlag;
    LineRenderer lr;

    List<GameObject> greenFlagList = new List<GameObject>();
    List<GameObject> redFlagList = new List<GameObject>();
    List<GameObject> blueFlagList = new List<GameObject>();
    List<GameObject> yellowFlagList = new List<GameObject>();

    int greenFlag = 0;
    int redFlag = 0;
    int blueFlag = 0;
    int yellowFlag = 0;

    Vector3 mousePos;
    Vector3 objectPos;

    string color = "green";
    public string getColor () { return color; }
    void Update()
    {
        Place();
        ColorChange();
    }

    void Place()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetGlobalPos();
        }
        if (Input.GetMouseButtonUp(0)){
            if (color.Equals("green"))
            {
                if(greenFlag == 2)
                {
                    float d1 = Vector3.Distance(objectPos, greenFlagList[0].transform.position);
                    float d2 = Vector3.Distance(objectPos, greenFlagList[1].transform.position);
                    if (d1 > d2)
                    {
                        GetGlobalPos();
                        greenFlagList[1].transform.position = objectPos;
                    }
                    else
                    {
                        GetGlobalPos();
                        greenFlagList[0].transform.position = objectPos;
                    }
                }
                else
                {
                    GetGlobalPos();
                    GameObject gFlag = Instantiate(GreenFlag, objectPos, Quaternion.identity);
                    greenFlagList.Add(gFlag);
                    greenFlag++;
                }
            }
            if (color.Equals("red"))
            {
                if (redFlag == 2)
                {
                    float d1 = Vector3.Distance(objectPos, redFlagList[0].transform.position);
                    float d2 = Vector3.Distance(objectPos, redFlagList[1].transform.position);
                    if (d1 > d2)
                    {
                        GetGlobalPos();
                        redFlagList[1].transform.position = objectPos;
                    }
                    else
                    {
                        GetGlobalPos();
                        redFlagList[0].transform.position = objectPos;
                    }
                }
                else
                {
                    GetGlobalPos();
                    GameObject rFlag = Instantiate(RedFlag, objectPos, Quaternion.identity);
                    redFlagList.Add(rFlag);
                    redFlag++;
                }
            }
            if (color.Equals("blue"))
            {
                if (blueFlag == 2)
                {
                    float d1 = Vector3.Distance(objectPos, blueFlagList[0].transform.position);
                    float d2 = Vector3.Distance(objectPos, blueFlagList[1].transform.position);
                    if (d1 > d2)
                    {
                        GetGlobalPos();
                        blueFlagList[1].transform.position = objectPos;
                    }
                    else
                    {
                        GetGlobalPos();
                        blueFlagList[0].transform.position = objectPos;
                    }
                }
                else
                {
                    GetGlobalPos();
                    GameObject bFlag = Instantiate(BlueFlag, objectPos, Quaternion.identity);
                    blueFlagList.Add(bFlag);
                    blueFlag++;
                }
            }
            if (color.Equals("yellow"))
            {
                if (yellowFlag == 2)
                {
                    float d1 = Vector3.Distance(objectPos, yellowFlagList[0].transform.position);
                    float d2 = Vector3.Distance(objectPos, yellowFlagList[1].transform.position);
                    if (d1 > d2)
                    {
                        GetGlobalPos();
                        yellowFlagList[1].transform.position = objectPos;
                    }
                    else
                    {
                        GetGlobalPos();
                        yellowFlagList[0].transform.position = objectPos;
                    }
                }
                else
                {
                    GetGlobalPos();
                    GameObject yFlag = Instantiate(YellowFlag, objectPos, Quaternion.identity);
                    yellowFlagList.Add(yFlag);
                    yellowFlag++;
                } 
            }
        }
    }

    // For debug
    void ColorChange() {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            color = "green";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            color = "red";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            color = "blue";
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            color = "yellow";
        }
    }

    void GetGlobalPos()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 2f;
        objectPos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    public List<GameObject> ColorCall(string color)
    {
        if (color.Equals("green"))
        {
            return greenFlagList;
        }else if (color.Equals("red"))
        {
            return redFlagList;
        }else if (color.Equals("blue"))
        {
            return blueFlagList;   
        }
        else
        {
            return yellowFlagList;
        }
    }
}
