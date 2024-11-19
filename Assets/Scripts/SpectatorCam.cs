using Unity.Netcode;

public class SpectatorCam : NetworkBehaviour
{
	public override void OnNetworkSpawn()
	{
		if (CustomNetworkManager.networkManager.IsClient)
			transform.GetChild(0).gameObject.SetActive(true);

		CustomNetworkManager.spectator = CustomNetworkManager.networkManager.SpawnManager.GetPlayerNetworkObject(OwnerClientId).GetComponent<SpectatorCam>();

		base.OnNetworkSpawn();
	}
}
