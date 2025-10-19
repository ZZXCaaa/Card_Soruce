using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Soruce.View.UI
{
    public class OpenButtonView : MonoBehaviour
    {
            public void clickConnection()
            {
                SceneManager.LoadScene("hall");
            }
            public void clickExit()
            {
                Application.Quit();
            }
    }
}