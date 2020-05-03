using System.Collections;
using UnityEngine;

public partial class EffectsManager
{
    public static IEnumerator PrepareMagicFromStreamingAssets(string magicPackName)
    {
        AssetBundle readingMagicBundle;
        var resultAssetBundle = AssetBundle.LoadFromFileAsync(Application.dataPath + "/StreamingAssets/Magics/" + magicPackName);
        yield return new WaitWhile(() => resultAssetBundle.isDone == false);
        readingMagicBundle = resultAssetBundle.assetBundle;//AB包
        yield return readingMagicBundle;
    }
}
