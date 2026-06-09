using System.Collections.Generic;
using UnityEngine;

public class SequenceTab : MonoBehaviour
{
    public List<int> Numbers = new();
    public int OrderHash = 0;

    [SerializeField] private Transform[] patterns;

    public void SetPatterns(List<int> inPatternOrder)
    {
        for (int i = 0; i < this.patterns.Length; ++i)
        {
            var pattern = this.patterns[i];
            var shouldShow = i < inPatternOrder.Count;

            pattern.gameObject.SetActive(shouldShow);

            if (shouldShow)
                pattern.SetSiblingIndex(inPatternOrder[i]);
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
}