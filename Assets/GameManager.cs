using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("UI: Paneles de Feedback")]
    public GameObject panelAcertaste;
    public GameObject panelErraste;

    [Header("Estadísticas")]
    public int aciertos = 0;
    public int fallos = 0;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        panelAcertaste.SetActive(false);
        panelErraste.SetActive(false);
    }

    public void SumarPuntos()
    {
        aciertos++;
        MostrarFeedback(panelAcertaste);
    }

    public void RegistrarFallo()
    {
        fallos++;
        MostrarFeedback(panelErraste);
    }

    private void MostrarFeedback(GameObject panelAMostrar)
    {
        panelAcertaste.SetActive(false);
        panelErraste.SetActive(false);

        panelAMostrar.SetActive(true);

        StopAllCoroutines(); 
        StartCoroutine(ApagarPanelesDespuesDe(0.5f)); // Se apaga en medio segundo
    }

    private IEnumerator ApagarPanelesDespuesDe(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        panelAcertaste.SetActive(false);
        panelErraste.SetActive(false);
    }
}
