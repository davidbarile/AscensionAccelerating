using System;
using System.Linq;
using UnityEngine;

public class SequenceRandomizer : MonoBehaviour
{
    [SerializeField] private SequenceTab openingSequenceTab;
    [SerializeField] private SequenceTab[] sequenceTabs;

    private int[] openingSequence = { 5, 4, 3, 2, 1 };//set custom order here
    private int[] numbers = { 0, 1, 2, 3, 4, 5 };

    public void GenerateRandomPatterns()
    {
        var openingSequence = this.openingSequence.ToList();
        this.openingSequenceTab.SetPatterns(openingSequence);

        var randomNums = this.numbers.ToList();
        foreach(var tab in this.sequenceTabs)
        {
            randomNums.RandomizeList();
            tab.SetPatterns(randomNums);
        }
    }
}