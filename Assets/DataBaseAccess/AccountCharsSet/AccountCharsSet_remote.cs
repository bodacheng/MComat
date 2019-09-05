using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;
using LitJson;

using System.Text;
using Api.Common;
using Api.Dto.Form;
using Api.Dto.Form.Common;
using Api.Dto.Model;
using Api.Dto.Model.Common;

namespace dataAccess
{
    public partial class AccountCharsSet
    {
        public IEnumerator loadAccountCharacterInfoListObjectsRemote(ApiLanguage apiLanguage)
        {
            // ==============================
            // フォームの生成
            // ==============================
            // フォーム
            CertificationForm form = new CertificationForm();
            form.sessionId = AccountSet.Instance.sessionId;

            // ==============================
            // API送信
            // ==============================
            // 送信
            List<MonsterOfPlayerListModel> getlist = new List<MonsterOfPlayerListModel>();
            yield return ApiCaller.Instance.Post<BaseModel<GetMonsterOfPlayerListModel>, CertificationForm>("http://160.16.187.230/AssetStoreFight/monster/getMonsterOfPlayerList", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model => {
                     getlist = model.data.monsterOfPlayerList;
                     accountCharacterInfoListObjectsDictionary.Clear();
                    Debug.Log("以下是获得的角色列表信息：");
                    for (int i = 0; i < getlist.Count; i++)
                    {
                        Debug.Log("monsterOfPlayerId:"+ getlist[i].monsterOfPlayerId + " monsterid :" + getlist[i].monsterId);
                        if (!accountCharacterInfoListObjectsDictionary.ContainsKey(getlist[i].monsterOfPlayerId))
                            accountCharacterInfoListObjectsDictionary.Add(getlist[i].monsterOfPlayerId, getlist[i]);
                        else
                            Debug.Log("巨大逻辑错误，重复的monsterOfPlayerId:" + getlist[i].monsterOfPlayerId);
                    }
                    Debug.Log("角色列表信息报告结束。");
                 }
                ,
                 model => {
                    Debug.Log("角色列表获取失败。");
                    accountCharacterInfoListObjectsDictionary.Clear();
                 }
            );
            yield break;
        }
        
        public IEnumerator loadAccountCharacterInfoRemote(string monsterlocalid,ApiLanguage apiLanguage)
        {
            GetMonsterOfPlayerDetailForm form = new GetMonsterOfPlayerDetailForm();
            form.sessionId = AccountSet.Instance.sessionId;
            form.monsterOfPlayerId = monsterlocalid;

            GetMonsterOfPlayerDetailModel _GetMonsterOfPlayerDetailModel = null;
            yield return ApiCaller.Instance.Post<BaseModel<GetMonsterOfPlayerDetailModel>, GetMonsterOfPlayerDetailForm>("http://160.16.187.230/AssetStoreFight/monster/getMonsterOfPlayerDetail", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model => {
                     _GetMonsterOfPlayerDetailModel = model.data;
                 }
                ,
                 model => {
                     _GetMonsterOfPlayerDetailModel = null;
                 }
            );
            yield return _GetMonsterOfPlayerDetailModel;
        }
        
        public IEnumerator updateCharRemote(GetMonsterOfPlayerDetailModel accountCharsSet,ApiLanguage apiLanguage)
        {
            SetMonsterSkillStoneForm form = new SetMonsterSkillStoneForm();
            form.sessionId = AccountSet.Instance.sessionId;
            form.monsterOfPlayerId = accountCharsSet.monsterOfPlayerId;
            form.a1SkillStoneOfPlayerId = accountCharsSet.a1_skill_stone_record_id;
            form.a2SkillStoneOfPlayerId = accountCharsSet.a2_skill_stone_record_id;
            form.a3SkillStoneOfPlayerId = accountCharsSet.a3_skill_stone_record_id;
            form.b1SkillStoneOfPlayerId = accountCharsSet.b1_skill_stone_record_id;
            form.b2SkillStoneOfPlayerId = accountCharsSet.b2_skill_stone_record_id;
            form.b3SkillStoneOfPlayerId = accountCharsSet.b3_skill_stone_record_id;
            form.c1SkillStoneOfPlayerId = accountCharsSet.c1_skill_stone_record_id;
            form.c2SkillStoneOfPlayerId = accountCharsSet.c2_skill_stone_record_id;
            form.c3SkillStoneOfPlayerId = accountCharsSet.c3_skill_stone_record_id;

            yield return ApiCaller.Instance.Post<BaseModel<BaseVoidModel>, SetMonsterSkillStoneForm>("http://160.16.187.230/AssetStoreFight/monster/setMonsterSkillStone", form, ApiCaller.Instance.getHeader(apiLanguage),
                 model => {
                     Debug.Log("技能编辑成功");
                 }
                ,
                 model => {
                     Debug.Log("的技能编辑失败");
                 }
            );
            yield break;
        }

        public IEnumerator plusExpForAccountCharRemote(string charlocalID, int plusExp)
        {
            if (AccountSet.instance._PlayerAccountInfo.Coin < plusExp)
            {
                yield break;
            }
        }

        // 针对玩家拥有角色的更新(monsters_of_player表的update操作),
        // 服务端需要有审核,下面是角色更新操作的一些要点
        // 1.monsterId，playerid 不可能变
        // 2.level与exp存在相互对应关系，待定
        // 3.a1Id到c3Id(技能id)都可能更新，在更新时，新的id需要索引skills查看type，
        //然后monsterId索引monsters表，查看对应的type与技能id对应type是否一致，如不一致不执行更新。
    }
}