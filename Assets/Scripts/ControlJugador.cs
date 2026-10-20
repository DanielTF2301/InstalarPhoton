using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class ControlJugador : NetworkBehaviour
{
    [Header("Configuración del disparo del jugador")]
    [SerializeField] private NetworkObject prefabBala;
    [SerializeField] private Transform puntoDisparo;

    public override void Render()
    {
        if (!HasStateAuthority) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Disparar();
        }
    }

    private void Disparar()
{
    if (prefabBala != null && puntoDisparo != null)
    {
        Runner.Spawn(
            prefabBala,
            puntoDisparo.position,
            puntoDisparo.rotation
        );
    }
}
}