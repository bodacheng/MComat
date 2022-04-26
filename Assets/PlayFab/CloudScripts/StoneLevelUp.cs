using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System;
using dataAccess;

public partial class CloudScript
{
    public static void UpdateStone(SkillStoneLevelUpForm form, Action success, Action fail)
    {
        var Items = new List<PlayFab.ServerModels.UpdateUserInventoryItemDataRequest>();
    }
}
