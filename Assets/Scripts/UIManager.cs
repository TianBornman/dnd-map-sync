using Unity.Netcode;
using UnityEngine.UIElements;

public class UIManager : NetworkBehaviour
{
	public static UIManager Instance;

	public EntityManager entityManager;
	public bool IsOverUi;

	public UIDocument uiManagement;

	private bool showManageEntity = false;

	private void Awake()
	{
		Instance = this;
	}

	public override void OnNetworkSpawn()
	{
		if (IsClient)
			Destroy(gameObject);

		uiManagement = GetComponent<UIDocument>();

		uiManagement.rootVisualElement.Q<Button>("SpawnEntity").clicked += entityManager.PlaceObject;
		uiManagement.rootVisualElement.Q<Button>("ManageEntity").clicked += ToggleManageEntity;

		uiManagement.rootVisualElement.Q<VisualElement>("Menu").RegisterCallback<PointerEnterEvent>(evt => IsOverUi = true);
		uiManagement.rootVisualElement.Q<VisualElement>("Menu").RegisterCallback<PointerLeaveEvent>(evt => IsOverUi = false);


		base.OnNetworkSpawn();
	}

	public void SetCurrentEntity(Entity entity)
	{
		uiManagement.rootVisualElement.Q<TextField>().dataSource = entity;
		uiManagement.rootVisualElement.Q<FloatField>().dataSource = entity;

		uiManagement.rootVisualElement.Q<IntegerField>("R").dataSource = entity;
		uiManagement.rootVisualElement.Q<IntegerField>("G").dataSource = entity;
		uiManagement.rootVisualElement.Q<IntegerField>("B").dataSource = entity;

		uiManagement.rootVisualElement.Q<Button>("EntityDestroy").clicked += entity.Destroy;
	}

	public void ToggleManageEntity()
	{
		showManageEntity = !showManageEntity;

		if (showManageEntity)
			uiManagement.rootVisualElement.Q<VisualElement>("EntityManagement").RemoveFromClassList("hidden");
		else
			uiManagement.rootVisualElement.Q<VisualElement>("EntityManagement").AddToClassList("hidden");
	}
}
