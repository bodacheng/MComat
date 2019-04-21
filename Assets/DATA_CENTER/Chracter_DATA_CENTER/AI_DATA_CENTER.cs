using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//我们可能把角色在战斗中的一些和实时战斗相关的基础数值放在这个环节。
// 因为首先这有利于规定好这些值的赋值规则。比如说最大ex槽不能超过多少这类逻辑。
// 但比如说HP这东西，放在这儿的话
[System.Serializable]
public class playerBattleInfo 
{
    public int maxHP = 100;
    public int power = 1;

    public playerBattleInfo()
    {
        maxHP = 200;
        power = 1;
    }

    public int MaxHP
    {
        get
        {
            //Some other code
            return maxHP;
        }
        set
        {
            //Some other code
            maxHP = (int)Mathf.Clamp(value, 0, Mathf.Infinity);//就是说角色最大ex槽最大100呗。
        }
    }
    public int AT
    {
        get
        {
            //Some other code
            return power;
        }
        set
        {
            //Some other code
            power = (int)Mathf.Clamp(value, 0f, Mathf.Infinity);//就是说角色最大ex槽最大100呗。
        }
    }
}

public class AI_DATA_CENTER : Data_Center
{
    //  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //  {
    //if (PhotonNetwork.connected && !PhotonNetwork.offlineMode) 
    //{
    //	Debug.Log ("sendingData?");
    //	if (stream.isWriting)
    //	{
    //		stream.SendNext(this.current_speed);
    //		stream.SendNext(this.groundedCount);
    //		stream.SendNext(this.airCount);
    //	}
    //	else
    //	{
    //		this.current_speed = (float)stream.ReceiveNext();
    //		this.groundedCount = (float)stream.ReceiveNext();
    //		this.airCount = (float)stream.ReceiveNext();
    //	}
    //}
    //}


    //public void clampPosition(float x_min, float x_max, float z_min, float z_max, float y_min)
    //{
    //    Vector3 position = gameObject.transform.position;
    //    if (position.x <= x_min || position.x >= x_max || position.z <= z_min || position.z >= z_max || position.y <= y_min)
    //    {            
    //        float x = position.x;
    //        x = Mathf.Clamp(x, x_min, x_max);
    //        position.x = x;

    //        float z = position.z;
    //        z = Mathf.Clamp(z, z_min, z_max);

    //        position.z = z;

    //        float y = position.y;
    //        y = Mathf.Clamp(y, y_min,Mathf.Infinity);
    //        position.y = y;

    //        gameObject.transform.position = position;
    //    }
    //}
}
