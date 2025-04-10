// Made with Amplify Shader Editor v1.9.2.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MathGraph"
{
    Properties
    {
        _Scale("Scale", Float) = 1

    }

    SubShader
    {
		LOD 0

        
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
        Pass
        {
			Name "Custom RT Init"
            CGPROGRAM
            
            #include "UnityCustomRenderTexture.cginc"

            #pragma vertex ASEInitCustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0
			#include "UnityShaderVariables.cginc"


			struct ase_appdata_init_customrendertexture
			{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				
			};

			// User facing vertex to fragment structure for initialization materials
			struct ase_v2f_init_customrendertexture
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float3 direction : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
			};

			uniform float _Scale;
			float2 UnStereo( float2 UV )
			{
				#if UNITY_SINGLE_PASS_STEREO
				float4 scaleOffset = unity_StereoScaleOffset[ unity_StereoEyeIndex ];
				UV.xy = (UV.xy - scaleOffset.zw) / scaleOffset.xy;
				#endif
				return UV;
			}
			


			ase_v2f_init_customrendertexture ASEInitCustomRenderTextureVertexShader (ase_appdata_init_customrendertexture v )
			{
				ase_v2f_init_customrendertexture o;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord2 = screenPos;
				
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = float3(v.texcoord.xy, CustomRenderTexture3DTexcoordW);
				o.direction = CustomRenderTextureComputeCubeDirection(v.texcoord.xy);
				return o;
			}

            float4 frag(ase_v2f_init_customrendertexture IN ) : COLOR
            {
                float4 finalColor;
				float4 screenPos = IN.ase_texcoord2;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 UV22_g17 = ase_screenPosNorm.xy;
				float2 localUnStereo22_g17 = UnStereo( UV22_g17 );
				float2 break5_g16 = ( localUnStereo22_g17 + -0.5 );
				float2 appendResult8_g16 = (float2(( ( _ScreenParams.x / _ScreenParams.y ) * break5_g16.x ) , break5_g16.y));
				float2 break45 = ( appendResult8_g16 * _Scale );
				float temp_output_2_0_g18 = break45.x;
				float temp_output_2_0 = ( temp_output_2_0_g18 * temp_output_2_0_g18 );
				float temp_output_2_0_g19 = break45.y;
				float temp_output_3_0 = ( temp_output_2_0_g19 * temp_output_2_0_g19 );
				float temp_output_6_0 = ( tan( ( temp_output_2_0 + temp_output_3_0 ) ) - 0.1 );
				float dotResult18 = dot( temp_output_2_0 , temp_output_3_0 );
				float temp_output_21_0 = abs( dotResult18 );
				float smoothstepResult10 = smoothstep( temp_output_6_0 , temp_output_21_0 , abs( temp_output_6_0 ));
				float4 temp_cast_1 = (smoothstepResult10).xxxx;
				
                finalColor = temp_cast_1;
				return finalColor;
            }
            ENDCG
        }
    }
	
	CustomEditor "ASEMaterialInspector"
	Fallback Off
}
/*ASEBEGIN
Version=19202
Node;AmplifyShaderEditor.FunctionNode;47;-1664,-224;Inherit;True;ScreenSpaceUV;0;;16;8fb4e1e6f2d6c584fb8a7b086be180c7;0;0;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;45;-1408,-224;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FunctionNode;2;-1280,-224;Inherit;False;Square;-1;;18;fea980a1f68019543b2cd91d506986e8;0;1;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;3;-1280,-144;Inherit;False;Square;-1;;19;fea980a1f68019543b2cd91d506986e8;0;1;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-1024,-224;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;18;-1024,0;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TanOpNode;5;-768,-224;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-544,-224;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;8;-272,-400;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;10;-64,-224;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;21;-512,0;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;22;-256,176;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;32;256,-224;Float;False;True;-1;2;ASEMaterialInspector;0;1;MathGraph;6ce779933eb99f049b78d6163735e06f;True;Custom RT Init;0;0;Custom RT Init;1;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;45;0;47;0
WireConnection;2;2;45;0
WireConnection;3;2;45;1
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;18;0;2;0
WireConnection;18;1;3;0
WireConnection;5;0;4;0
WireConnection;6;0;5;0
WireConnection;8;0;6;0
WireConnection;10;0;8;0
WireConnection;10;1;6;0
WireConnection;10;2;21;0
WireConnection;21;0;18;0
WireConnection;22;0;21;0
WireConnection;32;0;10;0
ASEEND*/
//CHKSM=4EE35144A3EFB762CE38C4C923A5B9F207592015