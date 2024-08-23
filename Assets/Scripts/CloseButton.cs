using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public void CloseApp()
    {
#if (UNITY_EDITOR)
UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
Application.Quit();
#endif
    }
}
