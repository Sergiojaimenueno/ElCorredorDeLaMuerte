using UnityEngine;

// Va en el objeto Demonio. Te persigue siempre y atraviesa cajas y huecos.
public class Demonio : MonoBehaviour
{
    public Transform jugador;          // arrastra aquí al Jugador
    public float velocidad = 2.5f;
    public float distanciaAtaque = 2f;
    public bool spriteMiraDerecha = false;
    public float velocidadVuelo = 2f;    // lo rápido que sube y baja
    public float alturaMinima = 3f;

    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator anim;
    float alturaSuelo;                 // la Y donde empieza (el suelo)
    float diferencia;                  // cuánto más alto está que el jugador

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        alturaSuelo = transform.position.y;
        diferencia = transform.position.y - jugador.position.y;
    }

    void Update()
    {
        // si estoy cerca del jugador, ataco
        float distancia = Mathf.Abs(jugador.position.x - transform.position.x);
        anim.SetBool("Atacar", distancia < distanciaAtaque);
        // si está por encima del suelo, saca las alas
        anim.SetBool("Volar", transform.position.y > alturaSuelo + 0.3f);
    }

    void FixedUpdate()
    {
        // voy hacia el jugador
        int direccion = jugador.position.x > transform.position.x ? 1 : -1;
        // si subes (cajas, saltos), vuela hasta tu altura; nunca baja del suelo
        float alturaObjetivo = jugador.position.y + diferencia;
        if (alturaObjetivo < alturaSuelo + alturaMinima) alturaObjetivo = alturaSuelo;
        float subir = (alturaObjetivo - transform.position.y) * velocidadVuelo;
        cuerpo.velocity = new Vector2(direccion * velocidad, subir);

        if (spriteMiraDerecha) dibujo.flipX = direccion < 0;
        else dibujo.flipX = direccion > 0;
    }

    void OnCollisionStay2D(Collision2D choque)
    {
        VidaJugador vida = choque.gameObject.GetComponent<VidaJugador>();
        if (vida != null) vida.RecibeDanno(1);
    }
}