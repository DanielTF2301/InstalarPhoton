using UnityEngine;
using Fusion;

public class VidaPersonaje : NetworkBehaviour
{
    [Networked] public int Vida { get; set; } = 100;

    [Header("Sistema de Color")]
    [SerializeField] private Renderer renderPersonaje;

    public override void Spawned()
    {
        ActualizarColorVisual();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_RecibirImpacto(int dano)
    {
        Vida = Mathf.Max(0, Vida - dano);
        ActualizarColorVisual();
        Debug.Log($"[RPC] ¡Impacto recibido! Nueva vida de {Object.InputAuthority}: {Vida}");
    }

    private void ActualizarColorVisual()
    {
        if (renderPersonaje != null)
        {
            float porcentajeVida = (float)Vida / 100f;
            renderPersonaje.material.color = Color.Lerp(Color.red, Color.green, porcentajeVida);
        }
    }

    public override void Render()
    {
        ActualizarColorVisual();
    }
}