using Unity.Netcode;

public class SpectatorCam : NetworkBehaviour
{
	private void Start()
	{
		if (CustomNetworkManager.networkManager.IsClient)
			transform.GetChild(0).gameObject.SetActive(true);

		CustomNetworkManager.spectator = CustomNetworkManager.networkManager.SpawnManager.GetPlayerNetworkObject(OwnerClientId).GetComponent<SpectatorCam>();
	}
}
