using Unity.Netcode;
using UnityEngine;

public class EntityMovement : NetworkBehaviour
{
	private bool isDragging = false;
	private Vector3 offset;

	private Camera mainCamera;
	private UIManager uiManagement;
	private Entity entity;

	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();

		if (!IsServer)
		{
			enabled = false;
			return;
		}

		mainCamera = Camera.main; // Cache the main camera for efficiency
		uiManagement = GameObject.FindGameObjectWithTag("UiManagement").GetComponent<UIManager>();
		entity = GetComponent<Entity>();
	}

	private void OnMouseDown()
	{
		// Start dragging and calculate the offset between the mouse and object
		isDragging = true;
		offset = transform.position - GetMouseWorldPosition();
		uiManagement.SetCurrentEntity(entity);
	}

	private void OnMouseUp()
	{
		// Stop dragging
		isDragging = false;
	}

	private void Update()
	{
		if (isDragging)
		{
			// Update the object's position based on the mouse's position
			transform.position = GetMouseWorldPosition() + offset;
		}
	}

	private Vector3 GetMouseWorldPosition()
	{
		// Convert mouse position to world coordinates
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(mainCamera.transform.position.z); // Use the camera's z-distance
		return mainCamera.ScreenToWorldPoint(mousePos);
	}
}
