
using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class PlayerHealth : NetworkBehaviour
{
    public MeshRenderer meshRenderer;

    [Networked, OnChangedRender(nameof(HealthChanged))] public int Health { get; set; } = 50;

    private void Update()
    {
        if (HasStateAuthority == false) return;

        if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            int damage = Random.Range(2, 6);

            Health = Mathf.Max(0, Health - damage);
        }
    }

    private void HealthChanged()
    {
        float healthPercentage = Health / 50f;

        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);

        meshRenderer.material.color = healthColor;

        Debug.Log("Ahora tienes: " + Health + " de vida");
    }
}
