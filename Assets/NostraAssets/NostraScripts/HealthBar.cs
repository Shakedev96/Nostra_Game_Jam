using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image _healthFill;

    [SerializeField] private Variables _playerHealth;
    [SerializeField] private Variables _playerMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Reseting the player health to 100 at the start of play mode
    _playerHealth.Value = _playerMaxHealth.Value;
    }

    // Update is called once per frame
    void Update()
    {
        _healthFill.fillAmount = _playerHealth.Value/ _playerMaxHealth.Value;
    }

    
}
