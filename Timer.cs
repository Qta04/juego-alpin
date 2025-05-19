using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoTiempo; // Texto que muestra el tiempo
    float elapsedTime;
    bool isTiming = true; // Variable para controlar el estado del temporizador

    void Update()
    {
        if (isTiming) // Solo actualiza el tiempo si el temporizador está en marcha
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeDisplay(); // Actualiza la visualización del tiempo
        }
    }

    void UpdateTimeDisplay() // Método para actualizar el texto del tiempo
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer() // Método para detener el temporizador
    {
        isTiming = false; // Detiene el temporizador
        ShowElapsedTime(); // Muestra el tiempo transcurrido
    }

    void ShowElapsedTime()
    {
        // Aquí podrías mostrar el tiempo en la consola o en otro lugar si lo deseas
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        Debug.Log("Tiempo transcurrido: " + string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    public string GetElapsedTime() // Método para obtener el tiempo transcurrido como string
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}