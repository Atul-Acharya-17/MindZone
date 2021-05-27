using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CortexStarter : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField profileField;
    public InputField headsetField;

    void Start()
    {
        CortexFacade.SetUpConfiguration();
        CortexFacade.InitializeTrainer();

        CortexFacade.Authorize();
    }

    public void QueryProfile()
    {
        CortexFacade.QueryProfile();
    }

    public void QueryHeadset()
    {
        CortexFacade.QueryHeadset();
    }

    public void Submit()
    {
        string profileName = profileField.text;
        string headsetID = headsetField.text;

        Debug.Log(profileName);
        Debug.Log(headsetID);

        // Check if Headset Exists

        bool profileExists = CortexFacade.FindProfile(profileName);

        if (!profileExists)
        {
            Debug.Log("Profile doesn't exist");
            return;
        }

        bool headsetExist = CortexFacade.FindHeadset(headsetID);

        if (!headsetExist)
        {
            Debug.Log("Headset doesn't exist");
            return;
        }

        

        Debug.Log(profileExists);
        Debug.Log(headsetExist);
         
        if (headsetExist && profileExists)
        {
            CortexFacade.Device = headsetID;
            CortexFacade.User = profileName;

            CortexFacade.LoadProfile(profileName);
            SceneManager.LoadScene("MainMenu");
        }

    }    
}
