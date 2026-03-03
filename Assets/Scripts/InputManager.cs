using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputManager : MonoBehaviour
{
    [System.Serializable]
    public struct MapeoTecla
    {
        public KeyCode tecla;
        public TipoForma forma;
    }

    public List<MapeoTecla> configuracionTeclas;

    void Update()
    {
        foreach (var mapeo in configuracionTeclas)
        {
            if (Input.GetKeyDown(mapeo.tecla))
            {
                ProcesarGolpe(mapeo.forma);
            }
        }
    }
    
    void ProcesarGolpe(TipoForma tipoBuscado)
    {
        var forma = Forma.formasActivas
            .Where(n => n.tipo == tipoBuscado)
            .FirstOrDefault(); // First = la más vieja (Cola FIFO)

        if (forma != null)
        {
            forma.Golpear();
        }
        else
        {
            // Apretó pero no había ninguna forma de ese tipo
            //GameManager.instancia.RegistrarFallo(); 
            Debug.Log("¡Fallo! No había ninguna " + tipoBuscado);
        }
    }
}
