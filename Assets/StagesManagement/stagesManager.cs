using UnityEngine;
using System.IO;
using System;
using System.Xml.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

//现在一场战斗的信息靠LocalFight去描述，而所谓的随机战斗其实就是根据玩家等级去生成一个LocalFight的实例
public class StagesManager : MonoBehaviour //这个模块本身在正式版本游戏应该是只存在于prepareScene里，进入战斗时候只是带入一个RandomLocalFight的实例用以生成战斗。。。
{
    public string fightScriptPath;
    public TextAsset FightScript;//存档文件。是我们拖给这个位置的一个东西，但如果说这个文件不存在，那应该要自动新建并指定到这个位置上
    public LocalFight editoringFight;
    public FightReward _FightReward;

    //关键在于我们要确保这个存档信息在出错的情况下，在不存在的情况下都怎么样来处理。一个游戏中某存档在一个固定的文件夹下这没有什么问题，为了读取把握这个地址我们要有一些更稳定的策略。
    //这个任务我们在不完全把握Application.dataPath在不同平台运行方式的情况下很难处理好
    public LocalFight LoadOneLocalFight(TextAsset Script)
    {
        LocalFight _localFight;
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LocalFight));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    _localFight = serializer.Deserialize(textReader) as LocalFight;
                }
            }
            else
            {
                var reader = new System.IO.StringReader(Script.text);
                _localFight = serializer.Deserialize(reader) as LocalFight;
                Debug.Log("貌似已经成功读取了4V4模式随机战斗信息");
            }
#if UNITY_EDITOR
            string _path = AssetDatabase.GetAssetPath(Script);
            string[] pathsplit = _path.Split(new string[] { "Assets" }, StringSplitOptions.None);
            _path = _path.Length > 1 ? pathsplit[1] : pathsplit[0];
            Debug.Log("4V4模式文件" + _path);
            fightScriptPath = _path;
#endif
            return _localFight;
        }
        catch (Exception e)
        {
            Debug.Log("4V4战斗信息读取失败");
            Debug.Log(e.ToString());
            return null;
        }
    }

    public void SaveFightAsXml(string path, LocalFight localFight)
    {
        if (localFight == null)
        {
            return;
        }

        localFight.EnemySets.ConvertDictionaryToSerializableArray();
        
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(LocalFight));
            FileStream FileStream;
            FileStream = new FileStream(Application.dataPath + "/" + path, FileMode.Create);
            XmlSerializer.Serialize(FileStream, localFight);
            Debug.Log(Application.dataPath + path + " 尝试进行关卡存储");
            FileStream.Close();
        }
        catch (Exception e)
        {
            Debug.Log("战斗信息保存失败");
            Debug.Log(e.ToString());
        }
    }

    public FightReward LoadFightReward(TextAsset Script)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FightReward));
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //FileStream FileStream = new FileStream(Application.dataPath + pathAndFileName, FileMode.Open);
                //list = XmlSerializer.Deserialize(FileStream) as List<State_Transition_Set>;
                //FileStream.Close();
                using (TextReader textReader = new StringReader(Script.text))
                {
                    _FightReward = serializer.Deserialize(textReader) as FightReward;
                }
            }
            else
            {
                var reader = new System.IO.StringReader(Script.text);
                _FightReward = serializer.Deserialize(reader) as FightReward;
                Debug.Log("貌似已经成功读取了FightReward信息");
            }
#if UNITY_EDITOR
            string _path = AssetDatabase.GetAssetPath(Script);
            string[] pathsplit = _path.Split(new string[] { "Assets" }, StringSplitOptions.None);
            _path = _path.Length > 1 ? pathsplit[1] : pathsplit[0];
            Debug.Log("FightReward" + _path);
            this.fightScriptPath = _path;
#endif
            return _FightReward;
        }
        catch (Exception e)
        {
            Debug.Log("FightReward读取失败");
            Debug.Log(e.ToString());
            return null;
        }
    }

    public void SaveFightRwardAsXml(string path, FightReward _FightReward)
    {
        if (_FightReward == null)
            return;
        try
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(FightReward));
            FileStream FileStream;
            FileStream = new FileStream(Application.dataPath + "/" + path, FileMode.Create);
            XmlSerializer.Serialize(FileStream, _FightReward);
            Debug.Log(Application.dataPath + path + " 尝试FightReward存储");
            FileStream.Close();
        }
        catch (Exception e)
        {
            Debug.Log("_FightReward保存失败");
            Debug.Log(e.ToString());
        }
    }

    //public IEnumerator loadStagesInfo()
    //{
    //    yield return new WaitForSeconds(0);
    //    loadLocalRandom4V4ModeStages();
    //    if (_Random4V4Mode == null)
    //    {
    //        Debug.Log("4V4模式随机战斗关卡信息(整整五个关卡)读取失败，尝试重新构造关卡信息");
    //        _Random4V4Mode = new Random4V4Mode();
    //        refreshStages(100);
    //        save4V4Mode();
    //    }
    //}

    //public LocalFight getALocalFightByPlayerLevel(int playerLevel)
    //{
    //    int battleGroundNum = _BattleGroundDataBase.RandomGetBattleGround().ID;
    //    CharacterDataInfo _zero = _CharsManager.RandomCreateAnEnemy(playerLevel);
    //    CharacterDataInfo _one = _CharsManager.RandomCreateAnEnemy(playerLevel);
    //    CharacterDataInfo _two = _CharsManager.RandomCreateAnEnemy(playerLevel);
    //    CharacterDataInfo _there = _CharsManager.RandomCreateAnEnemy(playerLevel);

    //    CharacterDataInfo[] _enemies = new CharacterDataInfo[] {_zero,_one,_two,_there};
    //    FightReward _FightReward = new FightReward(0,0);
    //    LocalFight _LocalFight = new LocalFight(battleGroundNum,_enemies,_FightReward);

    //    _LocalFight.checkAndFixLocalFight4V4ModePosInfo();
    //    return _LocalFight;
    //}

    //public void refresh4V4ModeSaveData(int playLevel)
    //{
    //    this.refreshStages(playLevel);
    //    save4V4Mode();
    //}

    //public void refreshStages(int playerLevel)
    //{
    //    _Random4V4Mode.stage1 = getALocalFightByPlayerLevel(playerLevel);
    //    _Random4V4Mode.stage2 = getALocalFightByPlayerLevel(playerLevel);
    //    _Random4V4Mode.stage3 = getALocalFightByPlayerLevel(playerLevel);
    //    _Random4V4Mode.stage4 = getALocalFightByPlayerLevel(playerLevel);
    //    _Random4V4Mode.stage5 = getALocalFightByPlayerLevel(playerLevel);
    //}

    //public void loadAndRefresh()
    //{
    //    StartCoroutine(loadStagesInfo());
    //}
}
