// CharacterSelect.cs (put on any GameObject in the scene)
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {
    public void Pick(string archetype) {
        PlayerPrefs.SetString("SelectedCharacter", archetype); // "Fire" | "Ice" | "Necromancy"
        SceneManager.LoadScene("Main_Scene"); // your gameplay scene name
    }
}
