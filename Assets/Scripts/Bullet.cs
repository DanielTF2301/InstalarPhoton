using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float velocidad = 20f;
    private bool haImpactado = false;

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
            return;

        transform.position += transform.forward * velocidad * Runner.DeltaTime;
    }

    private void OnTriggerEnter(Collider otro)
    {
        if (!HasStateAuthority || haImpactado)
            return;

        VidaPersonaje vidaPersonaje = otro.GetComponentInParent<VidaPersonaje>();

        if (vidaPersonaje != null)
        {
            if (vidaPersonaje.HasStateAuthority)
                return;

            haImpactado = true;

            vidaPersonaje.RPC_RecibirImpacto(20);

            Runner.Despawn(Object);
        }
    }
}