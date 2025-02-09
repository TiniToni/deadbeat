using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string sceneToLoad = "game";

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad); 
    }
}
