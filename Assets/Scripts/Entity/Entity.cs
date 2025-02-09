using TMPro;
using Unity.Netcode;
using UnityEngine;

[GenerateSerializationForType(typeof(string))]
public class Entity : NetworkBehaviour
{
	public TextMeshProUGUI nameText;

	#region Properties

	private string entityName;

	[SerializeField]
	public string EntityName
	{
		get { return entityName; }
		set
		{
			entityName = value;
			nameText.text = entityName;

			if (IsServer)
				SetClientNameClientRpc(entityName);
		}
	}

	private float entitySize = 1;

	[SerializeField]
	public float EntitySize
	{
		get { return entitySize; }
		set
		{
			entitySize = value;

			transform.localScale = new Vector3(entitySize, entitySize, entitySize);

			if (IsServer)
				SetClientSizeClientRpc(entitySize);
		}
	}

	#endregion

	[ClientRpc]
	public void SetClientNameClientRpc(string name)
	{
		if (IsServer)
			return;

		EntityName = name;
	}	
	
	[ClientRpc]
	public void SetClientSizeClientRpc(float size)
	{
		if (IsServer)
			return;

		EntitySize = size;
	}
}
