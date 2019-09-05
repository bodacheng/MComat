using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

public partial class CharsManager : MonoBehaviour {

    public Transform _dontDestroyOnLoadParent;
    public static Transform dontDestroyOnLoadParent;
    
    void Awake()
    {
        if (dontDestroyOnLoadParent == null)
            dontDestroyOnLoadParent = _dontDestroyOnLoadParent;
        else
            Debug.Log("已经找到非销毁对象parent");
        DontDestroyOnLoad(dontDestroyOnLoadParent);
    }
    
    public void preventTheseMyModelsFromDestroying(List<string> myCharLocalIDForNextBattle)
    {
        if (myCharLocalIDForNextBattle == null)
            return;

        for (int i = 0; i < myCharLocalIDForNextBattle.Count;i++)
        {
            if (myModelPool.Instance.ModelDicBasedOnPlayerLocalID.ContainsKey(myCharLocalIDForNextBattle[i])
               &&
                myModelPool.Instance.ModelDicBasedOnPlayerLocalID[myCharLocalIDForNextBattle[i]] != null)
            {
                myModelPool.Instance.ModelDicBasedOnPlayerLocalID[myCharLocalIDForNextBattle[i]].transform.parent = dontDestroyOnLoadParent;
            }
        }
    }

    //这个的目的是把加载好的各个角色给放到预定的位置上去。从而把安排角色位置这个工作给从角色生成环节给分离出去。
    public void ArrangeAllCharacterToPosition(MultiDictionary<int,int,Data_Center> heromultiDictionary,MultiDictionary<int,int,Data_Center> enemymultiDictionary,
                                                Transform[] Team1StandPoints, Transform[] Team2StandPoints)
    {
        foreach(KeyValuePair<int,List<int>> keys in heromultiDictionary.getAllUnNullKeys())
        {
            foreach(int key in keys.Value)
            {
                Data_Center character_data_Center = heromultiDictionary.Get(keys.Key,key);
                if (character_data_Center == null)
                {
                    continue;
                }
                switch ((PosNum)key)
                {
                    case PosNum.back:
                        character_data_Center.WholeT.transform.position = Team1StandPoints[0].position;
                        character_data_Center.WholeT.transform.rotation = Team1StandPoints[0].rotation;
                        break;
                    case PosNum.left:
                        character_data_Center.WholeT.transform.position = Team1StandPoints[1].position;
                        character_data_Center.WholeT.transform.rotation = Team1StandPoints[1].rotation;
                        break;
                    case PosNum.front:
                        character_data_Center.WholeT.transform.position = Team1StandPoints[2].position;
                        character_data_Center.WholeT.transform.rotation = Team1StandPoints[2].rotation;
                        break;
                    case PosNum.right:
                        character_data_Center.WholeT.transform.position = Team1StandPoints[3].position;
                        character_data_Center.WholeT.transform.rotation = Team1StandPoints[3].rotation;
                        break;
                }
                character_data_Center.WholeT.parent = null;
                character_data_Center.WholeT.gameObject.SetActive(true);
            }
        }
                
        foreach(KeyValuePair<int,List<int>> keys in enemymultiDictionary.getAllUnNullKeys())
        {
            foreach(int key in keys.Value)
            {
                Data_Center character_data_Center = enemymultiDictionary.Get(keys.Key,key);
                if (character_data_Center == null)
                {
                    continue;
                }
                switch ((PosNum)key)
                {
                    case PosNum.back:
                        character_data_Center.WholeT.transform.position = Team2StandPoints[0].position;
                        character_data_Center.WholeT.transform.rotation = Team2StandPoints[0].rotation;
                        break;
                    case PosNum.left:
                        character_data_Center.WholeT.transform.position = Team2StandPoints[1].position;
                        character_data_Center.WholeT.transform.rotation = Team2StandPoints[1].rotation;
                        break;
                    case PosNum.front:
                        character_data_Center.WholeT.transform.position = Team2StandPoints[2].position;
                        character_data_Center.WholeT.transform.rotation = Team2StandPoints[2].rotation;
                        break;
                    case PosNum.right:
                        character_data_Center.WholeT.transform.position = Team2StandPoints[3].position;
                        character_data_Center.WholeT.transform.rotation = Team2StandPoints[3].rotation;
                        break;
                }
                character_data_Center.WholeT.parent = null;
                character_data_Center.WholeT.gameObject.SetActive(true);
            }
        }
    }
 
    //这个函数是建立在这样的前提下：我们认为从数据库获取的玩家拥有角色，localid是正常的(不重复)
    //如果localid产生重复的情况下这个函数被执行，将产生大量紊乱。
    public IEnumerator buildTheseMyModels(GetMonsterOfPlayerDetailModel[] myChars)
    {
        foreach(GetMonsterOfPlayerDetailModel one in myChars)
        {
            if (one != null)
                yield return (buildShowModel(one));
        }
    }

    public IEnumerator buildShowModel(GetMonsterOfPlayerDetailModel myChar)
    {
        if (myChar == null)
        {
            Debug.Log("流程错误");
            yield break;
        }
        IEnumerator loadshowmodel;
        switch (ResourceLoadingSetting.Instance.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                loadshowmodel = CreateModelForShowingByCach(int.Parse(myChar.monsterId));
                break;
            case ResourceLoadMode.StreamingAssetAB:
                loadshowmodel = CreateModelForShowingByStreamingAssets(int.Parse(myChar.monsterId));
                break;
            case ResourceLoadMode.Resource:
                loadshowmodel = CreateModelForShowingByResource(int.Parse(myChar.monsterId));
                break;
            default:
                loadshowmodel = CreateModelForShowingByResource(int.Parse(myChar.monsterId));
                break;
        }
        yield return loadshowmodel;
        Data_Center targetmodel = (Data_Center)loadshowmodel.Current;
        if (targetmodel != null)
        {
            myModelPool.Instance.addToDic(myChar.monsterOfPlayerId,targetmodel.WholeT.gameObject, myModelPool.Instance.ModelDicBasedOnPlayerLocalID);
        }
    }
    
    //这些都是中间变量
    public IEnumerator CreateCharacter(CharacterDataInfo _CharacterDataInfo)
    {
        IEnumerator buildmodelproess = null;
        switch(ResourceLoadingSetting.Instance.ModelLoadingMode)
        {
                case ResourceLoadMode.CachAB:
                yield return buildmodelproess= (CreateModelForShowingByCach(_CharacterDataInfo.monsterId));
                break;
                case ResourceLoadMode.Resource:
                yield return buildmodelproess = (CreateModelForShowingByResource(_CharacterDataInfo.monsterId));
                break;
                case ResourceLoadMode.StreamingAssetAB:
                yield return buildmodelproess = (CreateModelForShowingByStreamingAssets(_CharacterDataInfo.monsterId));
                break;
        }
        Data_Center _TempDATACENTER = (Data_Center)buildmodelproess.Current;
        if (_TempDATACENTER == null)
        {
            Debug.Log("严重资源类错误");
            yield break;
        }
        
        CharacterResourceInfo _TempCharacterResourceInfo 
        = MonsterConfigInfos._monstersConfigTable.RowToCharacterResourceInfo(MonsterConfigInfos._monstersConfigTable.Find_ID(_CharacterDataInfo.monsterId.ToString()));
        
        yield return (_TempDATACENTER.step2Initialize
            (_TempCharacterResourceInfo.type,
             _CharacterDataInfo._NineAndTwo,
             _CharacterDataInfo.level,
             _TempCharacterResourceInfo._zokusei,
             _TempCharacterResourceInfo.personalMagicPack));
        yield return _TempDATACENTER;
    }    
}