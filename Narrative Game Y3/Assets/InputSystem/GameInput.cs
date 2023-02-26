//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSystem/Input Actions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Actions"",
    ""maps"": [
        {
            ""name"": ""GameInput"",
            ""id"": ""85ce029b-6177-4ad1-a2c8-ee0c40f4af1f"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""91ca54dd-cbb0-4166-a97c-695dad6124b3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""53afbcca-20dd-46fb-b000-6f0cab6254e7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""cfb27851-68bf-4843-8503-d00bce77465e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LMB"",
                    ""type"": ""Button"",
                    ""id"": ""dd265b15-efb7-417d-b367-46c852791db6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""374b4d94-c89a-4005-a2ae-b3fc6c2f528e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""37c085b5-309a-4832-89e9-afdf254313c1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Test"",
                    ""type"": ""Button"",
                    ""id"": ""90c78f55-0c5d-4835-b8bd-bc40fc966e20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3f1f1887-4bde-4aa3-8c7d-256811aed9f1"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43808d8a-cdb5-4941-b42a-ebc17b46ecf0"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4de352c3-fd4d-41fb-98c9-2147e5ebbbc4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LMB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26b0e2b1-c454-4244-a01e-53a31b7a659f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c68ba96d-6cb5-42cc-b67f-4f640cecb934"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e16f6ed-a8b7-41e6-8bed-8adb9aa42639"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6aed508d-e113-4110-9845-e5865133abbc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GameInput
        m_GameInput = asset.FindActionMap("GameInput", throwIfNotFound: true);
        m_GameInput_MousePosition = m_GameInput.FindAction("MousePosition", throwIfNotFound: true);
        m_GameInput_MouseDelta = m_GameInput.FindAction("MouseDelta", throwIfNotFound: true);
        m_GameInput_Map = m_GameInput.FindAction("Map", throwIfNotFound: true);
        m_GameInput_LMB = m_GameInput.FindAction("LMB", throwIfNotFound: true);
        m_GameInput_Back = m_GameInput.FindAction("Back", throwIfNotFound: true);
        m_GameInput_ScrollWheel = m_GameInput.FindAction("ScrollWheel", throwIfNotFound: true);
        m_GameInput_Test = m_GameInput.FindAction("Test", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GameInput
    private readonly InputActionMap m_GameInput;
    private IGameInputActions m_GameInputActionsCallbackInterface;
    private readonly InputAction m_GameInput_MousePosition;
    private readonly InputAction m_GameInput_MouseDelta;
    private readonly InputAction m_GameInput_Map;
    private readonly InputAction m_GameInput_LMB;
    private readonly InputAction m_GameInput_Back;
    private readonly InputAction m_GameInput_ScrollWheel;
    private readonly InputAction m_GameInput_Test;
    public struct GameInputActions
    {
        private @InputActions m_Wrapper;
        public GameInputActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_GameInput_MousePosition;
        public InputAction @MouseDelta => m_Wrapper.m_GameInput_MouseDelta;
        public InputAction @Map => m_Wrapper.m_GameInput_Map;
        public InputAction @LMB => m_Wrapper.m_GameInput_LMB;
        public InputAction @Back => m_Wrapper.m_GameInput_Back;
        public InputAction @ScrollWheel => m_Wrapper.m_GameInput_ScrollWheel;
        public InputAction @Test => m_Wrapper.m_GameInput_Test;
        public InputActionMap Get() { return m_Wrapper.m_GameInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameInputActions set) { return set.Get(); }
        public void SetCallbacks(IGameInputActions instance)
        {
            if (m_Wrapper.m_GameInputActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMousePosition;
                @MouseDelta.started -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.performed -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.canceled -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMouseDelta;
                @Map.started -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_GameInputActionsCallbackInterface.OnMap;
                @LMB.started -= m_Wrapper.m_GameInputActionsCallbackInterface.OnLMB;
                @LMB.performed -= m_Wrapper.m_GameInputActionsCallbackInterface.OnLMB;
                @LMB.canceled -= m_Wrapper.m_GameInputActionsCallbackInterface.OnLMB;
                @Back.started -= m_Wrapper.m_GameInputActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_GameInputActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_GameInputActionsCallbackInterface.OnBack;
                @ScrollWheel.started -= m_Wrapper.m_GameInputActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_GameInputActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_GameInputActionsCallbackInterface.OnScrollWheel;
                @Test.started -= m_Wrapper.m_GameInputActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_GameInputActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_GameInputActionsCallbackInterface.OnTest;
            }
            m_Wrapper.m_GameInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseDelta.started += instance.OnMouseDelta;
                @MouseDelta.performed += instance.OnMouseDelta;
                @MouseDelta.canceled += instance.OnMouseDelta;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @LMB.started += instance.OnLMB;
                @LMB.performed += instance.OnLMB;
                @LMB.canceled += instance.OnLMB;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
            }
        }
    }
    public GameInputActions @GameInput => new GameInputActions(this);
    public interface IGameInputActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnLMB(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnTest(InputAction.CallbackContext context);
    }
}
