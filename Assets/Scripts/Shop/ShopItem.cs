using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public SidedWeapon itemToBuy;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set les valeurs du nom / prix de l'item au début du jeu
        Text[] itemTexts = GetComponentsInChildren<Text>();
        foreach (Text t in itemTexts)
        {
            switch (t.name)
            {
                case "TextPrice":
                    t.text = itemToBuy.price.ToString();
                    break;
                case "ItemName":
                    t.text = itemToBuy.name;
                    break;
            }
        }
    }
}
