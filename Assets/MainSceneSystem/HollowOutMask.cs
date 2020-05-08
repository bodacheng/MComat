using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 实现镂空效果的Mask组件，该模块必须放在Canvas上，
/// 并且其作用方式是将这个Canvas的部分区域给剪掉并让其他区域变色来实现“缕空”目的
/// 这个剪掉并不是说将对应Canvas区域的图像也给扣去，而是让对象区域失去点击功能，并且辅之以颜色区别
/// 可推知，如果希望靠该缕空功能来让部分窗口高亮显示，
/// 对象窗口如果在本模块所在Canvas内部的话，对应区域会失去点击功能，内部的按钮也点不了了。
/// 如果希望实现高亮显示一些带按钮的窗口高亮显示，必须专门建立一个Mask Canvas来装载本组件，
/// 并且在内部安排与欲高亮显示区域一样大小的RectTransform来辅助功能完成。
/// 另外，Canvas的sort order在本功能里有着重要的影响。
/// Mask Canvas的sort order值必须最高，欲高亮显示窗口所在Canvas为其次，高亮显示期间欲屏蔽区域所在的Canvas为最低
/// </summary>

public class HollowOutMask : MaskableGraphic, ICanvasRaycastFilter
{
    [SerializeField]
    List<RectTransform> _target;
    
    Vector3 _targetMin = Vector3.zero, _targetMax = Vector3.zero;
    bool _canRefresh = true;
    Transform _cacheTrans = null;
    
    /// <summary>
    /// 设置镂空的目标
    /// </summary>
    public void SetTarget(List<RectTransform> target)
    {
        _canRefresh = true;
        _target = target;
        _RefreshView();
    }
    
    void _SetTarget(Vector3 min, Vector3 max)
    {
        if (min == _targetMin && max == _targetMax)
            return;
        _targetMin = min;
        _targetMax = max;
        SetAllDirty();
    }

    void _RefreshView()
    {
        if (!_canRefresh) return;
        _canRefresh = false;
        
        if (null == _target)
        {
            _SetTarget(Vector3.zero,Vector3.zero);
            SetAllDirty();
        }
        else
        {
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(_cacheTrans, _target[0]);
            _SetTarget(bounds.min, bounds.max);
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (_targetMin == Vector3.zero && _targetMax == Vector3.zero)
        {
            base.OnPopulateMesh(vh);
            return;
        }
        vh.Clear();
        
        // 填充顶点
        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;
        
        Vector2 selfPiovt = rectTransform.pivot;
        Rect selfRect = rectTransform.rect;
        float outerLx = -selfPiovt.x*selfRect.width;
        float outerBy = -selfPiovt.y*selfRect.height;
        float outerRx = (1 - selfPiovt.x)*selfRect.width;
        float outerTy = (1 - selfPiovt.y)*selfRect.height;
        // 0 - Outer:LT
        vert.position = new Vector3(outerLx, outerTy);
        vh.AddVert(vert);
        // 1 - Outer:RT
        vert.position = new Vector3(outerRx, outerTy);
        vh.AddVert(vert);
        // 2 - Outer:RB
        vert.position = new Vector3(outerRx, outerBy);
        vh.AddVert(vert);
        // 3 - Outer:LB
        vert.position = new Vector3(outerLx, outerBy);
        vh.AddVert(vert);
        
        // 4 - Inner:LT
        vert.position = new Vector3(_targetMin.x, _targetMax.y);
        vh.AddVert(vert);
        // 5 - Inner:RT
        vert.position = new Vector3(_targetMax.x, _targetMax.y);
        vh.AddVert(vert);
        // 6 - Inner:RB
        vert.position = new Vector3(_targetMax.x, _targetMin.y);
        vh.AddVert(vert);
        // 7 - Inner:LB
        vert.position = new Vector3(_targetMin.x, _targetMin.y);
        vh.AddVert(vert);
        
        // 设定三角形
        vh.AddTriangle(4, 0, 1);
        vh.AddTriangle(4, 1, 5);
        vh.AddTriangle(5, 1, 2);
        vh.AddTriangle(5, 2, 6);
        vh.AddTriangle(6, 2, 3);
        vh.AddTriangle(6, 3, 7);
        vh.AddTriangle(7, 3, 0);
        vh.AddTriangle(7, 0, 4);
    }

    // 将目标对象范围内的事件镂空（使其穿过）
    bool ICanvasRaycastFilter.IsRaycastLocationValid(Vector2 screenPos, Camera eventCamera)
    {
        if (null == _target) return true;        
        bool _R = true;
        // 以下完全是蒙的
        for (int i = 0; i < _target.Count; i++)
        {
            _R &= !RectTransformUtility.RectangleContainsScreenPoint(_target[i], screenPos, eventCamera);
        }
        return _R;
    }

    protected override void Awake()
    {
        base.Awake();
        _cacheTrans = GetComponent<RectTransform>();
    }

#if UNITY_EDITOR
    void Update()
    {
        _canRefresh = true;
        _RefreshView();
    }
#endif
}
