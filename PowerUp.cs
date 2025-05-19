using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float saltoExtra;
    public float velocidadExtra;
    public float vidaExtra;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ControladorPlayer controladorPlayer))
        {
            // Crear una lista de efectos de poder
            List<System.Action> efectosPowerUp = new List<System.Action>
            {
                () => controladorPlayer.SubirSaltoPowerUp(saltoExtra),
                () => controladorPlayer.SubirVelocidadPowerUp(velocidadExtra),
                () => controladorPlayer.SubirVidaPowerUp(vidaExtra)
            };

            // Escoger un efecto aleatorio
            int indiceAleatorio = Random.Range(0, efectosPowerUp.Count);
            efectosPowerUp[indiceAleatorio].Invoke();

            // Destruir el objeto de poder
            Destroy(gameObject);
        }
    }
}
