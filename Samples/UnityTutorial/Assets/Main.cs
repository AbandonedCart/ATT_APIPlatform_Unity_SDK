using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using ATT_MSSDK;
using ATT_MSSDK.Speechv3;

public class Main : MonoBehaviour {

	static String pleasePrompt = "Please hold down the 's' key to record a color (red, green, or blue)";
	static String releasePrompt = "Release the 's' key to stop recording";
	static String waitPrompt = "Please wait...";

	RequestFactory requestFactory;
	String prompt = pleasePrompt;
	int timeRecordingStarted;

	public static bool Validator(object sender, X509Certificate certificate, X509Chain chain,
	                             SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	// Use this for initialization
	void Start () {
		// API Gateway Endpoint. https://api.att.com
		string endPoint = "https://api.att.com";
		
		// Application key registered at developer portal.
		string apiKey = "37yi0iupaiqoaoxwypfff2td6poatmg8";   
		
		//Secret Key of the application as registered at developer portal.
		string secretKey = "y6rvmheod0jc9yfaymrvxfxhdpr06npo";
		
		// OAuth redirect URL configured at developer portal. This is required only for apps having Authorization credential model.
		string redirectURI = null;
		
		// Scopes the application is granted.
		List<RequestFactory.ScopeTypes> scopes = new List<RequestFactory.ScopeTypes>();
		scopes.Add (RequestFactory.ScopeTypes.Speech);
		scopes.Add (RequestFactory.ScopeTypes.STTC);

		ServicePointManager.ServerCertificateValidationCallback = Validator;
		string authTokenFile = Path.Combine (Directory.GetCurrentDirectory (), "auth_token.dat");
		requestFactory = new RequestFactory(endPoint, apiKey, secretKey, scopes, redirectURI, null, authTokenFile);
	}
	
	IEnumerator EndRecording()
	{
		Microphone.End(null);

		// if the user presses and quickly releases the 's' button, without enough
		// time to record a color, just display a message and ignore it. We'll assume
		// half a second is too short.
		int audioDurationInMilliseconds = (Environment.TickCount - timeRecordingStarted);
		if (audioDurationInMilliseconds < 500) {
			Debug.Log ("'s' key pressed and released too quickly");
			prompt = pleasePrompt;
			yield break;
		}

		prompt = waitPrompt;

		audio.Play();
		float audioDurationInSeconds = ((float)audioDurationInMilliseconds) / 1000f;
		Debug.Log("Playing audio for " + audioDurationInSeconds.ToString () + " seconds");
		yield return new WaitForSeconds (audioDurationInSeconds);
		audio.Stop ();

		Debug.Log("Constructing audio file");

		float[] clipData = new float[audio.clip.samples * audio.clip.channels];
		audio.clip.GetData(clipData, 0);
		
		//Format to 8KHz sampling rate
		WaveGen.WaveFormatChunk format = new WaveGen().MakeFormat(audio.clip);
		
		string filename = "recordedSpeech.wav";
		FileStream stream = File.OpenWrite(filename);

		new WaveGen().Write(clipData, format, stream);
		stream.Close();

		Debug.Log ("Calling speech-to-text webservice");

		int webserviceStartTimeInMilliseconds = Environment.TickCount;
		ATT_MSSDK.Speechv3.SpeechResponse response = SpeechToTextService(filename, "audio/wav", "RGB.srgs");
		Debug.Log ("Speech-to-text webservice call completed in " + (Environment.TickCount - webserviceStartTimeInMilliseconds).ToString() + " milliseconds");

		if (response != null) {
			Debug.Log ("Setting cube color");

			string speechOutput = response.Recognition.NBest [0].ResultText;
			Debug.Log (speechOutput);
			prompt = pleasePrompt;

			string text = speechOutput.ToLower ();

			if (text.Contains ("red")) {
				gameObject.renderer.material.color = Color.red;
			}

			if (text.Contains ("green")) {
				gameObject.renderer.material.color = Color.green;
			}

			if (text.Contains ("blue")) {
				gameObject.renderer.material.color = Color.blue;
			}
		}
	}

	/// <summary>
	/// Method that calls SpeechToText method of RequestFactory to transcribe to text
	/// </summary>
	/// <param name="FileName">Wave file to transcribe</param>
	private ATT_MSSDK.Speechv3.SpeechResponse SpeechToTextService(String AudioFileName, String AudioContentType, String GrammarFileName)
	{
		try
		{
			if (string.IsNullOrEmpty(AudioFileName))
			{
				Debug.Log("No sound file specified");
				return null;
			}

			XSpeechCustomContext speechContext = XSpeechCustomContext.GrammarList;
			string xArgData = "ClientApp=SpeechApp";

			return this.requestFactory.SpeechToTextCustom(AudioFileName, null, GrammarFileName, speechContext, xArgData, AudioContentType);
		}
		catch (InvalidScopeException invalidscope)
		{
			Debug.Log(invalidscope.Message);
		}
		catch (ArgumentException argex)
		{
			Debug.Log(argex.Message);
		}
		catch (InvalidResponseException ie)
		{
			Debug.Log(ie.Body);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			Debug.Log("SpeechToTextService completed.");
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			gameObject.renderer.material.color = Color.red;
		}
		if(Input.GetKeyDown(KeyCode.G)) {
			gameObject.renderer.material.color = Color.green;
		}
		if(Input.GetKeyDown(KeyCode.B)) {
			gameObject.renderer.material.color = Color.blue;
		}
		if (Microphone.IsRecording(null) && Input.GetKeyUp(KeyCode.S)) {
			StartCoroutine(EndRecording());
		} else if (!Microphone.IsRecording(null) && !audio.isPlaying && Input.GetKeyDown (KeyCode.S)) {
			Debug.Log("Recording");
			audio.clip = Microphone.Start(null, false, 5, 8000);
			timeRecordingStarted = Environment.TickCount;
			prompt = releasePrompt;
		}
	}

	void OnGUI () {
		// Display useful instructions
		GUI.Box(new Rect(10,10,500,40), prompt);
	}
}
