public partial class SkillIconsDic {
    
    //public IEnumerator FindSkillIconByCach(string skillID)
    //{
    //    SkillIconDic.TryGetValue(skillID, out GameObject readingSprite);
    //    if (readingSprite != null)
    //    {
    //        yield return readingSprite;
    //        yield break;
    //    }
        
    //    IEnumerator ienObj = CachManager.Instance.getABFromCach("skillIcons", skillID);
    //    while (ienObj.MoveNext())
    //    {
    //        yield return null;
    //    }
    //    AssetBundle readingBundle = null;
    //    if (ienObj.Current != null)
    //    {
    //        readingBundle = (AssetBundle)ienObj.Current;
    //    }
    //    else
    //    {
    //        Debug.Log("技能图标读取失败：" + skillID);
    //        yield return null;
    //        yield break;
    //    }
        
    //    var resultObject = readingBundle.LoadAssetAsync<Image>(skillID);
    //    yield return new WaitWhile(() => resultObject.isDone == false);
        
    //    if (resultObject.asset != null)
    //    {
    //        GameObject pretab = (GameObject)resultObject.asset;
    //        if (pretab != null)
    //            readingSprite = Object.Instantiate(pretab) as GameObject;
    //        else
    //        {
    //            yield return null; 
    //            yield break;
    //        }
    //        DicAdd<string, GameObject>.Add(SkillIconDic, skillID, readingSprite);
    //        readingBundle.Unload(false);
    //    }
    //    else
    //    {
    //        readingBundle.Unload(false);
    //        Debug.Log("图标提取失败"+ skillID);
    //        yield return null;
    //        yield break;
    //    }
    //    yield return readingSprite;
    //}
}
