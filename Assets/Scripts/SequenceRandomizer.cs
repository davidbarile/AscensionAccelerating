using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequenceRandomizer : MonoBehaviour
{
    [SerializeField] private int maxPatterns = 5;
    [SerializeField] private SequenceTab openingSequenceTab;
    [SerializeField] private SequenceTab[] sequenceTabs;

    private int[] openingSequence = { 5, 4, 3, 2, 1 };//set custom order here
    private int[] numbers = { 0, 1, 2, 3, 4, 5 };

    public void SetOpeningPattern()
    {
        var openingSequence = this.openingSequence.ToList();
        this.openingSequenceTab.SetPatterns(openingSequence);
    }

    public void GenerateRandomPatterns()
    {
        SetOpeningPattern();

        foreach(var tab in this.sequenceTabs)
        {
            tab.Numbers.Clear();
        }

        var randomNums = this.numbers.ToList();

        foreach (var tab in this.sequenceTabs)
        {
            randomNums.RandomizeList();
            tab.SetNumbers(randomNums);
            //tab.SetPatterns(randomNums);
        }
        
        for (int i = 0; i < this.sequenceTabs.Length; ++i)
        {
            if (i == 0) continue;

            var tab = this.sequenceTabs[i];

            var isUnique = true;

            //loop thru previous tabs
            for (int j = 0; j < i; ++j)
            {
                var prevTab = this.sequenceTabs[j];
                if (tab.OrderHash == prevTab.OrderHash)
                    isUnique = false;
                else if (tab.Numbers[0] == prevTab.Numbers[0])
                    isUnique = false;
            }

            while(!isUnique)
            {
                randomNums.RandomizeList();
                tab.SetNumbers(randomNums);
                isUnique = true;

                //loop thru previous tabs
                for (int j = 0; j < i; ++j)
                {
                    var prevTab = this.sequenceTabs[j];
                    if (tab.OrderHash == prevTab.OrderHash)
                        isUnique = false;
                    else if (tab.Numbers[0] == prevTab.Numbers[0])
                        isUnique = false;
                }
            }
        }
        
        foreach(var tab in this.sequenceTabs)
        {
            tab.ApplyNumbers();
        }
    }
}