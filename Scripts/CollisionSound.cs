using UnityEngine;
using System.Collections;

// Esta clase se encarga de reproducir un sonido cuando el GameObject
// padre colisiona contra cualquier objeto
public class CollisionSound : MonoBehaviour
{
    public float m_ProjectileDamage = 35;
    /// <summary>
    /// AudioClip público. Será el sonido que se reproducirá cuando el GameObject colisione
    /// </summary>
    public AudioClip m_CollisionSound;
    // Esta función se llama cada vez que el objeto colisiona contra algún objeto
    // ## TO-DO 1.1 - Añadir la función de la API que se lanza cada vez que el GameObject colisiona. Pista: OnCollisi...
    void OnCollisionEnter(Collision collision)
    {
        // ## TO- DO 1.2 - En caso de que haya sonido, reproducirlo una única vez.Pista: audio.PlayOne...
        if (m_CollisionSound != null ) //si collisiona con un tipo proyectile ignorar
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(m_CollisionSound);
            /*foreach(Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }

            Destroy(GetComponent<Rigidbody>());*/
        }

    }


}
