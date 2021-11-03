using System.Collections.Generic;
using UnityEngine;

public partial class LoadingCanvas : MonoBehaviour {

    #region 高亮显示
    /// <summary>
    // 注：参数列表内的第一个(Ts[0])元素是会高亮显示，而其他transform对应的区域并不会高亮显示，但也会缕空
    // 我们曾经尝试让复数个对象区域都高亮度显示，但失败了。
    /// </summary>
    public void HigtLightRect(List<Transform> Ts)
    {
        hollowOutMask.gameObject.SetActive(true);
        if (bigCurtain != null)
        {
            bigCurtain.color = Color.clear;
        }
        //Loading_Canvas.sortingOrder = 1;
        hollowOutMask.raycastTarget = true;
        List<RectTransform> rectTransforms = new List<RectTransform>();
        for (int i = 0; i < Ts.Count; i++)
        {
            rectTransforms.Add(Ts[i].GetComponent<RectTransform>());
        }
        hollowOutMask.SetTarget(rectTransforms);
        hollowOutMask.color = new Color(0, 0, 0, 0.6f);
    }
    
    public void HigtLightRect(Transform _Transform)
    {
        hollowOutMask.gameObject.SetActive(true);
        if (bigCurtain != null)
            bigCurtain.color = Color.clear;
        //Loading_Canvas.sortingOrder = 1;
        hollowOutMask.raycastTarget = true;
        hollowOutMask.SetTarget(new List<RectTransform>{ _Transform.GetComponent<RectTransform>() });
        hollowOutMask.color = new Color(0, 0, 0, 0.6f);
    }
    
    public void ClearHigtLight()
    {
        hollowOutMask.SetTarget(null);
        hollowOutMask.color = Color.clear;
        hollowOutMask.gameObject.SetActive(false);
    }
    #endregion
}
