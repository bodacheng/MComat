// Upgrade NOTE: upgraded instancing buffer 'MalbersColor4x2' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Malbers/Color4x2"
{
	Properties
	{
		_Color1("Color 1", Color) = (1,0.1544118,0.1544118,0.397)
		_Color2("Color 2", Color) = (1,0.1544118,0.8017241,0.334)
		_Color3("Color 3", Color) = (0.2535501,0.1544118,1,0.228)
		_Color4("Color 4", Color) = (0.1544118,0.5451319,1,0.472)
		_Color5("Color 5", Color) = (0.9533468,1,0.1544118,0.353)
		_Color6("Color 6", Color) = (0.8483773,1,0.1544118,0.341)
		_Color7("Color 7", Color) = (0.1544118,0.6151115,1,0.316)
		_Color8("Color 8", Color) = (0.4849697,0.5008695,0.5073529,0.484)
		_Smoothness("Smoothness", Range( 0 , 1)) = 1
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_GradientColor("Gradient Color", Color) = (0,0,0,0)
		_GradientIntensity("Gradient Intensity", Range( 0 , 1)) = 0.75
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _GradientColor;
		uniform float _GradientIntensity;
		uniform float _Metallic;
		uniform float _Smoothness;

		UNITY_INSTANCING_BUFFER_START(MalbersColor4x2)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color1)
#define _Color1_arr MalbersColor4x2
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color2)
#define _Color2_arr MalbersColor4x2
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color3)
#define _Color3_arr MalbersColor4x2
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color4)
#define _Color4_arr MalbersColor4x2
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color5)
#define _Color5_arr MalbersColor4x2
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color6)
#define _Color6_arr MalbersColor4x2
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color7)
#define _Color7_arr MalbersColor4x2
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color8)
#define _Color8_arr MalbersColor4x2
		UNITY_INSTANCING_BUFFER_END(MalbersColor4x2)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _Color1_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color1_arr, _Color1);
			float temp_output_3_0_g176 = 1.0;
			float temp_output_7_0_g176 = 4.0;
			float temp_output_9_0_g176 = 2.0;
			float temp_output_8_0_g176 = 2.0;
			float4 _Color2_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color2_arr, _Color2);
			float temp_output_3_0_g180 = 2.0;
			float temp_output_7_0_g180 = 4.0;
			float temp_output_9_0_g180 = 2.0;
			float temp_output_8_0_g180 = 2.0;
			float4 _Color3_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color3_arr, _Color3);
			float temp_output_3_0_g179 = 3.0;
			float temp_output_7_0_g179 = 4.0;
			float temp_output_9_0_g179 = 2.0;
			float temp_output_8_0_g179 = 2.0;
			float4 _Color4_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color4_arr, _Color4);
			float temp_output_3_0_g175 = 4.0;
			float temp_output_7_0_g175 = 4.0;
			float temp_output_9_0_g175 = 2.0;
			float temp_output_8_0_g175 = 2.0;
			float4 _Color5_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color5_arr, _Color5);
			float temp_output_3_0_g178 = 1.0;
			float temp_output_7_0_g178 = 4.0;
			float temp_output_9_0_g178 = 1.0;
			float temp_output_8_0_g178 = 2.0;
			float4 _Color6_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color6_arr, _Color6);
			float temp_output_3_0_g182 = 2.0;
			float temp_output_7_0_g182 = 4.0;
			float temp_output_9_0_g182 = 1.0;
			float temp_output_8_0_g182 = 2.0;
			float4 _Color7_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color7_arr, _Color7);
			float temp_output_3_0_g181 = 3.0;
			float temp_output_7_0_g181 = 4.0;
			float temp_output_9_0_g181 = 1.0;
			float temp_output_8_0_g181 = 2.0;
			float4 _Color8_Instance = UNITY_ACCESS_INSTANCED_PROP(_Color8_arr, _Color8);
			float temp_output_3_0_g177 = 4.0;
			float temp_output_7_0_g177 = 4.0;
			float temp_output_9_0_g177 = 1.0;
			float temp_output_8_0_g177 = 2.0;
			float4 temp_output_155_0 = ( ( ( _Color1_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g176 - 1.0 ) / temp_output_7_0_g176 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g176 / temp_output_7_0_g176 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g176 - 1.0 ) / temp_output_8_0_g176 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g176 / temp_output_8_0_g176 ) ) * 1.0 ) ) ) ) + ( _Color2_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g180 - 1.0 ) / temp_output_7_0_g180 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g180 / temp_output_7_0_g180 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g180 - 1.0 ) / temp_output_8_0_g180 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g180 / temp_output_8_0_g180 ) ) * 1.0 ) ) ) ) + ( _Color3_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g179 - 1.0 ) / temp_output_7_0_g179 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g179 / temp_output_7_0_g179 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g179 - 1.0 ) / temp_output_8_0_g179 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g179 / temp_output_8_0_g179 ) ) * 1.0 ) ) ) ) + ( _Color4_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g175 - 1.0 ) / temp_output_7_0_g175 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g175 / temp_output_7_0_g175 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g175 - 1.0 ) / temp_output_8_0_g175 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g175 / temp_output_8_0_g175 ) ) * 1.0 ) ) ) ) ) + ( ( _Color5_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g178 - 1.0 ) / temp_output_7_0_g178 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g178 / temp_output_7_0_g178 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g178 - 1.0 ) / temp_output_8_0_g178 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g178 / temp_output_8_0_g178 ) ) * 1.0 ) ) ) ) + ( _Color6_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g182 - 1.0 ) / temp_output_7_0_g182 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g182 / temp_output_7_0_g182 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g182 - 1.0 ) / temp_output_8_0_g182 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g182 / temp_output_8_0_g182 ) ) * 1.0 ) ) ) ) + ( _Color7_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g181 - 1.0 ) / temp_output_7_0_g181 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g181 / temp_output_7_0_g181 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g181 - 1.0 ) / temp_output_8_0_g181 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g181 / temp_output_8_0_g181 ) ) * 1.0 ) ) ) ) + ( _Color8_Instance * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g177 - 1.0 ) / temp_output_7_0_g177 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g177 / temp_output_7_0_g177 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g177 - 1.0 ) / temp_output_8_0_g177 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g177 / temp_output_8_0_g177 ) ) * 1.0 ) ) ) ) ) );
			float4 ColorShart214 = temp_output_155_0;
			float4 temp_cast_0 = ((i.uv_texcoord.y*2.0 + -1.0)).xxxx;
			float temp_output_3_0_g173 = 1.0;
			float temp_output_7_0_g173 = 1.0;
			float temp_output_9_0_g173 = 2.0;
			float temp_output_8_0_g173 = 2.0;
			float4 temp_cast_1 = ((i.uv_texcoord.y*2.0 + 0.0)).xxxx;
			float temp_output_3_0_g174 = 1.0;
			float temp_output_7_0_g174 = 1.0;
			float temp_output_9_0_g174 = 1.0;
			float temp_output_8_0_g174 = 2.0;
			float4 clampResult224 = clamp( ( ( ( ( temp_cast_0 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g173 - 1.0 ) / temp_output_7_0_g173 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g173 / temp_output_7_0_g173 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g173 - 1.0 ) / temp_output_8_0_g173 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g173 / temp_output_8_0_g173 ) ) * 1.0 ) ) ) ) + ( temp_cast_1 * ( ( ( 1.0 - step( i.uv_texcoord.x , ( ( temp_output_3_0_g174 - 1.0 ) / temp_output_7_0_g174 ) ) ) * ( step( i.uv_texcoord.x , ( temp_output_3_0_g174 / temp_output_7_0_g174 ) ) * 1.0 ) ) * ( ( 1.0 - step( i.uv_texcoord.y , ( ( temp_output_9_0_g174 - 1.0 ) / temp_output_8_0_g174 ) ) ) * ( step( i.uv_texcoord.y , ( temp_output_9_0_g174 / temp_output_8_0_g174 ) ) * 1.0 ) ) ) ) ) + _GradientColor ) + ( 1.0 - _GradientIntensity ) ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Albedo = ( ColorShart214 * clampResult224 ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = ( (temp_output_155_0).a * _Smoothness );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
250;158;1189;592;203.5708;-101.6965;3.101762;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;181;760.072,954.4623;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;152;-194.2135,166.9271;Float;False;InstancedProperty;_Color3;Color 3;2;0;Create;True;0;0;False;0;0.2535501,0.1544118,1,0.228;0.2535501,0.1544118,1,0.228;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;196;1059.348,878.5837;Float;False;3;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;150;-207.7412,-66.93771;Float;False;InstancedProperty;_Color2;Color 2;1;0;Create;True;0;0;False;0;1,0.1544118,0.8017241,0.334;1,0.1544118,0.8017241,0.334;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;23;-199.8005,-326.2955;Float;False;InstancedProperty;_Color1;Color 1;0;0;Create;True;0;0;False;0;1,0.1544118,0.1544118,0.397;1,0.1544118,0.1544118,0.397;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;197;1072.785,1121.41;Float;False;3;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;154;-195.6228,411.2479;Float;False;InstancedProperty;_Color4;Color 4;3;0;Create;True;0;0;False;0;0.1544118,0.5451319,1,0.472;0.1544118,0.5451319,1,0.472;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;159;-187.9672,688.0273;Float;False;InstancedProperty;_Color5;Color 5;4;0;Create;True;0;0;False;0;0.9533468,1,0.1544118,0.353;0.9533468,1,0.1544118,0.353;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;158;-183.7895,1424.406;Float;False;InstancedProperty;_Color8;Color 8;7;0;Create;True;0;0;False;0;0.4849697,0.5008695,0.5073529,0.484;0.4849697,0.5008695,0.5073529,0.484;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;157;-182.3802,1181.25;Float;False;InstancedProperty;_Color7;Color 7;6;0;Create;True;0;0;False;0;0.1544118,0.6151115,1,0.316;0.1544118,0.6151115,1,0.316;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;156;-195.9079,947.3851;Float;False;InstancedProperty;_Color6;Color 6;5;0;Create;True;0;0;False;0;0.8483773,1,0.1544118,0.341;0.8483773,1,0.1544118,0.341;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;160;119.8096,947.4603;Float;True;ColorShartSlot;-1;;182;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;2;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;149;107.9764,-66.86263;Float;True;ColorShartSlot;-1;;180;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;2;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;151;121.5042,167.0022;Float;True;ColorShartSlot;-1;;179;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;3;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;163;127.7504,688.1025;Float;True;ColorShartSlot;-1;;178;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;161;133.3375,1181.325;Float;True;ColorShartSlot;-1;;181;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;3;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;145;115.9171,-326.2204;Float;True;ColorShartSlot;-1;;176;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;153;122.0185,410.1585;Float;True;ColorShartSlot;-1;;175;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;4;False;9;FLOAT;2;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;195;1328.392,1096.723;Float;True;ColorShartSlot;-1;;174;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;1;False;7;FLOAT;1;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;193;1327.674,876.1544;Float;True;ColorShartSlot;-1;;173;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;1;False;9;FLOAT;2;False;7;FLOAT;1;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;162;133.8517,1424.481;Float;True;ColorShartSlot;-1;;177;231fe18505db4a84b9c478d379c9247d;0;5;38;COLOR;0.7843138,0.3137255,0,0;False;3;FLOAT;4;False;9;FLOAT;1;False;7;FLOAT;4;False;8;FLOAT;2;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;164;1130.732,57.40811;Float;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;202;1672.228,1423.309;Float;False;Property;_GradientIntensity;Gradient Intensity;11;0;Create;True;0;0;False;0;0.75;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;201;1697.634,987.8407;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;205;1722.708,1223.28;Float;False;Property;_GradientColor;Gradient Color;10;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;146;1124.026,-170.6852;Float;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;223;2038.081,1279.951;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;225;1996.289,906.7085;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;155;1392.04,-26.9957;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;214;1969.677,-86.93118;Float;False;ColorShart;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;216;2173.958,1015.755;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;215;2026.611,689.4277;Float;False;214;ColorShart;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;179;1786.961,259.5521;Float;False;False;False;False;True;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;166;1287.072,404.6109;Float;False;Property;_Smoothness;Smoothness;8;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;224;2331.655,867.6575;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;178;2026.21,404.9984;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;165;2015.057,193.266;Float;False;Property;_Metallic;Metallic;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;203;2475.923,555.3262;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2856.367,138.5725;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Malbers/Color4x2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;196;0;181;2
WireConnection;197;0;181;2
WireConnection;160;38;156;0
WireConnection;149;38;150;0
WireConnection;151;38;152;0
WireConnection;163;38;159;0
WireConnection;161;38;157;0
WireConnection;145;38;23;0
WireConnection;153;38;154;0
WireConnection;195;38;197;0
WireConnection;193;38;196;0
WireConnection;162;38;158;0
WireConnection;164;0;163;0
WireConnection;164;1;160;0
WireConnection;164;2;161;0
WireConnection;164;3;162;0
WireConnection;201;0;193;0
WireConnection;201;1;195;0
WireConnection;146;0;145;0
WireConnection;146;1;149;0
WireConnection;146;2;151;0
WireConnection;146;3;153;0
WireConnection;223;0;202;0
WireConnection;225;0;201;0
WireConnection;225;1;205;0
WireConnection;155;0;146;0
WireConnection;155;1;164;0
WireConnection;214;0;155;0
WireConnection;216;0;225;0
WireConnection;216;1;223;0
WireConnection;179;0;155;0
WireConnection;224;0;216;0
WireConnection;178;0;179;0
WireConnection;178;1;166;0
WireConnection;203;0;215;0
WireConnection;203;1;224;0
WireConnection;0;0;203;0
WireConnection;0;3;165;0
WireConnection;0;4;178;0
ASEEND*/
//CHKSM=8C5CF6DB3C4DB3CFAC6F1B3C7F71F4561FA5B9F8