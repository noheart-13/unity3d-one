using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressBarGameObject;
    [SerializeField] private Slider progressBarSlider;
    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = progressBarGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
          Debug.LogError("GameObject " + progressBarGameObject.name + " does not have a component that implements IHasProgress.");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        progressBarSlider.value = 0f;

        gameObject.SetActive(false);
    }
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        progressBarSlider.value = e.progressNormalized;
        if (e.progressNormalized == 1f || e.progressNormalized == 0f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
