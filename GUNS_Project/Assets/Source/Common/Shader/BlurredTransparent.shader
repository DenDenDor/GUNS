// Made with Amplify Shader Editor v1.9.2.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Sacb0y/BlurredTransparent"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_NoiseScale("NoiseScale", Float) = 10000
		_BlurIntensity("Blur Intensity", Range( 0 , 1)) = 0.05
		_Max("Max", Float) = 1
		_Min("Min", Float) = 0
		_BlurIntencity("BlurIntencity", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#define ASE_NEEDS_VERT_POSITION


			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float _Min;
			uniform float _Max;
			uniform float _NoiseScale;
			uniform float _BlurIntensity;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;
			uniform float _BlurIntencity;
			float3 RGBToHSV(float3 c)
			{
				float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
				float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
				float d = q.x - min( q.w, q.y );
				float e = 1.0e-10;
				return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
			}
			float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }
			float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }
			float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }
			float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }
			float snoise( float3 v )
			{
				const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
				float3 i = floor( v + dot( v, C.yyy ) );
				float3 x0 = v - i + dot( i, C.xxx );
				float3 g = step( x0.yzx, x0.xyz );
				float3 l = 1.0 - g;
				float3 i1 = min( g.xyz, l.zxy );
				float3 i2 = max( g.xyz, l.zxy );
				float3 x1 = x0 - i1 + C.xxx;
				float3 x2 = x0 - i2 + C.yyy;
				float3 x3 = x0 - 0.5;
				i = mod3D289( i);
				float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
				float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
				float4 x_ = floor( j / 7.0 );
				float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
				float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
				float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
				float4 h = 1.0 - abs( x ) - abs( y );
				float4 b0 = float4( x.xy, y.xy );
				float4 b1 = float4( x.zw, y.zw );
				float4 s0 = floor( b0 ) * 2.0 + 1.0;
				float4 s1 = floor( b1 ) * 2.0 + 1.0;
				float4 sh = -step( h, 0.0 );
				float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
				float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
				float3 g0 = float3( a0.xy, h.x );
				float3 g1 = float3( a0.zw, h.y );
				float3 g2 = float3( a1.xy, h.z );
				float3 g3 = float3( a1.zw, h.w );
				float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
				g0 *= norm.x;
				g1 *= norm.y;
				g2 *= norm.z;
				g3 *= norm.w;
				float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
				m = m* m;
				m = m* m;
				float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
				return 42.0 * dot( m, px);
			}
			
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord4 = screenPos;
				float3 vertexPos147 = v.vertex.xyz;
				float4 ase_clipPos147 = UnityObjectToClipPos(vertexPos147);
				float4 screenPos147 = ComputeScreenPos(ase_clipPos147);
				o.ase_texcoord5 = screenPos147;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_MainTex = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 screenPos = i.ase_texcoord4;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 temp_output_177_0 = ( (ase_screenPosNorm).xy / ase_screenPosNorm.w );
				float simplePerlin3D181 = snoise( float3( temp_output_177_0 ,  0.0 )*_NoiseScale );
				simplePerlin3D181 = simplePerlin3D181*0.5 + 0.5;
				float2 temp_cast_1 = (simplePerlin3D181).xx;
				float cos197 = cos( 90.0 );
				float sin197 = sin( 90.0 );
				float2 rotator197 = mul( temp_cast_1 - float2( 0.5,0.5 ) , float2x2( cos197 , -sin197 , sin197 , cos197 )) + float2( 0.5,0.5 );
				float simplePerlin2D96 = snoise( temp_output_177_0*_NoiseScale );
				simplePerlin2D96 = simplePerlin2D96*0.5 + 0.5;
				float2 appendResult231 = (float2(simplePerlin2D96 , simplePerlin2D96));
				float4 screenPos147 = i.ase_texcoord5;
				float4 ase_screenPosNorm147 = screenPos147 / screenPos147.w;
				ase_screenPosNorm147.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm147.z : ase_screenPosNorm147.z * 0.5 + 0.5;
				float screenDepth147 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm147.xy ));
				float distanceDepth147 = saturate( abs( ( screenDepth147 - LinearEyeDepth( ase_screenPosNorm147.z ) ) / ( 0.5 ) ) );
				float clampResult148 = clamp( ( _BlurIntensity * distanceDepth147 ) , 0.0 , _BlurIntensity );
				float2 lerpResult127 = lerp( temp_output_177_0 , ( temp_output_177_0 + ( float2( 0.5,0.5 ) - rotator197 ) + ( float2( 0.5,0.5 ) - appendResult231 ) ) , clampResult148);
				float4 tex2DNode237 = tex2Dlod( _MainTex, float4( lerpResult127, 0, 2.0) );
				float3 hsvTorgb262 = RGBToHSV( tex2DNode237.rgb );
				float smoothstepResult257 = smoothstep( _Min , _Max , hsvTorgb262.z);
				

				finalColor = ( 1.0 - ( ( 1.0 - saturate( tex2Dlod( _MainTex, float4( uv_MainTex, 0, 2.0) ) ) ) * ( 1.0 - saturate( ( ( smoothstepResult257 * tex2DNode237 ) * _BlurIntencity ) ) ) ) );

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
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;138;1188.126,-808.1116;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;176;193.3592,-1094.624;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;110;557.8354,-719.691;Inherit;False;Property;_BlurIntensity;Blur Intensity;1;0;Create;True;0;0;0;False;0;False;0.05;0.08;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;134;965.0308,-863.3455;Inherit;False;2;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;177;401.3849,-1091.052;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;101;242.683,-857.5084;Inherit;False;Property;_NoiseScale;NoiseScale;0;0;Create;True;0;0;0;False;0;False;10000;821.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;233;1134.236,-1009.488;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;228;969.8455,-959.3483;Inherit;False;2;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;197;726.8383,-1003.669;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;115;-173.7442,-935.1298;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;181;499.6217,-992.5388;Inherit;False;Simplex3D;True;False;2;0;FLOAT3;1,1,0;False;1;FLOAT;100000;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;198;563.0162,-900.2881;Inherit;False;Constant;_Rotate90;Rotate90;3;0;Create;True;0;0;0;False;0;False;90;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;96;416.1735,-802.3885;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;1,1;False;1;FLOAT;100000;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;231;723.5525,-832.6438;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;127;1505.494,-1058.437;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;148;1423.817,-829.1748;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;147;784.7064,-573.4306;Inherit;False;True;True;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;144;523.6625,-566.3536;Inherit;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;239;1593.373,-1175.559;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;237;1800.704,-1078.343;Inherit;True;Property;_TextureSample1;Texture Sample 0;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;2;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;2;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;241;1823.748,-842.9826;Inherit;True;Property;_TextureSample2;Texture Sample 0;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipLevel;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;2;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;2;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;247;1699.75,-1365.062;Inherit;True;Property;_Texture0;Texture 0;2;0;Create;True;0;0;0;False;0;False;47b0a519530ccff4fa5a664f3251bde7;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;261;2135.997,-1288.51;Inherit;False;Property;_Min;Min;4;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;260;2140.012,-1219.71;Inherit;False;Property;_Max;Max;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;257;2361.071,-1171.368;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RGBToHSVNode;262;2139.341,-1030.423;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;252;3327.652,-956.1271;Inherit;False;ScreenBlending;-1;;4;e9ed7b4c301aaa845b3d81da28b82a2d;0;2;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;255;3019.196,-897.4656;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;254;3021.427,-964.9348;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;235;3660.442,-1016.109;Float;False;True;-1;2;ASEMaterialInspector;0;9;Sacb0y/BlurredTransparent;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;True;7;False;;False;True;0;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;259;2606.252,-1168.732;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;263;2959.412,-1148.252;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;264;2695.411,-920.4149;Inherit;False;Property;_BlurIntencity;BlurIntencity;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
WireConnection;138;0;110;0
WireConnection;138;1;147;0
WireConnection;176;0;115;0
WireConnection;134;1;231;0
WireConnection;177;0;176;0
WireConnection;177;1;115;4
WireConnection;233;0;177;0
WireConnection;233;1;228;0
WireConnection;233;2;134;0
WireConnection;228;1;197;0
WireConnection;197;0;181;0
WireConnection;197;2;198;0
WireConnection;181;0;177;0
WireConnection;181;1;101;0
WireConnection;96;0;177;0
WireConnection;96;1;101;0
WireConnection;231;0;96;0
WireConnection;231;1;96;0
WireConnection;127;0;177;0
WireConnection;127;1;233;0
WireConnection;127;2;148;0
WireConnection;148;0;138;0
WireConnection;148;2;110;0
WireConnection;147;1;144;0
WireConnection;237;0;239;0
WireConnection;237;1;127;0
WireConnection;241;0;239;0
WireConnection;257;0;262;3
WireConnection;257;1;261;0
WireConnection;257;2;260;0
WireConnection;262;0;237;0
WireConnection;252;7;255;0
WireConnection;252;8;254;0
WireConnection;255;0;241;0
WireConnection;254;0;263;0
WireConnection;235;0;252;0
WireConnection;259;0;257;0
WireConnection;259;1;237;0
WireConnection;263;0;259;0
WireConnection;263;1;264;0
ASEEND*/
//CHKSM=81F96D133C79022E7816306E1670309171533DDE