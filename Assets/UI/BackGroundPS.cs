using System.Collections.Generic;
using UnityEngine;

public class BackGroundPS : MonoBehaviour
{
    public Camera mainC;
    public List<ParticleSystem> BCPs;
    public static BackGroundPS target;

    public int playingNum = 0;

    void Awake()
    {
        target = this;
    }
    
    public void Next()
    {
        playingNum++;
        if (playingNum == BCPs.Count)
        {
            playingNum = 0;
        }
        SwitchBG(playingNum);
    }
    
    public void Off()
    {
        SwitchBG(-1);
    }
    
    public void ChangeBGByZokusei(Element element)
    {
        switch (element)
        {
            case Element.darkMagic:
            Dark();
            break;
            case Element.blueMagic:
            Blue();
            break;
            case Element.greenMagic:
            Green();
            break;
            case Element.lightMagic:
            Light();
            break;
            case Element.redMagic:
            Red();
            break;
            default:
            Default();
            break;
        }
    }
    
    void Default()
    {
        SwitchBG(14);
    }
    
    void Red()
    {
        SwitchBG(6);
    }
    
    void Blue()
    {
        SwitchBG(8);
    }
    
    void Green()
    {
        SwitchBG(7);
    }
    
    void Light()
    {
        SwitchBG(12);
    }
    
    void Dark()
    {
        SwitchBG(5);
    }
    
    void SwitchBG(int index)
    {
        for (int i = 0; i < BCPs.Count; i++)
        {
            if (i == index)
            {
                if (!BCPs[i].gameObject.activeSelf)
                {
                    BCPs[i].gameObject.SetActive(true);
                    BCPs[i].Play(true);
                }
            }else{
                BCPs[i].gameObject.SetActive(false);
            }
        }
    }
}
