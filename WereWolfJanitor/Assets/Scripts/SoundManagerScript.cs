using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerScript : MonoBehaviour
{
    public AudioClip FlourescentLight, MonsterMovement, PlayerDeath, SootMain, SootTitleScreen, WolfSound, Mopping, FaucetOn, PickUp, OpenDoor, CloseDoor;
    [SerializeField] 
    static AudioSource audioSrs;
    // Start is called before the first frame update
    void Start()
    {
        audioSrs = GetComponent<AudioSource>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MazeMain")
        {
            audioSrs.clip = SootMain;
            audioSrs.loop = true;
            audioSrs.Play();
        }
        else if (scene.name == "TitleScreen")
        {
            audioSrs.clip = SootTitleScreen;
            audioSrs.loop = true;
            audioSrs.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSrs.isPlaying)
        {
            audioSrs.clip = SootMain;
            audioSrs.loop = true;
            audioSrs.Play();
        }
    }

    public void PlaySound (string clip)
    {
        switch (clip) {
            case "FlourescentLightSFX":
                audioSrs.loop = false;
                audioSrs.clip = FlourescentLight;
                audioSrs.PlayOneShot(FlourescentLight);
                break;
            case "PlayerDeathSFX":
                //audioSrs.loop = false;
                audioSrs.clip = PlayerDeath;
                audioSrs.PlayOneShot(PlayerDeath);
                break;
            case "WolfSound":
                audioSrs.loop = false;
                audioSrs.clip = WolfSound;
                audioSrs.PlayOneShot(WolfSound);
                break;
            case "Mopping":
                //audioSrs.loop = false;
                audioSrs.clip = Mopping;
                audioSrs.PlayOneShot(Mopping);
                break;
            case "FaucetOn":
                //audioSrs.loop = false;
                audioSrs.clip = FaucetOn;
                audioSrs.PlayOneShot(FaucetOn);
                break;
            case "PickUp":
                //audioSrs.loop = false;
                audioSrs.clip = PickUp;
                audioSrs.PlayOneShot(PickUp);
                break;
            case "OpenDoor":
                //audioSrs.loop = false;
                audioSrs.clip = OpenDoor;
                audioSrs.PlayOneShot(OpenDoor);
                break;
            case "CloseDoor":
                //audioSrs.loop = false;
                audioSrs.clip = CloseDoor;
                audioSrs.PlayOneShot(CloseDoor);
                break;
        }

    }
}
