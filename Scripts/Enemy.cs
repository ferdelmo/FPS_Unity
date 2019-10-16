using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float sp=10;
    public int N=10;
    public float m_TimeBetweenShots = 2;
    public GameObject m_projectile = null;
    public Transform m_ShootPoint;
    public double t = 0;

    private Health player;
    private FIFO velocities;
    // Start is called before the first frame update


    private float m_TimeSinceLastShot = 0;
    void Start()
    {
        velocities = new FIFO(N);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
     
    // Update is called once per frame
    void Update()
    {
        velocities.Add(player.Velocity);
        Vector3 vw = velocities.Average();
        Vector3 xow = player.transform.position;
        float a = Vector3.Dot(vw, vw) - sp*sp;
        float b = 2 * Vector3.Dot(xow - transform.position, vw);
        Vector3 aux = xow - transform.position;
        float c = Vector3.Dot(aux, aux);

        double x2;
        SolveQuadratic(a, b, c, out t, out x2);

        if ((t > x2 && x2>0) || t<0)
        {
            t = x2;
        }

        Vector3 finalPos = xow + vw * (float)t;

        transform.LookAt(finalPos);
        m_TimeSinceLastShot += Time.deltaTime;
        if (CanShoot() && t>0)
        {
            ShootProjectile();
            m_TimeSinceLastShot = 0;
        }

    }

    private bool CanShoot()
    {
        //  ## TO-DO 8 - Comprobar si puedo disparar #

        return m_TimeSinceLastShot >= m_TimeBetweenShots;
    }

    private void ShootProjectile()
    {
        // TO-DO 2
        // 1.- Instanciar el proyectil pasado como variable pública de la clase, en la posición y rotación del punto de disparo "m_projectile"
        // 1.2.- Guardarse el objeto devuelto en una variable de tipo Rigidbody
        // 2.- Asignar una velocidad inicial en función de m_Velocity al campo velocity del rigidBody. La dirección será la del m_ShootPoint. Una vez que esté orientado el pollo simiplemente hay que añadirle velocidad.
        // 3.- Ignorar las colisiones entre nuestro proyectil y nosotros mismos
        GameObject project = Instantiate(m_projectile, m_ShootPoint.transform.position,
                            m_ShootPoint.rotation);
        Collider projectileCollider = project.GetComponent<Collider>();
        Collider mycollider = transform.root.GetComponent<Collider>();
        Physics.IgnoreCollision(projectileCollider, mycollider);

        project.GetComponent<ProjectileMov>().vel = sp;
        //Debug.Log("¡Pollo!");
    }

    public static void SolveQuadratic(double a, double b, double c, out double x1, out double x2)
    {
        //Quadratic Formula: x = (-b +- sqrt(b^2 - 4ac)) / 2a

        //Calculate the inside of the square root
        double insideSquareRoot = (b * b) - 4 * a * c;

        if (insideSquareRoot < 0)
        {
            //There is no solution
            x1 = double.NaN;
            x2 = double.NaN;
        }
        else
        {
            //Compute the value of each x
            //if there is only one solution, both x's will be the same
            double sqrt = Math.Sqrt(insideSquareRoot);
            x1 = (-b + sqrt) / (2 * a);
            x2 = (-b - sqrt) / (2 * a);
        }
    }


    class FIFO {
        Vector3[] data;
        int entry;

        public FIFO(int tam) {
            data = new Vector3[tam];
            entry = 0;
        }

        public void Add(Vector3 item) {
            data[entry] = item;
            entry = (entry + 1) % data.Length;
        }

        public Vector3 Average()
        {
            Vector3 sum = Vector3.zero;

            foreach (Vector3 v in data) {
                sum += v;
            }
            return sum/data.Length;
        }
    }
}
