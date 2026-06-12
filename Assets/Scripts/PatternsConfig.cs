using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternsConfig", menuName = "PatternsConfig")]
public class PatternsConfig : ScriptableObject
{
    public PatternData[] PatternDatas;

    [TextArea(0, 10)] public string[] PatternCompleteMessages;
    [TextArea(0, 10)] public string[] FinalMessages;
}

[Serializable]
public class PatternData
{
    public Sprite Sprite;
    public Sprite ThumbnailSprite;
    public AudioClip AudioClip;
}