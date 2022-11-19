using System.Collections.Generic;
using UnityEngine;

public class BackGroundPS : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> BCPs;
    public static BackGroundPS target;
    
    int playingNo = 0;
    
    void Awake()
    {
        target = this;
    }
    
    public void Next()
    {
        playingNo++;
        if (playingNo == BCPs.Count)
        {
            playingNo = 0;
        }
        SwitchBG(playingNo);
    }
    
    public void Off()
    {
        SwitchBG(-1);
    }
    
    public void ChangeBGByElement(Element element)
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
        SwitchBG(18);
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
        for (var i = 0; i < BCPs.Count; i++)
        {
            if (i == index)
            {
                if (BCPs[i].gameObject.activeSelf) continue;
                BCPs[i].gameObject.SetActive(true);
                BCPs[i].Play(true);
            }else{
                BCPs[i].gameObject.SetActive(false);
            }
        }
    }
}
