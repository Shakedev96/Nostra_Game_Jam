using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{   
    [SerializeField] private Transform spawnPt;

    [SerializeField] GameObject bulletPreFab;

    [SerializeField] public PerspectiveMove player;
    //[SerializeField] public rbMove player;

    [SerializeField] private float bulletSpeed;    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PerspectiveMove>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            player.shooting = true;
            Shoot();

        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            player.shooting = false;
        }
       
    }

    public void Shoot()
    {
         var bullet = Instantiate(bulletPreFab , spawnPt.position, spawnPt.rotation);
            bullet.GetComponent<Rigidbody>().velocity = spawnPt.forward * bulletSpeed;
    }
}
