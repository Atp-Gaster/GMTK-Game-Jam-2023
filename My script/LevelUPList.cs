using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPList : MonoBehaviour
{
    [Header("Link GameObject")]
    public GameObject Player;
    public GameObject Sword;

    public SpawnEnemy[] Spawners;
    public GameObject UpgradeUI;

    public bool AlreadyProjectile = false;
    public bool AlreadyShilde = false;

    public Button Slot2;
    public Text Slot2Text;
    public Button Slot3;
    public Text Slot3Text;
    public void UpgradeMaxHP() //Upgrade ID 0
    {
        Player.GetComponent<Player>().MaxHitpoint += 5; 
        Player.GetComponent<Player>().Hitpoint += 5;
    }

    public void EnableProjectile() //Upgrade ID 1
    {
        Sword.GetComponent<SwordController>().EnableProjectile = true;
        AlreadyProjectile = true;
    }

    public void EnableShilde() //Upgrade ID 2
    {
        Player.GetComponent<Player>().EnableShield = true;
        AlreadyShilde = true;
    }

    public void ShowInterface()
    {
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }

        foreach (var mapping in Spawners)
        {
            mapping.EnableSpawn = false;
        }
        UpgradeUI.SetActive(true);               
    }

    public void CloseInterface()
    {
        foreach (var mapping in Spawners)
        {
            mapping.RESpawn = true;
        }
        UpgradeUI.SetActive(false);
    }

    // Start is called before the first frame update    
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (AlreadyProjectile)
        {
            Slot2.interactable = false;
            Slot2Text.text = "Owned";
        }

        if (AlreadyShilde)
        {
            Slot3.interactable = false;
            Slot3Text.text = "Owned";
        }


       
    }
}
