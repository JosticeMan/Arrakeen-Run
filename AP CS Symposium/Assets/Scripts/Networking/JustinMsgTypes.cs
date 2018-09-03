using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JustinMsgTypes : MonoBehaviour {

	public const short PlayerPrefab = MsgType.Highest + 1;

	public class PlayerPrefabMsg : MessageBase
	{
		public short controllerID;    
		public short prefabIndex;
	}

}
