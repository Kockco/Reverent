Shader "Toon/Lit Swaying" {
	Properties{
		_Color("Color Primary", Color) = (0.5,0.5,0.5,1)
		_ColorSec("Color Secondary", Color) = (0.5,0.5,0.5,1)
		_ColorTert("Color Tertiary", Color) = (0.5,0.5,0.5,1)
		_MainTex("Main Texture", 2D) = "white" {}
		_Mask("Mask: R= Prim, G = Sec, B = Tert ", 2D) = "black" {}
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
		_Speed("MoveSpeed", Range(20,50)) = 25 // speed of the swaying
		_Rigidness("Rigidness", Range(0,1)) = 25 // lower makes it look more "liquid" higher makes it look rigid
		_SwayMax("Sway Max", Range(0, 0.1)) = .005 // how far the swaying goes
		_YOffset("Y offset", float) = 0.5// y offset, below this is no animation

	}

		SubShader{
			Tags { "RenderType" = "Opaque"  }
			LOD 200


		CGPROGRAM
		#pragma surface surf ToonRamp vertex:vert addshadow // addshadow applies shadow after vertex animation


		sampler2D _Ramp;

		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
		#pragma lighting ToonRamp exclude_path:prepass
		#pragma multi_compile_instancing

		inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
			#endif

			half d = dot(s.Normal, lightDir)*0.5 + 0.5;
			half3 ramp = tex2D(_Ramp, float2(d,d)).rgb;

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = 0;
			return c;
		}


		sampler2D _MainTex, _Mask;
		float _Speed;
		float _SwayMax;
		float _YOffset;
		float _Rigidness;


		struct Input {
			float2 uv_MainTex : TEXCOORD0;
		};

		void vert(inout appdata_full v)//
		{

			float3 wpos = mul(unity_ObjectToWorld, v.vertex).xyz;// world position         
			float wind = lerp(-1, 1, (wpos.x * _Rigidness) + _Time.x); // wind over work
			float z = sin(_Speed*wind);
			float x = cos(_Speed*wind);
			v.vertex.x += step(0,v.vertex.y - _YOffset) * x * _SwayMax;// apply the movement if the vertex's y above the YOffset
			v.vertex.z += step(0,v.vertex.y - _YOffset) * z * _SwayMax;

		}

		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _ColorSec)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _ColorTert)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			half4 m = tex2D(_Mask, IN.uv_MainTex);
			half4 prim = m.r * UNITY_ACCESS_INSTANCED_PROP(Props, _Color) * c;
			half4 sec = m.g * UNITY_ACCESS_INSTANCED_PROP(Props, _ColorSec) * c;
			half4 tert = m.b* UNITY_ACCESS_INSTANCED_PROP(Props, _ColorTert) * c;
			half4 noMask = c * (1 - (m.r + m.g + m.b));

			o.Albedo = noMask + prim + sec + tert;
			o.Alpha = c.a;
		}
		ENDCG

		}

			Fallback "Diffuse"
}