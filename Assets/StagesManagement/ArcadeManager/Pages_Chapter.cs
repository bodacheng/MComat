using UnityEngine;
using dataAccess;
using UniRx;

namespace mainMenu
{
    public partial class ArcadeManager : MonoBehaviour
    {
        // 放在滑动区上
        public void DragEnd()
        {
            JumpTo(PageV(_Scrollbar.value, StageCount));
        }
        
        void OneChapterFiveLevel()
        {
            autoCommand = new SingleAssignmentDisposable
            {
                Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (FightGlobalSetting.scenestep != 0 || JumpToNewStage.IsDestroyed() || JumpToNewStage == null || JumpToNewStage.gameObject == null)
                        {
                            autoCommand.Dispose();
                            return;
                        }
                        
                        // 不可拖向太超前的关卡
                        if (_Scrollbar.value > PageD(AccountSet._AccInfo.ArcadeProcess, StageCount) + (float) 0.5 / StageCount)
                        {
                            JumpTo(PageD(AccountSet._AccInfo.ArcadeProcess, StageCount));
                        }
                        
                        if (!JumpToNewStage.gameObject.activeSelf)
                        {
                            if (Mathf.Abs(PageD(AccountSet._AccInfo.ArcadeProcess, StageCount) - _Scrollbar.value) > 0.2f)
                            {
                                JumpToNewStage.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(PageD(AccountSet._AccInfo.ArcadeProcess, StageCount) - _Scrollbar.value) <= 0.2f)
                            {
                                JumpToNewStage.gameObject.SetActive(false);
                            }
                        }
                    }
                )
            };
            void temp()
            {
                JumpTo(PageD(AccountSet._AccInfo.ArcadeProcess, StageCount));
            }
            JumpToNewStage.onClick.RemoveAllListeners();
            JumpToNewStage.onClick.AddListener(temp);
            temp();
        }
        
        // 给关卡号返回应该的scrollbar值。
        // 利用了PageV。具体逻辑虽然混乱但以levelCount = 10做推导后判断应该是无误。
        float PageD(int targetLevel, int levelCount)
        {
            // temp这个值是“每5个关卡所占据的滚动条长度”
            // 如果关卡总数是a，那么显示第a-5到第a个关卡的时候，滚动条value是1
            // 假设有10个关卡，那么滚动条的两个定位节点是0（显示第1到5关）和 1 （显示第6到10关）
            // 不管有多少关，0永远是第0至5关的目标滚动轴值
            // 如果回头时候看不懂这个函数那可以假设有10个关卡，
            // 一步步推导看看什么意思。
            float temp = 0;
            temp = levelCount - 5;
            temp = System.Math.Abs(temp) < 0.001 ? 0f : 5f / (float)temp;
            
            int tempInt = (int)Mathf.Floor((float)targetLevel/5); // 整数（对应关卡超过了多少章节）
            int d = targetLevel % 5; //余数
            if (d > 0)
            {
                return PageV(tempInt * temp, levelCount);
            }else{
                return PageV((tempInt - 1) * temp, levelCount);
            }
        }
        
        // 滚动轴自动调整功能的辅助计算函数。画面一次显示5个关卡，
        // 那么给出当前滚动轴的值和最大关卡，得到应该调整到的滚动轴值
        // 这个函数显然不能“给出关卡号码，返回应该的滚动轴值）
        float PageV(float currentvalue, int levelCount)
        {
            int i = 0; // 相当于章节，一个章节5个小关
            float nextScrollBarPoint = -9999;
            
            // temp这个值是“每5个关卡所占据的滚动条长度”
            // 如果关卡总数是a，那么显示第a-5到第a个关卡的时候，滚动条value是1
            // 假设有10个关卡，那么滚动条的两个定位节点是0（显示第1到5关）和 1 （显示第6到10关）
            // 不管有多少关，0永远是第0至5关的目标滚动轴值
            // 如果回头时候看不懂这个函数那可以假设有10个关卡，
            // 一步步推导看看什么意思。
            float temp = 0;
            temp = levelCount - 5;
            temp = System.Math.Abs(temp) < 0.001 ? 0f : 5f / (float)temp;
            
            do
            {
                i++;
                nextScrollBarPoint = temp * i;
            } while (currentvalue > nextScrollBarPoint);
            
            float toPre = currentvalue - temp * (i - 1) ;
            float toNext = temp * i - currentvalue;
            return toNext >= toPre ? temp * (i - 1): temp * i;
        }
    }
}