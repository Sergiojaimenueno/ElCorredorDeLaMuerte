using UnityEngine;

// El periódico: unos segundos sin recibir daño
public class PowerUp : MonoBehaviour
{
    public float duracion = 5f;

    void OnTriggerEnter2D(Collider2D otro)
    {
        VidaJugador jugador = otro.GetComponent<VidaJugador>();
        if (jugador != null)
        {
            jugador.ActivarInvencible(duracion);
            Puntuacion.Sumar(25);
            Destroy(gameObject);
        }
    }
}