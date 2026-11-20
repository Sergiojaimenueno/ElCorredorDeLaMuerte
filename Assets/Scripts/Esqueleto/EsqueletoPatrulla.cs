using UnityEngine;

public class EsqueletoPatrulla : MonoBehaviour
{
    public float tiempoPatrulla = 3f;
    public float velocidad = 1f;
    public Vector2 tamannoAtaque = new Vector2(0.4f, 0.3f);
    public float distanciaAtaque = 1.2f;
    public LayerMask capaJugador;

    float temporizador;
    int direccion = 1;
    Animator anim;
    SpriteRenderer dibujo;
    Rigidbody2D cuerpo;
    GameObject aGolpear;

    void Start()
    {
        temporizador = tiempoPatrulla;
        anim = GetComponent<Animator>();
        dibujo = GetComponent<SpriteRenderer>();
        cuerpo = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        if (aGolpear != null)
            cuerpo.velocity = new Vector2(0, cuerpo.velocity.y);
        else
            cuerpo.velocity = new Vector2(direccion * velocidad, cuerpo.velocity.y);
    }

    // Lo llama el golpe del jugador (AtaqueJugador)
    public void RecibirGolpe(int danio)
    {
        Puntuacion.Sumar(50);
        Destroy(gameObject);
    }

    // Dibuja en la Scene la zona donde te ve
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position
            + Vector3.right * direccion * distanciaAtaque, tamannoAtaque);
    }
}