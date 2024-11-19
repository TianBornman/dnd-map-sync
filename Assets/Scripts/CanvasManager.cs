using Unity.Netcode;

public class CanvasManager : NetworkBehaviour
{
	public override void OnNetworkSpawn()
	{
		if (IsClient)
			Destroy(gameObject);

		base.OnNetworkSpawn();
	}
}
