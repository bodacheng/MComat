using UnityEngine;
using UnityEngine.UI;

public class ArenaRankIcon : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] rankIcons; // 暂定13个等级 

    public void Set(int point)
    {
        var rank = point / 100;
        rank = Mathf.Clamp(rank, 0, rankIcons.Length - 1);
        _image.sprite = rankIcons[rank];
    }
}