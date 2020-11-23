using UnityEngine;
using UnityEngine.UI;

public class RankInfo : MonoBehaviour
{
    public Image rankImage;
    public Text CurrentRank;
    public Text RankPlus;
    
    public void RankPointChange(int beforePoint, int nowPoint)
    {
        RankPlus.text = (nowPoint - beforePoint).ToString();
    }
}
