using System.Collections.Generic;
using UnityEngine;

public class SequenceTab : MonoBehaviour
{
    [SerializeField] private Transform[] patterns;

    public void SetPatterns(List<int> inPatternOrder)
    {
        for(int i = 0; i < this.patterns.Length; ++i)
        {
            var pattern = this.patterns[i];
            var shouldShow = i < inPatternOrder.Count;

            pattern.gameObject.SetActive(shouldShow);

            if(shouldShow)
                pattern.SetSiblingIndex(inPatternOrder[i]);
        }
    }
}