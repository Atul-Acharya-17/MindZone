using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Handles the functionality of the buttons.
/// Switches between scenes and plays the button audio effects
/// </summary>
public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// Sound effect when the player presses the button
    /// </summary>
    private AudioSource audioData;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// Unlocks the cursor and allows the player to click the buttons.
    /// </summary>
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Changes the scene to the GameScene when the player presses the play button.
    /// </summary>
    public void ButtonClickPlay()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Invoke("RenderScene", 1.5f);
    }

    /// <summary>
    /// Plays the audio effect and invokes the Quit method
    /// </summary>
    public void ButtonClickQuit()
    {
        PlayAudio();
        Invoke("Quit", 1.5f);
    }

    /// <summary>
    /// Plays the audio effect and invokes the playRules method
    /// </summary>
    public void ButtonClickRules()
    {
        PlayAudio();
        Invoke("PlayRules", 1.5f); 
    }

    /// <summary>
    /// Plays the audio effect and invokes the goBack method
    /// </summary>
    public void ButtonClickBack()
    {
        PlayAudio();
        Invoke("GoBack", 1.5f); 
    }

    /// <summary>
    /// Loads the Game Scene
    /// </summary>
    void RenderScene()
    {
        SceneManager.LoadScene("GameScene");
    }


    /// <summary>
    /// Quits the application
    /// </summary>
    void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    /// <summary>
    /// Loads the Rules Menu Scene
    /// </summary>
    void PlayRules()
    {
        SceneManager.LoadScene("RulesMenu");
    }

    /// <summary>
    /// Loads the Main Menu Scene
    /// </summary>
    void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    /// <summary>
    /// Plays audioData if it is not null
    /// </summary>
    void PlayAudio()
    {
        audioData = GetComponent<AudioSource>();
        if (audioData != null)
        {
            audioData.Play(0);
        }
    }
}
