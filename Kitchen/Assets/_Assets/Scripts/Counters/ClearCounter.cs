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
            }
            else
            {
                //Player is not carrying anything, so give them the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
