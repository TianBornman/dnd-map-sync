using UnityEngine;

public class MapManager : MonoBehaviour 
{
	public static Color PeekColor;
	public static Color HoverColor;
	public static Color ClearColor;	
	
	public Color peekColor;
	public Color hoverColor;
	public Color clearColor;

	private void Awake()
	{
		PeekColor = peekColor;
		HoverColor = hoverColor;
		ClearColor = clearColor;
	}
}
