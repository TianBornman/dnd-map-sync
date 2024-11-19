using System;
using Unity.Netcode;
using UnityEngine;

public class EntityManager : NetworkBehaviour 
{
	//[SerializeField] private GameObject objectPrefab; // Prefab with NetworkObject component

	//public void SpawnObject()
	//{
	//	if (IsServer) // Ensure only the server spawns the object
	//	{
	//		// Instantiate the object
	//		GameObject newObject = Instantiate(objectPrefab, GetRandomPosition(), Quaternion.identity);

	//		// Spawn the object across all clients
	//		newObject.GetComponent<NetworkObject>().Spawn();
	//	}
	//	else
	//	{
	//		Debug.LogWarning("SpawnObject called from a non-server instance!");
	//	}
	//}

	//private Vector3 GetRandomPosition()
	//{
	//	return new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
	//}
}
