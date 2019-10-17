using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public AudioClip m_deathSound;

    private AudioSource m_audio;
    private GameObject m_GameManager;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        // TO-DO 1 Buscar al GameManager y cachearlo
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    public virtual void OnDeath()
    {
        if(gameObject.tag == "Player")
        {
            m_audio.clip = m_deathSound;
            m_audio.Play();
            // TO-DO 2 Respaunear usando el GameManager con el mensaje RespawnPlayer.
            m_GameManager.SendMessage("RespawnPlayer");
        }
        else if(gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        
    }
}
