using System.Collections.Generic;
using System;
using mainMenu;

public class MissionWatcher
{
    private readonly IDictionary<string, bool> _missionDic;
    private readonly Action _success, _fail;
    
    public MissionWatcher(List<string> missions, Action success = null, Action fail = null)
    {
        _missionDic = new Dictionary<string, bool>();
        foreach (var missionCode in missions)
        {
            _missionDic.Add(missionCode, false);
        }
        this._success = success;
        this._fail = fail;
    }

    public void Finish(string missionCode, bool value)
    {
        _missionDic[missionCode] = value;
        if (!value)
        {
            // 主动报告一个通信错误的时候才直接执行错误处理
            // 所有通信错误原则上都不该发生，发生了就返回主界面
            _fail?.Invoke();
            PreScene.ReturnToLobby("返回大厅？");
            return;
        }
        
        foreach (var kv in _missionDic)
        {
            if (!kv.Value)
            {
                return;
            }
        }
        _success?.Invoke();
    }
}
