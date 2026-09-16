using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void Interact()
    {
        Debug.Log("Interacting with ClearCounter");
        Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTranform.localPosition = Vector3.zero;
    }
}
