using Godot;
using System;

namespace t3
{
    public partial class StarUI : Control
    {
        [Export]
        Control closeButton;
        [Export]
        Label starName;
        [Export]
        PackedScene planetUIElement;
        [Export]
        Control planetContainer;

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

        public void PopulateStarInfo(Star star)
        {
            foreach (Node n in planetContainer.GetChildren())
            {
                n.QueueFree();
            }

            PRNG.Seed((ulong)star.GetStarSeed());
            starName.Text = star.GetStarName();
            float lastDist = 0;
            for (int i = 0; i < star.GetPlanetCount(); i++)
            {
                PlanetUIElement p = planetUIElement.Instantiate<PlanetUIElement>();
                lastDist = p.Generate(lastDist);
                planetContainer.AddChild(p);
            }
        }
    }
}
