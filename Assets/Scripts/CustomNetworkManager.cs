using Unity.Netcode;
using UnityEngine;

public class CustomNetworkManager : MonoBehaviour
{
	public static NetworkManager networkManager;


	void Awake()
	{
		networkManager = GetComponent<NetworkManager>();
		Screen.SetResolution(960, 540, FullScreenMode.Windowed);
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(10, 10, 300, 300));
		if (!networkManager.IsClient && !networkManager.IsServer)
		{
			StartButtons();
		}
		else
		{
			StatusLabels();
		}

		GUILayout.EndArea();
	}

	static void StartButtons()
	{
		if (GUILayout.Button("Host")) networkManager.StartHost();
		if (GUILayout.Button("Client")) StartClient();
		if (GUILayout.Button("Server")) StartServer();
	}

	static void StartClient()
	{
		networkManager.StartClient();
	}

	static void StartServer()
	{
		networkManager.StartServer();
	}

	static void StatusLabels()
	{
		var mode = networkManager.IsHost ?
			"Host" : networkManager.IsServer ? "Server" : "Client";

		GUILayout.Label("Transport: " +
			networkManager.NetworkConfig.NetworkTransport.GetType().Name);
		GUILayout.Label("Mode: " + mode);
	}
}