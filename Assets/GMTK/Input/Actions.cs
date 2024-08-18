//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/GMTK/Input/Actions.inputactions
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

namespace GMTK
{
    public partial class @Actions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Actions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""PlayerBody"",
            ""id"": ""82dbc453-2c8e-4032-8468-2f28e8986d93"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7eed9a70-124a-4169-8de0-a139388f4692"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bdb7d59b-7c78-49d5-aa3c-3ee50fd780e8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0cb5e8ec-20fa-432e-b069-47f505c3a0d6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0f3aab71-77eb-440a-834f-f063c375762f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""33b223be-5c43-4be4-82b6-30115f90c0ee"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5c6c5d0f-fe1c-4eb4-ba8c-e0e54d85da19"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""PlayerTurret"",
            ""id"": ""0c94d8b7-d505-4ed1-b8dd-f0aa76a7aa42"",
            ""actions"": [
                {
                    ""name"": ""Pointer"",
                    ""type"": ""Value"",
                    ""id"": ""5497c5c2-1abf-422c-ac5b-bc778c7ce934"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""f0e6bffc-ce56-4f8a-8b38-5ff261b5eb14"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e6e2c81f-bb45-49c9-bd13-20e48cab6248"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c764edb7-83aa-4030-90da-26ee617e292a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerPenetration"",
            ""id"": ""69f12287-da9f-4b89-be53-70e32eeae7b9"",
            ""actions"": [
                {
                    ""name"": ""Penetration"",
                    ""type"": ""Button"",
                    ""id"": ""48e1e559-7557-4439-86b9-ab81437c761e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cdbda100-c381-4746-a2f0-1b87bb3d10a1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Penetration"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GameOver"",
            ""id"": ""fdc67e28-7208-46c1-9415-37ff97cafe13"",
            ""actions"": [
                {
                    ""name"": ""AnyKey"",
                    ""type"": ""Button"",
                    ""id"": ""13859336-fe79-41a9-bb11-a50bbf183e71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8df92aa4-7428-4a94-b3d5-5b990d85651a"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PlayerBody
            m_PlayerBody = asset.FindActionMap("PlayerBody", throwIfNotFound: true);
            m_PlayerBody_Move = m_PlayerBody.FindAction("Move", throwIfNotFound: true);
            // PlayerTurret
            m_PlayerTurret = asset.FindActionMap("PlayerTurret", throwIfNotFound: true);
            m_PlayerTurret_Pointer = m_PlayerTurret.FindAction("Pointer", throwIfNotFound: true);
            m_PlayerTurret_Fire = m_PlayerTurret.FindAction("Fire", throwIfNotFound: true);
            // PlayerPenetration
            m_PlayerPenetration = asset.FindActionMap("PlayerPenetration", throwIfNotFound: true);
            m_PlayerPenetration_Penetration = m_PlayerPenetration.FindAction("Penetration", throwIfNotFound: true);
            // GameOver
            m_GameOver = asset.FindActionMap("GameOver", throwIfNotFound: true);
            m_GameOver_AnyKey = m_GameOver.FindAction("AnyKey", throwIfNotFound: true);
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

        // PlayerBody
        private readonly InputActionMap m_PlayerBody;
        private List<IPlayerBodyActions> m_PlayerBodyActionsCallbackInterfaces = new List<IPlayerBodyActions>();
        private readonly InputAction m_PlayerBody_Move;
        public struct PlayerBodyActions
        {
            private @Actions m_Wrapper;
            public PlayerBodyActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_PlayerBody_Move;
            public InputActionMap Get() { return m_Wrapper.m_PlayerBody; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerBodyActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerBodyActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerBodyActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerBodyActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }

            private void UnregisterCallbacks(IPlayerBodyActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
            }

            public void RemoveCallbacks(IPlayerBodyActions instance)
            {
                if (m_Wrapper.m_PlayerBodyActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerBodyActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerBodyActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerBodyActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerBodyActions @PlayerBody => new PlayerBodyActions(this);

        // PlayerTurret
        private readonly InputActionMap m_PlayerTurret;
        private List<IPlayerTurretActions> m_PlayerTurretActionsCallbackInterfaces = new List<IPlayerTurretActions>();
        private readonly InputAction m_PlayerTurret_Pointer;
        private readonly InputAction m_PlayerTurret_Fire;
        public struct PlayerTurretActions
        {
            private @Actions m_Wrapper;
            public PlayerTurretActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Pointer => m_Wrapper.m_PlayerTurret_Pointer;
            public InputAction @Fire => m_Wrapper.m_PlayerTurret_Fire;
            public InputActionMap Get() { return m_Wrapper.m_PlayerTurret; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerTurretActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerTurretActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerTurretActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerTurretActionsCallbackInterfaces.Add(instance);
                @Pointer.started += instance.OnPointer;
                @Pointer.performed += instance.OnPointer;
                @Pointer.canceled += instance.OnPointer;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }

            private void UnregisterCallbacks(IPlayerTurretActions instance)
            {
                @Pointer.started -= instance.OnPointer;
                @Pointer.performed -= instance.OnPointer;
                @Pointer.canceled -= instance.OnPointer;
                @Fire.started -= instance.OnFire;
                @Fire.performed -= instance.OnFire;
                @Fire.canceled -= instance.OnFire;
            }

            public void RemoveCallbacks(IPlayerTurretActions instance)
            {
                if (m_Wrapper.m_PlayerTurretActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerTurretActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerTurretActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerTurretActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerTurretActions @PlayerTurret => new PlayerTurretActions(this);

        // PlayerPenetration
        private readonly InputActionMap m_PlayerPenetration;
        private List<IPlayerPenetrationActions> m_PlayerPenetrationActionsCallbackInterfaces = new List<IPlayerPenetrationActions>();
        private readonly InputAction m_PlayerPenetration_Penetration;
        public struct PlayerPenetrationActions
        {
            private @Actions m_Wrapper;
            public PlayerPenetrationActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Penetration => m_Wrapper.m_PlayerPenetration_Penetration;
            public InputActionMap Get() { return m_Wrapper.m_PlayerPenetration; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerPenetrationActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerPenetrationActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerPenetrationActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerPenetrationActionsCallbackInterfaces.Add(instance);
                @Penetration.started += instance.OnPenetration;
                @Penetration.performed += instance.OnPenetration;
                @Penetration.canceled += instance.OnPenetration;
            }

            private void UnregisterCallbacks(IPlayerPenetrationActions instance)
            {
                @Penetration.started -= instance.OnPenetration;
                @Penetration.performed -= instance.OnPenetration;
                @Penetration.canceled -= instance.OnPenetration;
            }

            public void RemoveCallbacks(IPlayerPenetrationActions instance)
            {
                if (m_Wrapper.m_PlayerPenetrationActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerPenetrationActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerPenetrationActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerPenetrationActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerPenetrationActions @PlayerPenetration => new PlayerPenetrationActions(this);

        // GameOver
        private readonly InputActionMap m_GameOver;
        private List<IGameOverActions> m_GameOverActionsCallbackInterfaces = new List<IGameOverActions>();
        private readonly InputAction m_GameOver_AnyKey;
        public struct GameOverActions
        {
            private @Actions m_Wrapper;
            public GameOverActions(@Actions wrapper) { m_Wrapper = wrapper; }
            public InputAction @AnyKey => m_Wrapper.m_GameOver_AnyKey;
            public InputActionMap Get() { return m_Wrapper.m_GameOver; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameOverActions set) { return set.Get(); }
            public void AddCallbacks(IGameOverActions instance)
            {
                if (instance == null || m_Wrapper.m_GameOverActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_GameOverActionsCallbackInterfaces.Add(instance);
                @AnyKey.started += instance.OnAnyKey;
                @AnyKey.performed += instance.OnAnyKey;
                @AnyKey.canceled += instance.OnAnyKey;
            }

            private void UnregisterCallbacks(IGameOverActions instance)
            {
                @AnyKey.started -= instance.OnAnyKey;
                @AnyKey.performed -= instance.OnAnyKey;
                @AnyKey.canceled -= instance.OnAnyKey;
            }

            public void RemoveCallbacks(IGameOverActions instance)
            {
                if (m_Wrapper.m_GameOverActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IGameOverActions instance)
            {
                foreach (var item in m_Wrapper.m_GameOverActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_GameOverActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public GameOverActions @GameOver => new GameOverActions(this);
        public interface IPlayerBodyActions
        {
            void OnMove(InputAction.CallbackContext context);
        }
        public interface IPlayerTurretActions
        {
            void OnPointer(InputAction.CallbackContext context);
            void OnFire(InputAction.CallbackContext context);
        }
        public interface IPlayerPenetrationActions
        {
            void OnPenetration(InputAction.CallbackContext context);
        }
        public interface IGameOverActions
        {
            void OnAnyKey(InputAction.CallbackContext context);
        }
    }
}
