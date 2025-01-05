using Godot;
using System;


namespace t3
{
    public partial class SpaceChunk : Node2D
    {
        [Export]
        Label coordsLabel;
        [Export]
        Star myStar;

        public void SetCoords(string coords)
        {
            coordsLabel.Text = coords;
            PRNG.Seed(coords);
            myStar.Generate();
        }
    }
}