using Unity.Netcode;
using UnityEngine;

public class CameraMovement : NetworkBehaviour
{
	float speed = 5.0f;

	public NetworkVariable<float> projetionAmount = new NetworkVariable<float>(
			5, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);


	private void Update()
	{
		if (CustomNetworkManager.networkManager.IsServer)
			Move();

		Camera.main.orthographicSize = projetionAmount.Value;
	}

	private void Move()
	{
		float horizontal = Input.GetAxis("Horizontal"); // Maps to A/D or Left/Right Arrow
		float vertical = Input.GetAxis("Vertical");     // Maps to W/S or Up/Down Arrow

		// Create movement vector
		Vector3 movement = new Vector3(horizontal, vertical, 0);

		// Apply movement (scaled by speed and deltaTime)
		transform.Translate(movement * speed * Time.deltaTime, Space.World);

		if (Input.GetKeyUp(KeyCode.UpArrow))
			projetionAmount.Value++;		
		
		if (Input.GetKeyUp(KeyCode.DownArrow))
			projetionAmount.Value--;
	}
}