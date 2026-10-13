using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class PlayerColor : NetworkBehaviour
{
    public MeshRenderer meshRenderer;
    [Networked, OnChangedRender(nameof(ColorChanged))] public Color NetworkColor {get; set;}

    private void Update()
    {
        if (HasStateAuthority == false) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            NetworkColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
    }

    private void ColorChanged()
    {
        meshRenderer.material.color = NetworkColor;
    }
}
