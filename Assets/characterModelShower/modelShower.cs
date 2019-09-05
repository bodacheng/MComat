using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public class modelShower : MonoBehaviour
    {
        [Space(11)]
        [Header("Essentials")]
        public CameraManager _CameraManager;
        public CharsManager _CharSetManager;

        [Header("Team Member Positions For Show")]
        public Transform MembersStandCenterPoint;
        public Transform TeamEditWatchPoint;
        public Transform Member0StandPoint;
        public Transform Member1StandPoint;
        public Transform Member2StandPoint;
        public Transform Member3StandPoint;

        private IDictionary<PosNum, Transform> myShowCharPositionDic = new Dictionary<PosNum, Transform>();
        private PinchZoom pinchZoom = new PinchZoom();
        private GameObject showingChar;
        
        public IEnumerator StartUpProcess()
        {
            myShowCharPositionDic = new Dictionary<PosNum, Transform>();
            myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.back, Member0StandPoint));
            myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.left, Member1StandPoint));
            myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.front, Member2StandPoint));
            myShowCharPositionDic.Add(new KeyValuePair<PosNum, Transform>(PosNum.right, Member3StandPoint));
            yield break;
        }
        
        void Awake()
        {
            pinchZoom.camera = this._CameraManager.GetComponent<Camera>();
        }

        public IEnumerator showThisCharacterModel(string localID)
        {
            GameObject _char = myModelPool.Instance.getMyModel(localID);
            if (_char == null)
            {
                IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(localID);
                yield return getchar;
                GetMonsterOfPlayerDetailModel targetAccountCharacterInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
                yield return (this._CharSetManager.buildShowModel(targetAccountCharacterInfo));
                _char = myModelPool.Instance.getMyModel(localID);
            }
            
            if (this.showingChar == _char)
            {
                this.showingChar.SetActive(true);
            }else{
                if (showingChar != null)
                    showingChar.SetActive(false);
                this.showingChar = _char;
                if (this.showingChar != null)
                {
                    this.showingChar.SetActive(true);
                    this.showingChar.transform.parent = null;
                    this.showingChar.transform.position = this.caculateShowModelPosition(new Vector3(0.2f, 0.4f,10f));//右
                    this.showingChar.transform.localRotation = Quaternion.identity;
                }
                else
                {
                    Debug.Log("展示用模型加载严重错误. monsterOfPlayerId" + localID);
                }
            }
            yield return _char;
        }

        Vector3 FirstPoint;
        Vector3 SecondPoint;
        float xAngle;
        float xAngleTemp;
        float yAngle;
        float yAngleTemp;
        Vector3 modelPOnScreen;
        float fingertoshowmodelx, fingertoshowmodely;
        public void TranslateShowingCharToDefaultPos(Vector3 screenPos)//new Vector3(0.23f, 0.3f, 3f)
        {
            if (showingChar != null)
            {
                showingChar.transform.position = Vector3.Lerp(showingChar.transform.position, caculateShowModelPosition(screenPos), Time.deltaTime * 10f);
                if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor ||
                    Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
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
                        modelPOnScreen = caculateShowModelViewportPoint(showingChar.transform.position);
                        fingertoshowmodelx = Mathf.Abs(FirstPoint.x - modelPOnScreen.x)/ Screen.width;
                        fingertoshowmodely = (FirstPoint.y - modelPOnScreen.y)/Screen.height;
                        if (fingertoshowmodelx < 0.3f && fingertoshowmodely < 0.3f && fingertoshowmodely > 0)
                        {
                            SecondPoint = Input.mousePosition;
                            xAngle = xAngleTemp + (FirstPoint.x - SecondPoint.x) * 180 / Screen.width;
                            showingChar.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
                        }
                    }
                    pinchZoom.localUpdate();
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
                        modelPOnScreen = caculateShowModelViewportPoint(showingChar.transform.position);
                        fingertoshowmodelx = Mathf.Abs(FirstPoint.x - modelPOnScreen.x)/ Screen.width;
                        fingertoshowmodely = (FirstPoint.y - modelPOnScreen.y)/Screen.height;
                        if (fingertoshowmodelx < 0.3f && fingertoshowmodely < 0.3f && fingertoshowmodely > 0)
                        {
                            SecondPoint = Input.mousePosition;
                            xAngle = xAngleTemp + (FirstPoint.x - SecondPoint.x) * 180 / Screen.width;
                            showingChar.transform.rotation = Quaternion.Euler(0, xAngle, 0.0f);
                        }
                    }
                    pinchZoom.localUpdate();
                }
            }
        }

        //这个函数有这样的风险：如果你角色由这个函数正在调整位置的过程中step忽然间变了，那角色会停留在途中。而且风险可能不止这些。
        //说到底这个东西无非是为了确保四个角色在画面的上下左右四边，这不是必要的，只是我们所设计的一个外观小花样，而且这么正的排布这些角色其实只有在队伍编辑模式才有些意义。
        private Vector3 rotateTo;
        public void showModelPositionAdjusting()
        {
            Member0StandPoint.position = Vector3.Lerp(Member0StandPoint.position, caculateShowModelPosition(new Vector3(0.5f, 0.7f, 10)), 2 * Time.deltaTime);//后
            rotateTo = _CameraManager.transform.position - Member0StandPoint.position;
            //rotateTo.y = 0;
            Member0StandPoint.transform.rotation = Quaternion.Lerp(Member0StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
            foreach (Transform child in Member0StandPoint.transform)
            {
                child.localPosition = Vector3.zero;
                child.localRotation = Quaternion.identity;
            }

            Member1StandPoint.position = Vector3.Lerp(Member1StandPoint.position, caculateShowModelPosition(new Vector3(0.8f, 0.45f, 10)), 2 * Time.deltaTime);//左
            rotateTo = _CameraManager.transform.position - Member1StandPoint.position;
            //rotateTo.y = 0;
            Member1StandPoint.transform.rotation = Quaternion.Lerp(Member1StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
            foreach (Transform child in Member1StandPoint.transform)
            {
                child.localPosition = Vector3.zero;
                child.localRotation = Quaternion.identity;
            }

            Member2StandPoint.position = Vector3.Lerp(Member2StandPoint.position, caculateShowModelPosition(new Vector3(0.5f, 0.3f, 10)), 2 * Time.deltaTime);//前
            rotateTo = _CameraManager.transform.position - Member2StandPoint.position;
            //rotateTo.y = 0;
            Member2StandPoint.transform.rotation = Quaternion.Lerp(Member2StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
            foreach (Transform child in Member2StandPoint.transform)
            {
                child.localPosition = Vector3.zero;
                child.localRotation = Quaternion.identity;
            }

            Member3StandPoint.position = Vector3.Lerp(Member3StandPoint.position, caculateShowModelPosition(new Vector3(0.2f, 0.45f, 10)), 2 * Time.deltaTime);//右
            rotateTo = _CameraManager.transform.position - Member3StandPoint.position;
            //rotateTo.y = 0;
            Member3StandPoint.transform.rotation = Quaternion.Lerp(Member3StandPoint.transform.rotation, Quaternion.LookRotation(rotateTo), 2 * Time.deltaTime);
            foreach (Transform child in Member3StandPoint.transform)
            {
                child.localPosition = Vector3.zero;
                child.localRotation = Quaternion.identity;
            }
        }

        private Vector3 tempV;
        public Vector3 caculateShowModelPosition(Vector3 screenP)
        {
            tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
            return tempV;
        }
        
        public Vector3 caculateShowModelViewportPoint(Vector3 now)
        {
            tempV = CameraManager._camera.WorldToScreenPoint(now);
            return tempV;
        }
        
        private void arrangeShowModelOnTeam(string localID, PosNum PositionNum)//所以这是个可能把某个阵容位置里加入null的函数。
        {
            Transform t;
            myShowCharPositionDic.TryGetValue(PositionNum, out t);
            GameObject one = myModelPool.Instance.getMyModel(localID);
            if (one)
            {
                one.SetActive(true);
                one.transform.SetParent(t);
                one.transform.localPosition = Vector3.zero;
                one.transform.localRotation = Quaternion.identity;
            }
        }
        
        public void FrontPageModelsRotateShow()
        {
            MembersStandCenterPoint.position = caculateShowModelPosition(new Vector3(0.5f, 0.5f, 10));//后
            Member0StandPoint.rotation = Quaternion.Lerp(Member0StandPoint.rotation, Quaternion.LookRotation(Member0StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);
            Member1StandPoint.rotation = Quaternion.Lerp(Member1StandPoint.rotation, Quaternion.LookRotation(Member1StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);
            Member2StandPoint.rotation = Quaternion.Lerp(Member2StandPoint.rotation, Quaternion.LookRotation(Member2StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);
            Member3StandPoint.rotation = Quaternion.Lerp(Member3StandPoint.rotation, Quaternion.LookRotation(Member3StandPoint.position - MembersStandCenterPoint.position), Time.deltaTime);

            Member0StandPoint.localPosition = Vector3.Lerp(Member0StandPoint.localPosition, new Vector3(0, 0, -4), Time.deltaTime);
            Member1StandPoint.localPosition = Vector3.Lerp(Member1StandPoint.localPosition, new Vector3(-4, 0, 0), Time.deltaTime);
            Member2StandPoint.localPosition = Vector3.Lerp(Member2StandPoint.localPosition, new Vector3(0, 0, 4), Time.deltaTime);
            Member3StandPoint.localPosition = Vector3.Lerp(Member3StandPoint.localPosition, new Vector3(4, 0, 0), Time.deltaTime);

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                xAngle = Input.GetAxis("Mouse X");
                yAngle = Input.GetAxis("Mouse Y");
                MembersStandCenterPoint.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        FirstPoint = Input.GetTouch(0).position;
                        xAngleTemp = xAngle;
                        yAngleTemp = yAngle;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        SecondPoint = Input.GetTouch(0).position;
                        xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                        yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                        MembersStandCenterPoint.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                    }
                }
            }
        }
        
        // 这个函数是读取现在账户情报的。如果之前的更改没保存那读取出来的信息是旧的
        // 那也就是说这里的refresh_from_database，false的话其实才是最新情报，true的话反而可能是旧情报
        public IEnumerator displayMy4V4Team(PosNum myFocusingTeamPosition)
        {
            yield return TeamSet.Instance.loadTeamSet(TeamSetGameMode.story);
            List<GetMonsterOfPlayerDetailModel> onsetLocals = new List<GetMonsterOfPlayerDetailModel>();
            positionLocalCharKeySet _positionLocalCharKeySet4V4Mode = TeamSet.Instance.storyModeTeamSet;
            myModelPool.Instance.setAllMyCharactersModelActive(false);
            GetMonsterOfPlayerDetailModel _one;

            IEnumerator getchar1 = AccountCharsSet.instance.getAccountCharacterInfo(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.back));
            yield return getchar1;
            _one = (GetMonsterOfPlayerDetailModel)getchar1.Current;
            if (_one != null)
                onsetLocals.Add(_one);

            IEnumerator getchar2 = AccountCharsSet.instance.getAccountCharacterInfo(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.left));
            yield return getchar2;
            _one = (GetMonsterOfPlayerDetailModel)getchar2.Current;
            if (_one != null)
                onsetLocals.Add(_one);

            IEnumerator getchar3 = AccountCharsSet.instance.getAccountCharacterInfo(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.front));
            yield return getchar3;
            _one = (GetMonsterOfPlayerDetailModel)getchar3.Current;
            if (_one != null)
                onsetLocals.Add(_one);

            IEnumerator getchar4 = AccountCharsSet.instance.getAccountCharacterInfo(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.right));
            yield return getchar4;
            _one = (GetMonsterOfPlayerDetailModel)getchar4.Current;
            if (_one != null)
                onsetLocals.Add(_one);

            yield return (this._CharSetManager.buildTheseMyModels(onsetLocals.ToArray()));
            arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.back), PosNum.back);
            arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.left), PosNum.left);
            arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.front), PosNum.front);
            arrangeShowModelOnTeam(_positionLocalCharKeySet4V4Mode.getPositionMonsterOfPlayerId(PosNum.right), PosNum.right);
        }
    }
}