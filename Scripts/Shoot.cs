using UnityEngine;
using System.Collections;

// Contiene la declaraci�n de la clase Shoot, encargada de la mec�nica de disparo.
// Permite dos formas de disparo exclusivas:
//      - Proyectiles
//      - Raycast
public class Shoot : MonoBehaviour
{

    #region Exposed fields

    /// <summary>
    /// Proyectil a disparar. Si no est� asignado, la mec�nica de disparo utilizar�
    /// Raycast para calcular los puntos de impacto
    /// </summary>
    /// 
    public Rigidbody m_projectile = null;

    public GameObject bulletHole;
    /// <summary>
    /// Velocidad inicial del proyectil que se dispara
    /// </summary>
    public float m_InitialVelocity = 50f;

    /// <summary>
    /// Punto desde el que se dispara el proyectil
    /// </summary>
    public Transform m_ShootPoint;

    /// <summary>
    /// Tiempo transcurrido entre disparos
    /// </summary>
	public float m_TimeBetweenShots = 0.25f;
	
	/// <summary>
	/// Booleano para indicar si el arma es autom�tica
	/// </summary>
	public bool m_IsAutomatic = false;

    public bool m_IsShotgun = false;

    /// <summary>
    /// Particulas que saltan cuando un arma sin proyectil acierta en algo.
    /// </summary>
    public GameObject m_Sparkles;

    public ParticleSystem m_Shoot;

    /// <summary>
    /// Define el alcance del arma que no utiliza proyectiles.
    /// </summary>
    public float m_ShootRange = 100;

    /// <summary>
    /// Fuerza que aplican los disparos que no usan proyectiles.
    /// </summary>
    public float m_ShootForce = 10;

    /// <summary>
    /// Sonido del arma.
    /// </summary>
    public AudioClip m_ShootSound;

    public bool IA = false;

    public float m_MachineGunDamage = 10;

    public float m_ShotgunDamage = 20;
    #endregion

    #region Non exposed fields

    /// <summary>
    /// Tiempo transcurrido desde el �ltimo disparo
    /// </summary>
    private float m_TimeSinceLastShot = 0;

    /// <summary>
    /// Indica si estamos disparando (util en modo autom�tico).
    /// </summary>
    private bool m_IsShooting = false;

    private AudioSource audioSource=null;

    private WeaponManager wm; //to check if you can shoot
                              //if the weapon is changing, you cant
    private Recoil rec;

    
    #endregion

    #region Monobehaviour Calls

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_TimeSinceLastShot = m_TimeBetweenShots;
        audioSource.clip = m_ShootSound;
        wm = GetComponentInParent<WeaponManager>();
        rec = GetComponentInParent<Recoil>();
    }

    void OnEnable() {
        m_IsShooting = false;
    }
    /// <summary>
    /// En el m�todo Update se consultar� al Input si se ha pulsado el bot�n de disparo
    /// </summary>
    void Update() {

        // Ser� necesario llevar cuenta del tiempo transcurrido

        //  ## TO-DO 4 - Actualizar el contador m_TimeSinceLastShot ## 
        // Para ello, habr� que sumarle el tiempo de ejecuci�n del anterior frame

        m_TimeSinceLastShot += Time.deltaTime;
        if (CanShoot() && GetFireButton())
        {

            // ## TO-DO 5 - En funci�n de si hay proyectil o no, usar la funci�n de disparo
            // con proyectil, o la de disparo con rayo ## 

            if (!m_IsAutomatic && !m_IsShotgun)
            {
                rec.addRecoil(15);
                ShootProjectile();
            }
            else if (!m_IsAutomatic && m_IsShotgun)
            {
                rec.addRecoil(10);
                ShootShotgun();
                //ShootProjectile();
            }
            else
            {
                rec.addRecoil(5);
                ShootRay();
            }

            // ## TO-DO 6 - Reiniciar el contador m_TimeSinceLastShot ## 
            m_TimeSinceLastShot = 0;


            if (!m_IsShooting)
            {
                m_IsShooting = true;
                // ## TO-DO 7 Poner sonido de disparo.
                audioSource.Play();

            }
        }
        else
        {
            if (!GetFireButton() || wm.IsChanging)
            {
                m_IsShooting = false;
                if (m_IsAutomatic)
                {
                    audioSource.Stop();
                }
            }
            // ## TO-DO 8 Parar sonido de disparo.

        }

    }
	
	// 
    /// <summary>
    /// En esta funci�n comprobamos si el tiempo que ha pasado desde la �ltima vez que disparamos
    /// es suficiente para que nos dejen volver a disparar 
    /// </summary>
    /// <returns>true si puede disparar y falso si no puede.</returns>
	private bool CanShoot()
	{
        //  ## TO-DO 8 - Comprobar si puedo disparar #
        
        return m_TimeSinceLastShot>=m_TimeBetweenShots && !wm.IsChanging;
	}
	
    /// <summary>
    /// Devuelve si se ha pulsado el bot�n de disparo
    /// </summary>
    /// <returns>true si puede disparar y falso si no puede.</returns>
	private bool GetFireButton()
	{
        //Obtener el bot�n de disparo. Si es autom�tico se pulsar� GetButton y si no, GetButtonDown. El bot�n que usamoremos es "Fire"
        //  ## TO-DO 1 ## 
        if (m_IsAutomatic)
            return Input.GetButton("Fire1");
        else
            return Input.GetButtonDown("Fire1");
        
    }
	
    /// <summary>
    /// Disparamos un proyectil.
    /// </summary>
	private void ShootProjectile()
	{
        // TO-DO 2
        // 1.- Instanciar el proyectil pasado como variable p�blica de la clase, en la posici�n y rotaci�n del punto de disparo "m_projectile"
        // 1.2.- Guardarse el objeto devuelto en una variable de tipo Rigidbody
        // 2.- Asignar una velocidad inicial en funci�n de m_Velocity al campo velocity del rigidBody. La direcci�n ser� la del m_ShootPoint. Una vez que est� orientado el pollo simiplemente hay que a�adirle velocidad.
        // 3.- Ignorar las colisiones entre nuestro proyectil y nosotros mismos
        Rigidbody project = Instantiate(m_projectile, m_ShootPoint.transform.position, 
                            m_ShootPoint.rotation) as Rigidbody;
        project.velocity = project.transform.forward * m_InitialVelocity;
        Collider projectileCollider = project.GetComponent<Collider>();
        Collider mycollider = transform.root.GetComponent<Collider>();
        Physics.IgnoreCollision(projectileCollider, mycollider);
        audioSource.PlayOneShot(m_ShootSound);
        
        //Debug.Log("�Pollo!");
    }

    /// <summary>
    /// Disparamos usando un rayo.
    /// </summary>
    private void ShootRay()
    {
        // ## TO-DO 9 - Funci�n que dispara con rayos ## 
        // 1.- Lanzar un rayo utlizando para ello el m�dulo de f�sica -> pista Physics.Ra...
        // 2.- Aplicar una fuerza en el punto de impacto.
        // 3.- Colocar particulas de chispas en el punto de impacto -> pista Instanciamos pero no nos preocupasmo del destroy porque el asset puede autodestruirse (componente particle animator).
        m_Shoot.Emit(1);
        RaycastHit hit;
        if(Physics.Raycast(m_ShootPoint.position, m_ShootPoint.forward, out hit, m_ShootRange))
        {
            if(hit.transform.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
                hit.collider.SendMessage("Damage", m_MachineGunDamage);
            }
            else
            {
                Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            }
            GameObject chispitas = Instantiate(m_Sparkles, hit.point, Quaternion.identity) as GameObject;
            chispitas.GetComponent<ParticleSystem>().Emit(1);
            Destroy(chispitas, 0.5f);
           

        }
    }

    private void ShootShotgun()
    {
        Debug.Log("ShootShotgun");
        //the cone where the rays are generated
        float h = 5; //height
        float r = 0.3f; //base radius
        m_Shoot.Emit(1);
        RaycastHit hit1, hit2, hit3;
        if(Physics.Raycast(m_ShootPoint.position, transform.TransformDirection(Random.Range(-r, r), Random.Range(-r, r), h).normalized, out hit1, m_ShootRange)){
            if (hit1.transform.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
                hit1.collider.SendMessage("Damage", m_ShotgunDamage);
            }
            else
            {
                Instantiate(bulletHole, hit1.point, Quaternion.FromToRotation(Vector3.forward, hit1.normal));
            }
            GameObject chispitas = Instantiate(m_Sparkles, hit1.point, Quaternion.identity) as GameObject;
            chispitas.GetComponent<ParticleSystem>().Emit(1);
            Destroy(chispitas, 0.5f);

        }
        if (Physics.Raycast(m_ShootPoint.position, transform.TransformDirection(Random.Range(-r, r), Random.Range(-r, r), h).normalized, out hit2, m_ShootRange))
        {
            if (hit2.transform.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
                hit2.collider.SendMessage("Damage", m_ShotgunDamage);
            }
            else
            {
                Instantiate(bulletHole, hit2.point, Quaternion.FromToRotation(Vector3.forward, hit2.normal));
            }
            GameObject chispitas = Instantiate(m_Sparkles, hit2.point, Quaternion.identity) as GameObject;
            chispitas.GetComponent<ParticleSystem>().Emit(1);
            Destroy(chispitas, 0.5f);
            

        }
        if (Physics.Raycast(m_ShootPoint.position, transform.TransformDirection(Random.Range(-r, r), Random.Range(-r, r), h).normalized, out hit3, m_ShootRange))
        {
            if (hit3.transform.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
                hit3.collider.SendMessage("Damage", m_ShotgunDamage);
            }
            else
            {
                Instantiate(bulletHole, hit3.point, Quaternion.FromToRotation(Vector3.forward, hit3.normal));
            }
            GameObject chispitas = Instantiate(m_Sparkles, hit3.point, Quaternion.identity) as GameObject;
            chispitas.GetComponent<ParticleSystem>().Emit(1);
            Destroy(chispitas, 0.5f);
            
        }
    }

    //## TO-DO 3 Mostrar un puntero laser con la direcci�n de disparo.

    private void OnDrawGizmos()
    {
        if (m_ShootPoint != null)
            Debug.DrawRay(m_ShootPoint.position, m_ShootPoint.forward * 2f, Color.red);
    }



    #endregion
}
