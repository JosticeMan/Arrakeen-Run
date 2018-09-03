using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using GameSparks.Core;

public class JustinNetworkPlayerSpawner : NetworkManager {

	// in the Network Manager component, you must put your player prefabs 
	// in the Spawn Info -> Registered Spawnable Prefabs section 
	public short playerPrefabIndex;

	void Update() {
		UpdatePC ();
	}

	public override void OnStartServer()
	{
		NetworkServer.RegisterHandler(JustinMsgTypes.PlayerPrefab, OnResponsePrefab);
		base.OnStartServer();
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		client.RegisterHandler(JustinMsgTypes.PlayerPrefab, OnRequestPrefab);
		base.OnClientConnect(conn);
	}

	private void OnRequestPrefab(NetworkMessage netMsg)
	{
		JustinMsgTypes.PlayerPrefabMsg msg = new JustinMsgTypes.PlayerPrefabMsg();
		msg.controllerID = netMsg.ReadMessage<JustinMsgTypes.PlayerPrefabMsg>().controllerID;
		msg.prefabIndex = playerPrefabIndex;
		Debug.Log (playerPrefabIndex);
		client.Send(JustinMsgTypes.PlayerPrefab, msg);
	}

	private void OnResponsePrefab(NetworkMessage netMsg)
	{
		JustinMsgTypes.PlayerPrefabMsg msg = netMsg.ReadMessage<JustinMsgTypes.PlayerPrefabMsg>();  
		playerPrefab = spawnPrefabs[msg.prefabIndex];
		base.OnServerAddPlayer(netMsg.conn, msg.controllerID);
		Debug.Log(playerPrefab.name + " spawned!");
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		JustinMsgTypes.PlayerPrefabMsg msg = new JustinMsgTypes.PlayerPrefabMsg();
		msg.controllerID = playerControllerId;
		NetworkServer.SendToClient(conn.connectionId, JustinMsgTypes.PlayerPrefab, msg);
	}
		
	public void UpdatePC ()
	{
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LP").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("player_Data");
				int skinEquipped = (int) data.GetInt("currentSkin");
				playerPrefabIndex = System.Convert.ToInt16(skinEquipped);
			} 
		});
	}

}
