using Unity.Netcode;
using UnityEngine.UIElements;

public class UIManager : NetworkBehaviour
{
	public EntityManager entityManager;

	private UIDocument managementUI;

	public override void OnNetworkSpawn()
	{
		if (IsClient)
			Destroy(gameObject);

		managementUI = GetComponent<UIDocument>();

		managementUI.rootVisualElement.Q<Button>("SpawnEntity").clicked += () => entityManager.SpawnObject();

		base.OnNetworkSpawn();
	}
}
