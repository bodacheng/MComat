using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Api.Dto.Model;
using Skill;
using Newtonsoft.Json;
using System;

namespace dataAccess
{
    public partial class MySkillStones
    {
        public static void Read(SkillStoneOfPlayerInfoModel one)
        {
            DicAdd<string, SkillStoneOfPlayerInfoModel>.Add(Dic, one.skillStoneOfPlayerId, one);
            GenerateStoneModelByAccID(one.skillStoneOfPlayerId);
        }

        static void ConvertListToDic(List<SkillStoneOfPlayerInfoModel> list)
        {
            foreach (SkillStoneOfPlayerInfoModel stoneinfo in list)
            {
                Read(stoneinfo);
            }
        }

        public static void LoadAMySkillstones(Action<int> finished)
        {
            Dic.Clear();
            RenderModelDic.Clear();

            List<SkillStoneOfPlayerInfoModel> list;
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    list = LoadAll_Json(Application.persistentDataPath + "/MyStones");
                    ConvertListToDic(list);
                    finished(1);
                break;
                case PlayerInfoRefMode.remoteTestPlayer:
                    PlayFabRead.LoadItems(finished);
                break;
                case PlayerInfoRefMode.formalVersion:
                break;
            }
        }
    }
}