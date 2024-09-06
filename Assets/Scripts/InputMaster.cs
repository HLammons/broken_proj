// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Robot"",
            ""id"": ""a2c4d7aa-2ac9-423c-90d4-a4a0351c37a2"",
            ""actions"": [
                {
                    ""name"": ""Driving"",
                    ""type"": ""Button"",
                    ""id"": ""1f88e36e-c23e-4436-8682-88121c0acb59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Steering"",
                    ""type"": ""Button"",
                    ""id"": ""0da74df7-a5bc-41af-bdc0-7604e7ed9aab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PanTilt"",
                    ""type"": ""Button"",
                    ""id"": ""b4f27e9b-9de0-4823-bcfc-abb3249f0548"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""02c7e85c-bfe4-45db-b44f-4883899270f1"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Driving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f54d6e7-5744-4c89-b82a-f5f05e2bcd2f"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Driving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58c6396a-36aa-4494-91cb-cdef180c5862"",
                    ""path"": ""<XInputController>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a52374d3-abf9-48f4-b20e-812630801b8a"",
                    ""path"": ""<XInputController>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5f76597-c6ec-42dc-9836-3edd8f234be9"",
                    ""path"": ""<XInputController>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""PanTilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f450ddfc-b9f6-4f90-be20-90f2a4a5abe2"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""PanTilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""069d1360-8f31-481b-b166-e101f1e517d6"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""PanTilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac337f35-8414-4d93-812c-c32e6d9dcd65"",
                    ""path"": ""<XInputController>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XBox Control Scheme"",
                    ""action"": ""PanTilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XBox Control Scheme"",
            ""bindingGroup"": ""XBox Control Scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Robot
        m_Robot = asset.FindActionMap("Robot", throwIfNotFound: true);
        m_Robot_Driving = m_Robot.FindAction("Driving", throwIfNotFound: true);
        m_Robot_Steering = m_Robot.FindAction("Steering", throwIfNotFound: true);
        m_Robot_PanTilt = m_Robot.FindAction("PanTilt", throwIfNotFound: true);
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

    // Robot
    private readonly InputActionMap m_Robot;
    private IRobotActions m_RobotActionsCallbackInterface;
    private readonly InputAction m_Robot_Driving;
    private readonly InputAction m_Robot_Steering;
    private readonly InputAction m_Robot_PanTilt;
    public struct RobotActions
    {
        private @InputMaster m_Wrapper;
        public RobotActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Driving => m_Wrapper.m_Robot_Driving;
        public InputAction @Steering => m_Wrapper.m_Robot_Steering;
        public InputAction @PanTilt => m_Wrapper.m_Robot_PanTilt;
        public InputActionMap Get() { return m_Wrapper.m_Robot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RobotActions set) { return set.Get(); }
        public void SetCallbacks(IRobotActions instance)
        {
            if (m_Wrapper.m_RobotActionsCallbackInterface != null)
            {
                @Driving.started -= m_Wrapper.m_RobotActionsCallbackInterface.OnDriving;
                @Driving.performed -= m_Wrapper.m_RobotActionsCallbackInterface.OnDriving;
                @Driving.canceled -= m_Wrapper.m_RobotActionsCallbackInterface.OnDriving;
                @Steering.started -= m_Wrapper.m_RobotActionsCallbackInterface.OnSteering;
                @Steering.performed -= m_Wrapper.m_RobotActionsCallbackInterface.OnSteering;
                @Steering.canceled -= m_Wrapper.m_RobotActionsCallbackInterface.OnSteering;
                @PanTilt.started -= m_Wrapper.m_RobotActionsCallbackInterface.OnPanTilt;
                @PanTilt.performed -= m_Wrapper.m_RobotActionsCallbackInterface.OnPanTilt;
                @PanTilt.canceled -= m_Wrapper.m_RobotActionsCallbackInterface.OnPanTilt;
            }
            m_Wrapper.m_RobotActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Driving.started += instance.OnDriving;
                @Driving.performed += instance.OnDriving;
                @Driving.canceled += instance.OnDriving;
                @Steering.started += instance.OnSteering;
                @Steering.performed += instance.OnSteering;
                @Steering.canceled += instance.OnSteering;
                @PanTilt.started += instance.OnPanTilt;
                @PanTilt.performed += instance.OnPanTilt;
                @PanTilt.canceled += instance.OnPanTilt;
            }
        }
    }
    public RobotActions @Robot => new RobotActions(this);
    private int m_XBoxControlSchemeSchemeIndex = -1;
    public InputControlScheme XBoxControlSchemeScheme
    {
        get
        {
            if (m_XBoxControlSchemeSchemeIndex == -1) m_XBoxControlSchemeSchemeIndex = asset.FindControlSchemeIndex("XBox Control Scheme");
            return asset.controlSchemes[m_XBoxControlSchemeSchemeIndex];
        }
    }
    public interface IRobotActions
    {
        void OnDriving(InputAction.CallbackContext context);
        void OnSteering(InputAction.CallbackContext context);
        void OnPanTilt(InputAction.CallbackContext context);
    }
}
