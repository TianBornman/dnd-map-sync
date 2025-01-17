using Unity.Netcode;
using UnityEngine.UIElements;

public class UIManager : NetworkBehaviour
{
	public EntityManager entityManager;

	private UIDocument uiManagement;

	public override void OnNetworkSpawn()
	{
		if (IsClient)
			Destroy(gameObject);

		uiManagement = GetComponent<UIDocument>();

		uiManagement.rootVisualElement.Q<Button>("SpawnEntity").clicked += () => entityManager.SpawnObject();

		base.OnNetworkSpawn();
	}

	public void SetCurrentEntity(Entity entity)
	{
		uiManagement.rootVisualElement.Q<TextField>().dataSource = entity;
	}
}
