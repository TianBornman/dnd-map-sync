using Unity.Netcode;
using UnityEngine;

public class MapSegment : NetworkBehaviour
{
	private SpriteRenderer spriteRenderer;

	public NetworkVariable<bool> isLerping = new NetworkVariable<bool>(
		writePerm: NetworkVariableWritePermission.Server);

	float timer = 0;
	float duration = 2;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public override void OnNetworkSpawn()
	{
		if (IsServer)
			spriteRenderer.color = MapManager.PeekColor;

		base.OnNetworkSpawn();
	}

	private void Update()
	{
		if (isLerping.Value)
		{
			// Increment the timer by the time elapsed since the last frame
			timer += Time.deltaTime;

			// Calculate the lerp factor (clamped between 0 and 1)
			float t = Mathf.Clamp01(timer / duration);

			// Interpolate the color and apply it to the material
			spriteRenderer.color = Color.Lerp(MapManager.HoverColor, MapManager.ClearColor, t);

			// Stop lerping once the duration is reached
			if (t >= 1f)
			{
				isLerping.Value = false;
				timer = 0;
				gameObject.SetActive(false);
			}
		}
	}

	private void OnMouseEnter()
	{
		if (!isLerping.Value && IsServer)
			spriteRenderer.color = MapManager.HoverColor;
	}

	private void OnMouseExit()
	{
		if (!isLerping.Value && IsServer)
			spriteRenderer.color = MapManager.PeekColor;
	}

	private void OnMouseDown()
	{
		if (IsServer)
			isLerping.Value = true;
	}
}
