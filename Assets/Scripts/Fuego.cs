using UnityEngine;

// Va en el objeto Fuego. Si lo tocas, pierdes vida.
public class Fuego : MonoBehaviour
{
    public int danno = 1;

    void OnTriggerStay2D(Collider2D otro)   // mientras estés dentro
    {
        VidaJugador jugador = otro.GetComponent<VidaJugador>();
        if (jugador != null)
            jugador.Morir();
    }
}