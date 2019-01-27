// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInput/InputPlayer.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class InputPlayer : InputActionAssetReference
{
    public InputPlayer()
    {
    }
    public InputPlayer(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Movement = m_Player.GetAction("Movement");
        m_Player_Stop1 = m_Player.GetAction("Stop1");
        m_Player_Stop2 = m_Player.GetAction("Stop2");
        m_Player_Pause = m_Player.GetAction("Pause");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Player = null;
        m_Player_Movement = null;
        m_Player_Stop1 = null;
        m_Player_Stop2 = null;
        m_Player_Pause = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Player
    private InputActionMap m_Player;
    private InputAction m_Player_Movement;
    private InputAction m_Player_Stop1;
    private InputAction m_Player_Stop2;
    private InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private InputPlayer m_Wrapper;
        public PlayerActions(InputPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Player_Movement; } }
        public InputAction @Stop1 { get { return m_Wrapper.m_Player_Stop1; } }
        public InputAction @Stop2 { get { return m_Wrapper.m_Player_Stop2; } }
        public InputAction @Pause { get { return m_Wrapper.m_Player_Pause; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
    }
    public PlayerActions @Player
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerActions(this);
        }
    }
    private int m_GenericGamepadSchemeIndex = -1;
    public InputControlScheme GenericGamepadScheme
    {
        get

        {
            if (m_GenericGamepadSchemeIndex == -1) m_GenericGamepadSchemeIndex = asset.GetControlSchemeIndex("GenericGamepad");
            return asset.controlSchemes[m_GenericGamepadSchemeIndex];
        }
    }
}
