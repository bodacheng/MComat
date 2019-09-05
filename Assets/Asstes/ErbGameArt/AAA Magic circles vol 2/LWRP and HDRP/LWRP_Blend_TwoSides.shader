Shader "ErbGameArt/LWRP/Particles/Blend_TwoSides"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_MainTex("Main Tex", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Noise("Noise", 2D) = "white" {}
		_SpeedMainTexUVNoiseZW("Speed MainTex U/V + Noise Z/W", Vector) = (0,0,0,0)
		_FrontFacesColor("Front Faces Color", Color) = (0,0.2313726,1,1)
		_BackFacesColor("Back Faces Color", Color) = (0.1098039,0.4235294,1,1)
		_Emission("Emission", Float) = 2
		[Toggle(_USEFRESNEL_ON)] _UseFresnel("Use Fresnel?", Float) = 1
		_FresnelColor("Fresnel Color", Color) = (1,1,1,1)
		_Fresnel("Fresnel", Float) = 1
		_FresnelEmission("Fresnel Emission", Float) = 1
		[Toggle(_USECUSTOMDATA_ON)] _UseCustomData("Use Custom Data?", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="LightweightPipeline" "PreviewType"="Plane"}
		Cull Off
		HLSLINCLUDE
		#pragma target 3.0
		ENDHLSL
		
		Pass
		{
			Tags { "LightMode"="LightweightForward" }
			Name "Base"
			
			Blend SrcAlpha OneMinusSrcAlpha
			//ZWrite On
			//ZTest LEqual
			//Offset 0,0
			ColorMask RGBA
			
		    HLSLPROGRAM
			#pragma multi_compile lines
		    #pragma prefer_hlslcc gles
		    //#pragma exclude_renderers d3d11_9x	
		    #pragma vertex vert
		    #pragma fragment frag
			#define _AlphaClip 1
			#pragma shader_feature _USEFRESNEL_ON
			#pragma shader_feature _USECUSTOMDATA_ON
		    #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/functions.hlsl"
		    //#include "LWRP/ShaderLibrary/Core.hlsl"
		    //#include "LWRP/ShaderLibrary/Lighting.hlsl"
		    //#include "CoreRP/ShaderLibrary/Color.hlsl"
		    //#include "ShaderGraphLibrary/Functions.hlsl"		
			uniform float4 _FrontFacesColor;
			uniform float _Fresnel;
			uniform float _FresnelEmission;
			uniform float4 _FresnelColor;
			uniform float4 _BackFacesColor;
			uniform float _Emission;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float4 _SpeedMainTexUVNoiseZW;
			uniform sampler2D _Mask;
			uniform float4 _Mask_ST;
			uniform sampler2D _Noise;
			uniform float4 _Noise_ST;
			uniform float _Cutoff = 0.5;
					
			struct GraphVertexInput
			{
				float4 vertex : POSITION;
				float4 ase_normal : NORMAL;
				float4 ase_color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
	
		    struct GraphVertexOutput
		    {
		        float4 position : POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				float4 ase_texcoord2 : TEXCOORD2;
		        UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
		    };
		
		    GraphVertexOutput vert (GraphVertexInput v )
			{
		        GraphVertexOutput o = (GraphVertexOutput)0;
		        UNITY_SETUP_INSTANCE_ID(v);
		        UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord.xyz = ase_worldPos;
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal.xyz);
				o.ase_texcoord1.xyz = ase_worldNormal;
				
				o.ase_color = v.ase_color;
				o.ase_texcoord2 = v.ase_texcoord;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.w = 0;
				v.vertex.xyz +=  float3( 0, 0, 0 ) ;
				v.ase_normal =  v.ase_normal ;
		        o.position = TransformObjectToHClip(v.vertex.xyz);
		        return o;
			}
		
		    half4 frag( GraphVertexOutput IN  ) : SV_Target
		    {
		        UNITY_SETUP_INSTANCE_ID(IN);
				float3 ase_worldPos = IN.ase_texcoord.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord1.xyz;
				float fresnelNdotV95 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode95 = ( 0.0 + 1.0 * pow( abs(1.0 - fresnelNdotV95), _Fresnel ) );
				#ifdef _USEFRESNEL_ON
				float4 staticSwitch101 = ( ( _FrontFacesColor * ( 1.0 - fresnelNode95 ) ) + ( _FresnelEmission * _FresnelColor * fresnelNode95 ) );
				#else
				float4 staticSwitch101 = _FrontFacesColor;
				#endif
				float dotResult87 = dot( ase_worldNormal , ase_worldViewDir );
				float4 lerpResult91 = lerp( staticSwitch101 , _BackFacesColor , (1.0 + (sign( dotResult87 ) - -1.0) * (0.0 - 1.0) / (1.0 - -1.0)));
				float2 uv_MainTex = IN.ase_texcoord2.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 appendResult21 = (float2(_SpeedMainTexUVNoiseZW.x , _SpeedMainTexUVNoiseZW.y));
				
				float2 uv_Mask = IN.ase_texcoord2.xy * _Mask_ST.xy + _Mask_ST.zw;
				float4 uv_Noise = IN.ase_texcoord2;
				uv_Noise.xy = IN.ase_texcoord2.xy * _Noise_ST.xy + _Noise_ST.zw;
				float2 appendResult22 = (float2(_SpeedMainTexUVNoiseZW.z , _SpeedMainTexUVNoiseZW.w));
				#ifdef _USECUSTOMDATA_ON
				float staticSwitch103 = uv_Noise.z;
				#else
				float staticSwitch103 = 1.0;
				#endif
				
		        float3 Color = ( lerpResult91 * _Emission * IN.ase_color * tex2D( _MainTex, ( uv_MainTex + ( appendResult21 * _Time.y ) ) ) * IN.ase_color.a ).rgb;
		        float Alpha = 1;
		        //float AlphaClipThreshold = ( tex2D( _Mask, uv_Mask ) * tex2D( _Noise, ( (uv_Noise).xy + ( _Time.y * appendResult22 ) ) ) * staticSwitch103 ).r;
				clip( Alpha *( tex2D( _Mask, uv_Mask ) * tex2D( _Noise, ( (uv_Noise).xy + ( _Time.y * appendResult22 ) ) ) * staticSwitch103 ).r - _Cutoff );
		/*#if _AlphaClip
		        clip(Alpha - (AlphaClipThreshold - _Cutoff));
		#endif*/
		    	return half4(Color, Alpha);
		    }
		    ENDHLSL
		}		
		
		Pass
		{	
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }
			ZWrite On
			ColorMask 0		
			HLSLPROGRAM
			#pragma multi_compile lines
			#pragma prefer_hlslcc gles
			#pragma multi_compile_instancing
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
			//#include "LWRP/ShaderLibrary/Core.hlsl"		
			#define _AlphaClip 1
			#pragma shader_feature _USECUSTOMDATA_ON
			uniform sampler2D _Mask;
			uniform float4 _Mask_ST;
			uniform sampler2D _Noise;
			uniform float4 _Noise_ST;
			uniform float4 _SpeedMainTexUVNoiseZW;
			uniform float _Cutoff = 0.5;
			
			struct GraphVertexInput
			{
				float4 vertex : POSITION;
				float4 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct GraphVertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			GraphVertexOutput vert (GraphVertexInput v)
			{
				GraphVertexOutput o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.ase_texcoord = v.ase_texcoord;
				v.vertex.xyz +=  float3(0,0,0) ;
				v.ase_normal =  v.ase_normal ;
				o.clipPos = TransformObjectToHClip(v.vertex.xyz);
				return o;
			}

			half4 frag (GraphVertexOutput IN ) : SV_Target
		    {
		    	UNITY_SETUP_INSTANCE_ID(IN);

				float2 uv_Mask = IN.ase_texcoord.xy * _Mask_ST.xy + _Mask_ST.zw;
				float4 uv_Noise = IN.ase_texcoord;
				uv_Noise.xy = IN.ase_texcoord.xy * _Noise_ST.xy + _Noise_ST.zw;
				float2 appendResult22 = (float2(_SpeedMainTexUVNoiseZW.z , _SpeedMainTexUVNoiseZW.w));
				#ifdef _USECUSTOMDATA_ON
				float staticSwitch103 = uv_Noise.z;
				#else
				float staticSwitch103 = 1.0;
				#endif				
				float Alpha = 1;
				//float AlphaClipThreshold = ( tex2D( _Mask, uv_Mask ) * tex2D( _Noise, ( (uv_Noise).xy + ( _Time.y * appendResult22 ) ) ) * staticSwitch103 ).r;
				clip( Alpha *( tex2D( _Mask, uv_Mask ) * tex2D( _Noise, ( (uv_Noise).xy + ( _Time.y * appendResult22 ) ) ) * staticSwitch103 ).r - _Cutoff );
				/*#if _AlphaClip
					clip(Alpha - (AlphaClipThreshold - _Cutoff));
				#endif*/
				return Alpha;
				return 0;
		    }
			ENDHLSL
		}
	}	
	FallBack "Hidden/InternalErrorShader"	
}