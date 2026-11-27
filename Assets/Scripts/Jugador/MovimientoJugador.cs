using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 12f;

    Vector3 movimiento;
    Animator anim;
    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    bool enSuelo;

    void Start()
    {
        anim = GetComponent<Animator>();
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.ResetTrigger("Saltar");
        movimiento = new Vector3();
        if (Input.GetKey(KeyCode.D))
            movimiento += new Vector3(1, 0, 0);
        else if (Input.GetKey(KeyCode.A))
            movimiento -= new Vector3(1, 0, 0);

        // saltar solo si toco el suelo
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            cuerpo.velocity = new Vector2(cuerpo.velocity.x, fuerzaSalto);
            anim.SetTrigger("Saltar");
        }

        // mirar hacia donde ando
        if (movimiento.x != 0)
            dibujo.flipX = movimiento.x < 0;

        // avisar al Animator
        anim.SetFloat("Velocidad", Mathf.Abs(movimiento.x));
        anim.SetBool("EnSuelo", enSuelo);
        anim.SetFloat("VelocidadY", cuerpo.velocity.y);
    }

    void FixedUpdate()
    {
        enSuelo = false;
        cuerpo.velocity = new Vector2(movimiento.x * velocidad, cuerpo.velocity.y);
    }

    // justo al aterrizar
    void OnCollisionEnter2D(Collision2D choque)
    {
        OnCollisionStay2D(choque);
    }

    // ¿estoy tocando algo que tengo debajo?
    void OnCollisionStay2D(Collision2D choque)
    {
        foreach (ContactPoint2D punto in choque.contacts)
            if (punto.normal.y > 0.5f)
                enSuelo = true;
    }
}