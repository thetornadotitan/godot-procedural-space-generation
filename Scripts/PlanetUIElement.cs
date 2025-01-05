using Godot;
using System;

namespace t3 {
    public partial class PlanetUIElement : VBoxContainer
    {
        [Export]
        Label nameUI;
        [Export]
        TextureRect planetGfx;
        [Export]
        Label distanceUI;

        public float Generate(float lastDist = 0)
        {
            // Name / Data Stuff
            nameUI.Text = Naming.GenerateName();

            //GFX / Shader / Texture Stuff
            NoiseTexture2D noiseTexture = new()
            {
                Seamless = true,
                Width = 64,
                Height = 64,
            };

            FastNoiseLite noise = new()
            {
                Frequency = 0.05f,
                NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex
            };

            noiseTexture.Noise = noise;
            (planetGfx.Material as ShaderMaterial).SetShaderParameter("noiseTex", noiseTexture);
            (planetGfx.Material as ShaderMaterial).SetShaderParameter("rotationDirection", new Vector2(PRNG.NextFloat(0, 1) * 2 - 1, PRNG.NextFloat(0, 1) * 2 - 1));
            (planetGfx.Material as ShaderMaterial).SetShaderParameter("waterlevel", PRNG.NextFloat(0.4f, 0.6f));
            (planetGfx.Material as ShaderMaterial).SetShaderParameter("groundColor", new Color(
                PRNG.NextFloat(0.45f, 0.9f),
                PRNG.NextFloat(0.45f, 0.9f),
                PRNG.NextFloat(0.25f, 0.33f),
                1.00f
            ));

            (planetGfx.Material as ShaderMaterial).SetShaderParameter("waterColor", new Color(
                PRNG.NextFloat(0.1f, 2.0f),
                PRNG.NextFloat(0.1f, 1.0f),
                PRNG.NextFloat(1.1f, 3.0f), 
                1.00f
            ));

            // Distance Stuff
            float dist = lastDist += PRNG.NextFloat(3, 240);
            string units = (dist >= 60) ? " Light Hours" : " Light Minutes";
            distanceUI.Text = ((dist >= 60) ? (dist / 60).ToString() : dist.ToString()) + units;
            return dist;
        }
    }
}