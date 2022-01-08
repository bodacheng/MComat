using System.Collections;
using UnityEngine;
using dataAccess;

public class ModelShower : MonoBehaviour
{
    [SerializeField] Camera _camera;
    readonly PinchZoom pinchZoom = new PinchZoom();
    GameObject model;

    public static ModelShower target;
    
    void Awake()
    {
        pinchZoom.camera = _camera;
        target = this;
    }
    
    public IEnumerator ShowMyModel(string instanceID)
    {
        UnitInfo info = MyMonsters.Get(instanceID);
        IEnumerator p = ShowModel(info?.r_id);
        yield return p;
        yield return p.Current;
    }
    
    public IEnumerator ShowModel(string recordID) 
    {
        if (model != null)
        {
            model.SetActive(false);
        }
        if (recordID == null)
        {
            yield break;
        }
        
        IEnumerator p = GeneralModelPool.GetModel(recordID, true);
        yield return p;
        if (p.Current == null)
        {
            Debug.Log("模型错误");
            yield break;
        }
        
        Data_Center aI_DATA_CENTER = (Data_Center)p.Current;        
        aI_DATA_CENTER._ShaderManager.FlatColorForAShortTime(10f, 0, 0.5f, Color.black); // 这个短暂变色是为了掩盖一些模型刚加载瞬间有些渲染没到位的尴尬。比如裙子摇晃 
        model = aI_DATA_CENTER.WholeT.gameObject;
        model.SetActive(true);
        model.transform.parent = null;
        model.transform.position = CalculateShowModelPosition(new Vector3(0.2f, 0.4f, 10));//右
        //showingChar.transform.LookAt(_CameraManager.transform, Vector3.up);
        model.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
        yield return model;
    }
    
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    Vector3 modelPOnScreen;
    float xAngle;
    float xAngleTemp;
    readonly float yAngle;
    readonly float yAngleTemp;
    float fingertoshowmodelx, fingertoshowmodely;
    // 注意，整个模型的上下移动靠的是StartToEndMode相机
    public void TranslateShowingCharToDefaultPos(Vector3 screenPos)//new Vector3(0.23f, 0.3f, 3f)
    {
        if (model != null)
        {
            model.transform.position = Vector3.Lerp(model.transform.position, CalculateShowModelPosition(screenPos), Time.deltaTime * 20f);
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                //xAngle = Input.GetAxis("Mouse X");
                //yAngle = Input.GetAxis("Mouse Y");
                //showingChar.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                
                if (Input.GetMouseButtonDown(0))//Input.GetTouch(0).phase == TouchPhase.Began
                {
                    FirstPoint = Input.mousePosition;
                    xAngleTemp = xAngle;
                }
                else if (Input.GetMouseButton(0))
                {
                    modelPOnScreen = CalculateShowModelViewportPoint(model.transform.position);
                    fingertoshowmodelx = Mathf.Abs(FirstPoint.x - modelPOnScreen.x) / Screen.width;
                    fingertoshowmodely = (FirstPoint.y - modelPOnScreen.y) / Screen.height;
                    if (fingertoshowmodelx < 0.3f && fingertoshowmodely < 0.3f && fingertoshowmodely > 0)
                    {
                        SecondPoint = Input.mousePosition;
                        xAngle = xAngleTemp + (FirstPoint.x - SecondPoint.x) * 180 / Screen.width;
                        model.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
                    }
                }
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.GetMouseButtonDown(0))//Input.GetTouch(0).phase == TouchPhase.Began
                {
                    FirstPoint = Input.mousePosition;
                    xAngleTemp = xAngle;
                }
                else if (Input.GetMouseButton(0))
                {
                    modelPOnScreen = CalculateShowModelViewportPoint(model.transform.position);
                    fingertoshowmodelx = Mathf.Abs(FirstPoint.x - modelPOnScreen.x)/ Screen.width;
                    fingertoshowmodely = (FirstPoint.y - modelPOnScreen.y)/ Screen.height;
                    if (fingertoshowmodelx < 0.3f && fingertoshowmodely < 0.3f && fingertoshowmodely > 0)
                    {
                        SecondPoint = Input.mousePosition;
                        xAngle = xAngleTemp + (FirstPoint.x - SecondPoint.x) * 180 / Screen.width;
                        model.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
                    }
                }
            }
            pinchZoom.LocalUpdate();
        }
    }
    
    public void CFollowCharZ()
    {
        if (model != null)
        {
            if (Mathf.Abs(_camera.transform.position.z - model.transform.position.z) < 10)
            {
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, _camera.transform.position + Vector3.forward * 
                (10 - Mathf.Abs(_camera.transform.position.z - model.transform.position.z)),Time.deltaTime * 10f);
            }
        }
    }
    
    Vector3 tempV;
    Vector3 CalculateShowModelPosition(Vector3 screenP)
    {
        tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
        return tempV;
    }
    
    Vector3 CalculateShowModelViewportPoint(Vector3 now)
    {
        tempV = CameraManager._camera.WorldToScreenPoint(now);
        return tempV;
    }
}

    //void ArrangeShowModelOnTeam(string localID, int PositionNum)//所以这是个可能把某个阵容位置里加入null的函数。
    //{
    //    myShowCharPositionDic.TryGetValue(PositionNum, out Transform t);
    //    GameObject one = MyModelPool.Instance.GetMyModel(localID);
    //    if (one)
    //    {
    //        one.SetActive(true);
    //        one.transform.SetParent(t);
    //        one.transform.localPosition = Vector3.zero;
    //        one.transform.localRotation = Quaternion.identity;
    //    }
    //}
   
    // 这个函数是读取现在账户情报的。如果之前的更改没保存那读取出来的信息是旧的
    // 那也就是说这里的refresh_from_database，false的话其实才是最新情报，true的话反而可能是旧情报
    //public IEnumerator DisplayMy4V4Team()
    //{
    //    yield return TeamSet.LoadTeamSet(TeamSetGameMode.story);
    //    List<GetMonsterOfPlayerDetailModel> onsetLocals = new List<GetMonsterOfPlayerDetailModel>();
    //    PosKeySet _positionLocalCharKeySet4V4Mode = TeamSet.Default;
    //    MyModelPool.Instance.SetAllMyCharactersModelActive(false);
    //    GetMonsterOfPlayerDetailModel _one;

    //    _one = AccountCharsSet.Get(_positionLocalCharKeySet4V4Mode.GetMonsterOfPlayerIdOnPos(0));
    //    if (_one != null)
    //        onsetLocals.Add(_one);

    //    _one = AccountCharsSet.Get(_positionLocalCharKeySet4V4Mode.GetMonsterOfPlayerIdOnPos(1));
    //    if (_one != null)
    //        onsetLocals.Add(_one);

    //    _one = AccountCharsSet.Get(_positionLocalCharKeySet4V4Mode.GetMonsterOfPlayerIdOnPos(2));
    //    if (_one != null)
    //        onsetLocals.Add(_one);
            
    //    yield return CharsManager.target.BuildTheseMyModels(onsetLocals.ToArray());
    //    ArrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.GetMonsterOfPlayerIdOnPos(0), 0);
    //    ArrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.GetMonsterOfPlayerIdOnPos(1), 1);
    //    ArrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.GetMonsterOfPlayerIdOnPos(2), 2);
    //}

    //这个函数有这样的风险：如果你角色由这个函数正在调整位置的过程中step忽然间变了，那角色会停留在途中。而且风险可能不止这些。
    //说到底这个东西无非是为了确保四个角色在画面的上下左右四边，这不是必要的，只是我们所设计的一个外观小花样，而且这么正的排布这些角色其实只有在队伍编辑模式才有些意义。
    //Vector3 rotateTo;
    //public void ShowModelPositionAdjusting()
    //{
    //    Member0StandPoint.position = Vector3.Lerp(Member0StandPoint.position, CaculateShowModelPosition(new Vector3(0.5f, 0.7f, 10)), 2 * Time.deltaTime);//后
    //    rotateTo = _CameraManager.transform.position - Member0StandPoint.position;
    //    //rotateTo.y = 0;
    //    Member0StandPoint.transform.rotation = Quaternion.Lerp(Member0StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
    //    foreach (Transform child in Member0StandPoint.transform)
    //    {
    //        child.localPosition = Vector3.zero;
    //        child.localRotation = Quaternion.identity;
    //    }

    //    Member1StandPoint.position = Vector3.Lerp(Member1StandPoint.position, CaculateShowModelPosition(new Vector3(0.8f, 0.45f, 10)), 2 * Time.deltaTime);//左
    //    rotateTo = _CameraManager.transform.position - Member1StandPoint.position;
    //    //rotateTo.y = 0;
    //    Member1StandPoint.transform.rotation = Quaternion.Lerp(Member1StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
    //    foreach (Transform child in Member1StandPoint.transform)
    //    {
    //        child.localPosition = Vector3.zero;
    //        child.localRotation = Quaternion.identity;
    //    }

    //    Member2StandPoint.position = Vector3.Lerp(Member2StandPoint.position, CaculateShowModelPosition(new Vector3(0.5f, 0.3f, 10)), 2 * Time.deltaTime);//前
    //    rotateTo = _CameraManager.transform.position - Member2StandPoint.position;
    //    //rotateTo.y = 0;
    //    Member2StandPoint.transform.rotation = Quaternion.Lerp(Member2StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
    //    foreach (Transform child in Member2StandPoint.transform)
    //    {
    //        child.localPosition = Vector3.zero;
    //        child.localRotation = Quaternion.identity;
    //    }

    //    Member3StandPoint.position = Vector3.Lerp(Member3StandPoint.position, CaculateShowModelPosition(new Vector3(0.2f, 0.45f, 10)), 2 * Time.deltaTime);//右
    //    rotateTo = _CameraManager.transform.position - Member3StandPoint.position;
    //    //rotateTo.y = 0;
    //    Member3StandPoint.transform.rotation = Quaternion.Lerp(Member3StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
    //    foreach (Transform child in Member3StandPoint.transform)
    //    {
    //        child.localPosition = Vector3.zero;
    //        child.localRotation = Quaternion.identity;
    //    }
    //}
    
    //public void FrontPageModelsRotateShow()
    //{
    //    MembersStandCenterPoint.position = CaculateShowModelPosition(new Vector3(0.5f, 0.5f, 10));//后
    //    Member0StandPoint.rotation = Quaternion.Lerp(Member0StandPoint.rotation, Quaternion.LookRotation(Member0StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);
    //    Member1StandPoint.rotation = Quaternion.Lerp(Member1StandPoint.rotation, Quaternion.LookRotation(Member1StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);
    //    Member2StandPoint.rotation = Quaternion.Lerp(Member2StandPoint.rotation, Quaternion.LookRotation(Member2StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);
    //    Member3StandPoint.rotation = Quaternion.Lerp(Member3StandPoint.rotation, Quaternion.LookRotation(Member3StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);
    
    //    Member0StandPoint.localPosition = Vector3.Lerp(Member0StandPoint.localPosition, new Vector3(0, 0, -4), Time.deltaTime);
    //    Member1StandPoint.localPosition = Vector3.Lerp(Member1StandPoint.localPosition, new Vector3(-4, 0, 0), Time.deltaTime);
    //    Member2StandPoint.localPosition = Vector3.Lerp(Member2StandPoint.localPosition, new Vector3(0, 0, 4), Time.deltaTime);
    //    Member3StandPoint.localPosition = Vector3.Lerp(Member3StandPoint.localPosition, new Vector3(4, 0, 0), Time.deltaTime);
    
    //    if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor ||
    //        Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
    //    {
    //        xAngle = UnityEngine.Input.GetAxis("Mouse X");
    //        yAngle = UnityEngine.Input.GetAxis("Mouse Y");
    //        MembersStandCenterPoint.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    //    }
    //    else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
    //    {
    //        if (UnityEngine.Input.touchCount > 0)
    //        {
    //            if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
    //            {
    //                FirstPoint = UnityEngine.Input.GetTouch(0).position;
    //                xAngleTemp = xAngle;
    //                yAngleTemp = yAngle;
    //            }
    //            if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
    //            {
    //                SecondPoint = UnityEngine.Input.GetTouch(0).position;
    //                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
    //                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
    //                MembersStandCenterPoint.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    //            }
    //        }
    //    }
    //}