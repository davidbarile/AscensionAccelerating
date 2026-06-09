using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SupportPage : MonoBehaviour 
{
	[SerializeField] TMP_Text label;

	private readonly string format1 = "<color=#FFF19B><u><link=\"";
	private readonly string format2 = "\">tap here</link></u></color>";

	private string url_Email1 = "";
	private string url_UsersGuide = "";
	private string url_Chandra = "";
	private string url_Email2 = "";


	//"<color=#0000FF><u><link=\"https://unity.com\">here</link></u></color>";

	private void Start()
	{

		this.url_Email1 = $"{format1}mailto:support@goldenpodinternational.com?subject=Ascension%20Rising%20Usage{format2}";
		this.url_UsersGuide = $"{format1}http://www.goldenpodinternational.com/ascension-rising{format2}";
		this.url_Chandra = $"{format1}https://www.grandmachandra.com{format2}";
		this.url_Email2 = $"{format1}mailto:support@goldenpodinternational.com?subject=Ascension%20Rising%20Support{format2}";

		this.label.text = "This is the second app, in a series designed to assist you in harnessing the magnetic energies of the sacred geometries within the Octuple Dorje. Grandma Chandra’s 33rd Dimensional Masterhood will boost your ascension interconnections with these magnetic energies to accelerate your reach and interaction with the multiple dimensions within the Quantum Light Field.";
		this.label.text += "\n\n";
		this.label.text += "In this app you will experience three different videos of the Octuple Dorje flashing, spinning, or multiple Dorjes merging in kaleidoscopic rotations. The fourth video is comprised of three Light Language Symbols, which along with the Divine sound of the Hu chant, truly boost your vibratory frequency.  Meditating on these movements and listening to the audios facilitate Grandma Chandra’s delivering to you, layer-by-layer, these specialized codes and frequencies into your physical and etheric bodies so that you can progress into higher energetic frequencies much faster.  By working through the menu of this app, you facilitate your development, expansion and activation of your light flowing crystalline networks.  For most effective listening, please use ear phones or a headset.";
		this.label.text += "\n\n";
		this.label.text += "These crystalline networks link you into the Quantum Light Field where your Greater Multidimensional Aspects and huge stores of knowledge are accessible.  The Octuple Dorje accelerates your transition from carbon to crystalline DNA and the network building, while Grandma Chandra continually adjusts the frequencies and codes to optimize your level-by-level progress.   As you progress, your network activation accelerates your remembrance of the energetics, frequencies, codes and the knowledge of the Sacred Geometries of the Light Language.  You will experience ever more strongly your interconnection to the Source within this Unified Quantum Light Field.";
		this.label.text += "\n\n";
		this.label.text += "Grandma Chandra highly recommends using this app daily, ideally first thing in the morning. Take eight minutes to listen to all four modules. This will help you receive richer detail from your intuition and wisdom.";
		this.label.text += "\n\n";

		this.label.text += $"To receive a personal app usage recommendation from Grandma Chandra, {url_Email1} to send an e-mail.";
		this.label.text += "\n\n";

		this.label.text += $"To learn more about the app, including link to a User’s Guide, {url_UsersGuide}.";
		this.label.text += "\n\n";

		// this.label.text += "To learn more about the Octuple Dorje or to purchase one, [url=https://www.grandmachandra.com/collections/geometric-forms/products/octuple-dorje][u][7C04B0FF]tap here[-][/u][/url].";
		// this.label.text += "\n\n";

		this.label.text += $"For more ascension help {url_Chandra}.";
		this.label.text += "\n\n";


		this.label.text += $"For app technical help, {url_Email2}.";
	}

	public void HandleURLButtonPress()
	{
		Application.OpenURL( "https://www.grandmachandra.com/ascensionassist" );
	}
}
