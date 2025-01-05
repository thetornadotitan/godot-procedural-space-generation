shader_type canvas_item;

uniform sampler2D shapeTex : filter_nearest;
uniform sampler2D noiseTex : filter_nearest, repeat_enable;
uniform vec2 rotationDirection = vec2(1.0, 0.0);

void fragment() {
	COLOR *= texture(shapeTex, UV);
	COLOR *= texture(noiseTex, mod(UV / 2.0f + TIME * rotationDirection / 10.0f, 1.0));
}
