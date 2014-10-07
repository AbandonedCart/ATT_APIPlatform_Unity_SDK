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

	RequestFactory requestFactory;
	
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
		scopes.Add (RequestFactory.ScopeTypes.STTC);
		
		ServicePointManager.ServerCertificateValidationCallback = Validator;
		requestFactory = new RequestFactory(endPoint, apiKey, secretKey, scopes, redirectURI, null);
	}
	
	IEnumerator DoRecording()
	{
		Debug.Log("Recording");
		audio.clip = Microphone.Start(null, false, 5, 8000);
		yield return new WaitForSeconds(5);
		Debug.Log("Playing");
		audio.Play();
		Microphone.End(null);

		float[] clipData = new float[audio.clip.samples * audio.clip.channels];
		audio.clip.GetData(clipData, 0);
		
		//Format to 8KHz sampling rate
		WaveGen.WaveFormatChunk format = new WaveGen().MakeFormat(audio.clip);
		
		string filename = "recordedSpeech.wav";
		FileStream stream = File.OpenWrite(filename);
		
		new WaveGen().Write(clipData, format, stream);
		stream.Close();

		ATT_MSSDK.Speechv3.SpeechResponse response = SpeechToTextService(filename, "audio/wav", "RGB.srgs");
        string speechOutput = response.Recognition.NBest[0].ResultText;
        Debug.Log(speechOutput);

		string text = speechOutput.ToLower();
		
		if (text.Contains("red"))
		{
			gameObject.renderer.material.color = Color.red;
		}
		
		if (text.Contains("green"))
		{
			gameObject.renderer.material.color = Color.green;
		}
		
		if (text.Contains("blue"))
		{
			gameObject.renderer.material.color = Color.blue;
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
		if(Input.GetKeyDown(KeyCode.S)) {
			StartCoroutine(DoRecording());
		}
	}
}
