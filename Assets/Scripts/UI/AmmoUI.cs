using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField, Tooltip("The type of ammo to display the UI for")]
    private AmmoType type;
    private TextMeshProUGUI text;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (type) {
            case AmmoType.Projectile:
                text.text = gameManager.ProjectileAmmo.ToString();
                break;
            case AmmoType.Laser:
                text.text = gameManager.LaserAmmo.ToString();
                break;
            default:
                text.text = "Ammo type set to a nonexistant value!";
                break;
        }
    }
}
