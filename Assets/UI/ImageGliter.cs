using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageGliter : MonoBehaviour
{
    public Color set1, set2;
    public float interval = 1f;
    public Image[] toChange;
    
    void ColorChange(Image target, Color color1, Color color2)
    {
        target.DOColor(color1,interval).OnComplete(() => { ColorChange(target, color2, color1); });
    }

    void OnEnable()
    {
        for (int i = 0; i < toChange.Length; i++)
        {
            ColorChange(toChange[i], set1, set2);
        }
    }
}
