
public interface IEvent { }

#region StructEvents

#region MenuEvents

public struct MenuButtonClickEvent : IEvent
{
    public MenuButtonType MenuButtonType { get; }

    public MenuButtonClickEvent(MenuButtonType menuButtonType)
    {
        MenuButtonType = menuButtonType;
    }
}

public struct MenuPanelActivationEvent : IEvent
{
    public MenuPanelType MenuPanelType { get; }

    public MenuPanelActivationEvent(MenuPanelType menuPanelType)
    {
        MenuPanelType = menuPanelType;
    }
}

#endregion

#endregion

#region ClassEvents

public class TestClassEvent : IEvent
{
	
}

#endregion

#region Utils


public enum MenuPanelType
{
    ConnectionPanel,
    LoadingPanel,
    SettingsPanel,
}

public enum MenuButtonType
{
    PlayButton,
    SettingsButton,
    CloseSettingsButton,
}

public enum InGameButtonType
{
    ReadyButton,

}

#endregion