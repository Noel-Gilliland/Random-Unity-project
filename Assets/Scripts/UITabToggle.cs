using UnityEngine;

public class UITabToggle : MonoBehaviour
{
    public GameObject tabPanel;

    public void ToggleTab_SpellBook()
    {
        tabPanel.SetActive(!tabPanel.activeSelf);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleTab_SpellBook();
        }
    }
}
