using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillStoneDetail : MonoBehaviour
{
    [Space(2)]
    [Header("技能信息")]
    public Text keyname;
    public Text Showname;
    public Text type;
    
    public GameObject Ex1Icon,Ex2Icon,Ex3Icon;
    
    public void showSkillStoneExType(int eX)
    {
        switch (eX)
        {
            case 0:
                Ex1Icon.SetActive(false);
                Ex2Icon.SetActive(false);
                Ex3Icon.SetActive(false);
            break;
            case 1:
                Ex1Icon.SetActive(true);
                Ex2Icon.SetActive(false);
                Ex3Icon.SetActive(false);
            break;
            case 2:
                Ex1Icon.SetActive(true);
                Ex2Icon.SetActive(true);
                Ex3Icon.SetActive(false);
            break;
            case 3:
                Ex1Icon.SetActive(true);
                Ex2Icon.SetActive(true);
                Ex3Icon.SetActive(true);
            break;
            case -1:
                Ex1Icon.SetActive(false);
                Ex2Icon.SetActive(false);
                Ex3Icon.SetActive(false);
                break;
        }
    }
}
