using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormasManager : MonoBehaviour
{
    public GameObject[] prefabsFormas;
    
    [Header("El Contenedor con BoxCollider2D")]
    public Collider2D contenedorDeLimites;

    [Header("Configuración de Spawn")]
    public float tiempoEntreSpawns = 2f;
    private float timer = 0f;

    [Header("Superposición")]
    public float distanciaMinima = 1.5f; 
    public int maxIntentosPosicion = 10; 

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= tiempoEntreSpawns)
        {
            IntentarSpawnearForma();
            timer = 0f; 
        }
    }

    void IntentarSpawnearForma()
    {
        Vector2 posicionAleatoria = Vector2.zero;
        bool encontroLugarLibre = false;
        int intentos = 0;

        Bounds limites = contenedorDeLimites.bounds;

        while (!encontroLugarLibre && intentos < maxIntentosPosicion)
        {
            float posX = Random.Range(limites.min.x, limites.max.x);
            float posY = Random.Range(limites.min.y, limites.max.y);
            posicionAleatoria = new Vector2(posX, posY);

            encontroLugarLibre = ChequearSiEstaLibre(posicionAleatoria);
            intentos++;
        }

        if (encontroLugarLibre)
        {
            GameObject instanciaForma = Instantiate(
                prefabsFormas[Random.Range(0, prefabsFormas.Length)], 
                posicionAleatoria, 
                Quaternion.identity, 
                contenedorDeLimites.transform
            );
        }
        else
        {
            Debug.Log("Pantalla muy llena, se abortó el spawn en este turno.");
        }
    }

    bool ChequearSiEstaLibre(Vector2 posicionCandidata)
    {
        foreach (Forma f in Forma.formasActivas)
        {
            float distancia = Vector2.Distance(posicionCandidata, f.transform.position);
            if (distancia < distanciaMinima)
            {
                return false; 
            }
        }
        return true; 
    }
}
