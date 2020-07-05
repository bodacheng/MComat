using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

namespace FightScene
{
    public class FightTeam : MonoBehaviour
    {
        public TeamMode TeamMode;
        public MultiDictionary<int, int, Data_Center> TeamMembers = new MultiDictionary<int, int, Data_Center>();
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

        public IEnumerator CharsLoad(MultiDictionary<int, int, CharDataInfo> MembersSets)
        {
            foreach (KeyValuePair<int, List<int>> keys in MembersSets.GetAllUnNullKeys())
            {
                foreach (int key in keys.Value)
                {
                    CharDataInfo _one = MembersSets.Get(keys.Key, key);
                    Data_Center model = TeamMembers.Get(keys.Key, key);
                    if (model == null)
                    {
                        IEnumerator char_DC = CharsManager.target.CreateCharacter(_one);
                        yield return char_DC;
                        model = (Data_Center)char_DC.Current;
                    }
                    TeamMembers.Set(keys.Key, key, model);
                    DicAdd<Data_Center, CharDataInfo>.Add(CharDataInfoRef, TeamMembers.Get(keys.Key, key), _one);
                }
            }
        }

        public virtual void Clear()
        {
            CharIconDic.Clear();
        }

        public virtual List<Transform> TeamMemberTransforms()
        {
            return null;
        }

        protected SideCharIcon _tempSI;
        public void BarsPositionUpdate()
        {
            foreach (Data_Center _one in TeamMembers.values)
            {
                CharIconDic.TryGetValue(_one, out _tempSI);
                _tempSI.transform.position = Vector3.Lerp(_tempSI.transform.position, CameraManager._camera.WorldToScreenPoint(_one.transform.position + Vector3.up * 3f), Time.deltaTime * 20f);
            }
        }

        protected virtual void InstantiateCharsIconsAndFloatHPBar()
        {
        }

        protected virtual void TeamsFightInitialize(float extraHP)
        {
        }

        // for multiRaid
        public virtual void ArrangeAllTeamMembersToPosition(MultiDictionary<int, int, Data_Center> heromultiDictionary)
        {
        }
        
        // 浮动HPBar和角色头像，共斗模式和轮番模式下头像按钮的作用不一样。一个是换focusing一个是直接切人
        public IEnumerator Instantiate(MultiDictionary<int, int, CharDataInfo> CharacterSets, float extraHP)
        {
            yield return CharsLoad(CharacterSets);
            InstantiateCharsIconsAndFloatHPBar();
            TeamsFightInitialize(extraHP);
        }

        SideCharIcon SideCharIcon;
        protected void RefreshResistanceBar(Data_Center data_Center)
        {
            CharIconDic.TryGetValue(data_Center, out SideCharIcon);
            DOTween.To(() => SideCharIcon.ResistBar.value, (x) => SideCharIcon.ResistBar.value = x, data_Center._ResistanceManager.Resistance.Value / 10f, 0.2f);
            SideCharIcon.ResistBarFillImage.color = data_Center._ResistanceManager.Resistance.Value > 0 ? Color.yellow : Color.clear;
        }
        
        SideCharIcon SideCharIcon2;
        protected void RefreshHPBar(Data_Center data_Center, float current_hp, float wholeHP)
        {
            CharIconDic.TryGetValue(data_Center, out SideCharIcon2);
            SideCharIcon2.HpText.text = current_hp.ToString();
            DOTween.To(() => SideCharIcon2.HpBar.value, (x) => SideCharIcon2.HpBar.value = x, current_hp / wholeHP, 0.2f);
        }
        protected void RefreshExBar(Data_Center data_Center, float current_ex, float wholeex)
        {
            CharIconDic.TryGetValue(data_Center, out SideCharIcon2);
            DOTween.To(() => SideCharIcon2.ExBar.value, (x) => SideCharIcon2.ExBar.value = x, current_ex / wholeex, 0.2f);
        }

        //这个刷新是倾向于画面制御
        SideCharIcon SideCharIcon3;
        public virtual void Refresh()
        {
            foreach (Data_Center _datacenter in TeamMembers.values)
            {
                CharIconDic.TryGetValue(_datacenter, out SideCharIcon3);
                if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
                {
                    SideCharIcon3.transform.localScale = _datacenter != RealTimeGameProcessManager.focusingChar ? Vector3.one : Vector3.one * 1.2f;
                    SideCharIcon3.transform.SetParent(sideIconsContainer.transform);
                    SideCharIcon3.focusingCharIcon.gameObject.SetActive(true);
                    SideCharIcon3.ExBar.gameObject.SetActive(true);
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
        
        public virtual void LocalFightingUpdate()
        {
        }
        
        public bool IfAllCharsPreparedForBattle()
        {
            foreach (Data_Center oneMember in TeamMembers.values)
            {
                if (!oneMember.IfPreparedForBattle())
                    return false;
            }
            return true;
        }
        
        public void LetAllCharactersStartOff()
        {
            foreach (Data_Center oneMember in TeamMembers.values)
            {
                oneMember._MyBehaviorRunner.scarecrow = false;
                oneMember._MyBehaviorRunner.ChangeToWaitingState();
            }
        }
        
        public void LetAllCharactersChangeToTestMode()
        {
            foreach (Data_Center oneMember in TeamMembers.values)
            {
                oneMember._MyBehaviorRunner.scarecrow = true;
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
                List<SkillStoneOfPlayerInfoModel> mystones = MySkillStonesReader.GetEquipingStones(keyValuePair.Value.monsterOfPlayerId);
                for (int i = 0; i < mystones.Count; i++)
                {
                    stones.Add(mystones[i].skillStoneOfPlayerId);
                }
            }
            return stones;
        }
    }
}