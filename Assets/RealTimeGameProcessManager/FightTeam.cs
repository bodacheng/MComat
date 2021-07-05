using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

namespace FightScene
{
    public delegate void LocalFightingUpdate();

    public class FightTeam : MonoBehaviour
    {
        public TeamMode TeamMode;
        public MultiDict<int, int, Data_Center> TeamMembers = new MultiDict<int, int, Data_Center>();
        public IDictionary<Data_Center, CharDataInfo> CharDataInfoRef = new Dictionary<Data_Center, CharDataInfo>();
        public TeamConfig teamConfig;
        public RectTransform sideIconsContainer;
        public Canvas _targetCanvas;
        public SideCharIcon button_prefab;
        public GameObject selectedFrame;
        public Text HitCombo;

        [HideInInspector]
        public Transform[] TeamStandPoints;
        protected IDictionary<Data_Center, SideCharIcon> CharIconDic = new Dictionary<Data_Center, SideCharIcon>();

        public SideCharIcon GetSideIcon(Data_Center d)
        {
            return CharIconDic.ContainsKey(d) ? CharIconDic[d]: null;
        }

        public IEnumerator CharsLoad(MultiDict<int, int, CharDataInfo> MembersSets)
        {
            foreach (KeyValuePair<(int, int), CharDataInfo> kv in MembersSets.mDict)
            {
                CharDataInfo _one = kv.Value;
                Data_Center dcenter = TeamMembers.Get(kv.Key.Item1, kv.Key.Item2);
                if (dcenter == null)
                {
                    IEnumerator char_DC = UnitCreator.CreateCharacter(_one);
                    yield return char_DC;
                    dcenter = (Data_Center)char_DC.Current;
                }
                TeamMembers.Set(kv.Key.Item1, kv.Key.Item2, dcenter);
                DicAdd<Data_Center, CharDataInfo>.Add(CharDataInfoRef, dcenter, _one);
            }
        }

        public virtual void Clear()
        {
        }

        public LocalFightingUpdate localFightingUpdate;

        public virtual List<Transform> TeamMemberTransforms()
        {
            return null;
        }

        protected SideCharIcon _tempSI;
        public void BarsPositionUpdate()
        {
            foreach (Data_Center _one in TeamMembers.GetValues())
            {
                CharIconDic.TryGetValue(_one, out _tempSI);
                _tempSI.transform.position = Vector3.Lerp(_tempSI.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f), Time.deltaTime * 20f);
            }
        }

        protected virtual void InstantiateCharsIconsAndFloatHPBar()
        {
        }

        protected virtual void TeamsFightInitialize(float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
        }

        // for multiRaid
        public virtual void ArrangeAllTeamMembersToPosition(MultiDict<int, int, Data_Center> heromultiDictionary)
        {
        }
        
        // 浮动HPBar和角色头像，共斗模式和轮番模式下头像按钮的作用不一样。一个是换focusing一个是直接切人
        public IEnumerator Instantiate(MultiDict<int, int, CharDataInfo> CharacterSets, float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            yield return CharsLoad(CharacterSets);
            InstantiateCharsIconsAndFloatHPBar();
            TeamsFightInitialize(TeamHpRate, teamCGMode);
        }
        
        // 全队无敌
        public void TurnAllMembersInvincible(bool _Invincible)
        {
            foreach (Data_Center a_char in TeamMembers.GetValues())
            {
                a_char.FightDataRef.Invincible = _Invincible;
            }
        }

        SideCharIcon SideCharIcon;
        protected void RefreshResistanceBar(Data_Center data_Center)
        {
            CharIconDic.TryGetValue(data_Center, out SideCharIcon);
            SideCharIcon.RefreshResistanceBar();
        }
        
        protected void RefreshHPBar(Data_Center data_Center, float current_hp, float wholeHP)
        {
            CharIconDic.TryGetValue(data_Center, out SideCharIcon);
            SideCharIcon.RefreshHpBar(current_hp, wholeHP);
        }
        protected void RefreshExBar(Data_Center data_Center, int current_ex, int wholeex)
        {
            CharIconDic.TryGetValue(data_Center, out SideCharIcon);
            SideCharIcon.RefreshExBar(current_ex, wholeex);
        }

        //这个刷新是倾向于画面制御
        SideCharIcon SideCharIcon3;
        public virtual void Refresh()
        {
            foreach (Data_Center _datacenter in TeamMembers.GetValues())
            {
                CharIconDic.TryGetValue(_datacenter, out SideCharIcon3);
                if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
                {
                    SideCharIcon3.transform.localScale = _datacenter != RealTimeGameProcessManager.focusingChar ? Vector3.one : Vector3.one * 1.2f;
                    SideCharIcon3.transform.SetParent(sideIconsContainer.transform);
                    SideCharIcon3.focusingCharIcon.gameObject.SetActive(true);
                    SideCharIcon3.ExBar.gameObject.SetActive(true);
                    SideCharIcon3.ExBar.transform.SetSiblingIndex(4);
                    SideCharIcon3.RecallBars();
                }
                else
                {
                    SideCharIcon3.focusingCharIcon.gameObject.SetActive(false);
                    SideCharIcon3.ExBar.gameObject.SetActive(false);
                    SideCharIcon3.transform.SetParent(_targetCanvas.transform);
                }
            }
        }

        public bool IfAllCharsPreparedForBattle()
        {
            foreach (Data_Center oneMember in TeamMembers.GetValues())
            {
                if (!oneMember.IfPreparedForBattle())
                    return false;
            }
            return true;
        }
        
        public void LetAllCharactersStartOff()
        {
            foreach (Data_Center oneMember in TeamMembers.GetValues())
            {
                oneMember._MyBehaviorRunner.controller.TestMode = false;
                RealTimeGameProcessManager.AddOrRemoveFightingMember(oneMember, this.teamConfig.myTeam, true);
                oneMember._MyBehaviorRunner.ChangeToWaitingState();
            }
        }
        
        public void LetAllCharactersChangeToTestMode()
        {
            foreach (Data_Center oneMember in TeamMembers.GetValues())
            {
                oneMember._MyBehaviorRunner.controller.TestMode = true;
                RealTimeGameProcessManager.AddOrRemoveFightingMember(oneMember, this.teamConfig.myTeam, true);
                oneMember._MyBehaviorRunner.ChangeToTestMode();
            }
        }
        
        // 队伍模式对应行为运行第一步。
        public virtual void ModeStart()
        {
        }
        
        // 获取该队伍所有账户技能石id（只有在这个队伍是玩家账户队员组成情况下有效）
        public List<string> GetAllUsingStoneOfAcc()
        {
            List<string> stones = new List<string>();
            foreach (KeyValuePair<Data_Center, CharDataInfo> keyValuePair in CharDataInfoRef)
            {
                List<StoneOfPlayerInfo> mystones = Stones.GetEquipingStones(keyValuePair.Value.id);
                for (int i = 0; i < mystones.Count; i++)
                {
                    stones.Add(mystones[i].InstanceId);
                }
            }
            return stones;
        }
    }
}