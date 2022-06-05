// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Stardrop / World Bending Matcap"
{
	Properties
	{
		_ColorTint("Color Tint", Color) = (0.8207547,0.8207547,0.8207547,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
		};

		uniform float4 _ColorTint;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 objToWorld60 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 temp_cast_2 = (( 0.0 * pow( ( mul( float4( ase_worldPos , 0.0 ), transpose( float4x4( 0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1 ) ) ).xyz - _WorldSpaceCameraPos ).z , 2.0 ) )).xxxx;
			v.vertex.xyz += ( objToWorld60 + mul( temp_cast_2, UNITY_MATRIX_IT_MV ).x );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 temp_output_31_0 = _ColorTint;
			o.Albedo = temp_output_31_0.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
364;73;915;531;2402.226;664.7374;3.825174;False;False
Node;AmplifyShaderEditor.WorldPosInputsNode;37;-2176,0;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransposeOpNode;63;-2155.13,200.0751;Inherit;False;1;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4x4;0
Node;AmplifyShaderEditor.WorldSpaceCameraPos;51;-1984,384;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-1904,176;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;52;-1696.917,222.1742;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;53;-1539.56,353.3576;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;41;-1632.924,568.4945;Inherit;False;Constant;_Exp;Exp;5;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;40;-1318.053,437.6024;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;27;-1659.845,-1413.378;Inherit;False;1647.881;798.4109;Comment;12;34;33;32;31;30;29;28;25;23;21;20;19;Matcap;1,1,1,1;0;0
Node;AmplifyShaderEditor.InverseTranspMVMatrixNode;62;-896,512;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-789.4663,319.6656;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-477.3652,381.0565;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;60;-560,128;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;19;-1467.845,-1077.378;Inherit;False;549.8301;190.5432;;3;26;24;22;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;28;-891.8455,-837.3778;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;57.50799;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1520,-96;Inherit;False;Property;_BendX;Bend X;5;0;Create;True;0;0;0;False;0;False;0;0.012;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-1520,32;Inherit;False;Property;_BendY;Bend Y;6;0;Create;True;0;0;0;False;0;False;0;0.015;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;31;-1195.845,-1413.378;Inherit;False;Property;_ColorTint;Color Tint;1;0;Create;True;0;0;0;False;0;False;0.8207547,0.8207547,0.8207547,0;1,0.4784313,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1435.845,-1029.378;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1067.845,-1029.378;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1275.845,-1029.378;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;32;-859.8455,-1253.378;Inherit;False;Property;_Usetexture;Use texture;0;0;Create;True;0;0;0;False;0;False;1;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-139.8453,-1221.378;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RotatorNode;29;-763.8455,-1061.378;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ViewMatrixNode;20;-1595.845,-1029.378;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.WorldNormalVector;21;-1659.845,-933.3777;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;25;-1211.845,-837.3778;Float;False;Property;_LightAngle;Light Angle;4;0;Create;True;0;0;0;False;0;False;0.5;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;33;-507.8454,-1093.378;Inherit;True;Property;_Matcap;Matcap;3;0;Create;True;0;0;0;False;0;False;-1;768511ed8be0ffe42a891bd9d995e75b;768511ed8be0ffe42a891bd9d995e75b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;64;-987.9944,191.8661;Inherit;False;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-256,128;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1611.845,-789.3777;Float;False;Constant;_Float0;Float 0;-1;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;30;-1627.845,-1333.378;Inherit;True;Property;_Texture;Texture;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;65;-1232.448,196.4073;Inherit;False;Constant;_Vector0;Vector 0;7;0;Create;True;0;0;0;False;0;False;100,100;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;385.5942,-245.6962;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Stardrop / World Bending Matcap;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;50;0;37;0
WireConnection;50;1;63;0
WireConnection;52;0;50;0
WireConnection;52;1;51;0
WireConnection;53;0;52;0
WireConnection;40;0;53;2
WireConnection;40;1;41;0
WireConnection;57;1;40;0
WireConnection;59;0;57;0
WireConnection;59;1;62;0
WireConnection;28;0;25;0
WireConnection;22;0;20;0
WireConnection;22;1;21;0
WireConnection;26;0;24;0
WireConnection;26;1;23;0
WireConnection;24;0;22;0
WireConnection;24;1;23;0
WireConnection;32;1;31;0
WireConnection;32;0;30;0
WireConnection;34;0;32;0
WireConnection;34;1;33;0
WireConnection;29;0;26;0
WireConnection;29;2;28;0
WireConnection;33;1;29;0
WireConnection;64;0;44;0
WireConnection;64;1;65;0
WireConnection;14;0;60;0
WireConnection;14;1;59;0
WireConnection;0;0;31;0
WireConnection;0;11;14;0
ASEEND*/
//CHKSM=133BAFAE8877410DE0E0EF2AC5B45387CF603EA4