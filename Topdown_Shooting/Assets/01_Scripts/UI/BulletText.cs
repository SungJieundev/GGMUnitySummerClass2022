using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BulletText : MonoBehaviour
{
    public AgentWeapon _agentWeapon;
    public Weapon _weapon;
    public TextMeshProUGUI text;
    
    private void Update() {
        text.text = _weapon.Ammo.ToString() + " / " + _agentWeapon._totalAmmo.ToString();
    }

    
}
