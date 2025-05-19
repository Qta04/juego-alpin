using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    [SerializeField] private GameObject marcaDialogo;
    [SerializeField] private GameObject PanelDialogo;

    [SerializeField] private GameObject Botones;
    [SerializeField] private TMP_Text TextoDialogo;

    [SerializeField,TextArea(1,5)]private string[] lineasDialogo;
    private bool JugadorRango;
    private bool ComenzoDialogo;
    private int lineaMostrando;
    private float tiempoEspera = 0.05f;

        
    void Update()
    {
        

        if(JugadorRango && Input.GetKeyDown(KeyCode.Space))
        {
            
           if(!ComenzoDialogo )
           {
                IniciarDialogo();
           }
           else if(TextoDialogo.text == lineasDialogo[lineaMostrando])
           {
                siguienteLinea();
           }
            else
            {
                StopAllCoroutines();
                TextoDialogo.text = lineasDialogo[lineaMostrando];
            }
        }
    }

    public void BotonPasar()
    {
        if(!ComenzoDialogo )
           {
                IniciarDialogo();
           }
           else if(TextoDialogo.text == lineasDialogo[lineaMostrando])
           {
                siguienteLinea();
           }
            else
            {
                StopAllCoroutines();
                TextoDialogo.text = lineasDialogo[lineaMostrando];
            }
    }

    private void IniciarDialogo()
    {
        ComenzoDialogo = true;
        PanelDialogo.SetActive(true);
        Botones.SetActive(false);
        marcaDialogo.SetActive(false);
        lineaMostrando = 0;
        Time.timeScale = 0f;
        StartCoroutine(LineaLenta());
        
    }

    private void FinalizarDialogo()
    {
        ComenzoDialogo = false;
        Botones.SetActive(true);
        PanelDialogo.SetActive(false);
        marcaDialogo.SetActive(true);
        Time.timeScale = 1f;
    }

    private void siguienteLinea()
    {
        lineaMostrando++;
        if(lineaMostrando < lineasDialogo.Length)
        {
            StartCoroutine(LineaLenta());
            
        }
        else if(lineaMostrando == lineasDialogo.Length)
        {
            ComenzoDialogo = false;
            Botones.SetActive(true);
            PanelDialogo.SetActive(false);
            marcaDialogo.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator LineaLenta()
    {
        TextoDialogo.text = string.Empty;

        foreach(char ch in lineasDialogo[lineaMostrando])
        {
            TextoDialogo.text += ch;
            yield return new WaitForSecondsRealtime(tiempoEspera);
        }
        
    }

    private IEnumerator EsperarParaIniciarDialogo()
    {
        yield return new WaitForSeconds(4f); // Esperar 4 segundos
        if (JugadorRango) // Verificar si el jugador sigue en rango
        {
            if(!ComenzoDialogo )
           {
                IniciarDialogo();
           }
           else if(TextoDialogo.text == lineasDialogo[lineaMostrando])
           {
                siguienteLinea();
           }
            else
            {
                StopAllCoroutines();
                TextoDialogo.text = lineasDialogo[lineaMostrando];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            JugadorRango = true;
            marcaDialogo.SetActive(true);
            StartCoroutine(EsperarParaIniciarDialogo());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            JugadorRango = false;
            marcaDialogo.SetActive(false);
            StopAllCoroutines();
        }
    }

}
