using UnityEngine;

/// <summary>
/// Attach this class to make object pickable.
/// </summary>
[RequireComponent(typeof(Rigidbody))]

public class PickableItem : MonoBehaviour
{
    public Texture2D texture;
    [TextArea(2, 10)] public string description;
    private Rigidbody rb;
    public Rigidbody Rb => rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}