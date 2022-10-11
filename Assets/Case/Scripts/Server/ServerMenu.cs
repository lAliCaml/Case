using UnityEngine;
using System;
using Photon.Bolt;
using Photon.Realtime;
using Photon.Bolt.Matchmaking;
using UdpKit.Platform;



public class ServerMenu : GlobalEventListener
{
	[SerializeField] private string _roomName;
	private void Awake()
	{
		Application.targetFrameRate = 60;
	}

	public void StartServer()
	{
		BoltLauncher.StartServer();
	}

	public void StartClient()
	{
		BoltLauncher.StartClient();
	}

	public override void BoltStartDone()
	{
		if (BoltNetwork.IsServer)
		{
			string matchName = Guid.NewGuid().ToString();

			BoltMatchmaking.CreateSession(
				sessionID: matchName,
				sceneToLoad: _roomName
			);
		}

		if (BoltNetwork.IsClient)
		{
			BoltMatchmaking.JoinRandomSession();
		}
	}


}
