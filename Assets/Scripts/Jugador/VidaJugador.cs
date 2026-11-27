using UnityEngine;
using UnityEngine.SceneManagement;

// Va en el Player. Lo llaman los enemigos, los peligros y el power-up.
public class VidaJugador : MonoBehaviour
{
    public int vidaRestante = 3;

    float tiempoInvencible;
    SpriteRenderer dibujo;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        tiempoInvencible -= Time.deltaTime;
        // amarillo mientras soy invencible
        dibujo.color = tiempoInvencible > 0 ? Color.yellow : Color.white;
    }

    public void RecibeDanno(int cuanto)
    {
        if (tiempoInvencible > 0) return;
        vidaRestante -= cuanto;
        tiempoInvencible = 1f;          // 1 segundo para escapar
        if (vidaRestante <= 0) Morir();
    }

    public void Morir()   
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    
        Puntuacion.puntos = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public void ActivarInvencible(float segundos)
    {
        tiempoInvencible = segundos;
    }
}
