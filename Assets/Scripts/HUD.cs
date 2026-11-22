using UnityEngine;
using TMPro;

// Va en el Canvas. Escribe los puntos y las vidas cada fotograma.
public class HUD : MonoBehaviour
{
    public TMP_Text textoPuntos;
    public TMP_Text textoVidas;
    public VidaJugador jugador;

    void Update()
    {
        textoPuntos.text = "Puntos: " + Puntuacion.puntos;
        textoVidas.text = "Vidas: " + jugador.vidaRestante;
    }
}