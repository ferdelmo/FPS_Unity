using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
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
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            collision.transform.SendMessage("Damage", m_ProjectileDamage);
        }

    }
}
