using System;
using UnityEngine;

[RequireComponent(typeof(UiManager))]
[RequireComponent(typeof(SequenceRandomizer))]
[RequireComponent(typeof(SaveManager))]
public class AppManager : MonoBehaviour
{
	public static int CurrentSequenceIndex = -1;
	private UiManager uiManager;
	private SequenceRandomizer sequenceRandomizer;
	private SaveManager saveManager;

	private void Awake()
	{
		this.uiManager = GetComponent<UiManager>();
		this.sequenceRandomizer = GetComponent<SequenceRandomizer>();
		this.saveManager = GetComponent<SaveManager>();

		SaveManager.IN = this.saveManager;
	}

	private void Start()
	{
		this.uiManager.Init();

		this.sequenceRandomizer.SetOpeningPattern();

		var isFirstUse = this.saveManager.CheckIfFirstUse();

		if (isFirstUse)
		{
			this.sequenceRandomizer.GenerateRandomPatterns();
			AudioListener.volume = .75f;
			this.saveManager.SaveVolume(AudioListener.volume);
			CurrentSequenceIndex = -1;
			Debug.Log($"<color=#00FF00>First Use - GenerateRandomPatterns()</color>");
		}
		else
		{
			this.sequenceRandomizer.LoadSequences();
			AudioListener.volume = this.saveManager.GetVolume();

			CurrentSequenceIndex = this.saveManager.GetSequenceIndex();

			if (CurrentSequenceIndex != -1 && DateTime.Now.Date.DayOfYear != this.saveManager.GetDateInt())
			{
				Debug.Log($"<color=yellow>New Day!  Setting CurrentSequenceIndex from {CurrentSequenceIndex} to 0</color>");
				CurrentSequenceIndex = 0;
			}

			Debug.Log($"<color=red>Not First Use - LoadSequences().  CurrentSequenceIndex = {CurrentSequenceIndex}</color>. volume = {AudioListener.volume}");
		}

		this.uiManager.HowThisWorksTab.SetSelected(isFirstUse);

		this.saveManager.SetFirstUse();

		this.uiManager.SetTabStatesToIndex(CurrentSequenceIndex);
		this.uiManager.SequenceVisualizer.SetVolumeSliderValue(AudioListener.volume);
	}

	private void OnApplicationQuit()
	{
		this.saveManager.SaveDate(DateTime.Now.Date.DayOfYear);
		Debug.Log($"<color=red>OnApplicationQuit.  CurrentSequenceIndex = {CurrentSequenceIndex}</color>");
	}
}