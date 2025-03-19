using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using mainMenu;

public partial class FightPrepareLayer : UILayer
{
    #region Gangbang
    [SerializeField] GangbangHeroIcon gangbangFighterIcon;
    [SerializeField] Text team1Flg;
    [SerializeField] Text team2Flg;
    [SerializeField] Text team1WholeCount;
    [SerializeField] Text team2WholeCount;

    [SerializeField] Text groupCount1;
    [SerializeField] Text groupCount2;
    [SerializeField] Text groupCount3;
    
    [SerializeField] BOButton countSet1;
    [SerializeField] BOButton countSet2;
    [SerializeField] BOButton countSet3;

    [SerializeField] GameObject countSelectedFrame1;
    [SerializeField] GameObject countSelectedFrame2;
    [SerializeField] GameObject countSelectedFrame3;
    
    private Func<int, string, int, int, int> _setTeamUnitCount;
    private Func<int, string, int> _getTeamUnitCount;
    private List<GangbangHeroIcon> _gangbangHeroIconsM;
    private List<GangbangHeroIcon> _gangbangHeroIconsE;
    #endregion

    private int _selectedMaxTeamCount;

    public int SelectedMaxTeamCount
    {
        get => _selectedMaxTeamCount;
        set => _selectedMaxTeamCount = value;
    }
    
    public void SetGangbangFeature(FightInfo stage, Action toGangbangFront, string gangbangStageNo, 
        Func<int, string, int, int, int> setTeamUnitCount, Func<int, string, int> getTeamUnitCount)
    {
        void UnitCountSetting(int maxUnitPerTeam)
        {
            _selectedMaxTeamCount = maxUnitPerTeam;
            var team1UnitCount = stage.GangbangAutoAdjustTeamUnitByMaxCount(1, stage.FightMembers.HeroSets.GetValues(), _selectedMaxTeamCount, true);
            var team2UnitCount = stage.GangbangAutoAdjustTeamUnitByMaxCount(2, stage.FightMembers.EnemySets.GetValues(), _selectedMaxTeamCount, true);
            
            RefreshCountDisplay(1, team1UnitCount, _selectedMaxTeamCount);
            RefreshCountDisplay(2, team2UnitCount, _selectedMaxTeamCount);
            foreach (var icon in _gangbangHeroIconsE)
            {
                icon.RefreshCount();
            }
            foreach (var icon in _gangbangHeroIconsM)
            {
                icon.RefreshCount();
            }
        }

        groupCount1.text = CommonSetting.GangbangModeMaxUnitPerTeam1.ToString();
        groupCount2.text = CommonSetting.GangbangModeMaxUnitPerTeam2.ToString();
        groupCount3.text = CommonSetting.GangbangModeMaxUnitPerTeam3.ToString();
        
        countSet1.SetListener(() =>
        {
            PlayerPrefs.SetInt("gangbangCountOption", 1);
            PlayerPrefs.Save();
            UnitCountSetting(CommonSetting.GangbangModeMaxUnitPerTeam1);
            countSelectedFrame1.SetActive(true);
            countSelectedFrame2.SetActive(false);
            countSelectedFrame3.SetActive(false);
        });
        countSet2.SetListener(() =>
        {
            PlayerPrefs.SetInt("gangbangCountOption", 2);
            PlayerPrefs.Save();
            UnitCountSetting(CommonSetting.GangbangModeMaxUnitPerTeam2);
            countSelectedFrame1.SetActive(false);
            countSelectedFrame2.SetActive(true);
            countSelectedFrame3.SetActive(false);
        });
        countSet3.SetListener(() =>
        {
            PlayerPrefs.SetInt("gangbangCountOption", 3);
            PlayerPrefs.Save();
            UnitCountSetting(CommonSetting.GangbangModeMaxUnitPerTeam3);
            countSelectedFrame1.SetActive(false);
            countSelectedFrame2.SetActive(false);
            countSelectedFrame3.SetActive(true);
        });
        
        arcadeStageNoText.gameObject.SetActive(true);
        arcadeStageNoText.text = "Stage " + gangbangStageNo;
        toArcadeFrontBtn.gameObject.SetActive(PlayerAccountInfo.Me.tutorialProgress == "Finished");
        toArcadeFrontBtn.SetListener(toGangbangFront);
        
        var rewardDic = PlayFabReadClient.GangbangAwards;
        var reward = rewardDic[gangbangStageNo];
        rewardUI.ShowRewards(reward.d,reward.g);
        int.TryParse(gangbangStageNo, out var arcadeStageNoInt);
        rewardUI.AwardRender(PlayerAccountInfo.Me.gangbangProcess + 1 > arcadeStageNoInt);
        rewardUI.gameObject.SetActive(true);
        nineForShow.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(RECORD_ID);
                connector.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
        nineForShowE.AddOnClickToSlots(
            (RECORD_ID) =>
            {
                var skillConfig = SkillConfigTable.GetSkillConfigByRecordId(RECORD_ID);
                connectorE.SkillShowRunWithPrepare(skillConfig.REAL_NAME).Forget();
            }
        );
        
        _setTeamUnitCount = (i, s, arg3,maxCount) =>
        {
            var returnValue = setTeamUnitCount(i, s, arg3, maxCount);
            RefreshCountDisplay(i, returnValue, maxCount);
            return returnValue;
        };
        _getTeamUnitCount = getTeamUnitCount;
    }
    
    public async UniTask GangbangStageUnitsDisplay(FightInfo stage)
    {
        async UniTask player1IconFeature(string x)
        {
            //PreScene.target.Focusing.id = x;
            //PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillEdit);
            await FocusTeamUnit(x, stage.FightMembers.HeroSets.GetValues(), connector, nineForShow);
        }
        
        async UniTask player2IconFeature(string x)
        {
            await FocusTeamUnit(x, stage.FightMembers.EnemySets.GetValues(), connectorE, nineForShowE);
        }
        
        _gangbangHeroIconsM = GangbangInfosShow(stage.FightMembers.HeroSets.GetValues(), (x) =>
        {
            player1IconFeature(x).Forget();
        }, myTeamShowT, true, 1, PlayerAccountInfo.Me.tutorialProgress == "Finished");
        var default1InstanceId = _gangbangHeroIconsM.FirstOrDefault()?.InstanceID;
        
        if (stage.FightMembers.HeroSets.GetValues().Count < 1)
        {
            teamEditIndicatorText.text = Translate.Get("HasExtraSeat");
            teamEditIndicator.SetActive(true);
        }
        else
        {
            teamEditIndicatorText.text = string.Empty; // Translate.Get("MakeYourTeam");
            teamEditIndicator.SetActive(false);
        }
        _gangbangHeroIconsE = GangbangInfosShow(stage.FightMembers.EnemySets.GetValues(), 
            (x) =>
            {
                player2IconFeature(x).Forget();
            },
            enemyTeamShowT, false, 2);
        var default2InstanceId = _gangbangHeroIconsE.FirstOrDefault()?.InstanceID;
        
        await UniTask.WhenAll(player1IconFeature(default1InstanceId), player2IconFeature(default2InstanceId));
        
        _gangbangHeroIconsE.FirstOrDefault()?.iconButton.onClick.Invoke();
        team1Name.text = "YOU";

        switch (PlayerPrefs.GetInt("gangbangCountOption", 1))
        {
            case 1:
                countSet1.onClick.Invoke();
                break;
            case 2:
                countSet2.onClick.Invoke();
                break;
            case 3:
                countSet3.onClick.Invoke();
                break;
        }
        
    }
    
    List<GangbangHeroIcon> GangbangInfosShow(List<UnitInfo> unitSets, Action<string> iconBehaviour, RectTransform showT, bool withSkillCheck, int team, bool btnInteractive = true)
    {
        foreach (Transform t in showT)
        {
            Destroy(t.gameObject);
        }
        var icons = new List<GangbangHeroIcon>();
        int wholeTeamCount = 0;
        foreach(var unitInfo in unitSets)
        {
            var v = GangbangHeroIcon.ArrangeGangbangHeroIconToParent(
                (x) => _setTeamUnitCount(team, unitInfo.id, x, SelectedMaxTeamCount),
                ()=> _getTeamUnitCount(team, unitInfo.id),
                gangbangFighterIcon, unitInfo, iconBehaviour,
                showT, withSkillCheck, team == 1, true, unitIconSize);
            v.iconButton.interactable = btnInteractive;
            icons.Add(v);
            wholeTeamCount += _getTeamUnitCount(team, unitInfo.id);
        }
        RefreshCountDisplay(team, wholeTeamCount, SelectedMaxTeamCount);
        return icons;
    }
    
    void RefreshCountDisplay(int teamID, int currentTeamUnitCount, int maxTeamCount)
    {
        if (teamID == 1)
        {
            team1Flg.text = Translate.Get("Player") + Translate.Get("WholeUnitCount");
            team1WholeCount.text = currentTeamUnitCount + "/" + maxTeamCount;
            if (_gangbangHeroIconsM == null) return;
            foreach (var icon in _gangbangHeroIconsM)
            {
                icon.RefreshCount();
            }
        }
        else
        {
            team2Flg.text = Translate.Get("Enemy") + Translate.Get("WholeUnitCount");
            team2WholeCount.text = currentTeamUnitCount + "/" + maxTeamCount;
            if (_gangbangHeroIconsE == null) return;
            foreach (var icon in _gangbangHeroIconsE)
            {
                icon.RefreshCount();
            }
        }
    }
}
