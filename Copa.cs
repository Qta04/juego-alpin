using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Copa : MonoBehaviour
{
    [SerializeField] GameObject PanelPuntaje;
    [SerializeField] GameObject Botones;  // Panel que contiene el texto
    [SerializeField] Timer timer; // Referencia al script Timer
    TextMeshProUGUI textoPuntaje; // Referencia al TextMeshProUGUI

    private void Start()
    {
        // Obtiene el componente TextMeshProUGUI del PanelPuntaje
        textoPuntaje = PanelPuntaje.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            // Activa el panel de puntaje
            PanelPuntaje.SetActive(true);
            Botones.SetActive(false);
            
            // Detiene el temporizador
            timer.StopTimer(); 

            // Muestra el tiempo transcurrido en el texto del panel
            textoPuntaje.text = timer.GetElapsedTime();
        }
        else
        {
            PanelPuntaje.SetActive(false);
        }
    }
}
