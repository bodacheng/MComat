using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public class SelfFightManager : MonoBehaviour
    {
        public preparingScene _preparingScene;

        public Transform selfFightUI;
        public Button FightStartBUtton;
        public charIcon team1back, team1front, team1left, team1right;
        public charIcon team2back, team2front, team2left, team2right;

        public QuestPreparePage _QuestPreparePage;

        private IDictionary<int, charIcon> team1ButtonDic = new Dictionary<int, charIcon>();
        private IDictionary<int, charIcon> team2ButtonDic = new Dictionary<int, charIcon>();

        private LocalFight _selfFight;

        private Team focusingTeam; //team1或者是team2
        private int focusingPosition; // 0到3
        private charIcon focusingPosButton;

        private positionLocalCharKeySet _team1positionLocalCharKeySet = new positionLocalCharKeySet();
        private positionLocalCharKeySet _team2positionLocalCharKeySet = new positionLocalCharKeySet();

        public void clear()
        {
            foreach (KeyValuePair<int, charIcon> keyValuePair in team1ButtonDic)
            {
                keyValuePair.Value.changeIcon(null, zokusei.Null);
            }
            foreach (KeyValuePair<int, charIcon> keyValuePair in team2ButtonDic)
            {
                keyValuePair.Value.changeIcon(null, zokusei.Null);
            }
            _team1positionLocalCharKeySet = new positionLocalCharKeySet();
            _team2positionLocalCharKeySet = new positionLocalCharKeySet();
        }

        public IEnumerator FightStart()
        {
            _preparingScene._MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
            IEnumerator enumerator1 = _team1positionLocalCharKeySet.convertToMultiDictionary();
            yield return enumerator1;
            _selfFight.HeroSets = (MultiDictionary<int, int, CharacterDataInfo>)enumerator1.Current;
            
            IEnumerator enumerator2 = _team2positionLocalCharKeySet.convertToMultiDictionary();
            yield return enumerator2;
            _selfFight.EnemySets = (MultiDictionary<int, int, CharacterDataInfo>)enumerator2.Current;
            
            StageScriptableObject stage = new StageScriptableObject();
            stage.BattleGroundID = 2;
            stage.localFight = _selfFight;
            stage.fightModeType = fightModeType.combat;
            stage._fightEventType = fightEventType.Self;
            stage.Team1Mode = TeamMode.multiraid;
            stage.Team2Mode = TeamMode.multiraid;
            yield return _QuestPreparePage.getReadyToBattle(stage, SceneMode.MyPetsFight);
            yield break;
        }

        public IEnumerator monsterIConButton(string localID)
        {
            if (this.focusingTeam == Team.none || this.focusingPosition < 0)
                yield break;
            IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(localID);
            yield return getchar;
            GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (myfighter == null)
            {
                Debug.Log("角色存档问题。localid：" + localID);
                yield break;
            }
            switch (this.focusingTeam)
            {
                case Team.player1:
                    _team1positionLocalCharKeySet.setPosMemInfoByLocalID((PosNum)this.focusingPosition,localID);
                    yield return changeIconOnPos((PosNum)this.focusingPosition,team1ButtonDic,_team1positionLocalCharKeySet);
                    break;
                case Team.player2:
                    _team2positionLocalCharKeySet.setPosMemInfoByLocalID((PosNum)this.focusingPosition,localID);
                    yield return changeIconOnPos((PosNum)this.focusingPosition,team2ButtonDic,_team2positionLocalCharKeySet);
                    break;
            }
            yield break;
            //以下为防重复版本选人。
            //switch (this.focusingTeam)
            //{
            //    case Team.player1:
            //        PosNumWithLocalKey _PosNumWithLocalKeyfromteam2 = _team2positionLocalCharKeySet.getPosMemInfoByLocalID(localID);
            //        if (_PosNumWithLocalKeyfromteam2 != null)
            //        {
            //            positionLocalCharKeySet.
            //            changePositionBetweenDifferentTeamSets
            //            ((PosNum)this.focusingPosition,_team1positionLocalCharKeySet,_PosNumWithLocalKeyfromteam2.posNum,_team2positionLocalCharKeySet);
            //            yield return changeIconOnPos((PosNum)this.focusingPosition,team1ButtonDic,_team1positionLocalCharKeySet);
            //            yield return changeIconOnPos(_PosNumWithLocalKeyfromteam2.posNum,team2ButtonDic,_team2positionLocalCharKeySet);
            //        }else{
            //            posNums = _team1positionLocalCharKeySet.setPosMemInfoByLocalIDConservationMode((PosNum)this.focusingPosition,localID);
            //            for (int i = 0; i < posNums.Count; i++)
            //            {
            //                yield return changeIconOnPos(posNums[i].posNum,team1ButtonDic,_team1positionLocalCharKeySet);
            //            }
            //        }
            //        break;
            //    case Team.player2:
            //        PosNumWithLocalKey _PosNumWithLocalKeyfromteam1 = _team1positionLocalCharKeySet.getPosMemInfoByLocalID(localID);
            //        if (_PosNumWithLocalKeyfromteam1 != null)
            //        {
            //            positionLocalCharKeySet.
            //            changePositionBetweenDifferentTeamSets
            //            ((PosNum)this.focusingPosition,_team2positionLocalCharKeySet,_PosNumWithLocalKeyfromteam1.posNum,_team1positionLocalCharKeySet);
            //            yield return changeIconOnPos((PosNum)this.focusingPosition,team2ButtonDic,_team2positionLocalCharKeySet);
            //            yield return changeIconOnPos(_PosNumWithLocalKeyfromteam1.posNum,team1ButtonDic,_team1positionLocalCharKeySet);
            //        }else{
            //            posNums = _team2positionLocalCharKeySet.setPosMemInfoByLocalIDConservationMode((PosNum)this.focusingPosition,localID);
            //            for (int i = 0; i < posNums.Count; i++)
            //            {
            //                yield return changeIconOnPos(posNums[i].posNum,team2ButtonDic,_team2positionLocalCharKeySet);
            //            }
            //        }
            //        break;
            //} 
        }
        
        private IEnumerator changeIconOnPos(PosNum posNum,IDictionary<int, charIcon> teamButtonDic,positionLocalCharKeySet positionLocalCharKey)
        {
            if (posNum == PosNum.none)
            {
                Debug.Log("请检查changeIconOnPos函数执行顺序");
                yield break;
            }
            charIcon tar = null;
            if (teamButtonDic.ContainsKey((int)posNum))
            {
                teamButtonDic.TryGetValue((int)posNum,out tar);
            }else{
                Debug.Log("错误的位置值："+posNum);
                yield break;
            }        
            if (tar == null)
            {
                Debug.Log("严重错误");
                yield break;
            }
    
            string PositionMonsterOfPlayerId = positionLocalCharKey.getPositionMonsterOfPlayerId(posNum);
            if (PositionMonsterOfPlayerId != null)
            {
                GetMonsterOfPlayerDetailModel _one;
                CharacterResourceInfo characterResourceInfo = null;
                IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(PositionMonsterOfPlayerId);
                yield return getchar;
                if (getchar.Current == null)
                    yield break;
                _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
                characterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(int.Parse(_one.monsterId));
                tar.changeIcon(characterResourceInfo == null ? null: monsterIconsDic.Instance.getMonsterIconSyn(characterResourceInfo.monsterId),
                    characterResourceInfo == null ? zokusei.Null : characterResourceInfo._zokusei);
            }else{
                tar.changeIcon(null,zokusei.Null);
            }
            yield break;
        }

        public IEnumerator INITeamPosButtons()
        {
            _selfFight = new LocalFight();
            _selfFight.BattleGroundID = 2;

            team1ButtonDic.Clear();
            team2ButtonDic.Clear();

            team1ButtonDic.Add(0, team1back);
            team1ButtonDic.Add(1, team1left);
            team1ButtonDic.Add(2, team1front);
            team1ButtonDic.Add(3, team1right);

            team2ButtonDic.Add(0, team2back);
            team2ButtonDic.Add(1, team2left);
            team2ButtonDic.Add(2, team2front);
            team2ButtonDic.Add(3, team2right);

            team1back.changeIcon(null, zokusei.Null);
            team1left.changeIcon(null, zokusei.Null);
            team1front.changeIcon(null, zokusei.Null);
            team1right.changeIcon(null, zokusei.Null);

            team2back.changeIcon(null, zokusei.Null);
            team2left.changeIcon(null, zokusei.Null);
            team2front.changeIcon(null, zokusei.Null);
            team2right.changeIcon(null, zokusei.Null);

            team1back.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos1B = () =>
            {
                OneTeamPosButtonBehaviour(Team.player1, 0);
            };
            team1back.iconButton.onClick.AddListener(pos1B);

            team1left.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos1L = () =>
            {
                OneTeamPosButtonBehaviour(Team.player1, 1);
            };
            team1left.iconButton.onClick.AddListener(pos1L);

            team1front.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos1F = () =>
            {
                OneTeamPosButtonBehaviour(Team.player1, 2);
            };
            team1front.iconButton.onClick.AddListener(pos1F);

            team1right.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos1R = () =>
            {
                OneTeamPosButtonBehaviour(Team.player1, 3);
            };
            team1right.iconButton.onClick.AddListener(pos1R);

            ////////////////////////////////////////////

            team2back.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos2B = () =>
            {
                OneTeamPosButtonBehaviour(Team.player2, 0);
            };
            team2back.iconButton.onClick.AddListener(pos2B);

            team2left.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos2L = () =>
            {
                OneTeamPosButtonBehaviour(Team.player2, 1);
            };
            team2left.iconButton.onClick.AddListener(pos2L);

            team2front.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos2F = () =>
            {
                OneTeamPosButtonBehaviour(Team.player2, 2);
            };
            team2front.iconButton.onClick.AddListener(pos2F);

            team2right.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction pos2R = () =>
            {
                OneTeamPosButtonBehaviour(Team.player2, 3);
            };
            team2right.iconButton.onClick.AddListener(pos2R);

            FightStartBUtton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction AskStartFight = () =>
            {
                _preparingScene.triggerMainProcess(FightStart());
            };
            FightStartBUtton.onClick.AddListener(AskStartFight);
            yield break;
        }

        private void OneTeamPosButtonBehaviour(Team team, int pos)
        {
            this.focusingTeam = team;
            this.focusingPosition = pos;
        }
    }
}