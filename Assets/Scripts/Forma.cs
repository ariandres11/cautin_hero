using UnityEngine;
using System.Collections.Generic;

public class Forma: MonoBehaviour
{
    public TipoForma tipo;
    public float tiempoVida = 2.0f; 

    [Header("Referencias")]
    public GameObject formaVacia;
    public GameObject formaLlena;

    public static List<Forma> formasActivas = new List<Forma>();

    private bool golpeada = false;

    void OnEnable()
    {
        formasActivas.Add(this);
    
        formaVacia.SetActive(true);
        formaLlena.SetActive(false);
        golpeada = false;

        Invoke(nameof(Desaparecer), tiempoVida);
    }

    void OnDisable()
    {
        formasActivas.Remove(this);
        CancelInvoke();
    }

    void Desaparecer()
    {
            if (!golpeada)
            {
                //GameManager.instancia.RegistrarFallo(); // Avisar que se perdió
                Destroy(gameObject); // Desaparecer
            }
    }

    public void Golpear()
    {
        if (golpeada) return;
        golpeada = true;

        CancelInvoke(nameof(Desaparecer));

        // Feedback Visual
        formaVacia.SetActive(false);
        formaLlena.SetActive(true);

        //GameManager.instancia.SumarPuntos();

        // Esperar 0.2s para que se vea el relleno y luego destruir
        Destroy(gameObject, 0.2f);
    }
}