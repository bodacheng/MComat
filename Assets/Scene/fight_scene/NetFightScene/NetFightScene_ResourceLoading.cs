using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public partial class NetFightScene : MonoBehaviour{
    public IEnumerator generateTeamChar(IDictionary<int, GameObject> ReferenceDic, int IDinReferenceDic, CharacterDataInfo characterDataInfo, Team team)
    {
        if (characterDataInfo == null)
        {
            yield break;
        }
        yield return (_CharSetManager.
            CreateCharacter(
            ReferenceDic,
            IDinReferenceDic,
            characterDataInfo,
           team));

    }

    // 通用系函数 根据玩家档案来生成角色。
    // CharacterDataInfo[] 在这里是参数，那么意义上来说就是要根据这个信息来生成角色，不再去索引任何其他信息才对。
    private IEnumerator LoadATeam(CharacterDataInfo[] characters, IDictionary<int, GameObject> ReferenceDic, Team team)
    {
        foreach (KeyValuePair<int, GameObject> _keyValuePair in ReferenceDic)
        {
            if (_keyValuePair.Value != null)
                _keyValuePair.Value.SetActive(false);
        }
        foreach (CharacterDataInfo _one in characters)
        {
            yield return (generateTeamChar(ReferenceDic, _one.localID, _one, team));
        }
    }
}
