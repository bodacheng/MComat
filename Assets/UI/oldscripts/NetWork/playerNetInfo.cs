using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 本信息模块应该是在一个玩家在加入进游戏时候为玩家生成，并实时更新数据，从而为其他客户端形成信息参考。
// 本模块应该由各个客户端来执行值的修改与更新。

// 这个模块反应的是某一个加入房间的玩家的情况，而不是玩家所生成的游戏角色的情况
// 它需要的包含的信息大概是这些：
// 玩家id， 玩家是否已经为战斗做好准备，玩家所有队员是不是已经都死亡。。。
// 总之所有应该用以裁判的信息。这些值必须同步至其他客户端

// 正因为这个模块中信息是同步的，所以类似判断输赢这样的事情，即便不由master client生成什么中介裁判，各个本地客户端也可以通过参考这些playerNetInfo来判断到底是哪一方获得了胜利。
//public class playerNetInfo : Photon.MonoBehaviour {

//	public playerNetInfo(int id)
//	{
//		this.playerID = id;
//		this.ifFightReady = false;
//	}

//	public int playerID;
//	public string playerTag;
//	public bool ifFightReady;
//	public bool ifAllMembersDead;

//	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//		if (stream.isWriting)
//		{
//			stream.SendNext(playerID);
//			stream.SendNext(playerTag);
//			stream.SendNext(ifFightReady);
//			stream.SendNext(ifAllMembersDead);

//		}
//		else
//		{
//			this.playerID = (int)stream.ReceiveNext();
//			this.playerTag = (string)stream.ReceiveNext();
//			this.ifFightReady = (bool)stream.ReceiveNext();
//			this.ifAllMembersDead = (bool)stream.ReceiveNext();
//		}
//	}

//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}
//}
