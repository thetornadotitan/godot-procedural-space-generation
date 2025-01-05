using Godot;
using System;

namespace t3 {
    public partial class Star : Area2D
    {
        [Export]
        Sprite2D gfx;

        private float maxXYOffset = 48;
        private int starSeed;
        private string name;
        private byte planets;
        private Vector2 rotationDirection;

        public void Generate()
        {
            float scale = PRNG.NextFloat(0.25f, 4);
            float xOff = PRNG.NextFloat(0, 1) * (maxXYOffset * 2) - maxXYOffset;
            float yOff = PRNG.NextFloat(0, 1) * (maxXYOffset * 2) - maxXYOffset;
            Color c = new (PRNG.NextFloat(0.1f, 1), PRNG.NextFloat(0.1f, 1), PRNG.NextFloat(0.1f, 1), 1);
            starSeed = PRNG.NextInt();
            planets = (byte)PRNG.NextInt(1, 12);
            rotationDirection = new Vector2(PRNG.NextFloat(0, 1) * 2 - 1, PRNG.NextFloat(0, 1) * 2 - 1);
            name = Naming.GenerateName();

            NoiseTexture2D noiseTexture = new()
            {
                Seamless = true,
                Width = 64,
                Height = 64,
            };

            FastNoiseLite noise = new()
            {
                Seed = starSeed,
                Frequency = 0.1f,
                NoiseType = FastNoiseLite.NoiseTypeEnum.Cellular
            };

            noiseTexture.Noise = noise;
            (gfx.Material as ShaderMaterial).SetShaderParameter("noiseTex", noiseTexture);
            (gfx.Material as ShaderMaterial).SetShaderParameter("rotationDirection", rotationDirection);
            gfx.Modulate = c;

            Position += Vector2.Down * xOff;
            Position += Vector2.Right * yOff;
            Scale *= scale;
        }

        public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
        {
            if (@event is InputEventMouseButton && !SpaceSceneManager.instance.IsUIOpen())
            {
                InputEventMouseButton mouseButton = (InputEventMouseButton)@event;
                if (mouseButton.ButtonIndex == MouseButton.Left && !mouseButton.Pressed)
                {
                    SpaceSceneManager.instance.OpenStarUI(this);
                }
            }
        }

        public int GetStarSeed()
        {
            return starSeed;
        }

        public byte GetPlanetCount()
        {
            return planets;
        }

        public string GetStarName()
        {
            return name;
        }
    }
}