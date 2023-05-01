using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField]
    private InputActionAsset _actionsAsset;
    public InputActionAsset ActionsAsset { get => _actionsAsset; }

    [SerializeField]
    private string _defaultActionMapName;

    private string _currentActionMap;

    private void Awake()
    {
        // Only one instance of this class can exist at a time
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        _currentActionMap = "";
    }

    // We need to enable the action map to read the values
    void OnEnable()
    {
        _actionsAsset.FindActionMap(_defaultActionMapName).Enable();
    }

    public void SwitchToActionMap(string actionMapName)
    {

    }
}
