using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    [System.Serializable]
    public class Amount
    {
      
        public int current;
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
        public enum Types
        {
            Ammunition,
            Bandages,
            Coin
        }

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