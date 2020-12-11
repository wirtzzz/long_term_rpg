using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class ink_manager : MonoBehaviour
{
    public Button button_prefab;
    public TextAsset ink_json_asset;
    private Story story;
    void Start()
    {
        story = new Story(ink_json_asset.text);
        Refresh();
    }

    void Update()
    {
        
    }
    private string GetNextBlock()
    {
        string text = "";
        if(story.canContinue)
        {
            text = story.Continue();
        }
        return text;
    }
    private void Refresh()
    {
        ClearUI();
        GameObject new_game_object = new GameObject("text_chunk");
        new_game_object.transform.SetParent(this.transform, false);
        Text new_text_object = new_game_object.AddComponent<Text>();
        new_text_object.fontSize = 24;
        new_text_object.text = GetNextBlock();
        //to do: font
        foreach (Choice choice in story.currentChoices)
        {
            Button choice_button = Instantiate(button_prefab) as Button;
            choice_button.transform.SetParent(this.transform, false);
            Text choice_text = choice_button.GetComponentInChildren<Text>();
            choice_text.text = choice.text;
            choice_button.onClick.AddListener(delegate { OnClickChoiceButton(choice); }) ;
        }
    }
    private void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        Refresh();
    }
    private void ClearUI()
    {
        int child_count = this.transform.childCount;
        for (int i = child_count-1; i >= 0 ; --i)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }
    }
}