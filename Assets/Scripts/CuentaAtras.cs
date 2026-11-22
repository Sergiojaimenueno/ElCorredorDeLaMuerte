using UnityEngine;
using TMPro;

// Va en TextoCuenta (dentro del Canvas). Cuenta 3, 2, 1 y suelta al demonio.
public class CuentaAtras : MonoBehaviour
{
    public GameObject demonio;          // el Demonio, que empieza apagado
    float tiempo = 3f;
    TMP_Text texto;

    void Start()
    {
        texto = GetComponent<TMP_Text>();
        Time.timeScale = 0;             // todo el juego quieto
    }

    void Update()
    {
        tiempo -= Mathf.Min(Time.unscaledDeltaTime, 0.1f);  // cuenta aunque el juego esté quieto
        if (tiempo > 0)
            texto.text = Mathf.Ceil(tiempo).ToString();   // 3, 2, 1
        else if (tiempo > -1)
        {
            texto.text = "¡CORRE!";
            Time.timeScale = 1;         // el juego arranca
            demonio.SetActive(true);    // sale el demonio
        }
        else
            gameObject.SetActive(false);    // quita el texto
    }
}