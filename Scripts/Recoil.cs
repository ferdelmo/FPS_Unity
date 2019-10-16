using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    //Magnitude to measure how much you need to up the look
    public float m_ForceUp = 0;
    //count how much you need to go down
    public float m_ToDown = 0;

    public float upVelocity = 20;
    public float downVelocity = 20;

    public float ForceUp {
        get { return m_ForceUp; } 
        set { m_ForceUp = value; }
    }

    public float ToDown {
        get { return m_ToDown; }
        set { m_ToDown = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRecoil();
    }

    void UpdateRecoil()
    {
        if (m_ForceUp > 0)
        {
            //ForceUp--
            m_ForceUp--;
            //rotate Y (maneja lo mucho que subes la camara
            transform.localRotation *= Quaternion.Euler(-upVelocity * Time.deltaTime, 0, 0);
        }
        else if (m_ToDown > 0) {
            //Todown--
            m_ToDown--;
            //rotate Y (maneja lo rapido que bajas la camara
            transform.localRotation *= Quaternion.Euler(downVelocity * Time.deltaTime, 0, 0);
        }
    }

    public void addRecoil(float force)
    {
        m_ForceUp += force;
        m_ToDown += force*upVelocity/downVelocity;
    }
}
