shader_type canvas_item;

uniform sampler2D shapeTex : filter_nearest;
uniform sampler2D noiseTex : filter_nearest, repeat_enable;
uniform sampler2D waterTex : filter_nearest, repeat_enable;
uniform vec2 rotationDirection = vec2(1.0, 0.0);
uniform float waterlevel = 0.5f;
uniform vec4 groundColor = vec4(0.45f, 0.90f, 0.45f, 1.00f);
uniform vec4 waterColor = vec4(0.1f, 0.1f, 3.0f, 1.00f);

void fragment() {
	COLOR *= texture(shapeTex, UV);
	COLOR *= texture(noiseTex, mod(UV / 2.0f + TIME * rotationDirection / 10.0f, 1.0));
	
	if (COLOR.r < waterlevel && COLOR.a > 0.0f) {
		COLOR = clamp(COLOR, waterlevel, 1.0f);
		COLOR *= waterColor;
		COLOR *= texture(waterTex, mod(UV + (TIME + 1.33f) / 23.4f + TIME * rotationDirection / 10.0f, 1.0)) / 2.0f + 0.5f;
		COLOR *= texture(waterTex, mod(UV - TIME / 27.7f + TIME * rotationDirection / 10.0f, 1.0)) / 2.0f + 0.5f;
	} else COLOR *= groundColor;
}
