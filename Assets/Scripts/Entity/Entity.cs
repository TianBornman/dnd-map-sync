using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

[GenerateSerializationForType(typeof(string))]
public class Entity : NetworkBehaviour
{
    public TextMeshProUGUI nameText;
    public SpriteRenderer spriteRenderer;

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

    private int r = 0;

    [SerializeField]
    public int R
    {
        get { return r; }
        set
        {
            r = value;

            spriteRenderer.color = GetColor();

            if (IsServer)
                SetClientRClientRpc(r);
        }
    }

    private int g = 0;

    [SerializeField]
    public int G
    {
        get { return g; }
        set
        {
            g = value;

            spriteRenderer.color = GetColor();

            if (IsServer)
                SetClientGClientRpc(b);
        }
    }

    private int b = 0;

    [SerializeField]
    public int B
    {
        get { return b; }
        set
        {
            b = value;

            spriteRenderer.color = GetColor();

            if (IsServer)
                SetClientBClientRpc(b);
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

    [ClientRpc]
    public void SetClientRClientRpc(int r)
    {
        if (IsServer)
            return;

        R = r;
    }

    [ClientRpc]
    public void SetClientGClientRpc(int g)
    {
        if (IsServer)
            return;

        G = g;
    }

    [ClientRpc]
    public void SetClientBClientRpc(int b)
    {
        if (IsServer)
            return;

        B = b;
    }

    public void Destroy()
    {
        if (IsServer)
            Destroy(gameObject);
    }

    private Color GetColor()
    {
        return new Color(r > 0 ? r / 255f : 0, 
                         g > 0 ? g / 255f : 0, 
                         b > 0 ? b / 255f : 0);
    }

    public override void OnNetworkDespawn()
    {
        UIManager.Instance.uiManagement.rootVisualElement.Q<Button>("EntityDestroy").clicked -= Destroy;

        base.OnNetworkDespawn();
    }
}
