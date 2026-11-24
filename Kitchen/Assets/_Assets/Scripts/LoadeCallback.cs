using UnityEngine;

public class LoadeCallback : MonoBehaviour
{
   private bool isFirstFrame = true;
    private void Update()
    {
        if (isFirstFrame)
        {
            isFirstFrame = false;
            Loader.LoaderCallback();
        }
    }
}
