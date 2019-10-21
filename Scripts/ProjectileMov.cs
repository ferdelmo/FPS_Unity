using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMov : MonoBehaviour
{
    // Start is called before the first frame update
    public float vel;
    public float dmg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * vel * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("Damage", dmg);
  
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hit.gameObject.SendMessage("Damage", dmg);
        Debug.Log("HIT");
    }
}
