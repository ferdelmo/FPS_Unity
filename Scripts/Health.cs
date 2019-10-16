using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    public float m_health;
    private float m_CurrentHealth;

    private Vector3 velocity;
    private Vector3 lastPos;
    public Vector3 Velocity { 
        get { return velocity; }
    }
    // Use this for initialization


    void Start () {
        ResetHealth();
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        velocity = (transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }

    public float CurrentHelth
    {
        get { return m_CurrentHealth; }
    }

    public void ResetHealth()
	{
        m_CurrentHealth = m_health;
    }

    /// <summary>
    /// Mensaje que aplica el daño y lanza el mensaje OnDeath cuando la salud es menor que 0.
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(float amount)
    {
        ///  // ## TO-DO 1 si la salud inicial es menor que 0 enviar mensaje void OnDeath() por si a alguien le interesa..
        m_CurrentHealth -= amount;
        if(m_CurrentHealth <= 0)
        {
            this.gameObject.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
        }

    }

}
