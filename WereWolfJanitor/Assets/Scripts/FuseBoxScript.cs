using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuseBoxScript : MonoBehaviour

{
    private PlayerMovement playerMovement;
    public Sprite PressedButton;
    public Sprite WhiteButtonNotPressed;
    private bool HasPressed;
    private GameManager gm;
    public bool isWhiteButton = false;
    public GameObject whiteButton;
    public string correspondingBlock;
    private GameObject[] coloredWalls;
    [SerializeField]
    private Animator whiteout;
    [SerializeField]
    private GameObject whitescreen;

    [SerializeField] GameObject whiteOut;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        HasPressed = false;
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasPressed==false && collision.gameObject.CompareTag("Player"))
        {
            SoundManagerScript soundManager = FindObjectOfType<SoundManagerScript>();
            if (isWhiteButton == true)
            {
                if (gm.canPressWhiteButton == true)
                {
                    Debug.Log("White button if statement reached");
                    HasPressed = true;
                    //playerMovement.UsedFuse();
                    gm.DecreaseButtonCount();
                    soundManager.PlaySound("SwitchSFX");
                    gm.RandomizeMonsters();
                    Debug.Log("White button sprite about to changes");
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = PressedButton;
                    Debug.Log("White button sprite changed");
                    StartCoroutine(WhiteOut());
                    
                    // white button functionality goes here
                }
            }
            else
            {
                HasPressed = true;
                playerMovement.UsedFuse();
                gm.DecreaseButtonCount();
                soundManager.PlaySound("SwitchSFX");
                gm.RandomizeMonsters();
                this.gameObject.GetComponent<SpriteRenderer>().sprite = PressedButton;
                coloredWalls = GameObject.FindGameObjectsWithTag(correspondingBlock);
                foreach (GameObject wall in coloredWalls)
                {
                    wall.GetComponent<ColoredWalls>().buttonPressed();
                }
                soundManager.PlaySound("WallCrumblingSFX");
                gm.numberButtonsPushed++;
                if (gm.numberButtonsPushed >= 3)
                {
                    whiteButton.GetComponent<SpriteRenderer>().sprite = WhiteButtonNotPressed;
                    gm.canPressWhiteButton = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Y))
        {
             SceneManager.LoadSceneAsync("Credits",LoadSceneMode.Single);
        }*/
    }

    IEnumerator WhiteOut()
    {
        whiteOut.SetActive(true);
        gm.RandomizeMonsters();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }
}
