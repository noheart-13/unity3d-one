using UnityEngine;

public class TrushCounter : BaseCounter
{
    public static event System.EventHandler OnAnyObjectTrushed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrushed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrushed?.Invoke(this, System.EventArgs.Empty);
        }
    }


}
