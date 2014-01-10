using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using System;

// This MonoBehaviour is responsible for controlling the CharacterGenerator,
// animating the character, and the user interface. When the user requests a 
// different character configuration the CharacterGenerator is asked to prepare
// the required assets. When all assets are downloaded and loaded a new
// character is created.
class Main : MonoBehaviour
{
    CharacterGenerator generator;
    GameObject character;
    bool usingLatestConfig;
    bool newCharacterRequested = true;
    bool firstCharacter = true;
    string nonLoopingAnimationToPlay;

    const float fadeLength = .6f;
    const int typeWidth = 124;
    const int buttonWidth = 84;
    const string prefName = "Character Generator Demo Pref";

    bool isRecording = false;

    string speechResult;
    bool displayHelp = false;
    bool displayChangeCharacterPrompt = false;
    Dictionary<string, string> propertiesToChange;
    Dictionary<string, string> propertyTitles = new Dictionary<string, string>();

    const int windowWidth = 420;
    const int windowHeight = 350;
	
	const int fontSize = 20;
	const int buttonHeight = 45;

    // Initializes the CharacterGenerator and load a saved config if any.
    IEnumerator Start()
    {
        while (!CharacterGenerator.ReadyToUse) yield return 0;
        if (PlayerPrefs.HasKey(prefName))
            generator = CharacterGenerator.CreateWithConfig(PlayerPrefs.GetString(prefName));
        else
            generator = CharacterGenerator.CreateWithRandomConfig("Female");

        Setup("http://wmssp.research.att.com/gdc/asr");

        //Set up propertyTitles
        propertyTitles.Add("face", "Head");
        propertyTitles.Add("eyes", "Eyes");
        propertyTitles.Add("hair", "Hair");
        propertyTitles.Add("top", "Shirt");
        propertyTitles.Add("pants", "Pants");
        propertyTitles.Add("shoes", "Shoes");
        
    }

    public static bool Validator(object sender, X509Certificate certificate, X509Chain chain,
                                      SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    public void Setup(string endpoint)
    {
        ServicePointManager.ServerCertificateValidationCallback = Validator;
    }

    public void RequestSpeech(AudioClip audio, GameObject receiver, string callback)
    {
        float[] clipData = new float[audio.samples * audio.channels];
        audio.GetData(clipData, 0);
        WaveGen.WaveFormatChunk format = new WaveGen().MakeFormat(audio);
        
        new Thread((ThreadStart)delegate
        {
            try
            {
                string filename = GetTempFileName() + ".wav";
                FileStream stream = File.OpenWrite(filename);
                new WaveGen().Write(clipData, format, stream);
                stream.Close();

                Debug.Log("Request Start time: " + DateTime.Now.ToLongTimeString());

                speechOutput = new SpeechService().ConvertToSpeech(filename);

                Debug.Log("Response = " + speechOutput);

                Debug.Log("Response received time: " + DateTime.Now.ToLongTimeString());
                showProcess = false;
                Debug.Log("response: " + speechResult);
                File.Delete(filename);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }).Start();
    }

    private string GetTempFileName()
    {
        return Path.GetTempFileName();
    }

    // Requests a new character when the required assets are loaded, starts
    // a non looping animation when changing certain pieces of clothing.
    void Update()
    {
        if (generator == null) return;
        if (usingLatestConfig) return;
        if (!generator.ConfigReady) return;

        usingLatestConfig = true;

        if (newCharacterRequested)
        {
            Destroy(character);
            character = generator.Generate();
            character.animation.Play("idle1");
            character.animation["idle1"].wrapMode = WrapMode.Loop;
            newCharacterRequested = false;

            // Start the walkin animation for the first character.
            if (!firstCharacter) return;
            firstCharacter = false;
            if (character.animation["walkin"] == null) return;
            
            // Set the layer to 1 so this animation takes precedence
            // while it's blended in.
            character.animation["walkin"].layer = 1;
            
            // Use crossfade, because it will also fade the animation
            // nicely out again, using the same fade length.
            character.animation.CrossFade("walkin", fadeLength);
            
            // We want the walkin animation to have full weight instantly,
            // so we overwrite the weight manually:
            character.animation["walkin"].weight = 1;
            
            // As the walkin animation starts outside the camera frustrum,
            // and moves the mesh outside its original bounding box,
            // updateWhenOffscreen has to be set to true for the
            // SkinnedMeshRenderer to update. This should be fixed
            // in a future version of Unity.
            character.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
        }
        else
        {
            character = generator.Generate(character);
            
            if (nonLoopingAnimationToPlay == null) return;
            
            character.animation[nonLoopingAnimationToPlay].layer = 1;
            character.animation.CrossFade(nonLoopingAnimationToPlay, fadeLength);
            nonLoopingAnimationToPlay = null;
        }
    }
	
	
	Texture2D getBackgroundTexture(Color bkgColor) {
		Texture2D bkgTexture = new Texture2D(24, 24);
		
		for (int y = 0; y < bkgTexture.height; ++y) {
            for(int x = 0; x < bkgTexture.width; ++x) {
				Color color = (x == 0 || x == (bkgTexture.width -1) || y == 0 || y == (bkgTexture.height-1)) ? Color.black : bkgColor;
				bkgTexture.SetPixel(x, y, color);
            }
        }
        // Apply all SetPixel calls
        bkgTexture.Apply();
		return bkgTexture;
	}
	
	Boolean skinSetup = false;
	void generateSkinStyling() {
		//Generate the background color texture
		Texture2D normalBkg = getBackgroundTexture(Color.gray);
		Texture2D hoverBkg = getBackgroundTexture(new Color(.3f,.3f,.3f,1f));
		Texture2D activeBkg = getBackgroundTexture(new Color(.2f,.2f,.2f,1f));
		Texture2D windowBkg = getBackgroundTexture(new Color(.4f,.4f,.4f,1f));
		
		//Box skin
		GUI.skin.box.fontSize = fontSize;
		GUI.skin.box.fixedHeight = buttonHeight;
		GUI.skin.box.normal.background = normalBkg;
		GUI.skin.box.hover.background = normalBkg;
		GUI.skin.box.focused.background = normalBkg;
		GUI.skin.box.active.background = normalBkg;
		GUI.skin.box.normal.textColor = Color.black;
		GUI.skin.box.hover.textColor = Color.black;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		
		//Button Skin
		GUI.skin.button.fontSize = fontSize;
		GUI.skin.button.fixedHeight = buttonHeight;
		GUI.skin.button.normal.background = normalBkg;
		GUI.skin.button.hover.background = hoverBkg;
		GUI.skin.button.focused.background = normalBkg;
		GUI.skin.button.active.background = activeBkg;
		
		//Label skin
		GUI.skin.label.fontSize = fontSize;
		
		//Window skin
		GUI.skin.window.fontSize = fontSize;
		GUI.skin.window.normal.background = windowBkg;
		GUI.skin.window.hover.background = windowBkg;
		GUI.skin.window.focused.background = windowBkg;
		GUI.skin.window.active.background = windowBkg;
		GUI.skin.window.onNormal.background = windowBkg;
		GUI.skin.window.onFocused.background = windowBkg;
		GUI.skin.window.onHover.background = windowBkg;
		GUI.skin.window.onActive.background = windowBkg;
		GUI.skin.window.hover.textColor = GUI.skin.box.normal.textColor;
		GUI.skin.window.active.textColor = GUI.skin.box.normal.textColor;
		GUI.skin.window.focused.textColor = GUI.skin.box.normal.textColor;
		GUI.skin.window.onNormal.textColor = GUI.skin.box.normal.textColor;
		GUI.skin.window.onHover.textColor = GUI.skin.box.normal.textColor;
		GUI.skin.window.onActive.textColor = GUI.skin.box.normal.textColor;
		GUI.skin.window.onFocused.textColor = GUI.skin.box.normal.textColor;
		
		Debug.Log ("Set up the skins!");
	}

    void OnGUI()
    {
        
        if (generator == null) return;
        GUI.enabled = usingLatestConfig && !character.animation.IsPlaying("walkin") &&
			speechResult == null && !displayHelp && !displayChangeCharacterPrompt && //A popup is displayed
				propertiesToChange == null && !isRecording && !showProcess;
		
		if(!skinSetup) 
		{
			generateSkinStyling();
			skinSetup = true;
		}
		
        GUILayout.BeginArea(new Rect(10, 10, typeWidth + buttonWidth + 4, 600));

        // Buttons for changing the active character.
        GUILayout.BeginHorizontal();
		
        GUILayout.Box("Character", GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            ChangeCharacter(true);

        GUILayout.EndHorizontal();

        AddCategory("face",  null);
        AddCategory("eyes", null);
        AddCategory("hair", null);
        AddCategory("top", "item_shirt");
        AddCategory("pants", "item_pants");
        AddCategory("shoes", "item_boots");

        // Buttons for saving and deleting configurations.
        // In a real world application you probably want store these
        // preferences on a server, but for this demo configurations 
        // are saved locally using PlayerPrefs.
        if (GUILayout.Button("Save Configuration"))
            PlayerPrefs.SetString(prefName, generator.GetConfig());

        if (GUILayout.Button("Delete Configuration"))
            PlayerPrefs.DeleteKey(prefName);

        if (!isRecording && !showProcess)
        {
            if (GUILayout.Button("Speech"))
            {
                ClearPopups();
				StartCoroutine(DoRecording());
            }
        }
        else
        {
            GUI.enabled = true;
			
			if (isRecording)
            {

                TimeSpan ts = endTime.Subtract(DateTime.Now);
                GUILayout.Box("Recording... " + ts.Seconds);
            }
            else
            {
                GUILayout.Box("Processing...");
            }
        }

        if (!string.IsNullOrEmpty(speechOutput))
        {
            OnSpeechReady(speechOutput);
            speechOutput = string.Empty;
        }
		
		GUI.enabled = true;
		
        //Show windows
        Rect windowRec = new Rect((Screen.width - windowWidth) / 2, 10, windowWidth, windowHeight);
        if (speechResult != null)
        {
            GUI.Window(0, windowRec, DidNotUnderstandWindow, "Did Not Understand");
        }
        else if (displayHelp)
        {
            GUI.Window(1, windowRec, HelpWindow, "Speech Help");
        }
        else if (displayChangeCharacterPrompt)
        {
            //GUI.Window(2, windowRec, CharacterChangeConfirmationWindow, "Change Character?");
            ChangeCharacter(true);
            ClearPopups();
        }
        else if (propertiesToChange != null)
        {
            generator.ChangeElements(propertiesToChange);
            usingLatestConfig = false;
            ClearPopups();
            //GUI.Window(3, windowRec, TraitConfirmationWindow, "Trait Changes");
        }

        // Show download progress or indicate assets are being loaded.
        if (!usingLatestConfig)
        {
            float progress = generator.CurrentConfigProgress;
            string status = "Loading";
            if (progress != 1) status = "Downloading " + (int)(progress * 100) + "%";
            GUILayout.Box(status);
        }

        GUILayout.EndArea();
    }    

    // Draws buttons for configuring a specific category of items, like pants or shoes.
    void AddCategory(string category, string displayName, string anim)
    {
        GUILayout.BeginHorizontal();
		
        GUILayout.Box(displayName, GUILayout.Width(typeWidth));

        if (GUILayout.Button(">", GUILayout.Width(buttonWidth)))
            ChangeElement(category, true, anim);

        GUILayout.EndHorizontal();
    }

    void AddCategory(string category, string anim)
    {
        string displayName = propertyTitles.ContainsKey(category) ? propertyTitles[category] : category;
        AddCategory(category, displayName, anim);
    }

    void ChangeCharacter(bool next)
    {
        generator.ChangeCharacter(next);
        usingLatestConfig = false;
        newCharacterRequested = true;
    }

    void ChangeElement(string catagory, bool next, string anim)
    {
        generator.ChangeElement(catagory, next);
        usingLatestConfig = false;
        
        if (!character.animation.IsPlaying(anim))
            nonLoopingAnimationToPlay = anim;
    }

    bool showProcess = false;
    DateTime endTime;

    IEnumerator DoRecording()
    {
        Debug.Log("Recording");
        isRecording = true;
        endTime = DateTime.Now.AddSeconds(6);
        audio.clip = Microphone.Start(null, false, 5, 8000);
        yield return new WaitForSeconds(5);
        //audio.Play();
        isRecording = false;
        Microphone.End(null);
        if (null != audio.clip)
        {
            showProcess = true;
            Debug.Log("Captured. calling speech");
            RequestSpeech(audio.clip, gameObject, "OnSpeechReady");
        }
    }

    public void OnSpeechReady(string speechText)
    {
        Debug.Log("result final: " + speechText);
        string text = speechText.ToLower();

        if (text.Contains("help"))
        {
            displayHelp = true;
            return;
        }

        Dictionary<string, string> propsInText = generator.ParseForProperties(text);
        if (text.Contains("try") || text.Contains("change") || text.Contains("james"))
        {
            bool mentionedChangeCharacter = propsInText.ContainsKey(CharacterGenerator.CHARACTER_PROP);
            if (mentionedChangeCharacter && propsInText.Keys.Count == 1)
            {
                displayChangeCharacterPrompt = true;
            }
            else
            {
                if (mentionedChangeCharacter) propsInText.Remove(CharacterGenerator.CHARACTER_PROP);
                if (propsInText.Keys.Count > 0)
                {
                    propertiesToChange = propsInText;
                }
                else
                {
                    speechResult = speechText;
                }
            }
        }
        else
        {
            string anim = null;

            if (propsInText.ContainsKey("shoes"))
            {
                anim = "item_boots";
            }
            else if (propsInText.ContainsKey("top"))
            {
                anim = "item_shirt";
            }
            else if (propsInText.ContainsKey("pants"))
            {
                anim = "item_pants";
            }

            if (anim == null)
            {
                //Nothing matched, show the results as unknown
                speechResult = speechText;
            }
            else if (!character.animation.IsPlaying(anim))
            {
                usingLatestConfig = false;
                nonLoopingAnimationToPlay = anim;
            }
        }
    }

    public void ClearPopups()
    {
        speechResult = null;
        displayHelp = false;
        displayChangeCharacterPrompt = false;
        propertiesToChange = null;
    }

    void DidNotUnderstandWindow(int windowID)
    {
        GUILayout.BeginArea(new Rect(10, 30, windowWidth - 20, windowHeight - 40));

        GUILayout.Label("I did not understand that command. Did you say \"" + speechResult + "\"?");
        GUILayout.Label("If not, please try again");
        GUILayout.Label("For a list of possible commands, simply say \"Help\" to pop up the help menu");

        GUILayout.FlexibleSpace();

        //Close button
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Try Again", GUILayout.Width(typeWidth)))
        {
            ClearPopups();
            StartCoroutine(DoRecording());
        }
        GUILayout.Space(20);
        if (GUILayout.Button("Close", GUILayout.Width(typeWidth)))
            ClearPopups();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void HelpWindow(int windowID)
    {
        GUILayout.BeginArea(new Rect(10, 30, windowWidth - 20, windowHeight - 40));

        GUILayout.Label("Press \"Speech\" button to record one of the following commands:");
        string commandList = "";
        commandList += "1. \"Help\"\n";
        commandList += "2. \"Change Character\"\n";
        commandList += "3. \"Change to [<color>] <property>...\"\n";
		commandList += "    e.g. \"Change to blue shoes and pink hair\"\n";
        commandList += "4. \"Look at your shoes\"\n";
        commandList += "5. \"That's a nice shirt\"\n";
        commandList += "6. \"What do you think of your pants?\"";
        GUILayout.Label(commandList);

        GUILayout.FlexibleSpace();

        //Close button
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Try It Out!", GUILayout.Width(typeWidth)))
        {
            ClearPopups();
            StartCoroutine(DoRecording());
        }
        GUILayout.Space(20);
        if (GUILayout.Button("Close", GUILayout.Width(typeWidth)))
            ClearPopups();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void CharacterChangeConfirmationWindow(int windowID)
    {
        GUILayout.BeginArea(new Rect(10, 30, windowWidth - 20, windowHeight - 40));

        GUILayout.Label("It sounded like you would like to change your character");
        GUILayout.Label("Note that this will lose all previous changes to your character unless you save now");
        GUILayout.Label("Would you like continue?");

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Change", GUILayout.Width(typeWidth)))
        {
            //Apply the changes
            ChangeCharacter(true);
            ClearPopups();
        }
        GUILayout.Space(20);
        if (GUILayout.Button("Cancel", GUILayout.Width(typeWidth)))
            ClearPopups();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void TraitConfirmationWindow(int windowID)
    {
        GUILayout.BeginArea(new Rect(10, 30, windowWidth - 20, windowHeight - 40));

        string changesList = "It sounded like you would like to change the following:\n";
        foreach (KeyValuePair<string, string> category in propertiesToChange)
        {
            changesList += "\n    - " + propertyTitles[category.Key];
            if (!category.Key.Equals("next")) changesList += " (" + category.Value + ")";
        }
        GUILayout.Label(changesList);
        GUILayout.Label("Would you like to apply these changes?");

        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Apply", GUILayout.Width(typeWidth)))
        {
            //Apply the changes
            generator.ChangeElements(propertiesToChange);
            ClearPopups();
            usingLatestConfig = false;
        }
        GUILayout.Space(20);
        if (GUILayout.Button("Cancel", GUILayout.Width(typeWidth)))
            ClearPopups();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    string speechOutput;
}