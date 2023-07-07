using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject Popup;

    [SerializeField]
    public GameObject yesButton;

    [SerializeField]
    public GameObject answerForm;

    [SerializeField]
    public GameObject noButton;

    [SerializeField]
    public GameObject badYesButton;

    [SerializeField]
    public GameObject oops;

    public void onYesClick()
    {
        answerForm.SetActive(true);
        answerForm.GetComponentInChildren<TMP_InputField>().Select();
        Popup.SetActive(false);
    }

    public void onBadYesClick()
    {
        oops.SetActive(true);
        Popup.SetActive(false);
    }

    public void onNoClick()
    {
        Popup.SetActive(false);
    }

    // For hiding and revealing the buttons

    public void setYesButton(bool set)
    {
        yesButton.SetActive(set);
    }

    public void setNoButton(bool set)
    {
        noButton.SetActive(set);
    }

    public void setBadYesButton(bool set)
    {
        badYesButton.SetActive(set);
    }
}
