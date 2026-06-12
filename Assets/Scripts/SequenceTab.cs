using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceTab : MonoBehaviour
{
    public List<int> Numbers = new();
    public int OrderHash = 0;

    [SerializeField] private PatternThumbnail[] patterns;

    [SerializeField] private GameObject selectedState;
    [SerializeField] private GameObject disabledOverlay;
    [SerializeField] private Button button;

    public void SetPatterns(List<int> inPatternOrder)
    {
        for(int i = 0; i < this.patterns.Length; ++i)
        {
            var pattern = this.patterns[i];
            var shouldShow = i < inPatternOrder.Count;
            pattern.gameObject.SetActive(shouldShow);

            if( shouldShow)
            {
                var index = inPatternOrder[i];
                pattern.SetPattern(index);
            }
        }
    }

    public void SetNumbers(List<int> inPatternOrder)
    {
        this.Numbers = new List<int>(inPatternOrder);

        this.OrderHash = 0;

        for(int i = 0; i < this.Numbers.Count; ++i)
        {
            this.OrderHash += this.Numbers[i] * (int)Mathf.Pow(10, 4 - i);
            if (i == 5) break;
        }
    }

    public void ApplyNumbers()
    {
        SetPatterns(this.Numbers);
    }

    public void SetSelected(bool inSelected)
    {
        if(this.selectedState)
            this.selectedState.SetActive(inSelected);
    }

    public void SetDisabled(bool inDisabled)
    {
        if(this.disabledOverlay)
            this.disabledOverlay.SetActive(inDisabled);
    }

    public void SetInteractable(bool inInteractable)
    {
        if(this.button)
            this.button.interactable = inInteractable;
    }
}