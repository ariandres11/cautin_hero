using UnityEngine;
using System.Collections.Generic;

public class Forma: MonoBehaviour
{
    public GameObject panelErrar;
    public GameObject panelAcertar;
    public TipoForma tipo;
    //TODO: variar por nivel
    public float tiempoVida = 5.0f;

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
                Debug.Log("¡Fallo! Se escapó una figura de tipo " + tipo);
                //panelErrar.SetActive(true);
                GameManager.instancia.RegistrarFallo(); // Avisar que se perdió
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
        Debug.Log("¡Acertaste una forma de tipo " + tipo + "!");
        //panelAcertar.SetActive(true);
        GameManager.instancia.SumarPuntos();

        //GameManager.instancia.SumarPuntos();

        Destroy(gameObject, 0.4f);
    }
}