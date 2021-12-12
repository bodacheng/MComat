///////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Welcome to your first Cloud Script revision!
//
// Cloud Script runs in the PlayFab cloud and has full access to the PlayFab Game Server API 
// (https://api.playfab.com/Documentation/Server), and it runs in the context of a securely
// authenticated player, so you can use it to implement logic for your game that is safe from
// client-side exploits. 
//
// Cloud Script functions can also make web requests to external HTTP
// endpoints, such as a database or private API for your title, which makes them a flexible
// way to integrate with your existing backend systems.
//
// There are several different options for calling Cloud Script functions:
//
// 1) Your game client calls them directly using the "ExecuteCloudScript" API,
// passing in the function name and arguments in the request and receiving the 
// function return result in the response.
// (https://api.playfab.com/Documentation/Client/method/ExecuteCloudScript)
// 
// 2) You create PlayStream event actions that call them when a particular 
// event occurs, passing in the event and associated player profile data.
// (https://api.playfab.com/playstream/docs)
// 
// 3) For titles using the Photon Add-on (https://playfab.com/marketplace/photon/),
// Photon room events trigger webhooks which call corresponding Cloud Script functions.
// 
// The following examples demonstrate all three options.
//
///////////////////////////////////////////////////////////////////////////////////////////////////////


// 建立玩家初始数据
handlers.buildBasicData = function (args, context) {

    var updateUserDataResult = server.UpdateUserReadOnlyData({
        PlayFabId: currentPlayerId,
        Data: {
            "stone_box_size": 50,
            "last_Level_completed": 0
        }
    });
    return { messageValue: updateUserDataResult };
};

handlers.completedLevel = function (args, context) {

    var playerData = server.GetUserReadOnlyData({
        PlayFabId: currentPlayerId,
        Keys: ["last_Level_completed"]
    });
    
    var lastLevelCompleted = playerData.Data["last_Level_completed"];
    
    // 传递过来的这个level是玩家试图更新到的进度，但这个数值来自客户端，并不能完全信任
    // 关卡更新机制我们只有一个逻辑就是一次只更新一关
    var level = args.level;
    
    if (level <= lastLevelCompleted.Value) {
        //log.debug("Didnt Set lastLevelCompleted for player ");
        return {
            success: true,
            progressLevel: Number(lastLevelCompleted.Value)
        };
    } else {
        var newLevelCompleted = Number(lastLevelCompleted.Value) + 1;
        var updateUserDataResult = server.UpdateUserReadOnlyData({
            PlayFabId: currentPlayerId,
            Data: {
                "last_Level_completed" : newLevelCompleted
            }
        });

        // 每个关卡的报酬我们是以stage_ + 关卡号码为索引保存在titledata里。
        // g代表金币，d为宝石
        // 我们唯一的担心是
        var stageKey = "stage_" + newLevelCompleted;
        var reward;
        var arr_from_reward_json;
        
        //get title data
        var TitleDataRequest = {"Keys":[stageKey]};
        var TitleDataResponse = server.GetTitleData(TitleDataRequest);
        if(!TitleDataResponse.Data.hasOwnProperty(stageKey))
        {
            //log.debug("报酬信息未找到？ ..." + stageKey);
            return {
                success: true,
                progressLevel: newLevelCompleted,
                gold: 0,
                diamond: 0
            };
        }
        else
        {
            reward = TitleDataResponse.Data[stageKey];
            arr_from_reward_json = JSON.parse(reward);// 这一步被确定有必要
        }
        
        var gold;
        var diamond;
        
        if (arr_from_reward_json !== null)
        {
            if(arr_from_reward_json.hasOwnProperty("d")){
                diamond = arr_from_reward_json.dia;
                log.debug("d" + arr_from_reward_json.dia);
            }else{
                diamond = 0;
            }

            if(arr_from_reward_json.hasOwnProperty("g")){
                gold = arr_from_reward_json.g;
                log.debug("g" + arr_from_reward_json.g);
            }else{
                gold = 0;
            }
            
            if (diamond > 0)
            {
                server.AddUserVirtualCurrency({
                    PlayFabID: currentPlayerId,
                    VirtualCurrency: "DM",
                    Amount: diamond
                });
            }
            
            if (gold > 0)
            {
                server.AddUserVirtualCurrency({
                    PlayFabID: currentPlayerId,
                    VirtualCurrency: "GD",
                    Amount: gold
                });
            }
            
            return {
                success: true,
                progressLevel: newLevelCompleted,
                gold: gold,
                diamond: diamond
            };
        }else{
            return {
                success: true,
                progressLevel: newLevelCompleted,
                gold: 0,
                diamond: 0
            };
        }
    }
};

// 技能石背包只能10个10个的往上买。但是必须应该有一个最大值。这个数字是多少要看这游戏是个什么感觉
handlers.expandBox10 = function (args, context) {
    
    var playerData = server.GetUserReadOnlyData({
        PlayFabId: currentPlayerId,
        Keys: ["stone_box_size"]
    });
    var StoneBoxSize = Number(playerData.Data["stone_box_size"].Value) + Number(10);
    StoneBoxSize = Math.min(Math.max(StoneBoxSize, 0), 200);// 假设最大尺寸是200
    var updateUserDataResult = server.UpdateUserReadOnlyData({
        PlayFabId: currentPlayerId,
        Data: {
            "stone_box_size": StoneBoxSize,
        }
    });
    
    return StoneBoxSize;
};

handlers.claimAllPresentMails = function (args, context) {
    var request = {
        "PlayFabId": currentPlayerId
    };
    var items = server.GetUserInventory(request);
    
    let UnlockedList = [];
    var allDM = 0;
    var allGD = 0;
    for (var i = 0; i < items.Inventory.length; i++) {
        var item = items.Inventory[i];
        if (item.CatalogVersion == "Present")
        {
            var UnlockContainerItemRequest = {
                "PlayFabId": currentPlayerId,
                "CatalogVersion" : item.CatalogVersion,
                "ContainerItemId" : item.ItemId
            };
            var result = server.UnlockContainerItem(UnlockContainerItemRequest);
            if (result.VirtualCurrency.DM) {
                allDM += result.VirtualCurrency.DM;
            }
            if (result.VirtualCurrency.GD) {
                allGD += result.VirtualCurrency.GD;
            }
            UnlockedList.push(result.UnlockedItemInstanceId);
        }
    }
    return {
        diamond: allDM,  
        gold: allGD, 
        UnlockedItemInstanceIds : UnlockedList
    };
}

handlers.skillEdit = function (args, context) {
    let log = [];
    for (let i = 0; i < args.inputValue.length; i++) {
        var item = args.inputValue[i];
        var request = {
            "PlayFabId": currentPlayerId,
            "ItemInstanceId": item.ItemInstanceId,
            "Data": item.Data
        };
        server.UpdateUserInventoryItemCustomData(request);
        // 返回被修改的技能石的id
        log.push({
            "ItemInstanceId": item.ItemInstanceId
        });
    }
    return { messageValue: log };
}

handlers.arenaDefendTeamSave = function (args, context) {
    let members = [];
    for (let i = 0; i < args.inputValue.length; i++) {
        var item = args.inputValue[i];
        members.push(item);
    }
    
    if (members.length != 3) {
        return { success: false  };
    }
    
    var request = {
        "PlayFabId": currentPlayerId,
        "Data": {
            "DefendTeam": JSON.stringify(members)
        }
    };
    var Result = server.UpdateUserData(request);
    return {
        success: true,
        messageValue: members
    };
}

// 竞技场分数+1
// 这个绝不应该是让客户端主动运行而是应该由服务端建立在胜负基准上运行。
handlers.arenaPointUpBy1 = function (args, context) {
    var getRequest = {
        PlayFabId: currentPlayerId
    };
    var playerStats = server.GetPlayerStatistics(getRequest).Statistics;
    var point = 0;
    
    for (i = 0; i < playerStats.length; ++i) {
        if (playerStats[i].StatisticName === "arenapoint") {
            point = playerStats[i].Value + 1;
        }
    }
    
    var playerStatResult = server.UpdatePlayerStatistics({
        PlayFabId: currentPlayerId,
        Statistics: [{
            StatisticName: "arenapoint",
            Value: point
        }]
    });

    var PlayerPosition;
    
    var resultleaderboard = server.GetLeaderboardAroundUser(
        {
            PlayFabID: currentPlayerId,
            StatisticName : args.Leaderboardname,
            MaxResultsCount : 1
        });

    if ((resultleaderboard != null) && (resultleaderboard.Error == null))
    {
        resultleaderboard.Leaderboard.forEach(element => {
            if (element.PlayFabId == currentPlayerId)
            {
                PlayerPosition = element.Position;
                return;
            }
        });
    }
    
    return { "arena point" : point };
};

handlers.GetLeaderboardAroundUser = function (args, context) {
    
    var request = {
        "PlayFabId": currentPlayerId,
        "MaxResultsCount": 4,
        "StatisticName": "arenapoint",
        "ProfileConstraints" : {
            "ShowDisplayName" : true
        }
    };
    var Result = server.GetLeaderboardAroundUser(request);

    let teamInfos = [];
    for (let i = 0; i < Result.Leaderboard.length; i++) {

        var playerTeamData = server.GetUserData({
            PlayFabId: Result.Leaderboard[i].PlayFabId,
            Keys: ["DefendTeam"]
        });

        var item = {
            "PlayerLeaderboardEntry": Result.Leaderboard[i],
            "Team": JSON.parse(playerTeamData.Data["DefendTeam"].Value)
        };
        teamInfos.push(item);
    }
    return { teamInfos };
}

handlers.Gotcha = function (args, context) {
    var request = {
        "CatalogVersion": args.CatalogVersion,
        "TableId": args.tableName
    };
    var Result = server.EvaluateRandomResultTable(request);

    let itemIds = [];
    itemIds.push(Result.ResultItemId);
    var grantRequest = {
        "PlayFabId": currentPlayerId,
        "CatalogVersion": args.CatalogVersion,
        "ItemIds": itemIds
    };

    var grantResult = server.GrantItemsToUser(grantRequest);
    return { messageValue: grantResult["ItemGrantResults"] };
}

handlers.Gotcha = function (args, context) {
    var request = {
        "CatalogVersion": args.CatalogVersion,
        "TableId": args.tableName
    };
    var Result = server.EvaluateRandomResultTable(request);

    let itemIds = [];
    itemIds.push(Result.ResultItemId);
    var grantRequest = {
        "PlayFabId": currentPlayerId,
        "CatalogVersion": args.CatalogVersion,
        "ItemIds": itemIds
    };

    var grantResult = server.GrantItemsToUser(grantRequest);
    return { messageValue: grantResult["ItemGrantResults"] };
}

handlers.GotchaX9 = function (args, context) {
    
    let itemIds = [];
    for (let i = 0; i < 10; i++) {
        var request = {
            "CatalogVersion": args.CatalogVersion,
            "TableId": args.tableName
        };
        var Result = server.EvaluateRandomResultTable(request);
        itemIds.push(Result.ResultItemId);
    }
    
    var grantRequest = {
        "PlayFabId": currentPlayerId,
        "CatalogVersion": args.CatalogVersion,
        "ItemIds": itemIds
    };

    var grantResult = server.GrantItemsToUser(grantRequest);
    return { messageValue: grantResult["ItemGrantResults"] };
}

// 我感觉我们还是应该一点点的按关卡进度把角色给玩家。。
handlers.getMonsterTest = function (args, context) {
    var request = {
        "CatalogVersion": "Monsters",
        "ItemGrants": [
            {"PlayFabId": currentPlayerId,"ItemId": "1"},
            {"PlayFabId": currentPlayerId,"ItemId": "2"},
            {"PlayFabId": currentPlayerId,"ItemId": "3"},
            {"PlayFabId": currentPlayerId,"ItemId": "4"},
            {"PlayFabId": currentPlayerId,"ItemId": "5"},
            {"PlayFabId": currentPlayerId, "ItemId": "6" },
            { "PlayFabId": currentPlayerId, "ItemId": "7" },
            { "PlayFabId": currentPlayerId, "ItemId": "8" },
            { "PlayFabId": currentPlayerId, "ItemId": "9" },
            { "PlayFabId": currentPlayerId, "ItemId": "10" },
            { "PlayFabId": currentPlayerId, "ItemId": "11" },
            { "PlayFabId": currentPlayerId, "ItemId": "13" }
        ]
    };
    var playerStatResult = server.GrantItemsToUsers(request);
};

handlers.getStonesTest = function (args, context) {var request = {"CatalogVersion": "stoneTest2","ItemGrants": [{"PlayFabId": currentPlayerId,"ItemId": "1"},{"PlayFabId": currentPlayerId,"ItemId": "2"},{"PlayFabId": currentPlayerId,"ItemId": "3"},{"PlayFabId": currentPlayerId,"ItemId": "4"},{"PlayFabId": currentPlayerId,"ItemId": "5"},{"PlayFabId": currentPlayerId,"ItemId": "6"},{"PlayFabId": currentPlayerId,"ItemId": "7"},{"PlayFabId": currentPlayerId,"ItemId": "8"},{"PlayFabId": currentPlayerId,"ItemId": "9"},{"PlayFabId": currentPlayerId,"ItemId": "10"},{"PlayFabId": currentPlayerId,"ItemId": "11"},{"PlayFabId": currentPlayerId,"ItemId": "12"},{"PlayFabId": currentPlayerId,"ItemId": "13"},{"PlayFabId": currentPlayerId,"ItemId": "14"},{"PlayFabId": currentPlayerId,"ItemId": "15"},{"PlayFabId": currentPlayerId,"ItemId": "16"},{"PlayFabId": currentPlayerId,"ItemId": "17"},{"PlayFabId": currentPlayerId,"ItemId": "18"},{"PlayFabId": currentPlayerId,"ItemId": "19"},{"PlayFabId": currentPlayerId,"ItemId": "20"},{"PlayFabId": currentPlayerId,"ItemId": "21"},{"PlayFabId": currentPlayerId,"ItemId": "22"},{"PlayFabId": currentPlayerId,"ItemId": "23"},{"PlayFabId": currentPlayerId,"ItemId": "24"},{"PlayFabId": currentPlayerId,"ItemId": "25"},{"PlayFabId": currentPlayerId,"ItemId": "26"},{"PlayFabId": currentPlayerId,"ItemId": "27"},{"PlayFabId": currentPlayerId,"ItemId": "28"},{"PlayFabId": currentPlayerId,"ItemId": "29"},{"PlayFabId": currentPlayerId,"ItemId": "30"},{"PlayFabId": currentPlayerId,"ItemId": "31"},{"PlayFabId": currentPlayerId,"ItemId": "32"},{"PlayFabId": currentPlayerId,"ItemId": "33"},{"PlayFabId": currentPlayerId,"ItemId": "34"},{"PlayFabId": currentPlayerId,"ItemId": "35"},{"PlayFabId": currentPlayerId,"ItemId": "36"},{"PlayFabId": currentPlayerId,"ItemId": "37"},{"PlayFabId": currentPlayerId,"ItemId": "38"},{"PlayFabId": currentPlayerId,"ItemId": "39"},{"PlayFabId": currentPlayerId,"ItemId": "40"},{"PlayFabId": currentPlayerId,"ItemId": "41"},{"PlayFabId": currentPlayerId,"ItemId": "42"},{"PlayFabId": currentPlayerId,"ItemId": "43"},{"PlayFabId": currentPlayerId,"ItemId": "44"},{"PlayFabId": currentPlayerId,"ItemId": "45"},{"PlayFabId": currentPlayerId,"ItemId": "46"},{"PlayFabId": currentPlayerId,"ItemId": "47"},{"PlayFabId": currentPlayerId,"ItemId": "48"},{"PlayFabId": currentPlayerId,"ItemId": "49"},{"PlayFabId": currentPlayerId,"ItemId": "50"},{"PlayFabId": currentPlayerId,"ItemId": "51"},{"PlayFabId": currentPlayerId,"ItemId": "52"},{"PlayFabId": currentPlayerId,"ItemId": "53"},{"PlayFabId": currentPlayerId,"ItemId": "54"},{"PlayFabId": currentPlayerId,"ItemId": "55"},{"PlayFabId": currentPlayerId,"ItemId": "56"},{"PlayFabId": currentPlayerId,"ItemId": "57"},{"PlayFabId": currentPlayerId,"ItemId": "58"},{"PlayFabId": currentPlayerId,"ItemId": "59"},{"PlayFabId": currentPlayerId,"ItemId": "60"},{"PlayFabId": currentPlayerId,"ItemId": "61"},{"PlayFabId": currentPlayerId,"ItemId": "62"},{"PlayFabId": currentPlayerId,"ItemId": "63"},{"PlayFabId": currentPlayerId,"ItemId": "64"},{"PlayFabId": currentPlayerId,"ItemId": "65"},{"PlayFabId": currentPlayerId,"ItemId": "66"},{"PlayFabId": currentPlayerId,"ItemId": "67"},{"PlayFabId": currentPlayerId,"ItemId": "68"},{"PlayFabId": currentPlayerId,"ItemId": "69"},{"PlayFabId": currentPlayerId,"ItemId": "70"},{"PlayFabId": currentPlayerId,"ItemId": "71"},{"PlayFabId": currentPlayerId,"ItemId": "72"},{"PlayFabId": currentPlayerId,"ItemId": "73"},{"PlayFabId": currentPlayerId,"ItemId": "74"},{"PlayFabId": currentPlayerId,"ItemId": "75"},{"PlayFabId": currentPlayerId,"ItemId": "76"},{"PlayFabId": currentPlayerId,"ItemId": "77"},{"PlayFabId": currentPlayerId,"ItemId": "78"},{"PlayFabId": currentPlayerId,"ItemId": "79"},{"PlayFabId": currentPlayerId,"ItemId": "80"},{"PlayFabId": currentPlayerId,"ItemId": "81"},{"PlayFabId": currentPlayerId,"ItemId": "82"},{"PlayFabId": currentPlayerId,"ItemId": "83"},{"PlayFabId": currentPlayerId,"ItemId": "84"},{"PlayFabId": currentPlayerId,"ItemId": "85"},{"PlayFabId": currentPlayerId,"ItemId": "86"},{"PlayFabId": currentPlayerId,"ItemId": "87"},{"PlayFabId": currentPlayerId,"ItemId": "88"},{"PlayFabId": currentPlayerId,"ItemId": "89"},{"PlayFabId": currentPlayerId,"ItemId": "90"},{"PlayFabId": currentPlayerId,"ItemId": "91"},{"PlayFabId": currentPlayerId,"ItemId": "92"},{"PlayFabId": currentPlayerId,"ItemId": "93"},{"PlayFabId": currentPlayerId,"ItemId": "94"},{"PlayFabId": currentPlayerId,"ItemId": "95"},{"PlayFabId": currentPlayerId,"ItemId": "96"},{"PlayFabId": currentPlayerId,"ItemId": "97"},{"PlayFabId": currentPlayerId,"ItemId": "98"},{"PlayFabId": currentPlayerId,"ItemId": "99"},{"PlayFabId": currentPlayerId,"ItemId": "100"},{"PlayFabId": currentPlayerId,"ItemId": "101"},{"PlayFabId": currentPlayerId,"ItemId": "102"},{"PlayFabId": currentPlayerId,"ItemId": "103"},{"PlayFabId": currentPlayerId,"ItemId": "104"},{"PlayFabId": currentPlayerId,"ItemId": "105"},{"PlayFabId": currentPlayerId,"ItemId": "106"},{"PlayFabId": currentPlayerId,"ItemId": "107"},{"PlayFabId": currentPlayerId,"ItemId": "108"},{"PlayFabId": currentPlayerId,"ItemId": "109"},{"PlayFabId": currentPlayerId,"ItemId": "110"},{"PlayFabId": currentPlayerId,"ItemId": "111"},{"PlayFabId": currentPlayerId,"ItemId": "112"},{"PlayFabId": currentPlayerId,"ItemId": "113"},{"PlayFabId": currentPlayerId,"ItemId": "114"},{"PlayFabId": currentPlayerId,"ItemId": "115"},{"PlayFabId": currentPlayerId,"ItemId": "116"},{"PlayFabId": currentPlayerId,"ItemId": "117"},{"PlayFabId": currentPlayerId,"ItemId": "118"},{"PlayFabId": currentPlayerId,"ItemId": "119"},{"PlayFabId": currentPlayerId,"ItemId": "120"},{"PlayFabId": currentPlayerId,"ItemId": "121"},{"PlayFabId": currentPlayerId,"ItemId": "122"},{"PlayFabId": currentPlayerId,"ItemId": "123"},{"PlayFabId": currentPlayerId,"ItemId": "124"},{"PlayFabId": currentPlayerId,"ItemId": "125"},{"PlayFabId": currentPlayerId,"ItemId": "126"},{"PlayFabId": currentPlayerId,"ItemId": "127"},{"PlayFabId": currentPlayerId,"ItemId": "128"},{"PlayFabId": currentPlayerId,"ItemId": "129"},{"PlayFabId": currentPlayerId,"ItemId": "130"},{"PlayFabId": currentPlayerId,"ItemId": "131"},{"PlayFabId": currentPlayerId,"ItemId": "132"},{"PlayFabId": currentPlayerId,"ItemId": "133"},{"PlayFabId": currentPlayerId,"ItemId": "134"},{"PlayFabId": currentPlayerId,"ItemId": "135"},{"PlayFabId": currentPlayerId,"ItemId": "136"},{"PlayFabId": currentPlayerId,"ItemId": "137"},{"PlayFabId": currentPlayerId,"ItemId": "138"},{"PlayFabId": currentPlayerId,"ItemId": "139"},{"PlayFabId": currentPlayerId,"ItemId": "140"},{"PlayFabId": currentPlayerId,"ItemId": "141"},{"PlayFabId": currentPlayerId,"ItemId": "142"},{"PlayFabId": currentPlayerId,"ItemId": "143"},{"PlayFabId": currentPlayerId,"ItemId": "144"},{"PlayFabId": currentPlayerId,"ItemId": "145"},{"PlayFabId": currentPlayerId,"ItemId": "146"},{"PlayFabId": currentPlayerId,"ItemId": "147"},{"PlayFabId": currentPlayerId,"ItemId": "148"},{"PlayFabId": currentPlayerId,"ItemId": "149"},{"PlayFabId": currentPlayerId,"ItemId": "150"},{"PlayFabId": currentPlayerId,"ItemId": "151"},{"PlayFabId": currentPlayerId,"ItemId": "152"},{"PlayFabId": currentPlayerId,"ItemId": "153"},{"PlayFabId": currentPlayerId,"ItemId": "156"},{"PlayFabId": currentPlayerId,"ItemId": "154"},{"PlayFabId": currentPlayerId,"ItemId": "155"},{"PlayFabId": currentPlayerId,"ItemId": "157"},{"PlayFabId": currentPlayerId,"ItemId": "158"},{"PlayFabId": currentPlayerId,"ItemId": "159"},{"PlayFabId": currentPlayerId,"ItemId": "160"},{"PlayFabId": currentPlayerId,"ItemId": "161"},{"PlayFabId": currentPlayerId,"ItemId": "162"},{"PlayFabId": currentPlayerId,"ItemId": "163"},{"PlayFabId": currentPlayerId,"ItemId": "164"},{"PlayFabId": currentPlayerId,"ItemId": "165"},{"PlayFabId": currentPlayerId,"ItemId": "166"},{"PlayFabId": currentPlayerId,"ItemId": "167"},{"PlayFabId": currentPlayerId,"ItemId": "168"},{"PlayFabId": currentPlayerId,"ItemId": "169"},{"PlayFabId": currentPlayerId,"ItemId": "170"},{"PlayFabId": currentPlayerId,"ItemId": "171"},{"PlayFabId": currentPlayerId,"ItemId": "172"},{"PlayFabId": currentPlayerId,"ItemId": "173"},{"PlayFabId": currentPlayerId,"ItemId": "174"},{"PlayFabId": currentPlayerId,"ItemId": "175"},{"PlayFabId": currentPlayerId,"ItemId": "176"},{"PlayFabId": currentPlayerId,"ItemId": "177"},{"PlayFabId": currentPlayerId,"ItemId": "178"},{"PlayFabId": currentPlayerId,"ItemId": "179"},{"PlayFabId": currentPlayerId,"ItemId": "180"},{"PlayFabId": currentPlayerId,"ItemId": "181"},{"PlayFabId": currentPlayerId,"ItemId": "182"} ]};var playerStatResult = server.GrantItemsToUsers(request);};

handlers.Remove25Stones = function (args, context) {

    var request = {
        "PlayFabId": currentPlayerId
    };

    var items = server.GetUserInventory(request);

    let toRemove = [];
    var deletedCount = 0;
    for (var i = 0; i < items.Inventory.length; i++) {

        if (items.Inventory[i].CatalogVersion != "stoneTest2")
            continue;

        var item = {
            "ItemInstanceId": items.Inventory[i].ItemInstanceId,
            "PlayFabId": currentPlayerId
        };
        toRemove.push(item);
        if ((toRemove.length == 25) || (i == items.Inventory.length - 1)) {
            var deleteRequest = {
                "Items": toRemove
            };
            deletedCount += toRemove.length;
            var Result = server.RevokeInventoryItems(deleteRequest);
            break;
        }
    }
    var currentItemCount = Number(items.Inventory.length) - Number(deletedCount);
    return { currentItemCount: currentItemCount};
}

// This an example of a function that calls a PlayFab Entity API. The function is called using the 
// 'ExecuteEntityCloudScript' API (https://api.playfab.com/documentation/CloudScript/method/ExecuteEntityCloudScript).
handlers.makeEntityAPICall = function (args, context) {

    // The profile of the entity specified in the 'ExecuteEntityCloudScript' request.
    // Defaults to the authenticated entity in the X-EntityToken header.
    var entityProfile = context.currentEntity;

    // The pre-defined 'entity' object has functions corresponding to each PlayFab Entity API,
    // including 'SetObjects' (https://api.playfab.com/documentation/Data/method/SetObjects).
    var apiResult = entity.SetObjects({
        Entity: entityProfile.Entity,
        Objects: [
            {
                ObjectName: "obj1",
                DataObject: {
                    foo: "some server computed value",
                    prop1: args.prop1
                }
            }
        ]
    });

    return {
        profile: entityProfile,
        setResult: apiResult.SetResults[0].SetResult
    };
};

// This is a simple example of making a web request to an external HTTP API.
handlers.makeHTTPRequest = function (args, context) {
    var headers = {
        "X-MyCustomHeader": "Some Value"
    };
    
    var body = {
        input: args,
        userId: currentPlayerId,
        mode: "foobar"
    };

    var url = "http://httpbin.org/status/200";
    var content = JSON.stringify(body);
    var httpMethod = "post";
    var contentType = "application/json";

    // The pre-defined http object makes synchronous HTTP requests
    var response = http.request(url, httpMethod, content, contentType, headers);
    return { responseContent: response };
};

// This is a simple example of a function that is called from a
// PlayStream event action. (https://playfab.com/introducing-playstream/)
handlers.handlePlayStreamEventAndProfile = function (args, context) {
    
    // The event that triggered the action 
    // (https://api.playfab.com/playstream/docs/PlayStreamEventModels)
    var psEvent = context.playStreamEvent;
    
    // The profile data of the player associated with the event
    // (https://api.playfab.com/playstream/docs/PlayStreamProfileModels)
    var profile = context.playerProfile;
    
    // Post data about the event to an external API
    var content = JSON.stringify({ user: profile.PlayerId, event: psEvent.EventName });
    var response = http.request('https://httpbin.org/status/200', 'post', content, 'application/json', null);

    return { externalAPIResponse: response };
};


// Below are some examples of using Cloud Script in slightly more realistic scenarios

// This is a function that the game client would call whenever a player completes
// a level. It updates a setting in the player's data that only game server
// code can write - it is read-only on the client - and it updates a player
// statistic that can be used for leaderboards. 
//
// A funtion like this could be extended to perform validation on the 
// level completion data to detect cheating. It could also do things like 
// award the player items from the game catalog based on their performance.

/////// Check In System

// defining these up top so we can easily change these later if we need to.
var CHECK_IN_TRACKER = "CheckInTracker";    				// used as a key on the UserPublisherReadOnlyData
var PROGRESSIVE_REWARD_TABLE = "ProgressiveRewardTable";	// TitleData key that contains the reward details
var PROGRESSIVE_MIN_CREDITS = "MinStreak";					// PROGRESSIVE_REWARD_TABLE property denoting the minium number of logins to be eligible for this item 
var PROGRESSIVE_REWARD = "Reward";							// PROGRESSIVE_REWARD_TABLE property denoting what item gets rewarded at this level
var TRACKER_NEXT_GRANT = "NextEligibleGrant";				// CHECK_IN_TRACKER property containing the time at which we 
var TRACKER_LOGIN_STREAK = "LoginStreak";					// CHECK_IN_TRACKER property containing the streak length


handlers.CheckIn = function(args) {

    var GetUserReadOnlyDataRequest = {
        "PlayFabId": currentPlayerId,
        "Keys": [ CHECK_IN_TRACKER ]
    };
    var GetUserReadOnlyDataResponse = server.GetUserReadOnlyData(GetUserReadOnlyDataRequest);

    // need to ensure that our data field exists
    var tracker = {}; // this would be the first login ever (across any title), so we have to make sure our record exists.
    if(GetUserReadOnlyDataResponse.Data.hasOwnProperty(CHECK_IN_TRACKER))
    {
        tracker = JSON.parse(GetUserReadOnlyDataResponse.Data[CHECK_IN_TRACKER].Value);
    }
    else
    {
        tracker = ResetTracker();

        // write back updated data to PlayFab
        UpdateTrackerData(tracker);

        log.info("This was your first login, Login tomorrow to get a bonus!");
        return JSON.stringify([]);
    }


    if(Date.now() > parseInt(tracker[TRACKER_NEXT_GRANT]))
    {
        // Eligible for an item grant.
        //check to ensure that it has been less than 24 hours since the last grant window opened
        var timeWindow = new Date(parseInt(tracker[TRACKER_NEXT_GRANT]));
        timeWindow.setDate(timeWindow.getDate() + 1); // add 1 day 

        if(Date.now() > timeWindow.getTime())
        {
            // streak ended :(			
            tracker = ResetTracker();
            UpdateTrackerData(tracker);

            log.info("Your consecutive login streak has been broken. Login tomorrow to get a bonus!");
            return JSON.stringify([]);
        }

        // streak continues
        tracker[TRACKER_LOGIN_STREAK] += 1;
        var dateObj = new Date(Date.now());
        dateObj.setDate(dateObj.getDate() + 1); // add one day 
        tracker[TRACKER_NEXT_GRANT] = dateObj.getTime();

        // write back updated data to PlayFab
        log.info("Your consecutive login streak increased to: " + tracker[TRACKER_LOGIN_STREAK]);
        UpdateTrackerData(tracker);

        // Get this title's reward table so we know what items to grant. 
        var GetTitleDataRequest = {
            "Keys": [ PROGRESSIVE_REWARD_TABLE ]
        };
        var GetTitleDataResult = server.GetTitleData(GetTitleDataRequest);

        // ---
        if(!GetTitleDataResult.Data.hasOwnProperty(PROGRESSIVE_REWARD_TABLE))
        {
            log.error("Rewards table could not be found. No rewards will be given. Exiting...");
            return JSON.stringify([]);
        }
        else
        {
            // parse our reward table
            var rewardTable = JSON.parse(GetTitleDataResult.Data[PROGRESSIVE_REWARD_TABLE]);

            // find a matching reward 
            var reward;
            for(var level in rewardTable)
            {
                if( tracker[TRACKER_LOGIN_STREAK] >= rewardTable[level][PROGRESSIVE_MIN_CREDITS])
                {
                    reward = rewardTable[level][PROGRESSIVE_REWARD];
                }
            }

            // make grants and pass info back to the client.
            var grantedItems = [];
            if(reward)
            {
                grantedItems = GrantItems(reward, tracker[TRACKER_LOGIN_STREAK]);
            }
            return JSON.stringify(grantedItems);
        }
    }

    return JSON.stringify([]);
};


function ResetTracker()
{
    var reset = {};

    reset[TRACKER_LOGIN_STREAK] = 1;

    var dateObj = new Date(Date.now());
    dateObj.setDate(dateObj.getDate() + 1); // add one day 

    reset[TRACKER_NEXT_GRANT] = dateObj.getTime();
    return JSON.stringify(reset);
}


function UpdateTrackerData(data)
{
    var UpdateUserReadOnlyDataRequest = {
        "PlayFabId": currentPlayerId,
        "Data": {}
    };
    UpdateUserReadOnlyDataRequest.Data[CHECK_IN_TRACKER] = JSON.stringify(data);

    server.UpdateUserReadOnlyData(UpdateUserReadOnlyDataRequest);
}


function GrantItems(items, count)
{
    log.info("Granting: " + items);
    var parsed = Array.isArray(items) ? items : [ items ];

    var GrantItemsToUserRequest = {
        "PlayFabId" : currentPlayerId,
        "ItemIds" : parsed,
        "Annotation" : "Granted for logging in over " + count + " consecutive days."
    };

    var GrantItemsToUserResult = server.GrantItemsToUser(GrantItemsToUserRequest);
    return JSON.stringify(GrantItemsToUserResult.ItemGrantResults);
}


///////


// In addition to the Cloud Script handlers, you can define your own functions and call them from your handlers. 
// This makes it possible to share code between multiple handlers and to improve code organization.
handlers.updatePlayerMove = function (args) {
    var validMove = processPlayerMove(args);
    return { validMove: validMove };
};


// This is a helper function that verifies that the player's move wasn't made
// too quickly following their previous move, according to the rules of the game.
// If the move is valid, then it updates the player's statistics and profile data.
// This function is called from the "UpdatePlayerMove" handler above and also is 
// triggered by the "RoomEventRaised" Photon room event in the Webhook handler
// below. 
//
// For this example, the script defines the cooldown period (playerMoveCooldownInSeconds)
// as 15 seconds. A recommended approach for values like this would be to create them in Title
// Data, so that they can be queries in the script with a call to GetTitleData
// (https://api.playfab.com/Documentation/Server/method/GetTitleData). This would allow you to
// make adjustments to these values over time, without having to edit, test, and roll out an
// updated script.
function processPlayerMove(playerMove) {
    var now = Date.now();
    var playerMoveCooldownInSeconds = 15;

    var playerData = server.GetUserInternalData({
        PlayFabId: currentPlayerId,
        Keys: ["last_move_timestamp"]
    });

    var lastMoveTimestampSetting = playerData.Data["last_move_timestamp"];

    if (lastMoveTimestampSetting) {
        var lastMoveTime = Date.parse(lastMoveTimestampSetting.Value);
        var timeSinceLastMoveInSeconds = (now - lastMoveTime) / 1000;
        log.debug("lastMoveTime: " + lastMoveTime + " now: " + now + " timeSinceLastMoveInSeconds: " + timeSinceLastMoveInSeconds);

        if (timeSinceLastMoveInSeconds < playerMoveCooldownInSeconds) {
            log.error("Invalid move - time since last move: " + timeSinceLastMoveInSeconds + "s less than minimum of " + playerMoveCooldownInSeconds + "s.");
            return false;
        }
    }

    var playerStats = server.GetPlayerStatistics({
        PlayFabId: currentPlayerId
    }).Statistics;
    var movesMade = 0;
    for (var i = 0; i < playerStats.length; i++)
        if (playerStats[i].StatisticName === "")
            movesMade = playerStats[i].Value;
    movesMade += 1;
    var request = {
        PlayFabId: currentPlayerId, Statistics: [{
                StatisticName: "movesMade",
                Value: movesMade
            }]
    };
    server.UpdatePlayerStatistics(request);
    server.UpdateUserInternalData({
        PlayFabId: currentPlayerId,
        Data: {
            last_move_timestamp: new Date(now).toUTCString(),
            last_move: JSON.stringify(playerMove)
        }
    });

    return true;
}

// This is an example of using PlayStream real-time segmentation to trigger
// game logic based on player behavior. (https://playfab.com/introducing-playstream/)
// The function is called when a player_statistic_changed PlayStream event causes a player 
// to enter a segment defined for high skill players. It sets a key value in
// the player's internal data which unlocks some new content for the player.
handlers.unlockHighSkillContent = function (args, context) {
    var playerStatUpdatedEvent = context.playStreamEvent;
    var request = {
        PlayFabId: currentPlayerId,
        Data: {
            "HighSkillContent": "true",
            "XPAtHighSkillUnlock": playerStatUpdatedEvent.StatisticValue.toString()
        }
    };
    var playerInternalData = server.UpdateUserInternalData(request);
    log.info('Unlocked HighSkillContent for ' + context.playerProfile.DisplayName);
    return { profile: context.playerProfile };
};

// Photon Webhooks Integration
//
// The following functions are examples of Photon Cloud Webhook handlers. 
// When you enable the Photon Add-on (https://playfab.com/marketplace/photon/)
// in the Game Manager, your Photon applications are automatically configured
// to authenticate players using their PlayFab accounts and to fire events that 
// trigger your Cloud Script Webhook handlers, if defined. 
// This makes it easier than ever to incorporate multiplayer server logic into your game.


// Triggered automatically when a Photon room is first created
handlers.RoomCreated = function (args) {
    log.debug("Room Created - Game: " + args.GameId + " MaxPlayers: " + args.CreateOptions.MaxPlayers);
};

// Triggered automatically when a player joins a Photon room
handlers.RoomJoined = function (args) {
    log.debug("Room Joined - Game: " + args.GameId + " PlayFabId: " + args.UserId);
};

// Triggered automatically when a player leaves a Photon room
handlers.RoomLeft = function (args) {
    log.debug("Room Left - Game: " + args.GameId + " PlayFabId: " + args.UserId);
};

// Triggered automatically when a Photon room closes
// Note: currentPlayerId is undefined in this function
handlers.RoomClosed = function (args) {
    log.debug("Room Closed - Game: " + args.GameId);
};

// Triggered automatically when a Photon room game property is updated.
// Note: currentPlayerId is undefined in this function
handlers.RoomPropertyUpdated = function (args) {
    log.debug("Room Property Updated - Game: " + args.GameId);
};

// Triggered by calling "OpRaiseEvent" on the Photon client. The "args.Data" property is 
// set to the value of the "customEventContent" HashTable parameter, so you can use
// it to pass in arbitrary data.
handlers.RoomEventRaised = function (args) {
    var eventData = args.Data;
    log.debug("Event Raised - Game: " + args.GameId + " Event Type: " + eventData.eventType);

    switch (eventData.eventType) {
        case "playerMove":
            processPlayerMove(eventData);
            break;

        default:
            break;
    }
};
