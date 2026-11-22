using UnityEngine;

// Almas, monedas… cualquier cosa que se coge y da puntos
public class Recogible : MonoBehaviour
{
    public int valor = 10;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<VidaJugador>() != null)   // ¿es el jugador?
        {
            Puntuacion.Sumar(valor);
            Destroy(gameObject);      // desaparece al cogerlo
        }
    }
}