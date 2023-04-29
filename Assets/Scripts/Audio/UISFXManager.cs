using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UISFXManager : MonoBehaviour
{
    public static UISFXManager Instance;

    [SerializeField]
    private AudioSource _buttonHoverSFX;
    [SerializeField]
    private AudioSource _buttonClickSFX;

    private List<UIDocument> uiDocuments;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    public void SetupCallbacks()
    {
        uiDocuments = new List<UIDocument>(FindObjectsOfType<UIDocument>());

        foreach (var uiDocument in uiDocuments)
        {
            var buttons = uiDocument.rootVisualElement.Query<Button>().ToList();
            foreach (var button in buttons)
            {
                button.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
                button.RegisterCallback<ClickEvent>(OnButtonClick);
            }
        }
    }

    private void OnMouseEnter(MouseEnterEvent evt)
    {
        Debug.Log("Mouse entered: " + ((Button)evt.currentTarget).name);
        _buttonHoverSFX.Play();
    }

    private void OnButtonClick(ClickEvent evt)
    {
        Debug.Log("Button clicked: " + ((Button)evt.currentTarget).name);
        _buttonClickSFX.Play();
    }
}
