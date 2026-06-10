using System.Collections.Generic;
using UnityEngine;

public class SequenceTab : MonoBehaviour
{
    public List<int> Numbers = new();
    public int OrderHash = 0;

    [SerializeField] private Transform[] patterns;

    public void SetPatterns(List<int> inPatternOrder)
    {
        var activeIndices = new HashSet<int>(inPatternOrder);
        int siblingIndex = 0;

        for (int orderIndex = 0; orderIndex < inPatternOrder.Count; ++orderIndex)
        {
            int patternIndex = inPatternOrder[orderIndex];
            if (patternIndex < 0 || patternIndex >= this.patterns.Length)
                continue;

            var pattern = this.patterns[patternIndex];
            pattern.gameObject.SetActive(true);
            pattern.SetSiblingIndex(siblingIndex++);
        }

        for (int i = 0; i < this.patterns.Length; ++i)
        {
            if (activeIndices.Contains(i))
                continue;

            var pattern = this.patterns[i];
            pattern.gameObject.SetActive(false);
            pattern.SetSiblingIndex(siblingIndex++);
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