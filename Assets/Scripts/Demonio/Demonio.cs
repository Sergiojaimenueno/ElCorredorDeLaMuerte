using UnityEngine;

public class Demonio : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 2.5f;
    public Vector2 tamannoAtaque = new Vector2(1.5f, 1.5f);
    public float distanciaAtaque = 1.5f;
    public float alturaAtaque = -1f;    // sube o baja el cuadro rojo
    public LayerMask capaJugador;
    public bool spriteMiraDerecha = false;
    public float velocidadVuelo = 2f;
    public float alturaMinima = 3f;

    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator anim;
    float alturaSuelo;
    float diferencia;
    int direccion = 1;
    GameObject aGolpear;

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
        direccion = jugador.position.x > transform.position.x ? 1 : -1;
        if (spriteMiraDerecha) dibujo.flipX = direccion < 0;
        else dibujo.flipX = direccion > 0;

        RaycastHit2D rayo = Physics2D.BoxCast(transform.position + Vector3.up * alturaAtaque,
            tamannoAtaque, 0, Vector2.right * direccion,
            distanciaAtaque, capaJugador);
        if (rayo) aGolpear = rayo.rigidbody.gameObject;
        else aGolpear = null;
        anim.SetBool("Atacar", aGolpear != null);

        anim.SetBool("Volar", transform.position.y > alturaSuelo + 0.3f);
    }

    void FixedUpdate()
    {
        float alturaObjetivo = jugador.position.y + diferencia;
        if (alturaObjetivo < alturaSuelo + alturaMinima) alturaObjetivo = alturaSuelo;
        float subir = (alturaObjetivo - transform.position.y) * velocidadVuelo;
        if (aGolpear != null) cuerpo.velocity = new Vector2(0, subir);
        else cuerpo.velocity = new Vector2(direccion * velocidad, subir);
    }

    public void Golpe()
    {
        if (aGolpear != null)
        {
            VidaJugador vida = aGolpear.GetComponent<VidaJugador>();
            if (vida != null) vida.RecibeDanno(1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * alturaAtaque
            + Vector3.right * direccion * distanciaAtaque, tamannoAtaque);
    }
}