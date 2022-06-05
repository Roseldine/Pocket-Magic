// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Stardop / Matcap / Matcap with Light Color Move"
{
	Properties
	{
		[Toggle(_USETEXTURE_ON)] _Usetexture("Use texture", Float) = 0
		_Color("Color", Color) = (0.8207547,0.8207547,0.8207547,0)
		_Texture("Texture", 2D) = "white" {}
		_Matcap("Matcap", 2D) = "white" {}
		_LightAngle("Light Angle", Range( 0 , 360)) = 0.5
		_Speed("Speed", Vector) = (0,1,0,0)
		_Tiling("Tiling", Vector) = (0,1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_local __ _USETEXTURE_ON
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
		};

		uniform float4 _Color;
		uniform sampler2D _Texture;
		uniform float2 _Speed;
		uniform float2 _Tiling;
		uniform sampler2D _Matcap;
		uniform float _LightAngle;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord41 = i.uv_texcoord * _Tiling;
			float2 panner42 = ( 1.0 * _Time.y * _Speed + uv_TexCoord41);
			#ifdef _USETEXTURE_ON
				float4 staticSwitch37 = tex2D( _Texture, panner42 );
			#else
				float4 staticSwitch37 = _Color;
			#endif
			float3 ase_worldNormal = i.worldNormal;
			float cos30 = cos( ( _LightAngle / 57.50799 ) );
			float sin30 = sin( ( _LightAngle / 57.50799 ) );
			float2 rotator30 = mul( ( ( mul( UNITY_MATRIX_V, float4( ase_worldNormal , 0.0 ) ).xyz * 0.5 ) + 0.5 ).xy - float2( 0.5,0.5 ) , float2x2( cos30 , -sin30 , sin30 , cos30 )) + float2( 0.5,0.5 );
			o.Emission = ( staticSwitch37 * tex2D( _Matcap, rotator30 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
407;73;873;546;1848.057;1445.678;1.458241;True;False
Node;AmplifyShaderEditor.CommentaryNode;20;-1362.555,-667.6934;Inherit;False;2291.328;1158.503;;11;37;35;31;30;28;27;25;23;22;21;40;Matcap;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;21;-1075.48,-246.4923;Inherit;False;549.8301;190.5432;;3;29;26;24;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ViewMatrixNode;22;-1206.899,-197.2933;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.WorldNormalVector;23;-1269.536,-98.9861;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;32;-1428.059,-1244.218;Inherit;False;896.1829;509.0197;Comment;5;41;42;34;43;44;Texture;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;44;-1378.283,-907.5876;Inherit;False;Property;_Tiling;Tiling;6;0;Create;True;0;0;0;False;0;False;0,1;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1046.451,-194.5782;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1228.634,45.91283;Float;False;Constant;_Float0;Float 0;-1;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-891.0118,-192.7991;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;41;-1416.145,-1192.412;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;-832,0;Float;False;Property;_LightAngle;Light Angle;4;0;Create;True;0;0;0;False;0;False;0.5;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;43;-1401.365,-1051.79;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;0;False;0;False;0,1;0.25,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleDivideOpNode;28;-512,0;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;57.50799;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-673.3848,-197.8353;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PannerNode;42;-1169.361,-1165.357;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;35;-814.6454,-576.0381;Inherit;False;Property;_Color;Color;1;0;Create;True;0;0;0;False;0;False;0.8207547,0.8207547,0.8207547,0;0.8207547,0.8207547,0.8207547,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;34;-870.8592,-1192.528;Inherit;True;Property;_Texture;Texture;2;0;Create;True;0;0;0;False;0;False;-1;None;b3b724363c6a3a84998255ddf09e9c72;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;30;-384,-224;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;31;-128,-256;Inherit;True;Property;_Matcap;Matcap;3;0;Create;True;0;0;0;False;0;False;-1;None;a2adf44078f957d49bc486cc7aee5463;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;37;-321.2481,-492.1607;Inherit;False;Property;_Usetexture;Use texture;0;0;Create;True;0;0;0;False;0;False;1;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;240,-384;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;19;1293.45,19.98717;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Stardop / Matcap / Matcap with Light Color;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;22;0
WireConnection;24;1;23;0
WireConnection;26;0;24;0
WireConnection;26;1;25;0
WireConnection;41;0;44;0
WireConnection;28;0;27;0
WireConnection;29;0;26;0
WireConnection;29;1;25;0
WireConnection;42;0;41;0
WireConnection;42;2;43;0
WireConnection;34;1;42;0
WireConnection;30;0;29;0
WireConnection;30;2;28;0
WireConnection;31;1;30;0
WireConnection;37;1;35;0
WireConnection;37;0;34;0
WireConnection;40;0;37;0
WireConnection;40;1;31;0
WireConnection;19;2;40;0
ASEEND*/
//CHKSM=651F0D3D50DDF5E2B3E4F3858AA144D785D5DBEE