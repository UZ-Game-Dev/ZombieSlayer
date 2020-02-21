using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Types
{
    Ammunition,
    Bandages,
    Coin
}

public class Item : MonoBehaviour
{
    public Types types;

    public float lifeTime;

    public bool touched;
    private PlayerMovement player;
    private Collider2D collision;

    void Update()
    {
        Destroy(this.gameObject, lifeTime);

        if (touched && Input.GetButtonDown("Use"))
        {
            player.Pickup(types, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        player = coll.GetComponent<PlayerMovement>();
        if (player != null)
        {
            collision = coll;
            touched = true;
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (touched)
        {
            touched = false;
        }
    }
}
/*
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