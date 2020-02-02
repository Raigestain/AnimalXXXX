using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaivor : MonoBehaviour
{
    public Image Main;
    public Image Instructions;
    public Image Credits;
    public Button Start;
    public Button Back;
    public Button Exit;
    public Button Continue;

    public void StartGame()
    {
        Main.gameObject.SetActive(true);
        Start.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);

        Instructions.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);

        Credits.gameObject.SetActive(false);
        Back.gameObject.SetActive(false);

        SceneManager.LoadScene("SampleScene");
    }

    public void CreditsGame()
    {
        Main.gameObject.SetActive(false);
        Start.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);

        Instructions.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);

        Credits.gameObject.SetActive(true);
        Back.gameObject.SetActive(true);
    }

    public void BackGame()
    {
        Main.gameObject.SetActive(true);
        Start.gameObject.SetActive(true);
        Exit.gameObject.SetActive(true);

        Instructions.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);

        Credits.gameObject.SetActive(false);
        Back.gameObject.SetActive(false);
    }

    public void InstructionsGame()
    {
        Main.gameObject.SetActive(false);
        Start.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);

        Instructions.gameObject.SetActive(true);
        Continue.gameObject.SetActive(true);

        Credits.gameObject.SetActive(false);
        Back.gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
