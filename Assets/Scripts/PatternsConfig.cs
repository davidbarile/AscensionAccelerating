using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternsConfig", menuName = "PatternsConfig")]
public class PatternsConfig : ScriptableObject
{
    public PatternData[] PatternDatas;
}

[Serializable]
public class PatternData
{
    public Sprite Sprite;
    public AudioClip AudioClip;
}