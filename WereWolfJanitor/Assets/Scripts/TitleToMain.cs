using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToMain : MonoBehaviour
{
    private int loadedScene = 0;
    private bool buttonPressed = false;
    private Scene scene;
    [SerializeField] GameObject controls;
    private SpriteRenderer rnderer;
    [SerializeField] GameObject soundManager;
    [SerializeField] GameObject titleCanvas;
   
    // Start is called before the first frame update
    void Start()
    {
        rnderer = gameObject.GetComponent<SpriteRenderer>();
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Return) && scene.name == "TitleScreen" && loadedScene == 1 && buttonPressed)
        {
            titleCanvas.SetActive(false);
            this.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("LoadedScene " + loadedScene);
            SceneManager.LoadScene("MazeMain", LoadSceneMode.Additive);
            soundManager.SetActive(false);
            scene = SceneManager.GetActiveScene();
            loadedScene++;
        }
        if (Input.GetKeyDown(KeyCode.Return) && scene.name == "TitleScreen" && loadedScene == 0)
        {
            loadedScene++;
            
            rnderer.sprite = controls;
            StartCoroutine(ButtonPressed());
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }*/
    }

    IEnumerator ButtonPressed()
    {
        yield return new WaitForSeconds(2f);
        buttonPressed = true;
    }
    public void LoadGame()
    {
        soundManager.SetActive(false);
        SceneManager.LoadScene("MazeMain");
    }

    public void ShowControls()
    {
        controls.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void GoBack()
    {
        titleCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
