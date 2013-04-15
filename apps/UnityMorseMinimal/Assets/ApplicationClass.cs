// Copyright (c) December 2012
// All Rights Reserved
// Advanced Training & Learning Technology, LLC
// 4445 Corporation Lane, Suite 207
// Virginia Beach, VA 23462
// admin@atlt-llc.com

// Parts of this software package was produced for the United States Government 
// under PM TRASYS Contract # M67854-12-C-8088.  

// Advanced Training & Learning Technology, LLC is providing this code under the
// GNU Lesser General Public License, version 2.1 or greater, while reserving 
// the right to distribute under another license. A copy of the LGPL license 
// should have been provided with this software distribution.  If not, a copy 
// of this license can be found at 
// http://www.gnu.org/licenses/lgpl-2.1.html.



using UnityEngine;
using System.Collections.Generic;
using Manager;
using ITAEngine;


public class ApplicationClass : MonoBehaviour {
	
	private string answerString;
	private string labelString;
	private string problemString; 
	private ServiceManager servMaster;
	private bool doingAssessment; 
	private int currentProblem; 
	private bool showNext; 
	private GUIStyle style, grayStyle;
	private Texture2D whiteTxt, grayTxt; 
	private List<AssetRecomendation> arRecom; 
	private List<string> skillList; 
	private int currentAsset = 0; 
	
	// Use this for initialization
	void Start () {
		servMaster = ServiceManager.GetInstance();
		answerString = "";
		labelString = "";
		servMaster.GetAssessment();
		currentProblem = 0;
		doingAssessment = true;
		showNext = false; 
		UpdateLabel();
		whiteTxt = (Texture2D)Resources.Load("white16x16",typeof(Texture2D));
		grayTxt = (Texture2D)Resources.Load("gray16x16",typeof(Texture2D));
		style = new GUIStyle();
		style.fontSize = 16;
		style.normal.background = whiteTxt; 
		style.focused.background = whiteTxt; 
		style.active.background = whiteTxt;
		style.hover.background = whiteTxt;
				
		grayStyle = new GUIStyle();
		grayStyle.fontSize = 16;
		grayStyle.normal.background = grayTxt; 
		grayStyle.focused.background = grayTxt;
		grayStyle.active.background = grayTxt;
		grayStyle.hover.background = grayTxt;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void OnGUI() {
		GUI.Label(new Rect(10,10,400, 80), labelString, style);
		GUI.Label(new Rect(10, 95, 60, 25), "Answer: ", grayStyle);
		answerString = GUI.TextField(new Rect(70,95,400,25), answerString, style);
		ButtonDraw();
	}
	
	private void ButtonDraw()
	{
		if (doingAssessment) {
			AssessmentButtonDraw();
		} else {
			TutorButtonDraw();	
		}
	}
	
	private void AssessmentButtonDraw()
	{
		if (!showNext) {
			if (GUI.Button(new Rect(50, 130, 60, 25), "Answer")) {
				AnswerClick();
			}
		} else { 
			if (GUI.Button(new Rect(50, 130, 60, 25), "Next")) {
				NextProblemClick();
			}
		}
	}

	private void TutorButtonDraw()
	{
		if (GUI.Button(new Rect(50, 130, 60, 25), "Next")) {
			Debug.Log("TutorButtonDraw: " + currentAsset);
			if (currentAsset == arRecom.Count) {
				ResetForNewAssessment();
			} else {
				servMaster.are.MarkAssetCompleted(arRecom[currentAsset].GetAssetIdentifier());
				NextAssetClick();
				currentAsset++;
			}
		}		
	}
	
	private void NextAssetClick()
	{
		answerString = "";

		Debug.Log("Next Asset Click: " + currentAsset);
		servMaster.are.MarkAssetStarted(arRecom[currentAsset].GetAssetIdentifier());
		labelString = "***** Tutoring: *****\n"; 
		string assetString = servMaster.dm.assetData.ad[arRecom[currentAsset].GetAssetIdentifier()].desc;
		foreach(string line in assetString.Split(new string [] {"\\n"}, System.StringSplitOptions.None)) {
			labelString += (line.Trim() + "\n");
		}
	}
	
	private void AnswerClick() 
	{
		string ans = servMaster.trans.MorseToAlpha(answerString);
		showNext = true; 
		if (ans == problemString) {
			labelString = problemString + "\n***** Correct *****"; 
			servMaster.MarkProblemCompleted(servMaster.probList[currentProblem], true);
			currentProblem++;
			Debug.Log ("hasFinished: " + servMaster.actResponse.HasFinished.ToString() + " Current Problem: " + currentProblem);
			if (servMaster.actResponse.HasFinished || (currentProblem == 3)) {
				servMaster.CloseAssessment();
				ResetForNewAssessment();
			}
		} else {
			labelString = problemString + "\n***** Wrong *****"; 
			servMaster.MarkProblemCompleted(servMaster.probList[currentProblem], false);
			if (servMaster.actResponse.HasFailed)
				StartTutor();
			else
				currentProblem++; 
		}
	}
	
	private void ResetForNewAssessment()
	{
		Debug.Log("ResetForNewAssessment");
		arRecom = null; 
		skillList = null;
		doingAssessment = true; 
		servMaster.GetAssessment();
		currentProblem = 0; 
		UpdateLabel();
	}
	
	private void StartTutor() 
	{
		skillList = new List<string>(servMaster.assessment.GetSkills());
		servMaster.CloseAssessment();
		doingAssessment = false;
		labelString = "Should be tutoring";
		arRecom = new List<AssetRecomendation>(servMaster.GetRecommendationForProblemGroup(skillList[0]));
		currentAsset = 0;
		NextAssetClick();
		TutorButtonDraw();
	}
	
	private void NextProblemClick() 
	{
		showNext = false; 
		answerString = "";
		UpdateLabel();
	}
	
	private void UpdateLabel()
	{
		if (doingAssessment && !showNext) {
			problemString = servMaster.dm.problemData.pd[servMaster.probList[currentProblem]].statement;
			labelString = problemString + "\nTranslate into Morse Code.";
		}
	}
//		if ((Event.current.character == char.Parse("\n"))) {}

}
