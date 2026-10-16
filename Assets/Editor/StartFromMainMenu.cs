using UnityEditor;
using UnityEditor.SceneManagement;

// Ao apertar Play no editor, o jogo sempre comeca pelo menu principal,
// mesmo que outra cena esteja aberta (igual ao jogo buildado).
[InitializeOnLoad]
public static class StartFromMainMenu
{
    static StartFromMainMenu()
    {
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/MainMenu.unity");
    }
}
