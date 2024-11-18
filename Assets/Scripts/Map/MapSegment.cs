using Unity.Netcode;
using UnityEngine;

public class MapSegment : NetworkBehaviour
{
	private Color originalColor;
	public Color hoverColor;
	public Color clearColor;

	private SpriteRenderer spriteRenderer;

	public NetworkVariable<bool> isLerping = new NetworkVariable<bool>(
		writePerm: NetworkVariableWritePermission.Server);

	float timer = 0;
	float duration = 2;

	bool isServer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalColor = spriteRenderer.color;

		CustomNetworkManager.OnStart += SetServer;
	}

	private void SetServer()
	{
		isServer = CustomNetworkManager.networkManager.IsServer;
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
			spriteRenderer.color = Color.Lerp(hoverColor, clearColor, t);

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
		if (!isLerping.Value && isServer)
			spriteRenderer.color = hoverColor;
	}

	private void OnMouseExit()
	{
		if (!isLerping.Value && isServer)
			spriteRenderer.color = originalColor;
	}

	private void OnMouseDown()
	{
		if (isServer)
			isLerping.Value = true;
	}
}
