using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SupportPage : MonoBehaviour 
{
	[SerializeField] TMP_Text label;

	private readonly string format1 = "<color=#FFF19B><link=\"";
	private readonly string format2 = "\">tap here</link></color>";

	private string url_Email1 = "";
	private string url_LearnMore = "";

	private string lineBreak = "<size=40>\n\n</size>";

	private int tapCounter = 0;

	//"<color=#0000FF><u><link=\"https://unity.com\">here</link></u></color>";

	private void Start()
	{
		this.url_Email1 = $"{format1}mailto:support@goldenpodinternational.com?subject=Support%20Request%20for%20Ascension%20Accelerating%20App{format2}";
		this.url_LearnMore = $"{format1}http://www.goldenpodinternational.com/ascension-accelerating{format2}";

		this.label.text = "Ascension Accelerating is the third app in a series. It is all about energies and waves of vibration. It harnesses the power of the sacred geometry of the Melchizedek Vortex as one of the carriers of these energies. I am bringing the real presence of energy, sound, vibration, color and coding to you through this app. ";
		this.label.text += this.lineBreak;
		this.label.text += "You can work with this app at any level of Ascension.  Wherever you are on your Ascension Path, you can use this tool to get the best results for yourself. This is where the individual codes will come into play because they are reading your vibrational field and attuning the waves of vibration to your level to give you the best guidance, leading you on your Path.";
		this.label.text += this.lineBreak;
		this.label.text += "These codes and frequencies designed especially for you will come in through your eyes, your 3rd eye and your throat chakra.";
		this.label.text += this.lineBreak;
		this.label.text += "All these frequencies have a distinct purpose and will be calibrated for your personal needs. They will address the maladies of your body and spirit while clearing influences hindering the raising of your consciousness. The intent is to assist in the Ascension of your soul to its highest purpose in conjunction with Divine order.";
		this.label.text += this.lineBreak;
		this.label.text += "Open your heart to allow the energies to come into your energetic field or your Aura. Be sure your breathing is deep and rhythmic. Fill your chest and abdomen, expanding them with every in breath and collapsing them with every out breath, abdomen first and then chest. With every breath you take, the energy is penetrating deeper and deeper into your energetic body.";
		this.label.text += this.lineBreak;
		this.label.text += "This app is an amplifier for anything and everything that is in Divine Flow. You can play the app before or after using other tools to support them. You can use the app individually or in a group. During group work the app is amplifying your part of the work and the contributions you make to the group since the app is based on your custom codes and frequencies.";
		this.label.text += this.lineBreak;
		this.label.text += "The Ascension Accelerator is based on your energetic characteristics of the sound, color and vibration, physical characteristics and your energetic field or Aura.";
		this.label.text += this.lineBreak;
		this.label.text += "Don't expect physical sensations when working with the app. All the work is taking place on a multidimensional or quantum level. Your vibrational field will be changing. You will not be able to recognize the changes with your physical 3D senses.";
		this.label.text += this.lineBreak;
		this.label.text += "The opening sequence is connecting you to Humanity’s Grid at the highest level that humanity has achieved at the moment you are playing the app. I am tuning the app to the highest conscious vibrational level of humanity.";
		this.label.text += this.lineBreak;
		this.label.text += "Sequence 1 thru 5 are personally customized for each person who is working with the app. The sequences are updated each time you play the app to meet you at your current vibrational level.";
		this.label.text += "\n\n";
		this.label.text += "<b>Ascension Accelerated Recommended Usage</b>";
		this.label.text += this.lineBreak;
		this.label.text += "Ground before using the app. Use your intention to be grounded into Gaia.";
		this.label.text += this.lineBreak;
		this.label.text += "Work with the app in a quiet place where you will not be interrupted.";
		this.label.text += this.lineBreak;
		this.label.text += "It is recommended that you play all sequences each time you work with the app.  Play all sequences 1x/day. More than once could cause overload.";
		this.label.text += "\n\n";

		this.label.text += $"To Learn more about the app\n{url_LearnMore}";
		this.label.text += "\n\n";

		this.label.text += $"For app support\n{url_Email1}.";
	}

    private void OnEnable()
    {
        this.tapCounter = 0;
    }

    public void HandleURLButtonPress()
	{
		Application.OpenURL("http://www.goldenpodinternational.com/ascension-accelerating");
	}
	
	public void HandleSecretButtonPress()
	{
		++this.tapCounter;

		var numTapsToTrigger = 20;

		if (Application.isEditor)
			numTapsToTrigger = 5;
			
		Debug.Log($"Taps left to clear data: {this.tapCounter}/{numTapsToTrigger}");

		if(this.tapCounter == numTapsToTrigger)
		{
			this.tapCounter = 0;

			PlayerPrefs.DeleteAll();

			if (Application.isEditor)
				UnityEditor.EditorApplication.isPlaying = false;
			else
				Application.Quit();
		}
	}
}