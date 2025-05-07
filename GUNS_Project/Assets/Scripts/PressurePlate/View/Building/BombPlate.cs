using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BombPlate : AbstractPlateByBuilding
{
    private Image _fill;
    private TextMeshProUGUI _text;

    private float _targetFill;
    
    private void Update()
    {
        _fill.fillAmount = _targetFill;
        
        if (_updatedText != null)
        {
            _text.text = _updatedText();
        }
    }

    private Func<string> _updatedText;

    private void OnEnable()
    {
        _fill = FindFill();
        _text = FindText();
    }

    private Image FindFill()
    {
        return GetComponentsInChildren<Image>().FirstOrDefault(x => x.name == "Progress");
    }   
    
    private TextMeshProUGUI FindText()
    {
        return GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Title");
    }

    public void UpdateBar(float currentFillness)
    {
        _targetFill = currentFillness;
    }

    public void UpdateText(Func<string> updatedText)
    {
        _updatedText = updatedText;

        if (updatedText == null)
        {
            _updatedText = () => "Ready!";
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        BoxCollider boxCollider = GetComponent<BoxCollider>();

        boxCollider.center = new Vector3(0, -112.82f, 0);
        boxCollider.size = new Vector3(67, 95f, 70.61f);
    }
}
