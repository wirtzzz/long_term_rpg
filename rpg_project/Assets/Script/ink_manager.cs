using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class ink_manager : MonoBehaviour
{
    public Font font;
    public bool is_active = false;
    public Button button_prefab;
    public TextAsset ink_json_asset;
    public GameObject gui_object;
    public Button end_button;
    private Story story;
    public void StartStory()
    {
        gui_object.SetActive(true);
        if (!is_active)
        {
            story = new Story(ink_json_asset.text);
            Refresh();
            is_active = true;
        }
    }
    private string GetNextBlock()
    {
        string text = "";
        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }
        return text;
    }
    private void Refresh()
    {
        ClearUI();

        GameObject new_game_object = new GameObject("text_chunk");
        new_game_object.transform.SetParent(this.transform, false);
        FormatText(ref new_game_object);
        foreach (Choice choice in story.currentChoices)
        {
            Button choice_button = Instantiate(button_prefab) as Button;
            choice_button.transform.SetParent(this.transform, false);
            choice_button.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
            FormatButtonText(ref choice_button, choice);
        }
        if (story.currentChoices.Count == 0)
        {
            Button end_dialog_button = Instantiate(end_button) as Button;
            end_dialog_button.transform.SetParent(this.transform, false);
            FormatButtonText(ref end_dialog_button);
            end_dialog_button.onClick.AddListener(delegate { OnClickEndButton(); });
        }
    }
    private void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        Refresh();
    }
    private void OnClickEndButton()
    {
        EndDialogBox();
    }
    private void EndDialogBox()
    {
        ink_json_asset = null;
        story.ResetState();
        is_active = false;
        ClearUI();
        gui_object.SetActive(false);

    }
    private void ClearUI()
    {
        int child_count = this.transform.childCount;
        for (int i = child_count - 1; i >= 0; --i)
        {
            if (this.transform.GetChild(i).gameObject != gui_object)
                GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }

    }

    //formatting text at the bottom
    private void FormatButtonText(ref Button button, Choice choice = null)
    {
        Text text = button.GetComponentInChildren<Text>();
        if (choice != null)
            text.text = choice.text;
        else
            text.text = "";
        text.font = font;
        text.color = Color.white;
        text.fontSize = 20;
        text.alignByGeometry = true;
    }
    private void FormatText(ref GameObject game_object)
    {
        Text text = game_object.AddComponent<Text>();
        text.alignment = TextAnchor.MiddleCenter;
        text.fontSize = 24;
        text.font = font;
        text.resizeTextMaxSize = 24;
        text.resizeTextMinSize = 10;
        text.text = GetNextBlock();
        text.color = Color.red;
        text.alignByGeometry = true;
        text.resizeTextForBestFit = true;
    }
}