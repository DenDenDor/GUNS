// Made with Amplify Shader Editor v1.9.2.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CircleSquare"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

        _Texture("Texture", 2D) = "white" {}
        _Dialation("Dialation", Float) = 0.04
        _AberrationTransparency("AberrationTransparency", Range( 0 , 1)) = 1

    }

    SubShader
    {
		LOD 0

        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

        Stencil
        {
        	Ref [_Stencil]
        	ReadMask [_StencilReadMask]
        	WriteMask [_StencilWriteMask]
        	Comp [_StencilComp]
        	Pass [_StencilOp]
        }


        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        
        Pass
        {
            Name "Default"
        CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            #define ASE_NEEDS_FRAG_COLOR


            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4  mask : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
                
            };

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            uniform sampler2D _Texture;
            uniform float _Dialation;
            uniform float _AberrationTransparency;

            
            v2f vert(appdata_t v )
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                

                v.vertex.xyz +=  float3( 0, 0, 0 ) ;

                float4 vPosition = UnityObjectToClipPos(v.vertex);
                OUT.worldPosition = v.vertex;
                OUT.vertex = vPosition;

                float2 pixelSize = vPosition.w;
                pixelSize /= float2(1, 1) * abs(mul((float2x2)UNITY_MATRIX_P, _ScreenParams.xy));

                float4 clampedRect = clamp(_ClipRect, -2e10, 2e10);
                float2 maskUV = (v.vertex.xy - clampedRect.xy) / (clampedRect.zw - clampedRect.xy);
                OUT.texcoord = v.texcoord;
                OUT.mask = float4(v.vertex.xy * 2 - clampedRect.xy - clampedRect.zw, 0.25 / (0.25 * half2(_UIMaskSoftnessX, _UIMaskSoftnessY) + abs(pixelSize.xy)));

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN ) : SV_Target
            {
                //Round up the alpha color coming from the interpolator (to 1.0/256.0 steps)
                //The incoming alpha could have numerical instability, which makes it very sensible to
                //HDR color transparency blend, when it blends with the world's texture.
                const half alphaPrecision = half(0xff);
                const half invAlphaPrecision = half(1.0/alphaPrecision);
                IN.color.a = round(IN.color.a * alphaPrecision)*invAlphaPrecision;

                float2 _Vector1 = float2(1,1);
                float2 _Vector2 = float2(0,0);
                float2 normalizeResult91 = normalize( float2( 0,1 ) );
                float2 texCoord78 = IN.texcoord.xy * _Vector1 + ( _Vector2 + ( normalizeResult91 * _Dialation ) );
                float smoothstepResult45 = smoothstep( 0.495 , 0.505 , tex2D( _Texture, texCoord78 ).a);
                float4 break44 = ( IN.color * smoothstepResult45 );
                float2 normalizeResult95 = normalize( float2( -0.87,-0.5 ) );
                float2 texCoord81 = IN.texcoord.xy * _Vector1 + ( _Vector2 + ( normalizeResult95 * _Dialation ) );
                float smoothstepResult113 = smoothstep( 0.495 , 0.505 , tex2D( _Texture, texCoord81 ).a);
                float4 break112 = ( IN.color * smoothstepResult113 );
                float2 normalizeResult97 = normalize( float2( 0.87,-0.5 ) );
                float2 texCoord83 = IN.texcoord.xy * _Vector1 + ( _Vector2 + ( normalizeResult97 * _Dialation ) );
                float smoothstepResult116 = smoothstep( 0.495 , 0.505 , tex2D( _Texture, texCoord83 ).a);
                float4 break115 = ( IN.color * smoothstepResult116 );
                float4 appendResult122 = (float4(break44.r , break112.g , break115.b , 0.0));
                float2 texCoord29 = IN.texcoord.xy * _Vector1 + _Vector2;
                float smoothstepResult30 = smoothstep( 0.495 , 0.505 , tex2D( _Texture, texCoord29 ).a);
                float4 temp_output_35_0 = ( IN.color * smoothstepResult30 );
                float4 break133 = ( appendResult122 + temp_output_35_0 );
                float4 appendResult132 = (float4(break133.x , break133.y , break133.z , ( temp_output_35_0.a + ( ( break44.a + break112.a + break115.a ) * _AberrationTransparency ) )));
                

                half4 color = appendResult132;

                #ifdef UNITY_UI_CLIP_RECT
                half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                color.a *= m.x * m.y;
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                color.rgb *= color.a;

                return color;
            }
        ENDCG
        }
    }
    CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19202
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;1121.001,278.9848;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;30;782.5829,301.0995;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.495;False;2;FLOAT;0.505;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;127.5912,277.0302;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0.25,0.25;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;77;403.6749,1452.916;Inherit;True;Property;_TextureSample4;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;2aa87fa45bd4b094c8a7d4d24112c165;2aa87fa45bd4b094c8a7d4d24112c165;True;0;False;white;Auto;False;Instance;31;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;1170.848,914.894;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;44;1398.971,915.6564;Inherit;True;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SmoothstepOpNode;45;926.9137,904.4285;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.495;False;2;FLOAT;0.505;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;111;1167.148,1173.739;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;112;1395.271,1174.501;Inherit;True;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SmoothstepOpNode;113;923.2139,1163.274;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.495;False;2;FLOAT;0.505;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;1198.597,1444.467;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;115;1426.719,1445.23;Inherit;True;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SmoothstepOpNode;116;954.662,1434.002;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.495;False;2;FLOAT;0.505;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;73;397.31,1068.903;Inherit;True;Property;_TextureSample2;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;2aa87fa45bd4b094c8a7d4d24112c165;2aa87fa45bd4b094c8a7d4d24112c165;True;0;False;white;Auto;False;Instance;31;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;76;398.3707,1258.788;Inherit;True;Property;_TextureSample3;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;2aa87fa45bd4b094c8a7d4d24112c165;2aa87fa45bd4b094c8a7d4d24112c165;True;0;False;white;Auto;False;Instance;31;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;92;-984.8298,1097.331;Inherit;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;96;-1045.389,1303.569;Inherit;False;Constant;_Vector3;Vector 0;4;0;Create;True;0;0;0;False;0;False;-0.87,-0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;98;-1038.115,1498.882;Inherit;False;Constant;_Vector4;Vector 0;4;0;Create;True;0;0;0;False;0;False;0.87,-0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.NormalizeNode;91;-801.1943,1095.561;Inherit;True;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalizeNode;95;-803.354,1301.8;Inherit;True;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalizeNode;97;-805.6802,1499.512;Inherit;True;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;83;-40.25109,1573.207;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0.25,0.25;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;78;-42.66416,1026.972;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0.25,0.25;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;81;-36.8079,1292.123;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0.25,0.25;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;79;-203.2205,1138.268;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;80;-204.5641,1249.019;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;-201.6074,1374.104;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;-390.895,1369.134;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-396.8751,1262.238;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;-399.2351,1152.541;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;94;-782.377,951.4816;Inherit;False;Constant;_Vector2;Vector 1;4;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;93;-276.0494,960.6334;Inherit;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;118;-524.8671,1684.656;Inherit;False;Property;_Dialation;Dialation;1;0;Create;True;0;0;0;False;0;False;0.04;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;122;1743.175,851.2794;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;126;2008.343,645.8169;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2;2771.907,577.6362;Float;False;True;-1;2;ASEMaterialInspector;0;3;CircleSquare;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;False;True;3;1;False;;10;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;True;True;True;True;True;0;True;_ColorMask;False;False;False;False;False;False;False;True;True;0;True;_Stencil;255;True;_StencilReadMask;255;True;_StencilWriteMask;0;True;_StencilComp;0;True;_StencilOp;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;2;False;;True;0;True;unity_GUIZTestMode;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.BreakToComponentsNode;133;2313.955,708.1904;Inherit;True;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;136;1856.276,257.4525;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;137;2113.791,318.0864;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;132;2544.483,499.4242;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.BreakToComponentsNode;139;1452.749,292.5313;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;141;2377.349,292.1005;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;138;1843.092,526.825;Inherit;False;Property;_AberrationTransparency;AberrationTransparency;2;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;31;396.6484,275.8323;Inherit;True;Property;_Texture;Texture;0;0;Create;True;0;0;0;False;0;False;-1;2aa87fa45bd4b094c8a7d4d24112c165;2aa87fa45bd4b094c8a7d4d24112c165;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;142;849.6709,65.13876;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
WireConnection;35;0;142;0
WireConnection;35;1;30;0
WireConnection;30;0;31;4
WireConnection;29;0;93;0
WireConnection;29;1;94;0
WireConnection;77;1;83;0
WireConnection;42;0;142;0
WireConnection;42;1;45;0
WireConnection;44;0;42;0
WireConnection;45;0;73;4
WireConnection;111;0;142;0
WireConnection;111;1;113;0
WireConnection;112;0;111;0
WireConnection;113;0;76;4
WireConnection;114;0;142;0
WireConnection;114;1;116;0
WireConnection;115;0;114;0
WireConnection;116;0;77;4
WireConnection;73;1;78;0
WireConnection;76;1;81;0
WireConnection;91;0;92;0
WireConnection;95;0;96;0
WireConnection;97;0;98;0
WireConnection;83;0;93;0
WireConnection;83;1;82;0
WireConnection;78;0;93;0
WireConnection;78;1;79;0
WireConnection;81;0;93;0
WireConnection;81;1;80;0
WireConnection;79;0;94;0
WireConnection;79;1;117;0
WireConnection;80;0;94;0
WireConnection;80;1;119;0
WireConnection;82;0;94;0
WireConnection;82;1;120;0
WireConnection;120;0;97;0
WireConnection;120;1;118;0
WireConnection;119;0;95;0
WireConnection;119;1;118;0
WireConnection;117;0;91;0
WireConnection;117;1;118;0
WireConnection;122;0;44;0
WireConnection;122;1;112;1
WireConnection;122;2;115;2
WireConnection;126;0;122;0
WireConnection;126;1;35;0
WireConnection;2;0;132;0
WireConnection;133;0;126;0
WireConnection;136;0;44;3
WireConnection;136;1;112;3
WireConnection;136;2;115;3
WireConnection;137;0;136;0
WireConnection;137;1;138;0
WireConnection;132;0;133;0
WireConnection;132;1;133;1
WireConnection;132;2;133;2
WireConnection;132;3;141;0
WireConnection;139;0;35;0
WireConnection;141;0;139;3
WireConnection;141;1;137;0
WireConnection;31;1;29;0
ASEEND*/
//CHKSM=C2D7DAF25E0B664232E0F6C8C3924EE21CC76F56