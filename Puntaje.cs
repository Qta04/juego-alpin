using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;

public class Puntaje : MonoBehaviour
{
    public float cantidadPuntos;
    private TextMeshProUGUI txtPuntos;
    // Start is called before the first frame update
    void Start()
    {
        txtPuntos = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txtPuntos.text = cantidadPuntos.ToString("0");
    }

    public void SumPuntos(float Puntos)
    {
        cantidadPuntos += Puntos;
    }
}
