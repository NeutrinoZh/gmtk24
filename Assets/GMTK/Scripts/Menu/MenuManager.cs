using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play() => SceneManager.LoadScene(1);
    public void Exit() => Application.Quit(0);
}
