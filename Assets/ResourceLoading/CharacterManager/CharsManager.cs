using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;

public partial class CharsManager : MonoBehaviour {

    public static CharsManager target;
    
    void Start()
    {
        target = this;
    }

    public void PreventTheseMyModelsFromDestroying(List<string> myCharLocalIDForNextBattle)
    {
        if (myCharLocalIDForNextBattle == null)
            return;

        for (int i = 0; i < myCharLocalIDForNextBattle.Count;i++)
        {
            if (myCharLocalIDForNextBattle[i] == null)
            {
                continue;
            }
            if (MyModelPool.Instance.ModelDicBasedOnPlayerLocalID.ContainsKey(myCharLocalIDForNextBattle[i])
               &&
                MyModelPool.Instance.ModelDicBasedOnPlayerLocalID[myCharLocalIDForNextBattle[i]] != null)
            {
                MyModelPool.Instance.ModelDicBasedOnPlayerLocalID[myCharLocalIDForNextBattle[i]].transform.parent = ResourceKeeper.dontDestroyOnLoadParent;
            }
        }
    }
    
    //这个函数是建立在这样的前提下：我们认为从数据库获取的玩家拥有角色，localid是正常的(不重复)
    //如果localid产生重复的情况下这个函数被执行，将产生大量紊乱。
    public IEnumerator BuildTheseMyModels(GetMonsterOfPlayerDetailModel[] myChars)
    {
        foreach(GetMonsterOfPlayerDetailModel one in myChars)
        {
            if (one != null)
                yield return (BuildShowModel(one));
        }
    }

    public IEnumerator BuildShowModel(GetMonsterOfPlayerDetailModel myChar)
    {
        if (myChar == null)
        {
            yield break;
        }
        IEnumerator loadshowmodel;
        switch (ResourceLoadingSetting.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                loadshowmodel = CreateModelForShowingByCach(myChar.monsterId);
                break;
            case ResourceLoadMode.StreamingAssetAB:
                loadshowmodel = CreateModelForShowingByStreamingAssets(myChar.monsterId);
                break;
            case ResourceLoadMode.Resource:
                loadshowmodel = CreateModelForShowingByResource(myChar.monsterId);
                break;
            default:
                loadshowmodel = CreateModelForShowingByResource(myChar.monsterId);
                break;
        }
        yield return loadshowmodel;
        Data_Center targetmodel = (Data_Center)loadshowmodel.Current;
        if (targetmodel != null)
        {
            MyModelPool.Instance.AddToDic(myChar.monsterOfPlayerId,targetmodel.WholeT.gameObject, MyModelPool.Instance.ModelDicBasedOnPlayerLocalID);
        }
    }
    
    //这些都是中间变量
    public IEnumerator CreateCharacter(CharDataInfo _CharacterDataInfo)
    {
        IEnumerator buildmodelproess = null;
        switch(ResourceLoadingSetting.ModelLoadingMode)
        {
            case ResourceLoadMode.CachAB:
            yield return buildmodelproess= (CreateModelForShowingByCach(_CharacterDataInfo.ResourceID));
            break;
            case ResourceLoadMode.Resource:
            yield return buildmodelproess = (CreateModelForShowingByResource(_CharacterDataInfo.ResourceID));
            break;
            case ResourceLoadMode.StreamingAssetAB:
            yield return buildmodelproess = (CreateModelForShowingByStreamingAssets(_CharacterDataInfo.ResourceID));
            break;
        }
        Data_Center _TempDATACENTER = (Data_Center)buildmodelproess.Current;
        if (_TempDATACENTER == null)
        {
            Debug.Log("严重资源类错误");
            yield break;
        }
        CharConfig _TempCharacterResourceInfo = MonstersConfigTable.Instance.RowToCharConfigInfo(MonstersConfigTable.Instance.Find_RECORD_ID(_CharacterDataInfo.ResourceID.ToString()));
        yield return (_TempDATACENTER.Step2Initialize
            (_TempCharacterResourceInfo.TYPE,
             _CharacterDataInfo._NineAndTwo,
             _TempCharacterResourceInfo._zokusei,
             _TempCharacterResourceInfo.SPECIAL_ZOKUSEI));
        yield return _TempDATACENTER;
    }    
}