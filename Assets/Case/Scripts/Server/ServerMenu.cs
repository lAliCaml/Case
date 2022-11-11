using UnityEngine;
using System;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UnityEngine.UI;
using TMPro;
using UdpKit;

public class ServerMenu : GlobalEventListener
{
	[SerializeField] private string _roomName;
	[SerializeField] private InputField inputField;
	private void Awake()
	{
		Application.targetFrameRate = 60;
	}

	public void StartServer()
	{
		BoltLauncher.StartServer();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log(BoltNetwork.SessionList.Count);

		}
	}

	public void StartClient()
	{
		Debug.Log("StartClient1");
		BoltLauncher.StartClient();
		Debug.Log("StartClient2");
	}

	public override void BoltStartDone()
	{
		Debug.Log("BoltStartDone");
		if (BoltNetwork.IsServer)
		{
			string matchName = UnityEngine.Random.Range(0, 5000).ToString();//  inputField.text;

			BoltMatchmaking.CreateSession(
				sessionID: matchName,
				sceneToLoad: _roomName
			);
		}

		if (BoltNetwork.IsClient)
		{
			BoltMatchmaking.JoinRandomSession();
			//BoltMatchmaking.JoinSession(inputField.text);
			Debug.Log("JoinRandomSession");
		}
	}

    public override void ConnectFailed(UdpEndPoint endpoint, IProtocolToken token)
    {
		Debug.Log("ConnectFailed");
	}

    public override void BoltStartFailed(UdpConnectionDisconnectReason disconnectReason)
    {
		Debug.Log("BoltStartFailed");
	}

    public override void Disconnected(BoltConnection connection)
    {
		Debug.Log("Disconnected");
	}

    public override void SessionConnectFailed(UdpSession session, IProtocolToken token, UdpSessionError errorReason)
    {
		BoltNetwork.Shutdown();
		StartClient();
	}

    public override void SessionCreationFailed(UdpSession session, UdpSessionError errorReason)
    {
		Debug.Log("SessionCreationFailed");
	}


}
