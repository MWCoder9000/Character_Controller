using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    public enum Weapons
    {
        Bow,
        Gun
    };

    public enum ContainerName
    {
        Guns,
        Bows
    };

    [SerializeField]
    private Weapons weapons = Weapons.Gun;

    Dictionary<Weapons, ContainerName> weaponNameByWeapons = new Dictionary<Weapons, ContainerName>();
    Dictionary<ContainerName, Transform> weaponContainerByName = new Dictionary<ContainerName, Transform>();

    void Start()
    {
        InitialiseContainers();
        WeaponsSwitch(weapons);
    }


    void InitialiseContainers()
    {
        weaponNameByWeapons.Add(Weapons.Bow, ContainerName.Bows);
        weaponNameByWeapons.Add(Weapons.Gun, ContainerName.Guns);

        weaponContainerByName.Add(ContainerName.Guns, gameObject.transform.Find(ContainerName.Guns.ToString()));
        weaponContainerByName.Add(ContainerName.Bows, gameObject.transform.Find(ContainerName.Bows.ToString()));
    }

    void DisableAllContainers()
    {
        foreach (KeyValuePair<ContainerName, Transform> container in weaponContainerByName)
        {
            container.Value.gameObject.SetActive(false);
        }
    }

    void ActivateWeapons(Weapons weapons)
    {
        ContainerName container;
        if (weaponNameByWeapons.TryGetValue(weapons, out container))
        {
            if (weaponContainerByName.TryGetValue(container, out Transform transform))
            {
                transform.gameObject.SetActive(true);
            }
        }
    }

    void WeaponsSwitch(Weapons weapons)
    {
        DisableAllContainers();
        ActivateWeapons(weapons);
    }

    public Weapons GetWeapons()
    {
        return weapons;
    }

    public void SetWeapons(Weapons weapons)
    {
        if (this.weapons == weapons)
        {
            return;
        }

        this.weapons = weapons;

        WeaponsSwitch(weapons);
    }
}
