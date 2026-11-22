using UnityEngine;

// Debajo del nivel: si te caes, pierdes
public class ZonaMuerte : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otro)
    {
        VidaJugador jugador = otro.GetComponent<VidaJugador>();
        if (jugador != null)
            jugador.Morir();
    }
}