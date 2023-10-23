Shader "BluWizard/Glass FX"
{
	Properties
	{
		// Header
		[HideInInspector]shader_is_using_thry_editor("", Float) = 0
		[HideInInspector]shader_master_label("BluWizard<color=#0092d9>LABS</color> Glass FX 1.0", Float) = 0
		[HideInInspector]shader_on_swap_to("--{actions:[{type:SET_PROPERTY,data:LightmapFlags=0},{type:SET_PROPERTY,data:DSGI=0}]}", Float) = 0

		// Main
		[HideInInspector]m_Main("Main", Float) = 0
		[NoScaleOffset][SingleLineTexture]_AlbedoMapreference_property_Color("Albedo Map--{reference_property:_Color}}", 2D) = "white" {}
		[HideInInspector]_Color("Color + Transparency", Color) = (1,1,1,0)
		_Metaliic("Metallic Slider", Range( 0 , 1)) = 1
		_Glossiness("Smoothness Slider", Range( 0 , 1)) = 1
		_AntiAliasingVarianceSm("Anti Aliasing Variance", Range( 0 , 5)) = 0.01
		_AntiAliasingThresholdSm("Anti Aliasing Threshold", Range( 0 , 1)) = 1
		[Toggle(_NormalMap_ON)] _UseNormalMap("Enable Normal Map", Float) = 0
		[NoScaleOffset][Normal][SingleLineTexture]_BumpMap("Normal Map--{reference_property:_NormalMapSlider,condition_show:{type:PROPERTY_BOOL,data:_UseNormalMap==1}}", 2D) = "bump" {}
		[HideInInspector]_NormalMapSlider("Normal Map Slider", Range( 0 , 5)) = 1
		[Vector2][Space]_Tiling("Tiling", Vector) = (1,1,0,0)
		[Vector2]_Offset("Offset", Vector) = (0,0,0,0)

		// Rim
		[HideInInspector]m_Rim("Rim", Float) = 0
		[HDR]_EmissionColor("Color", Color) = (0,0,0,0)
		_Bias("Bias", Range( 0 , 1)) = 0
		_Scale("Scale", Range( 0 , 4)) = 4
		_Power("Power", Range( 0 , 4)) = 4
		_Strength("Strength", Range( 0 , 1)) = 0.75
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1

		// Rendering
		[HideInInspector]m_Rendering("Rendering", Float) = 0
		[Enum(None,0,Front,1,Back,2)]_Cull("Cull", Float) = 2
		[HideInInspector]LightmapFlags("LightmapFlags", Float) = 0
		[HideInInspector]DSGI("DSGI", Float) = 0
		[HideInInspector]Instancing("Instancing", Float) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+3000" "IgnoreProjector" = "True" "IsEmissive" = "true" "VRCFallback" = "Hidden"  }
		Cull [_Cull]
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _NormalMap_ON
		#define ASE_USING_SAMPLING_MACROS 1
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#endif//ASE Sampling Macros

		#pragma exclude_renderers xbox360 xboxone xboxseries ps4 playstation psp2 n3ds wiiu switch 
		#pragma surface surf Standard alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nometa noforwardadd 

		// Structs
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			half3 worldNormal;
			INTERNAL_DATA
		};
		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;

			UNITY_VERTEX_INPUT_INSTANCE_ID
		};
		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;

			UNITY_VERTEX_OUTPUT_STEREO
		};

		// Vert Method
		v2f vert (appdata v)
		{
			v2f o;

			UNITY_SETUP_INSTANCE_ID(v);
			UNITY_INITIALIZE_OUTPUT(v2f, o);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

			o.vertex = UnityObjectToClipPos(v.vertex);

			o.uv = v.uv;

			return o;
		}

		uniform half shader_properties_label_file;
		uniform half LightmapFlags;
		uniform half shader_is_using_thry_editor;
		uniform half footer_patreon;
		uniform half Instancing;
		uniform half footer_discord;
		uniform half footer_github;
		uniform half m_Main;
		uniform half m_Rim;
		uniform half DSGI;
		uniform half _Cull;
		uniform half shader_master_label;
		uniform half footer_booth;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_BumpMap);
		uniform half2 _Tiling;
		uniform half2 _Offset;
		SamplerState sampler_linear_repeat;
		uniform half _NormalMapSlider;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_AlbedoMapreference_property_Color);
		uniform half4 _Color;
		uniform half4 _EmissionColor;
		uniform half _Bias;
		uniform half _Scale;
		uniform half _Power;
		uniform half _Strength;
		uniform half _Metaliic;
		uniform half _Glossiness;
		uniform half _AntiAliasingVarianceSm;
		uniform half _AntiAliasingThresholdSm;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord263 = i.uv_texcoord * _Tiling + _Offset;
			#ifdef _NormalMap_ON
				half3 staticSwitch258 = UnpackScaleNormal( SAMPLE_TEXTURE2D( _BumpMap, sampler_linear_repeat, uv_TexCoord263 ), _NormalMapSlider );
			#else
				half3 staticSwitch258 = half3(0,0,1);
			#endif
			half3 NormalData277 = staticSwitch258;
			o.Normal = NormalData277;
			o.Albedo = ( SAMPLE_TEXTURE2D( _AlbedoMapreference_property_Color, sampler_linear_repeat, uv_TexCoord263 ) * _Color ).rgb;
			float3 ase_worldPos = i.worldPos;
			half3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 newWorldNormal266 = (WorldNormalVector( i , NormalData277 ));
			half fresnelNdotV211 = dot( newWorldNormal266, ase_worldViewDir );
			half fresnelNode211 = ( _Bias + _Scale * pow( 1.0 - fresnelNdotV211, _Power ) );
			half temp_output_288_0 = ( fresnelNode211 * _Strength );
			half4 clampResult240 = clamp( ( _EmissionColor * temp_output_288_0 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Emission = clampResult240.rgb;
			o.Metallic = _Metaliic;
			half3 temp_output_1_0_g320 = newWorldNormal266;
			half3 temp_output_4_0_g320 = ddx( temp_output_1_0_g320 );
			half dotResult6_g320 = dot( temp_output_4_0_g320 , temp_output_4_0_g320 );
			half3 temp_output_5_0_g320 = ddy( temp_output_1_0_g320 );
			half dotResult8_g320 = dot( temp_output_5_0_g320 , temp_output_5_0_g320 );
			half lerpResult282 = lerp( _Glossiness , 0.0 , sqrt( sqrt( saturate( min( ( ( ( dotResult6_g320 + dotResult8_g320 ) * _AntiAliasingVarianceSm ) * 2.0 ) , _AntiAliasingThresholdSm ) ) ) ));
			half clampResult272 = clamp( ( lerpResult282 + ( temp_output_288_0 * 0.5 * lerpResult282 ) ) , 0.0 , 1.0 );
			o.Smoothness = clampResult272;
			half clampResult223 = clamp( ( _Color.a + temp_output_288_0 ) , 0.0 , 1.0 );
			o.Alpha = clampResult223;
		}

		ENDCG
	}
	CustomEditor "Thry.ShaderEditor"
}
