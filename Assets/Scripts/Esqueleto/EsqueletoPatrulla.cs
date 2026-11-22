using UnityEngine;

// Va en el objeto Esqueleto. El archivo se llama EsqueletoPatrulla.cs
public class EsqueletoPatrulla : MonoBehaviour
{
    public float tiempoPatrulla = 3f;
    public float velocidad = 1f;
    public Vector2 tamannoAtaque = new Vector2(0.4f, 0.3f);
    public float distanciaAtaque = 1.2f;
    public float distanciaVision = 4f;   // a cuánto te ve por detrás
    public int vida = 1;               // golpes que aguanta (el jefe, 3)
    public LayerMask capaJugador;

    float temporizador;
    int direccion = 1;
    Animator anim;
    SpriteRenderer dibujo;
    Rigidbody2D cuerpo;
    GameObject aGolpear;
    bool muerto;                       // ya le han matado

    void Start()
    {
        temporizador = tiempoPatrulla;
        anim = GetComponent<Animator>();
        dibujo = GetComponent<SpriteRenderer>();
        cuerpo = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (muerto) return;            // muerto no hace nada
        // si el jugador está detrás de mí, me doy la vuelta
        RaycastHit2D detras = Physics2D.BoxCast(transform.position,
            tamannoAtaque, 0, Vector2.right * -direccion,
            distanciaVision, capaJugador);
        if (detras) direccion *= -1;

        dibujo.flipX = direccion < 0;
        if (DetectarJugador())
        {
            anim.SetBool("Atacar", true);
        }
        else
        {
            anim.SetBool("Atacar", false);
            ActualizarTemporizador();
        }
    }

    bool DetectarJugador()
    {
        RaycastHit2D rayo = Physics2D.BoxCast(transform.position,
            tamannoAtaque, 0, Vector2.right * direccion,
            distanciaAtaque, capaJugador);
        if (rayo)
        {
            aGolpear = rayo.rigidbody.gameObject;
            return true;
        }
        aGolpear = null;
        return false;
    }

    // La llama la animación EsqueletoAtacar (Animation Event, frame del golpe)
    public void Espadazo()
    {
        if (aGolpear != null)
        {
            VidaJugador jugador = aGolpear.GetComponent<VidaJugador>();
            if (jugador != null) jugador.RecibeDanno(1);
        }
    }

    void ActualizarTemporizador()
    {
        temporizador -= Time.deltaTime;
        if (temporizador < 0)
        {
            direccion *= -1;
            temporizador = tiempoPatrulla;
        }
    }

    void FixedUpdate()
    {
        if (muerto || aGolpear != null)
            cuerpo.velocity = new Vector2(0, cuerpo.velocity.y);
        else
            cuerpo.velocity = new Vector2(direccion * velocidad, cuerpo.velocity.y);
    }

    // Lo llama el golpe del jugador (AtaqueJugador)
    public void RecibirGolpe(int danio)
    {
        if (muerto) return;            // no se muere dos veces
        vida -= danio;                 // le quito vida
        if (vida > 0) return;          // si aún le queda, sigue vivo
        muerto = true;
        Puntuacion.Sumar(50);
        anim.SetTrigger("Morir");      // animación de morir
        Destroy(gameObject, 1f);       // desaparece al cabo de 1 segundo
    }

    // Dibuja en la Scene la zona donde te ve
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position
            + Vector3.right * direccion * distanciaAtaque, tamannoAtaque);
    }
}