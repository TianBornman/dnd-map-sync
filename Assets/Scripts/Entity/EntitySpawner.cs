using System;
using Unity.Netcode;
using UnityEngine;

public class EntityManager : NetworkBehaviour 
{
	public GameObject entity;

	public void SpawnObject()
	{
		if (IsServer) // Ensure only the server spawns the object
		{
			// Instantiate the object
			GameObject entityInstance = Instantiate(entity, new Vector2(-11, -1), Quaternion.identity);

			// Spawn the object across all clients
			entityInstance.GetComponent<NetworkObject>().Spawn();
		}
	}
}
