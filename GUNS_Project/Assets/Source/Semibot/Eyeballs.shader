// Made with Amplify Shader Editor v1.9.2.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Eyeballs"
{
	Properties
	{
		[Header(Pupil scale)]_Eye_R_Pupil_Scale("Eye_R_Pupil_Scale", Vector) = (0.3,0.3,0,0)
		_Eye_L_Pupil_Scale("Eye_L_Pupil_Scale", Vector) = (0.3,0.3,0,0)
		[Header(Iris scale)]_Eye_R_Iris_Scale("Eye_R_Iris_Scale", Vector) = (0.1,0.1,0,0)
		_Eye_L_Iris_Scale("Eye_L_Iris_Scale", Vector) = (0.1,0.1,0,0)
		[Header(Offset)]_Eye_R_Offset("Eye_R_Offset", Vector) = (0,0,0,0)
		_Eye_L_Offset("Eye_L_Offset", Vector) = (0,0,0,0)
		[Header(Rotation)]_Eye_R_Rotation("Eye_R_Rotation", Float) = 0
		_Eye_L_Rotation("Eye_L_Rotation", Float) = 0
		[Header(Pupil color)]_Eye_R_Pupil_Color("Eye_R_Pupil_Color ", Color) = (0,0,0,1)
		_Eye_L_Pupil_Color("Eye_L_Pupil_Color ", Color) = (0,0,0,1)
		[Header(Sclera color)]_Eye_R_Sclera_Color("Eye_R_Sclera_Color ", Color) = (1,1,1,1)
		_Eye_L_Sclera_Color("Eye_L_Sclera_Color ", Color) = (1,1,1,1)
		[Header(Iris color)]_Eye_R_Iris_Color("Eye_R_Iris_Color ", Color) = (0,0.5498967,1,1)
		_Eye_L_Iris_Color("Eye_L_Iris_Color ", Color) = (0,0.5498967,1,1)
		_Metalic("Metalic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 2.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float2 _Eye_L_Offset;
		uniform float4 _Eye_L_Sclera_Color;
		uniform float4 _Eye_L_Pupil_Color;
		uniform float _Eye_L_Rotation;
		uniform float2 _Eye_L_Pupil_Scale;
		uniform float4 _Eye_L_Iris_Color;
		uniform float2 _Eye_L_Iris_Scale;
		uniform float2 _Eye_R_Offset;
		uniform float4 _Eye_R_Sclera_Color;
		uniform float4 _Eye_R_Pupil_Color;
		uniform float _Eye_R_Rotation;
		uniform float2 _Eye_R_Pupil_Scale;
		uniform float4 _Eye_R_Iris_Color;
		uniform float2 _Eye_R_Iris_Scale;
		uniform float _Metalic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 _Tiling = float2(2,2);
			float2 uv_TexCoord35 = i.uv_texcoord * _Tiling + ( _Eye_L_Offset + float2( 0,-1 ) );
			float2 appendResult10_g26 = (float2(1.0 , 1.0));
			float2 temp_output_11_0_g26 = ( abs( (uv_TexCoord35*2.0 + -1.0) ) - appendResult10_g26 );
			float2 break16_g26 = ( 1.0 - ( temp_output_11_0_g26 / fwidth( temp_output_11_0_g26 ) ) );
			float cos71 = cos( radians( _Eye_L_Rotation ) );
			float sin71 = sin( radians( _Eye_L_Rotation ) );
			float2 rotator71 = mul( uv_TexCoord35 - float2( 0.5,0.5 ) , float2x2( cos71 , -sin71 , sin71 , cos71 )) + float2( 0.5,0.5 );
			float MinScale201 = 0.0001;
			float2 appendResult11_g17 = (float2(max( _Eye_L_Pupil_Scale.x , MinScale201 ) , max( _Eye_L_Pupil_Scale.y , MinScale201 )));
			float temp_output_17_0_g17 = length( ( (rotator71*2.0 + -1.0) / appendResult11_g17 ) );
			float4 lerpResult102 = lerp( _Eye_L_Sclera_Color , _Eye_L_Pupil_Color , saturate( ( ( 1.0 - temp_output_17_0_g17 ) / fwidth( temp_output_17_0_g17 ) ) ));
			float2 appendResult11_g24 = (float2(max( _Eye_L_Iris_Scale.x , MinScale201 ) , max( _Eye_L_Iris_Scale.y , MinScale201 )));
			float temp_output_17_0_g24 = length( ( (rotator71*2.0 + -1.0) / appendResult11_g24 ) );
			float4 lerpResult124 = lerp( float4( 1,1,1,1 ) , _Eye_L_Iris_Color , saturate( ( ( 1.0 - temp_output_17_0_g24 ) / fwidth( temp_output_17_0_g24 ) ) ));
			float2 uv_TexCoord48 = i.uv_texcoord * _Tiling + ( _Eye_R_Offset + float2( -1,-1 ) );
			float2 appendResult10_g25 = (float2(1.0 , 1.0));
			float2 temp_output_11_0_g25 = ( abs( (uv_TexCoord48*2.0 + -1.0) ) - appendResult10_g25 );
			float2 break16_g25 = ( 1.0 - ( temp_output_11_0_g25 / fwidth( temp_output_11_0_g25 ) ) );
			float cos58 = cos( radians( _Eye_R_Rotation ) );
			float sin58 = sin( radians( _Eye_R_Rotation ) );
			float2 rotator58 = mul( uv_TexCoord48 - float2( 0.5,0.5 ) , float2x2( cos58 , -sin58 , sin58 , cos58 )) + float2( 0.5,0.5 );
			float2 appendResult11_g16 = (float2(max( _Eye_R_Pupil_Scale.x , MinScale201 ) , max( _Eye_R_Pupil_Scale.y , MinScale201 )));
			float temp_output_17_0_g16 = length( ( (rotator58*2.0 + -1.0) / appendResult11_g16 ) );
			float4 lerpResult87 = lerp( _Eye_R_Sclera_Color , _Eye_R_Pupil_Color , saturate( ( ( 1.0 - temp_output_17_0_g16 ) / fwidth( temp_output_17_0_g16 ) ) ));
			float2 appendResult11_g60 = (float2(max( _Eye_R_Iris_Scale.x , MinScale201 ) , max( _Eye_R_Iris_Scale.y , MinScale201 )));
			float temp_output_17_0_g60 = length( ( (rotator58*2.0 + -1.0) / appendResult11_g60 ) );
			float4 lerpResult111 = lerp( float4( 1,1,1,1 ) , _Eye_R_Iris_Color , saturate( ( ( 1.0 - temp_output_17_0_g60 ) / fwidth( temp_output_17_0_g60 ) ) ));
			o.Albedo = ( ( saturate( min( break16_g26.x , break16_g26.y ) ) * ( lerpResult102 * lerpResult124 ) ) + ( saturate( min( break16_g25.x , break16_g25.y ) ) * ( lerpResult87 * lerpResult111 ) ) ).rgb;
			o.Metallic = _Metalic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19202
Node;AmplifyShaderEditor.SimpleAddOpNode;50;1312,0;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;1072,128;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;47;-640,160;Inherit;True;Ellipse;-1;;16;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;44;-672,-1408;Inherit;True;Ellipse;-1;;17;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;1040,-1424;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;102;-400,-1184;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;111;-384,800;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;124;-416,-768;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-112,-1184;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;112;-624,800;Inherit;False;Property;_Eye_R_Iris_Color;Eye_R_Iris_Color ;12;1;[Header];Create;True;1;Iris color;0;0;False;0;False;0,0.5498967,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;123;-656,-768;Inherit;False;Property;_Eye_L_Iris_Color;Eye_L_Iris_Color ;13;0;Create;True;0;0;0;False;0;False;0,0.5498967,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;85;-608,544;Inherit;False;Property;_Eye_R_Pupil_Color;Eye_R_Pupil_Color ;8;1;[Header];Create;True;1;Pupil color;0;0;False;0;False;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;99;-608,384;Inherit;False;Property;_Eye_R_Sclera_Color;Eye_R_Sclera_Color ;10;1;[Header];Create;True;1;Sclera color;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;100;-656,-1024;Inherit;False;Property;_Eye_L_Pupil_Color;Eye_L_Pupil_Color ;9;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;101;-656,-1184;Inherit;False;Property;_Eye_L_Sclera_Color;Eye_L_Sclera_Color ;11;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;76;-2144,64;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;-1792,-1360;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;79;-2080,-1440;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;80;-1648,-1424;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;56;-2016,-1360;Inherit;False;Property;_Eye_L_Offset;Eye_L_Offset;5;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.WireNode;93;-1296,-1584;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;48;-1536,160;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;-1,-1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;55;-1744,208;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;-1,-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;58;-1248,160;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;77;-1616,144;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;95;-1264,-16;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RadiansOpNode;60;-1440,416;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;51;-1984,208;Inherit;False;Property;_Eye_R_Offset;Eye_R_Offset;4;1;[Header];Create;True;1;Offset;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.WireNode;116;-928,640;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;71;-1280,-1408;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-1568,-1408;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,-1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RadiansOpNode;105;-1472,-1152;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;107;-1504,-1072;Inherit;False;Constant;_Vector1;Vector 1;10;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;103;-2080,-1216;Inherit;False;Property;_Eye_L_Rotation;Eye_L_Rotation;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;127;-960,-976;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;52;-1184,384;Inherit;False;Property;_Eye_R_Pupil_Scale;Eye_R_Pupil_Scale;0;1;[Header];Create;True;1;Pupil scale;0;0;False;0;False;0.3,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;53;-1216,-1184;Inherit;False;Property;_Eye_L_Pupil_Scale;Eye_L_Pupil_Scale;1;0;Create;True;0;0;0;False;0;False;0.3,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;125;-1168,-576;Inherit;False;Property;_Eye_L_Iris_Scale;Eye_L_Iris_Scale;3;0;Create;True;0;0;0;False;0;False;0.1,0.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;92;592,-64;Inherit;True;Rectangle;-1;;25;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;89;576,-1632;Inherit;True;Rectangle;-1;;26;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-96,384;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1746.96,-54.01892;Float;False;True;-1;0;ASEMaterialInspector;0;0;Standard;Eyeballs;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.RangedFloatNode;191;1424,224;Inherit;False;Property;_Metalic;Metalic;14;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;192;1429.6,304;Inherit;False;Property;_Smoothness;Smoothness;15;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;109;-640,992;Inherit;True;Ellipse;-1;;60;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;108;-1472,496;Inherit;False;Constant;_Vector2;Vector 1;10;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;122;-672,-576;Inherit;True;Ellipse;-1;;24;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;110;-1274.86,995.7169;Inherit;False;Property;_Eye_R_Iris_Scale;Eye_R_Iris_Scale;2;1;[Header];Create;True;1;Iris scale;0;0;False;0;False;0.1,0.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;198;-2135.9,980.2383;Inherit;False;Constant;_MinScale;MinScale;16;0;Create;True;0;0;0;False;0;False;0.0001;0.0001;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;201;-1957.828,977.3905;Inherit;False;MinScale;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;202;-1278.758,1143.875;Inherit;False;201;MinScale;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;203;-1177.861,527.9086;Inherit;False;201;MinScale;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;204;-922.8685,480.3817;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;205;-919.0846,385.6956;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;197;-1023.766,1096.348;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;196;-1019.982,1001.662;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-2048,352;Inherit;False;Property;_Eye_R_Rotation;Eye_R_Rotation;6;1;[Header];Create;True;1;Rotation;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;206;-1170.741,-423.7624;Inherit;False;201;MinScale;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;207;-915.7487,-471.2893;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;208;-911.9647,-565.9753;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;209;-1235.136,-1029.054;Inherit;False;201;MinScale;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;210;-980.1432,-1076.581;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;211;-976.3593,-1171.267;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.0001;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;49;-2368,-320;Inherit;False;Constant;_Tiling;Tiling;0;0;Create;True;0;0;0;False;0;False;2,2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.LerpOp;87;-352,384;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
WireConnection;50;0;97;0
WireConnection;50;1;96;0
WireConnection;96;0;92;0
WireConnection;96;1;114;0
WireConnection;47;2;58;0
WireConnection;47;7;205;0
WireConnection;47;9;204;0
WireConnection;44;2;71;0
WireConnection;44;7;211;0
WireConnection;44;9;210;0
WireConnection;97;0;89;0
WireConnection;97;1;126;0
WireConnection;102;0;101;0
WireConnection;102;1;100;0
WireConnection;102;2;44;0
WireConnection;111;1;112;0
WireConnection;111;2;109;0
WireConnection;124;1;123;0
WireConnection;124;2;122;0
WireConnection;126;0;102;0
WireConnection;126;1;124;0
WireConnection;76;0;49;0
WireConnection;57;0;56;0
WireConnection;79;0;49;0
WireConnection;80;0;79;0
WireConnection;93;0;35;0
WireConnection;48;0;77;0
WireConnection;48;1;55;0
WireConnection;55;0;51;0
WireConnection;58;0;48;0
WireConnection;58;1;108;0
WireConnection;58;2;60;0
WireConnection;77;0;76;0
WireConnection;95;0;48;0
WireConnection;60;0;73;0
WireConnection;116;0;58;0
WireConnection;71;0;35;0
WireConnection;71;1;107;0
WireConnection;71;2;105;0
WireConnection;35;0;80;0
WireConnection;35;1;57;0
WireConnection;105;0;103;0
WireConnection;127;0;71;0
WireConnection;92;1;95;0
WireConnection;89;1;93;0
WireConnection;114;0;87;0
WireConnection;114;1;111;0
WireConnection;0;0;50;0
WireConnection;0;3;191;0
WireConnection;0;4;192;0
WireConnection;109;2;116;0
WireConnection;109;7;196;0
WireConnection;109;9;197;0
WireConnection;122;2;127;0
WireConnection;122;7;208;0
WireConnection;122;9;207;0
WireConnection;201;0;198;0
WireConnection;204;0;52;2
WireConnection;204;1;203;0
WireConnection;205;0;52;1
WireConnection;205;1;203;0
WireConnection;197;0;110;2
WireConnection;197;1;202;0
WireConnection;196;0;110;1
WireConnection;196;1;202;0
WireConnection;207;0;125;2
WireConnection;207;1;206;0
WireConnection;208;0;125;1
WireConnection;208;1;206;0
WireConnection;210;0;53;2
WireConnection;210;1;209;0
WireConnection;211;0;53;1
WireConnection;211;1;209;0
WireConnection;87;0;99;0
WireConnection;87;1;85;0
WireConnection;87;2;47;0
ASEEND*/
//CHKSM=5C8F96EDA868368F55BE1B81B48E5A5B2F131B47