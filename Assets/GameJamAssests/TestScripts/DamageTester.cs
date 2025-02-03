using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    [SerializeField] private Variables _playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DamgeTest();
        } 
    }
    private void DamgeTest()
    {
       
        
            _playerHealth.ApplyChange(-1f);
        
    }
    
}
