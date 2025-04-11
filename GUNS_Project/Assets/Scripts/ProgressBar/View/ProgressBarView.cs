using UnityEngine;
using UnityEngine.UI;

public class ProgressBarView : MonoBehaviour
{
    [SerializeField] private Slider _bar;

    public void UpdateBar(float value)
    {
        _bar.value = value;
    }
}
