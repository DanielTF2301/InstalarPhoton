using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class SaludoRCP : NetworkBehaviour
{
    //Update para tecla H

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.hKey.wasPressedThisFrame)
        {
            string idJugador = Runner.LocalPlayer.ToString();

            RPC_Saludo("¡Hola a todos! Soy el jugador " + idJugador);
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.Proxies)]
    public void RPC_Saludo(string mensaje)
    {
        SayHello interfaz = FindAnyObjectByType<SayHello>();

        if (interfaz != null)
        {
            interfaz.ShowMessage(mensaje);
        }
    }
}
