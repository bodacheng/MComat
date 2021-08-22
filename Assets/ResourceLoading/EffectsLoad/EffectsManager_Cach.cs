using System.Collections;

public partial class EffectsManager
{
    public static IEnumerator PrepareMagicFromCach(string Path,string magicPackName)
    {
        IEnumerator task = CachManager.Instance.DownloadAndCacheExactFile(Path,magicPackName);
        yield return task;
    }    
}
