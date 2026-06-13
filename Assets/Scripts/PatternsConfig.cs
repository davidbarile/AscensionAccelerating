using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternsConfig", menuName = "PatternsConfig")]
public class PatternsConfig : ScriptableObject
{
    [SerializeField] private bool doValidate;
    [Space] public PatternData[] PatternDatas;
    [TextArea(0, 10)] public string[] PatternCompleteMessages;
    [TextArea(0, 10)] public string[] FinalMessages;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!this.doValidate)
            return;

        this.doValidate = false;

        foreach (PatternData patternData in this.PatternDatas)
        {
            patternData.Name = $"{patternData.Sprite.name} - {patternData.AudioClip.name}";
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }
}
#endif

[Serializable]
public class PatternData
{
    public string Name;
    public PatternVisualization Prefab;
    public Sprite Sprite;
    public Sprite ThumbnailSprite;
    public AudioClip AudioClip;
}