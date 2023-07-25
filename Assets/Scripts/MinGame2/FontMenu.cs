using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FontMenu : MonoBehaviour
{

    public GameObject boldButton;
    private Button bB;
    public GameObject fontButton;
    private Button fB;

    private ColorBlock cb;

    public Color normal;
    public Color pressed;

    public GameObject content;
    TMP_Text text;

    public GameObject inputFieldObject;
    private TMP_InputField inputField;
    string textInput;

    public GameObject inputPlaceHolder;
    TMP_Text textPlaceHolder;


    private TMP_FontAsset monorFont;
    private TMP_FontAsset liberationSansFont;

    private float fontSize;
    private float minFont;
    private float maxFont;
    private bool bold;
    private bool monor;

    private bool incDown;
    private bool decDown;

    // Start is called before the first frame update
    void Start()
    {
        bB = boldButton.GetComponent<Button>();
        fB = fontButton.GetComponent<Button>();

        inputField = inputFieldObject.GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(delegate { IsSelected(false); });

        text = content.GetComponent<TMP_Text>();
        textPlaceHolder = inputPlaceHolder.GetComponent<TMP_Text>();

        monorFont = Resources.Load(System.IO.Path.Combine("Fonts", "Monor_Regular SDF"), typeof(TMP_FontAsset)) as TMP_FontAsset;
        liberationSansFont = Resources.Load("LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;

        fontSize = text.fontSize;
        Debug.Log(text.fontSize);
        minFont = 20;
        maxFont = 75;
        bold = false;
        monor = true;
    }

    private void IsSelected (bool isSelected)
    {
        if (isSelected == false)
        {
            EventSystem theSystem = EventSystem.current;
            if (!theSystem.alreadySelecting)
            {
                theSystem.SetSelectedGameObject(null);
            }
        }
    }

    private void Update()
    {
        textPlaceHolder.text = fontSize.ToString();
        if (incDown)
        {
            increaseFontSize();
        }
        else if (decDown)
        {
            decreaseFontSize();
        }
    }

    public void setDecDown(bool set)
    {
        decDown = set;
    }

    public void setIncDown(bool set)
    {
        incDown = set;
    }

    public void increaseFontSize()
    {
        if (fontSize != maxFont)
        {
            fontSize += 1f;
            text.fontSize += 1f;
        }
    }

    public void decreaseFontSize()
    {
        if (fontSize != minFont)
        {
            fontSize -= 1f;
            text.fontSize -= 1f;
        }
    }

    public void boldFont()
    {
        if (bold)
        {
            bold = false;
            text.fontStyle = FontStyles.Normal;

            cb = bB.colors;
            cb.normalColor = normal;
            bB.colors = cb;
        }
        else
        {
            bold = true;
            text.fontStyle = FontStyles.Bold;

            cb = bB.colors;
            cb.normalColor = pressed;
            bB.colors = cb;
        }
    }

    public void fontType()
    {
        if (monor)
        {
            monor = false;
            text.font = liberationSansFont;

            fB = fontButton.GetComponent<Button>();
            cb = fB.colors;
            cb.normalColor = pressed;
            fB.colors = cb;
        }
        else
        {
            monor = true;
            text.font = monorFont;

            fB = fontButton.GetComponent<Button>();
            cb = fB.colors;
            cb.normalColor = normal;
            fB.colors = cb;
        }
    }

    public void setPlaceHolderActive(bool set)
    {
        inputPlaceHolder.SetActive(set);
    }

    public void inputFontSize()
    {
        textInput = inputField.text;
        if (textInput != "")
        {
            float size = float.Parse(textInput);
            if (size >= minFont && size <= maxFont)
            {
                fontSize = size;
                text.fontSize = size;
            }
            else if (size < minFont)
            {
                fontSize = minFont;
                text.fontSize = minFont;
            }
            else if (size > maxFont)
            {
                fontSize = maxFont;
                text.fontSize = maxFont;
            }

            Debug.Log("Setting blank");
            inputField.text = "";
        }
    }
}
