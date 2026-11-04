using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Ingredient not valid for this plate
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Ingredient already on the plate
            return false;
        }else
        {
            //Ingredient not on the plate, add it
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }


}
