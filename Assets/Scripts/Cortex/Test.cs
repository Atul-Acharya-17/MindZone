using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{

    public GameObject box;
    
    // Update is called once per frame
    void Update()
    {
        string Action = CortexFacade.GetAction();

        if (Action != "neutral")
        {
            float x = box.transform.position.x + 0.01f;
            float y = 0;
            float z = 0;
            box.transform.position = new Vector3(x, y, z);
        }
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
