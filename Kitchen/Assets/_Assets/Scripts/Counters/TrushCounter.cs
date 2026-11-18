using UnityEngine;

public class TrushCounter : BaseCounter
{
    public static event System.EventHandler OnAnyObjectTrushed;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrushed?.Invoke(this, System.EventArgs.Empty);
        }
    }


}
