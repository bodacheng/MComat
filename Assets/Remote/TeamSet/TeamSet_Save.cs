using System;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using System.Collections.Generic;

namespace dataAccess
{
    public static partial class TeamSet
    {
        // 缺乏合理验证环节
        public static void SaveTeamSet(string Mode, Action<bool> success)
        {
            var form = new TeamPos();
            switch (Mode)
            {
                case "arcade":
                    var F = Default.GetInstanceIdOnPos(0);
                    var L = Default.GetInstanceIdOnPos(1);
                    var R = Default.GetInstanceIdOnPos(2);
                    
                    form.l = L;
                    form.f = F;
                    form.r = R;
                    break;
                case "arena":
                    form.f = Arena3V3.GetInstanceIdOnPos(0);
                    form.l = Arena3V3.GetInstanceIdOnPos(1);
                    form.r = Arena3V3.GetInstanceIdOnPos(2);
                    break;
            }
            
            var targetModeCode = "";
            switch (Mode)
            {
                case "arcade":
                    targetModeCode = "arcade";
                    break;
                case "arena":
                    targetModeCode = "arena";
                    break;
            }
            PlayFabReadClient.UpdateUserData(
                new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                    {
                        {targetModeCode, JsonConvert.SerializeObject(form) }
                    }
                },
                success
            );
        }
    }
}