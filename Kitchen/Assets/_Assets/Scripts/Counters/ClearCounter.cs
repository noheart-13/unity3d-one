using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchen object here, so give the player a kitchen object
            if (player.HasKitchenObject())
            {
                //Player is carrying something, so do nothing
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player is not carrying anything, so give them a kitchen object
            }
        }
        else
        {
            //There is a kitchen object here, so give it to the player
            if (player.HasKitchenObject())
            {
                //Player is carrying something, so do nothing
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject1))
                {
                    //Player is holding a plate, so try to add the kitchen object to the plate
                      if (plateKitchenObject1.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                      {
                          GetKitchenObject().DestroySelf();
                      }
                }
                else
                {
                    //Player is not holding a plate, so do nothing
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject2))
                    {
                        //Counter is holding a plate, so try to add the player's kitchen object to the plate
                        if (plateKitchenObject2.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //Player is not carrying anything, so give them the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
