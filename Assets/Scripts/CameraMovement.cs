using Unity.Netcode;
using UnityEngine;

public class CameraMovement : NetworkBehaviour
{
	float speed = 5.0f;

	private void Update()
	{
		if (CustomNetworkManager.networkManager.IsServer)
			Move();
	}

	private void Move()
	{
		float horizontal = Input.GetAxis("Horizontal"); // Maps to A/D or Left/Right Arrow
		float vertical = Input.GetAxis("Vertical");     // Maps to W/S or Up/Down Arrow

		// Create movement vector
		Vector3 movement = new Vector3(horizontal, vertical, 0);

		// Apply movement (scaled by speed and deltaTime)
		transform.Translate(movement * speed * Time.deltaTime, Space.World);
	}
}