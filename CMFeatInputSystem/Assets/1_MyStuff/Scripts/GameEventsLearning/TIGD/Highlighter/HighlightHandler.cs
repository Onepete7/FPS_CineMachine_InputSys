using Highlighters;
using Highlighters_BuiltIn;
using UnityEngine;

public class HighlightHandler : MonoBehaviour
{
    public Highlighter monsterHighlighter;
    private HighlighterSettings monsterHighlighterSettings;
    // private bool updateIsCalled;
    private bool isKeyPressed;


    private void Awake()
    {
        monsterHighlighter = GetComponent<Highlighter>();
        monsterHighlighterSettings = monsterHighlighter.Settings;
        monsterHighlighterSettings.UseOuterGlow = true;
        // updateIsCalled = true;
    }

    private void Update()
    {
        // if (updateIsCalled)
        //     Debug.Log("UpdateIsCalled");

        // if (Input.anyKey)
        //     Debug.Log("Hey");

    }
}
