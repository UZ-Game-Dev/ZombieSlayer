using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Types
{
    Ammunition,
    Bandages,
    Coin,
    M4,
    Shotgun,
    SMG,
    GoldBullets_PU,
    Inv_PU,
    Speed_PU,
    Nuke_PU,
    Freeze_PU,
    Scare_PU,
    Gold,
    AK47,
}

public class Item : MonoBehaviour
{
    public Types types;
    public bool isWeapon;
    public float chanceToDrop;
    public float lifeTime;
    public float timeToPing;
    
    public bool touched;
    public float timer = 0;

    private PlayerMovement player;
    private WeaponMenager weaponMenager;

    private Collider2D collision;
    private int weaponNumber = 0;


    private void Start()
    {
        if (isWeapon)
        {
            switch (types)
            {
                case Types.M4:
                    weaponNumber = 1;
                    break;
                case Types.Shotgun:
                    weaponNumber = 2;
                    break;
                case Types.SMG:
                    weaponNumber = 3;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= lifeTime)
        {
            Destroy(this.gameObject);
        }
        if (timer >= timeToPing)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * 4, 1f));
        }
    }

    void Update()
    {
        //instant podnoszenie
        if (touched && types == Types.Gold)
        {
            player.playSound();
            player.Pickup(types);
            Destroy(gameObject);
        }
        else if (touched && Input.GetButtonDown("Use"))
        {
            if (isWeapon)
            {
                weaponMenager.weaponPickedUp = true;
                if (weaponMenager.currentWeapon != weaponNumber)
                {
                    weaponMenager.ChangeWeapon(weaponNumber);
                }
                else
                {
                    //add ammo
                    if (weaponMenager.currentWeapon != 0)//Sprawdz czy jest wybrany AK-47
                    {
                        player.GetComponentInChildren<WeaponDef>().AddAmmo();
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                if (types == Types.Ammunition)
                {
                    if (weaponMenager.currentWeapon == 0) return;
                }
                player.Pickup(types);
            }
            player.playSound();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        player = coll.GetComponent<PlayerMovement>();
        weaponMenager = coll.GetComponentInChildren<WeaponMenager>();
        if (player != null)
        {
            collision = coll;
            touched = true;
            player.pickUP_Sprite.SetActive(true);
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (touched)
        {
            touched = false;
            player.pickUP_Sprite.SetActive(false);
        }
    }

}
/* Rozumiem, że to nie potrzebne?
public class Inventory
{

    [System.Serializable]
    public class Amount
    {
        public current;
        public int max;

        public void AddCurrent(int value)
        {
            current = Mathf.Clamp(current + value, 0, max);
        }
        public Amount() { }
        public Amount(int current, int max)
        {
            this.max = max;
            this.AddCurrent(current);
        }
    }

    [System.Serializable]
    public class Consumables
    {
        public Types type;
        public Amount amount;
        public Consumables() { }

        public Consumables(Types type, Amount amount)
        {
            this.type = type;
            this.amount = amount;
        }

        public Consumables(Types type)
        {
            this.type = type;
            this.amount = new Amount();
        }
    }

 
    public Consumables[] consumables;

    public void AddConsumable(Consumables.Types type, int value)
    {
        for (int i = 0; i < this.consumables.Length; i++)
        {
            if (this.consumables[i].type != type) continue;

            this.consumables[i].amount.AddCurrent(value);
            break;
        }
    }   
}
*/