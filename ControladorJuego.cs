using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour
{
    public static ControladorJuego Instance;
    [SerializeField] private List<GameObject> puntosDeControl; // Lista de puntos de control
    [SerializeField] private GameObject jugadorPrefab; // Prefab del jugador
    private int indexPuntosControl;
    private GameObject jugadorInstanciado; // Referencia al jugador instanciado

    private void Awake()
    {
        Instance = this;

        // Cargar el índice guardado
        indexPuntosControl = PlayerPrefs.GetInt("puntosIndex", 0);

        // Verificar si el índice es válido
        if (indexPuntosControl < 0 || indexPuntosControl >= puntosDeControl.Count)
        {
            indexPuntosControl = 0; // Reiniciar a 0 si está fuera de rango
            PlayerPrefs.SetInt("puntosIndex", indexPuntosControl);
        }

        // Instanciar el jugador en la posición del punto de control
        InstanciarJugador();
    }

    private void InstanciarJugador()
    {
        // Si ya existe un jugador instanciado, destruirlo
        if (jugadorInstanciado != null)
        {
            Destroy(jugadorInstanciado);
        }

        // Instanciar un nuevo jugador en la posición del punto de control actual
        jugadorInstanciado = Instantiate(jugadorPrefab, puntosDeControl[indexPuntosControl].transform.position, Quaternion.identity);
    }

    public void UltimoPuntoControl(GameObject puntoControl)
    {
        for (int i = 0; i < puntosDeControl.Count; i++)
        {
            if (puntosDeControl[i] == puntoControl && i > indexPuntosControl)
            {
                // Actualizar el índice y guardar en PlayerPrefs
                PlayerPrefs.SetInt("puntosIndex", i);
                indexPuntosControl = i; // Actualizar el índice actual

                Debug.Log("Checkpoint actualizado: " + puntoControl.name + " | Nuevo índice: " + indexPuntosControl);

                // Instanciar el jugador en el nuevo punto de control
                InstanciarJugador();
                break; // Salir del bucle después de procesar el nuevo punto de control
            }
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
