using System;
using UnityEngine;

public class ContainCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);


            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
   
}