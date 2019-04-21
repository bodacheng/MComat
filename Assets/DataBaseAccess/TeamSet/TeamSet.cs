using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

//站位信息这个事情，非常重要的是和玩家拥有角色信息进行一个校准。
public partial class TeamSet
{
	public static TeamSet instance;
    public positionLocalCharKeySet _positionLocalCharKeySet4V4Mode;// //本单例模式的处理对象

	private TeamSet()
	{
	}
	public static TeamSet Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new TeamSet();
			}
			return instance;
		}
	}

    //整个函数的目的是修复错误的阵容设置
    public void refreshPositionLocalCharKeySet4V4Mode(CharacterDataInfo[] ownedChars)
    {
        if (_positionLocalCharKeySet4V4Mode == null)
            _positionLocalCharKeySet4V4Mode = new positionLocalCharKeySet();

        if (_positionLocalCharKeySet4V4Mode.check4V4ModeTeamPositionNums())//这个是确保PosNumsWithLocalKeys的key没问题
            return;
        else
            _positionLocalCharKeySet4V4Mode.clearSets();

        if (ownedChars == null)
            return;

        List<int> currentLocalKeys = new List<int>();

        foreach (CharacterDataInfo _one in ownedChars)
        {
            if (!currentLocalKeys.Contains(_one.localID))
            {
                currentLocalKeys.Add(_one.localID);
            }
            else
            {
                Debug.Log("LocalID产生重复。这是不该产生的错误，请检查系统结构");
            }
        }

        if (_positionLocalCharKeySet4V4Mode.PosNumsWithLocalKeys != null)//这轮是确保PosNumsWithLocalKeys的value没问题
        {
            foreach (PosNumWithLocalKey _set in _positionLocalCharKeySet4V4Mode.PosNumsWithLocalKeys)
            {
                if (currentLocalKeys.Contains(_set.LocalID))
                {
                }
                else
                {
                    _set.LocalID = -9999;//也就是说设置成一个怪异的值，代表这个位置啥也没有
                }
            }
        }
    }

    public CharacterDataInfo[] myTeamMembersByEntryMemberNum(int playerEntryNum)
    {
        CharacterDataInfo[] team1members = new CharacterDataInfo[playerEntryNum];
        for (int i = 0; i < playerEntryNum; i++)
        {
            PosNum posNum = PosNum.none;
            switch (i)
            {
                case 0:
                    posNum = PosNum.back;
                    break;
                case 1:
                    posNum = PosNum.left;
                    break;
                case 2:
                    posNum = PosNum.front;
                    break;
                case 3:
                    posNum = PosNum.right;
                    break;
            }

            CharacterDataInfo myfighter = AccountCharsSet.getTheCharacterOfMine(this._positionLocalCharKeySet4V4Mode.getPositionLocalID(posNum));
            team1members[i] = myfighter;
        }
        return team1members;
    }
}

// 这个类具体来说就是一个队伍现在的某个位置上到底是我一个账户里哪一个角色。所以monsterbox相关的一些操作里经常牵扯到的是一个什么问题呢...把某个位置的角色给换掉或和其他位置交换的问题
public class prepareSceneCharShowSet
{
    public PosNum positionNum; // positionNum应该和这个实际位置的tansform相对应
	public CharacterDataInfo _CharacterDataInfo;

	public prepareSceneCharShowSet(PosNum positionNum, CharacterDataInfo _CharacterDataInfo)
	{
		this.positionNum = positionNum;
		this._CharacterDataInfo = _CharacterDataInfo;
	}
}

