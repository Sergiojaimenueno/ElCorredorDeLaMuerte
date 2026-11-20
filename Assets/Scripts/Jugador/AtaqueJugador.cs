using UnityEngine;

// Va en el hijo "Ataque" del Player. Su collider es Trigger.
public class AtaqueJugador : MonoBehaviour
{
    public int danio = 1;

    Collider2D zona;
    SpriteRenderer dibujoJugador;
    Animator animJugador;

    void Start()
    {
        zona = GetComponent<Collider2D>();
        dibujoJugador = GetComponentInParent<SpriteRenderer>();
        animJugador = GetComponentInParent<Animator>();
        zona.enabled = false;
    }

    void Update()
    {
        // ponerme delante del jugador
        float lado = dibujoJugador.flipX ? -0.7f : 0.7f;
        transform.localPosition = new Vector3(lado, 0, 0);

        // J: animación de atacar y enciendo el golpe un momento
        if (Input.GetKeyDown(KeyCode.J))
        {
            animJugador.SetTrigger("Atacar");
            zona.enabled = true;
            Invoke("Apagar", 0.2f);
        }
    }

    void Apagar()
    {
        zona.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        EsqueletoPatrulla enemigo = otro.GetComponent<EsqueletoPatrulla>();
        if (enemigo != null)
            enemigo.RecibirGolpe(danio);
    }
}
