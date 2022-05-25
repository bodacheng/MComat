using System;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using System.Collections;
using dataAccess;
using mainMenu;
using Singleton;
using UnityEngine.Rendering.Universal;

namespace Cocone.ProjectP3
{
    public class DedicatedCameraConnector : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Camera camera;
        [SerializeField] private float UpDownRotateRangeMin = -10;
        [SerializeField] private float UpDownRotateRangeMax = 45;
        [SerializeField] private float rotateSpeed = 90;
        [SerializeField] private float extraZDis = 0;
        [SerializeField] private float extraZCameraDepth = 5f;
        private readonly Bounds tempBoundary = new Bounds();
        private RectTransform rect;
        private bool fixMode;

        private IEnumerator enumerator;
        
        public void Clear()
        {
            DestroyImmediate(camera.gameObject);
            if (target != null)
                DestroyImmediate(target.gameObject);
            DestroyImmediate(this.gameObject);
        }

        private void OnDestroy()
        {
            if (enumerator != null)
                StopCoroutine(enumerator);
            
            if (GeneralModelPool.process != null)
                StopCoroutine(GeneralModelPool.process);
                
            if (target != null)
                DestroyImmediate(target.gameObject);
        }

        public void ShowMyModel(string instanceID)
        {
            enumerator = _ShowMyModel(instanceID);
            StartCoroutine(enumerator);
        }
        
        GameObject _model;
        IEnumerator _ShowMyModel(string instanceID)
        {
            var info = MyMonsters.Get(instanceID);
            var p = _ShowModel(info?.r_id);
            yield return p;
            yield return p.Current;
        }
        
        public void ShowModel(string recordID)
        {
            enumerator = _ShowModel(recordID);
            StartCoroutine(enumerator);
        }
    
        IEnumerator _ShowModel(string recordID) 
        {
            if (enumerator != null)
                StopCoroutine(enumerator);
            if (_model != null)
            {
                DestroyImmediate(_model);
                _model = null;
            }
            if (recordID == null)
            {
                yield break;
            }
            
            var p = GeneralModelPool.GetModel(recordID, transform);
            yield return p;
            if (p.Current == null)
            {
                Debug.Log("模型错误");
                yield break;
            }
            
            var _dataCenter = p.Current;
            if (_dataCenter == null)
            {
                Debug.Log("模型错误");
                SkillShowSupporter.FocusingC = null;
                yield break;
            }
            var dataCenter = (Data_Center)_dataCenter;
            SkillShowSupporter.FocusRId = recordID;
            SkillShowSupporter.FocusingC = dataCenter;
            SkillShowSupporter.FocusingC.Animation_Manger.AnimatorRef.applyRootMotion = true;
            
            // 这个短暂变色是为了掩盖一些模型刚加载瞬间有些渲染没到位的尴尬。比如裙子摇晃 
            dataCenter._ShaderManager.FlatColorForAShortTime(Color.black, 0.5f, 0.5f);
            
            _model = dataCenter.WholeT.gameObject;
            _model.SetActive(true);
            
            Initialize(false,_model.transform, transform, PreScene.target.FxCamera);
            ItemDetailStartDirection(0,0,0);
            yield return _model;
        }
        
        void Initialize(bool fixMode, Transform focus, Transform camerasHolder = null, Camera stackParent = null)
        {
            this.fixMode = fixMode;
            rect = transform.GetComponent<RectTransform>();
            target = focus;
            target.SetParent(transform);
            _parentNodeRenderer = target.GetComponent<Renderer>();
            _renderers = target.GetComponentsInChildren<Renderer>().ToArray();
            
            foreach (var mesh in _renderers)
            {
                if (mesh is SkinnedMeshRenderer skinnedMesh)
                {
                    skinnedMesh.updateWhenOffscreen = true; // to fit camera bound
                }
            }
            
            camera.transform.SetParent(camerasHolder);
            if (stackParent != null)
            {
                var PCameraData = stackParent.transform.GetComponent<UniversalAdditionalCameraData>();
                var cameraData = camera.transform.GetComponent<UniversalAdditionalCameraData>();
                cameraData.renderType = CameraRenderType.Overlay;
                PCameraData.cameraStack.Add(camera);
            }
            
            wid = rect.rect.width;
            hei = rect.rect.height;
            
            camera.gameObject.SetActive(true);
            
            if (!this.fixMode)
                _basicOrthographicSize = CalMaxOrthographicSize();
        }

        void Update()
        {
            if (target != null) CameraPositionCal();
        }

        private Renderer _parentNodeRenderer;
        private Bounds _targetBounds;
        private Renderer[] _renderers;
        private float _basicOrthographicSize;
        
        void CameraPositionCal()
        {
            // 合成Bounds計算
            _targetBounds = tempBoundary;
            if (_parentNodeRenderer != null)
            {
                _targetBounds = _parentNodeRenderer.bounds;
            }

            foreach (var render in _renderers)
            {
                if (render == null)
                    continue; 

                if (_targetBounds == tempBoundary)
                {
                    _targetBounds = render.bounds;
                }
                else
                {
                    _targetBounds.Encapsulate(render.bounds);
                }
            }
            
            if (fixMode)
            {
                _basicOrthographicSize = Mathf.Max(_targetBounds.extents.x, _targetBounds.extents.y);
            }
            
            camera.orthographicSize = _basicOrthographicSize * (Screen.height / rect.rect.height);
            
            var viewCenter = GetCenterPosition(rect);
            var cViewWidth = camera.orthographicSize * 2 * camera.aspect;
            var cViewHeight = camera.orthographicSize * 2;
            
            camera.transform.position = _targetBounds.center + Vector3.forward * (_targetBounds.extents.z + extraZDis)
                        + (0.5f -　((float)viewCenter.x / Screen.width)) * cViewWidth * camera.transform.right 
                        + (0.5f - ((float)viewCenter.y / Screen.height)) * cViewHeight * Vector3.up;
            camera.farClipPlane = _targetBounds.extents.z * 2 + extraZDis + extraZCameraDepth;
        }
        
        static Vector2 GetCenterPosition(RectTransform rect)
        {
            var position = rect.transform.position;
            
            // 真ん中Pivotじゃなければ真ん中を計算する
            if (rect.pivot != new Vector2(0.5f, 0.5f))
            {
                var scaleX = rect.transform.lossyScale.x;
                var scaleY = rect.transform.lossyScale.y;
                var x = rect.rect.width / 2f * scaleX;
                var y = rect.rect.height / 2f * scaleY;
                position.x += Mathf.Lerp(x, -x, rect.pivot.x);
                position.y += Mathf.Lerp(y, -y, rect.pivot.y);
            }
            return position;
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
        
        public void RotateTarget(float left_right, float up_down, float Z = 0)
        {
            if (target == null) return;
            this.left_right = left_right;
            this.up_down = up_down;
            this._z = Z;
            target.localRotation = Quaternion.Euler(up_down, 0, Z);
            target.RotateAround(target.position, target.up, left_right);
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
            _targetBounds = tempBoundary;
            if (_parentNodeRenderer != null)
            {
                _targetBounds = _parentNodeRenderer.bounds;
            }

            foreach (Renderer render in _renderers)
            {
                if (_targetBounds == tempBoundary)
                {
                    _targetBounds = render.bounds;
                }
                else
                {
                    _targetBounds.Encapsulate(render.bounds);
                }
            }

            return Vector3.Distance(_targetBounds.max, _targetBounds.min) / 2f;
        }
    }
}
