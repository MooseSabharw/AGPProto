using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToCredits : MonoBehaviour
{

    public void DoThing()
    {
        SceneManager.LoadSceneAsync("Credits",LoadSceneMode.Single);
    }
  
}
