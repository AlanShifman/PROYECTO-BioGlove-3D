using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("proyectoMain");
    }

    public void AbrirTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void AbrirInformacion()
    {
        SceneManager.LoadScene("Información");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenúInicial");
    }
}