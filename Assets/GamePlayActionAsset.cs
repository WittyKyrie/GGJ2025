//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/GamePlayActionAsset.inputactions
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

public partial class @GamePlayActionAsset: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GamePlayActionAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GamePlayActionAsset"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""14cce64f-cd7e-4809-98e7-e33776ff7136"",
            ""actions"": [
                {
                    ""name"": ""P1Pour"",
                    ""type"": ""Button"",
                    ""id"": ""e48448f2-af23-4ca8-88dc-cc2d6308c7f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""P1Item1"",
                    ""type"": ""Button"",
                    ""id"": ""261479fb-adf5-4fed-ba72-05a6d0ae510e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""P1Item2"",
                    ""type"": ""Button"",
                    ""id"": ""dcbe528f-d403-4e5a-8b9b-258dc8a32ed3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""P1Item3"",
                    ""type"": ""Button"",
                    ""id"": ""5cd85538-6d53-4047-a879-fb07bafa6f3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""P2Pour1"",
                    ""type"": ""Button"",
                    ""id"": ""4683a26e-73fd-4a18-8978-a6f04be62ef7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""P2Item1"",
                    ""type"": ""Button"",
                    ""id"": ""90f3cd89-9b9c-4a36-a532-5d38cc736bf9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""P2Item2"",
                    ""type"": ""Button"",
                    ""id"": ""c83e9e8f-c2f9-494f-a943-d34ae24e4605"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""P2Item3"",
                    ""type"": ""Button"",
                    ""id"": ""0352c7fb-bed4-48e4-80cc-f9198ab7baaf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3b698a62-5559-4abd-8ad7-658db0121ccb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P1Pour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f54930c0-903e-4c40-8919-a128d3160000"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P2Item2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea1637c1-b918-4a29-bb28-ddc965ad5cc8"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P2Item3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0993caa2-6c75-45ff-a136-a4bdd57414a4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P1Item1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b38eba3a-5d5e-44f9-986c-415cc8b07e3c"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P2Item1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdfd5dc5-3ff5-4a00-9073-7bfbcb94757f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P2Pour1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4614508-f9af-4dd3-a4e7-2e32f1e47424"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P1Item2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1985092a-3023-400b-a96c-edd406106596"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""P1Item3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_P1Pour = m_GamePlay.FindAction("P1Pour", throwIfNotFound: true);
        m_GamePlay_P1Item1 = m_GamePlay.FindAction("P1Item1", throwIfNotFound: true);
        m_GamePlay_P1Item2 = m_GamePlay.FindAction("P1Item2", throwIfNotFound: true);
        m_GamePlay_P1Item3 = m_GamePlay.FindAction("P1Item3", throwIfNotFound: true);
        m_GamePlay_P2Pour1 = m_GamePlay.FindAction("P2Pour1", throwIfNotFound: true);
        m_GamePlay_P2Item1 = m_GamePlay.FindAction("P2Item1", throwIfNotFound: true);
        m_GamePlay_P2Item2 = m_GamePlay.FindAction("P2Item2", throwIfNotFound: true);
        m_GamePlay_P2Item3 = m_GamePlay.FindAction("P2Item3", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private List<IGamePlayActions> m_GamePlayActionsCallbackInterfaces = new List<IGamePlayActions>();
    private readonly InputAction m_GamePlay_P1Pour;
    private readonly InputAction m_GamePlay_P1Item1;
    private readonly InputAction m_GamePlay_P1Item2;
    private readonly InputAction m_GamePlay_P1Item3;
    private readonly InputAction m_GamePlay_P2Pour1;
    private readonly InputAction m_GamePlay_P2Item1;
    private readonly InputAction m_GamePlay_P2Item2;
    private readonly InputAction m_GamePlay_P2Item3;
    public struct GamePlayActions
    {
        private @GamePlayActionAsset m_Wrapper;
        public GamePlayActions(@GamePlayActionAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @P1Pour => m_Wrapper.m_GamePlay_P1Pour;
        public InputAction @P1Item1 => m_Wrapper.m_GamePlay_P1Item1;
        public InputAction @P1Item2 => m_Wrapper.m_GamePlay_P1Item2;
        public InputAction @P1Item3 => m_Wrapper.m_GamePlay_P1Item3;
        public InputAction @P2Pour1 => m_Wrapper.m_GamePlay_P2Pour1;
        public InputAction @P2Item1 => m_Wrapper.m_GamePlay_P2Item1;
        public InputAction @P2Item2 => m_Wrapper.m_GamePlay_P2Item2;
        public InputAction @P2Item3 => m_Wrapper.m_GamePlay_P2Item3;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void AddCallbacks(IGamePlayActions instance)
        {
            if (instance == null || m_Wrapper.m_GamePlayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GamePlayActionsCallbackInterfaces.Add(instance);
            @P1Pour.started += instance.OnP1Pour;
            @P1Pour.performed += instance.OnP1Pour;
            @P1Pour.canceled += instance.OnP1Pour;
            @P1Item1.started += instance.OnP1Item1;
            @P1Item1.performed += instance.OnP1Item1;
            @P1Item1.canceled += instance.OnP1Item1;
            @P1Item2.started += instance.OnP1Item2;
            @P1Item2.performed += instance.OnP1Item2;
            @P1Item2.canceled += instance.OnP1Item2;
            @P1Item3.started += instance.OnP1Item3;
            @P1Item3.performed += instance.OnP1Item3;
            @P1Item3.canceled += instance.OnP1Item3;
            @P2Pour1.started += instance.OnP2Pour1;
            @P2Pour1.performed += instance.OnP2Pour1;
            @P2Pour1.canceled += instance.OnP2Pour1;
            @P2Item1.started += instance.OnP2Item1;
            @P2Item1.performed += instance.OnP2Item1;
            @P2Item1.canceled += instance.OnP2Item1;
            @P2Item2.started += instance.OnP2Item2;
            @P2Item2.performed += instance.OnP2Item2;
            @P2Item2.canceled += instance.OnP2Item2;
            @P2Item3.started += instance.OnP2Item3;
            @P2Item3.performed += instance.OnP2Item3;
            @P2Item3.canceled += instance.OnP2Item3;
        }

        private void UnregisterCallbacks(IGamePlayActions instance)
        {
            @P1Pour.started -= instance.OnP1Pour;
            @P1Pour.performed -= instance.OnP1Pour;
            @P1Pour.canceled -= instance.OnP1Pour;
            @P1Item1.started -= instance.OnP1Item1;
            @P1Item1.performed -= instance.OnP1Item1;
            @P1Item1.canceled -= instance.OnP1Item1;
            @P1Item2.started -= instance.OnP1Item2;
            @P1Item2.performed -= instance.OnP1Item2;
            @P1Item2.canceled -= instance.OnP1Item2;
            @P1Item3.started -= instance.OnP1Item3;
            @P1Item3.performed -= instance.OnP1Item3;
            @P1Item3.canceled -= instance.OnP1Item3;
            @P2Pour1.started -= instance.OnP2Pour1;
            @P2Pour1.performed -= instance.OnP2Pour1;
            @P2Pour1.canceled -= instance.OnP2Pour1;
            @P2Item1.started -= instance.OnP2Item1;
            @P2Item1.performed -= instance.OnP2Item1;
            @P2Item1.canceled -= instance.OnP2Item1;
            @P2Item2.started -= instance.OnP2Item2;
            @P2Item2.performed -= instance.OnP2Item2;
            @P2Item2.canceled -= instance.OnP2Item2;
            @P2Item3.started -= instance.OnP2Item3;
            @P2Item3.performed -= instance.OnP2Item3;
            @P2Item3.canceled -= instance.OnP2Item3;
        }

        public void RemoveCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGamePlayActions instance)
        {
            foreach (var item in m_Wrapper.m_GamePlayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GamePlayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    public interface IGamePlayActions
    {
        void OnP1Pour(InputAction.CallbackContext context);
        void OnP1Item1(InputAction.CallbackContext context);
        void OnP1Item2(InputAction.CallbackContext context);
        void OnP1Item3(InputAction.CallbackContext context);
        void OnP2Pour1(InputAction.CallbackContext context);
        void OnP2Item1(InputAction.CallbackContext context);
        void OnP2Item2(InputAction.CallbackContext context);
        void OnP2Item3(InputAction.CallbackContext context);
    }
}
