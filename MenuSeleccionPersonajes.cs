using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonajes : MonoBehaviour
{
   public void CambiarNivel(string nombreNivel)
   {
        SceneManager.LoadScene(nombreNivel);
   }
   public void CambiarNivel(int numeroNivel)
   {
        SceneManager.LoadScene(numeroNivel);
   }
}
