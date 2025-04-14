using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WatchableNotificationView : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _text;
  [SerializeField] private Button _button;
  [SerializeField] private Image _bar;

  public event Action<WatchableNotificationView> Clicked;
  
  private void Awake()
  {
    _button.onClick.AddListener(() => Clicked?.Invoke(this));
  }

  public void UpdateInfo(WatchableReward watchableReward)
  {
    _text.text = watchableReward.Amount.ToString();
  }

  public void UpdateBar(float fillAmount)
  {
    _bar.fillAmount = fillAmount;
  }
}
