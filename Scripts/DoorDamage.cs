using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDamage : MonoBehaviour
{
    public Door door;
    public float damage;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && door.DoorState == Door.State.CLOSING)
        {
            player.SendMessage("Damage", damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
