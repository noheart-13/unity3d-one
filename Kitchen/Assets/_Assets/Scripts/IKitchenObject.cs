using UnityEngine;

public interface IKitchenObject
{
    public Transform GetKitchenObjectFollowTransfrom();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();

}
