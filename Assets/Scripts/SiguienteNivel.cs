using UnityEngine;
using UnityEngine.SceneManagement;

public class SiguienteNivel : MonoBehaviour
{
    public string escenaSiguiente;
    public int bonus = 100;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<VidaJugador>() != null)
        {
            Puntuacion.Sumar(bonus);
            SceneManager.LoadScene(escenaSiguiente);
        }
    }
}