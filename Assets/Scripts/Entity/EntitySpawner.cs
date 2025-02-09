using System;
using Unity.Netcode;
using UnityEngine;

public class EntityManager : NetworkBehaviour 
{
	public GameObject entity;
	public LayerMask spawnLayers;

	private bool placingEntity = false;

	private void Update()
	{
		if (!placingEntity)
			return;

		if (Input.GetMouseButtonDown(0)) // Left mouse click
		{
			SpawnObject();
		}
	}

	public void PlaceObject()
	{
		placingEntity = true;
	}

	private void SpawnObject()
	{
		if (IsServer) // Ensure only the server spawns the object
		{

			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = Vector2.right; // Change if needed (e.g., Vector2.up)

			RaycastHit2D hit = Physics2D.Raycast(mousePos, direction, 5000, spawnLayers);

			if (hit.collider != null)
			{
				// Instantiate the object
				GameObject entityInstance = Instantiate(entity, hit.point, Quaternion.identity);

				// Spawn the object across all clients
				entityInstance.GetComponent<NetworkObject>().Spawn();
				placingEntity = false;
			}
		}
	}
}
