using Godot;
using System;

namespace t3
{
    public partial class HelpUI : Control
    {
        [Export]
        Control closeButton;

        public override void _Ready()
        {
            closeButton.Connect(Control.SignalName.GuiInput, new Callable(this, MethodName.HandleClose));
        }

        private void HandleClose(InputEvent @event)
        {
            if (@event is InputEventMouseButton)
            {
                InputEventMouseButton mouseButton = @event as InputEventMouseButton;
                if (mouseButton.ButtonIndex == MouseButton.Left && !mouseButton.Pressed)
                {
                    Visible = false;
                }
            }
        }
    }
}