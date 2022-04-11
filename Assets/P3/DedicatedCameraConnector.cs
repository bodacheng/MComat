using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using dataAccess;

namespace Cocone.ProjectP3
{
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
        [SerializeField] private float extraZDis = 0;
        [SerializeField] private float extraZCameraDepth = 5f;
        [SerializeField] private RectTransform View3DSizeRef;
        private readonly Bounds tempBoundary = new Bounds();
        private RenderTexture renderTexture;
        
        public Camera GetCamera()
        {
            return camera;
        }
        
        private bool fixMode;
        private float sizeDiffRate = 1; // mesh表示範囲と実際のview範囲の広さ比例
        
        public void Clear()
        {
            DestroyImmediate(camera.gameObject);
            if (target != null)
                DestroyImmediate(target.gameObject);
            DestroyImmediate(this.gameObject);
        }
        
        GameObject model;
        public IEnumerator ShowMyModel(string instanceID)
        {
            var info = MyMonsters.Get(instanceID);
            var p = ShowModel(info?.r_id);
            yield return p;
            yield return p.Current;
        }
    
        public IEnumerator ShowModel(string recordID) 
        {
            if (model != null)
            {
                DestroyImmediate(model);
                model = null;
            }
            if (recordID == null)
            {
                yield break;
            }
        
            var p = GeneralModelPool.GetModel(recordID);
            yield return p;
            if (p.Current == null)
            {
                Debug.Log("模型错误");
                yield break;
            }
        
            var dataCenter = (Data_Center)p.Current;
            // 这个短暂变色是为了掩盖一些模型刚加载瞬间有些渲染没到位的尴尬。比如裙子摇晃 
            // 但是这个不知道为什么报warning
            // dataCenter._ShaderManager.FlatColorForAShortTime(10f, 0, 0.5f, Color.black); 
        
            model = dataCenter.WholeT.gameObject;
            model.SetActive(true);
            model.transform.parent = transform;
            
            Initialize(2048, true,model.transform, transform);
            ItemDetailStartDirection(0,0,0);
            yield return model;
        }

        public void Initialize(int resolution, bool fixMode, Transform focus, Transform camerasHolder = null)
        {
            this.resolution = resolution;
            Initialize(fixMode, focus, camerasHolder);
        }
        
        public void Initialize(bool fixMode, Transform focus, Transform camerasHolder = null)
        {
            this.fixMode = fixMode;
            target = focus;
            SetTexture();
            parentNodeRenderer = target.GetComponent<Renderer>();
            renderers = target.GetComponentsInChildren<Renderer>().ToArray();
            
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
            
            if (!this.fixMode)
                _basicOrthographicSize = CalMaxOrthographicSize();
            
            if (View3DSizeRef != null)
            {
                var rect = transform.GetComponent<RectTransform>();
                sizeDiffRate = (float)((decimal)View3DSizeRef.rect.width / (decimal)rect.rect.width);
            }
            else
            {
                sizeDiffRate = 1;
            }
        }

        public int GetResolution()
        {
            return resolution;
        }
        
        public void SetResolution(int value)
        {
            this.resolution = value;
        }
        
        public void SetTexture()
        {
            renderTexture = new RenderTexture(resolution, resolution, 16);
            renderTexture.Create();
            camera.targetTexture = renderTexture;
            view.texture = renderTexture;
        }
        
        private Renderer parentNodeRenderer;
        private Bounds targetBounds;
        private Renderer[] renderers;

        private float _basicOrthographicSize;
        public void CameraPositionCal()
        {
            // 合成Bounds計算
            targetBounds = tempBoundary;
            if (parentNodeRenderer != null)
            {
                targetBounds = parentNodeRenderer.bounds;
            }

            foreach (var render in renderers)
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

            camera.transform.position = targetBounds.center + Vector3.forward * (targetBounds.extents.z + extraZDis);
            camera.transform.rotation = Quaternion.LookRotation(targetBounds.center - camera.transform.position, Vector3.up);
            if (this.fixMode)
            {
                _basicOrthographicSize = Mathf.Max(targetBounds.extents.x, targetBounds.extents.y);
            }
            
            camera.orthographicSize = _basicOrthographicSize * sizeDiffRate + extraFrameSpace;
            camera.farClipPlane = targetBounds.extents.z * 2 + extraZDis + extraZCameraDepth;
            camera.transform.position += (camera.transform.right * cameraPosOffSet.x + Vector3.up * cameraPosOffSet.y);
        }
        
        public float ViewColorAlpha
        {
            get => view.color.a;
            set => view.color = new Color(view.color.r, view.color.g, view.color.b, value);
        }

        public Texture GetView()
        {
            return view.texture;
        }
        
        Texture2D GetViewT2D(int textureSize)
        {
            return TextureUtil.ToTexture2D(view.texture, textureSize);
        }
        
        //回転用
        Vector2 sPos; //タッチした座標
        float wid = 100, hei = 100; //スクリーンサイズ
        float left_right, left_right_old,　up_down, up_down_old, _z; //変数
        bool canLeftRight = true, canUpDown = true;

        
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
        
        public (float, float, float) GetCurrentRotation()
        {
            return (left_right, up_down, _z);
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
            RotateTarget(left_right, up_down, _z);
        }
        
        public void ItemDetailStartDirection(float x, float y, float z = 0)
        {
            up_down = y;
            if (y < UpDownRotateRangeMin)
            {
                UpDownRotateRangeMin = y;
            }
            if (y > UpDownRotateRangeMax)
            {
                UpDownRotateRangeMax = y;
            }
            
            up_down = Mathf.Clamp(up_down, UpDownRotateRangeMin, UpDownRotateRangeMax);
            RotateTarget(x, up_down, z);
            OnPointerDown();
        }
        
        public void ItemDetailStartDirection(bool front)
        {
            left_right = front ? 0 : 180;
            OnPointerDown();
            OnHold();
        }
        
        /// <summary>
        /// For non fixMode
        /// </summary>
        /// <returns></returns>
        float CalMaxOrthographicSize()
        {
            float value = 0;
            RotateTarget(0,0,0);
            value = Mathf.Max(value, CalMaxExtend());
            RotateTarget(0,90,0);
            value = Mathf.Max(value, CalMaxExtend());
            RotateTarget(90,0,0);
            return value = Mathf.Max(value, CalMaxExtend());
        }
        
        float CalMaxExtend()
        {
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

            return Vector3.Distance(targetBounds.max, targetBounds.min) / 2f;
        }
    }
}
