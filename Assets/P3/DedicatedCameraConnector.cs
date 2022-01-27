using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using dataAccess;

public class DedicatedCameraConnector : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Camera camera;
    [SerializeField] private RawImage view;
    [SerializeField] private int resolution = 256;
    [SerializeField] private float extraFrameSpace = 0;
    [SerializeField] private Vector2 cameraPosOffSet;
    [SerializeField] private float UpDownRotateRangeMin = -10;
    [SerializeField] private float UpDownRotateRangeMax = 45;
    [SerializeField] private float rotateSpeed = 90;
    
    private readonly Bounds tempBoundary = new Bounds();
    private RenderTexture renderTexture;

    public void Clear()
    {
        DestroyImmediate(camera.gameObject);
        if (target != null)
            target.gameObject.SetActive(false);
        DestroyImmediate(this.gameObject);
    }
    
    public IEnumerator Show_instanceID(string instanceID)
    {
        UnitInfo info = MyMonsters.Get(instanceID);
        var p = Show(info?.r_id);
        yield return p;
        yield return p.Current;
    }

    IEnumerator Show(string recordID)
    {
        var p = GeneralModelPool.GetModel(recordID, true);
        yield return p;
        if (p.Current == null)
        {
            Debug.Log("模型错误");
            yield break;
        }
        var dataCenter = (Data_Center)p.Current;
        Initialize(dataCenter.WholeT);
        ItemDetailStartDirection(true);
        DOTween.To(() => ViewColorAlpha, (x) => ViewColorAlpha = x, 1, 0.2f);
        EnableRotateDirection(true, true);
        yield return dataCenter.WholeT;
    }
    
    void Initialize(Transform focus, Transform camerasHolder = null)
    {
        target = focus;
        renderTexture = new RenderTexture(resolution, resolution, 16);
        renderTexture.Create();
        camera.targetTexture = renderTexture;
        view.texture = renderTexture;
        parentNodeRenderer = target.GetComponent<Renderer>();
        renderers = target.GetComponentsInChildren<Renderer>().Where(
            x =>
            {
                var matName = x.sharedMaterial.name;
                return (x is MeshRenderer || x is SkinnedMeshRenderer)
                       && x.renderingLayerMask != 0
                       && !matName.StartsWith("Mat_Dummy")
                       && !matName.StartsWith("Mat_Ground")
                       && !matName.StartsWith("Mat_Shadow");
            }).ToArray();
        
        foreach (var mesh in renderers)
        {
            if (mesh is SkinnedMeshRenderer skinnedMesh)
            {
                skinnedMesh.updateWhenOffscreen = true; // to fit camera bound
            }
        }
        
        camera.transform.SetParent(camerasHolder);

        wid = view.GetComponent<RectTransform>().rect.width;
        hei = view.GetComponent<RectTransform>().rect.height;
        
        camera.gameObject.SetActive(true);
    }
    
    private Renderer parentNodeRenderer;
    private Bounds targetBounds;
    private Renderer[] renderers;

    private float _basicOrthographicSize;
    void CameraPositionCal()
    {
        // 合成Bounds計算
        targetBounds = tempBoundary;
        if (parentNodeRenderer != null)
        {
            targetBounds = parentNodeRenderer.bounds;
        }

        foreach (Renderer render in renderers)
        {
            if (targetBounds == tempBoundary)
            {
                targetBounds = render.bounds;
            }
            else
            {
                targetBounds.Encapsulate(render.bounds);
            }
        }

        camera.transform.position = targetBounds.center + Vector3.forward * targetBounds.extents.z;
        camera.transform.rotation = Quaternion.LookRotation(targetBounds.center - camera.transform.position, Vector3.up);
        _basicOrthographicSize = Mathf.Max(targetBounds.extents.x, targetBounds.extents.y);
        camera.orthographicSize = _basicOrthographicSize + extraFrameSpace;
        camera.transform.position += (camera.transform.right * cameraPosOffSet.x + Vector3.up * cameraPosOffSet.y);
    }
    
    public float ViewColorAlpha
    {
        get => view.color.a;
        set => view.color = new Color(view.color.r, view.color.g, view.color.b, value);
    }
    
    //回転用
    Vector2 sPos; //タッチした座標
    float wid = 100, hei = 100; //スクリーンサイズ
    float left_right, left_right_old,　up_down, up_down_old, _z; //変数
    bool canLeftRight = true, canUpDown = true;

    public (float, float, float, float, Vector2) GetCurrentParams()
    {
        return (left_right, up_down, _z, extraFrameSpace, this.cameraPosOffSet);
    }
    
    public void EnableRotateDirection(bool x, bool y)
    {
        canLeftRight = x;
        canUpDown = y;
    }
    
    public void SetZoom(float extraFrameSpace)
    {
        this.extraFrameSpace = extraFrameSpace;
        CameraPositionCal();
    }
    
    public void SetCameraOffSet(Vector2 offSet)
    {
        this.cameraPosOffSet = offSet;
        CameraPositionCal();
    }

    public void SetUpDownRange(float min, float max)
    {
        this.UpDownRotateRangeMin = min;
        this.UpDownRotateRangeMax = max;
    }
    
    public void RotateTarget(float left_right, float up_down, float Z = 0)
    {
        if (target == null) return;
        this.left_right = left_right;
        this.up_down = up_down;
        this._z = Z;
        target.localRotation = Quaternion.Euler(up_down, 0, Z);
        target.RotateAround(target.position, target.up, left_right);
        CameraPositionCal();
    }
    
    public void OnPointerDown()
    {
        left_right_old = left_right;
        up_down_old = up_down;
        sPos = Input.mousePosition;
    }
    
    public void OnHold()
    {
        //回転
        left_right = left_right_old + (canLeftRight ? (sPos.x - Input.mousePosition.x) * rotateSpeed/ wid : 0); //横移動量(-1<tx<1)
        up_down = up_down_old + (canUpDown ? (sPos.y - Input.mousePosition.y) * rotateSpeed/ hei : 0); //縦移動量(-1<ty<1);
        up_down = Mathf.Clamp(up_down, UpDownRotateRangeMin, UpDownRotateRangeMax);
        RotateTarget(left_right, up_down);
    }
    
    void ItemDetailStartDirection(float x, float y)
    {
        left_right = x;
        up_down = y;
        up_down = Mathf.Clamp(up_down, UpDownRotateRangeMin, UpDownRotateRangeMax);
        RotateTarget(left_right, up_down);
    }
    
    public void ItemDetailStartDirection(bool front)
    {
        left_right = front ? 0 : 180;
        OnPointerDown();
        OnHold();
    }
}
