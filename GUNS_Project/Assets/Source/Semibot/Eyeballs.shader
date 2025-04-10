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
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
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
			float2 _Vector0 = float2(2,2);
			float2 uv_TexCoord35 = i.uv_texcoord * _Vector0 + ( _Eye_L_Offset + float2( 0,-1 ) );
			float2 appendResult10_g26 = (float2(1.0 , 1.0));
			float2 temp_output_11_0_g26 = ( abs( (uv_TexCoord35*2.0 + -1.0) ) - appendResult10_g26 );
			float2 break16_g26 = ( 1.0 - ( temp_output_11_0_g26 / fwidth( temp_output_11_0_g26 ) ) );
			float cos71 = cos( radians( _Eye_L_Rotation ) );
			float sin71 = sin( radians( _Eye_L_Rotation ) );
			float2 rotator71 = mul( uv_TexCoord35 - float2( 0.5,0.5 ) , float2x2( cos71 , -sin71 , sin71 , cos71 )) + float2( 0.5,0.5 );
			float2 appendResult11_g17 = (float2(_Eye_L_Pupil_Scale.x , _Eye_L_Pupil_Scale.y));
			float temp_output_17_0_g17 = length( ( (rotator71*2.0 + -1.0) / appendResult11_g17 ) );
			float4 lerpResult102 = lerp( _Eye_L_Sclera_Color , _Eye_L_Pupil_Color , saturate( ( ( 1.0 - temp_output_17_0_g17 ) / fwidth( temp_output_17_0_g17 ) ) ));
			float2 appendResult11_g24 = (float2(_Eye_L_Iris_Scale.x , _Eye_L_Iris_Scale.y));
			float temp_output_17_0_g24 = length( ( (rotator71*2.0 + -1.0) / appendResult11_g24 ) );
			float4 lerpResult124 = lerp( float4( 1,1,1,1 ) , _Eye_L_Iris_Color , saturate( ( ( 1.0 - temp_output_17_0_g24 ) / fwidth( temp_output_17_0_g24 ) ) ));
			float2 uv_TexCoord48 = i.uv_texcoord * _Vector0 + ( _Eye_R_Offset + float2( -1,-1 ) );
			float2 appendResult10_g25 = (float2(1.0 , 1.0));
			float2 temp_output_11_0_g25 = ( abs( (uv_TexCoord48*2.0 + -1.0) ) - appendResult10_g25 );
			float2 break16_g25 = ( 1.0 - ( temp_output_11_0_g25 / fwidth( temp_output_11_0_g25 ) ) );
			float cos58 = cos( radians( _Eye_R_Rotation ) );
			float sin58 = sin( radians( _Eye_R_Rotation ) );
			float2 rotator58 = mul( uv_TexCoord48 - float2( 0.5,0.5 ) , float2x2( cos58 , -sin58 , sin58 , cos58 )) + float2( 0.5,0.5 );
			float2 appendResult11_g16 = (float2(_Eye_R_Pupil_Scale.x , _Eye_R_Pupil_Scale.y));
			float temp_output_17_0_g16 = length( ( (rotator58*2.0 + -1.0) / appendResult11_g16 ) );
			float4 lerpResult87 = lerp( _Eye_R_Sclera_Color , _Eye_R_Pupil_Color , saturate( ( ( 1.0 - temp_output_17_0_g16 ) / fwidth( temp_output_17_0_g16 ) ) ));
			float2 appendResult11_g60 = (float2(_Eye_R_Iris_Scale.x , _Eye_R_Iris_Scale.y));
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
Node;AmplifyShaderEditor.LerpOp;87;-352,384;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;44;-672,-1408;Inherit;True;Ellipse;-1;;17;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;1040,-1424;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;102;-400,-1184;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;111;-384,800;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;122;-672,-576;Inherit;True;Ellipse;-1;;24;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;124;-416,-768;Inherit;True;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,1;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-112,-1184;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;112;-624,800;Inherit;False;Property;_Eye_R_Iris_Color;Eye_R_Iris_Color ;12;1;[Header];Create;True;1;Iris color;0;0;False;0;False;0,0.5498967,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;123;-656,-768;Inherit;False;Property;_Eye_L_Iris_Color;Eye_L_Iris_Color ;13;0;Create;True;0;0;0;False;0;False;0,0.5498967,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;85;-608,544;Inherit;False;Property;_Eye_R_Pupil_Color;Eye_R_Pupil_Color ;8;1;[Header];Create;True;1;Pupil color;0;0;False;0;False;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;99;-608,384;Inherit;False;Property;_Eye_R_Sclera_Color;Eye_R_Sclera_Color ;10;1;[Header];Create;True;1;Sclera color;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;100;-656,-1024;Inherit;False;Property;_Eye_L_Pupil_Color;Eye_L_Pupil_Color ;9;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;101;-656,-1184;Inherit;False;Property;_Eye_L_Sclera_Color;Eye_L_Sclera_Color ;11;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;76;-2144,64;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;49;-2368,-320;Inherit;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;0;False;0;False;2,2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
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
Node;AmplifyShaderEditor.Vector2Node;108;-1472,496;Inherit;False;Constant;_Vector2;Vector 1;10;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;51;-1984,208;Inherit;False;Property;_Eye_R_Offset;Eye_R_Offset;4;1;[Header];Create;True;1;Offset;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;73;-2048,352;Inherit;False;Property;_Eye_R_Rotation;Eye_R_Rotation;6;1;[Header];Create;True;1;Rotation;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;116;-928,640;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;71;-1280,-1408;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-1568,-1408;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,-1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RadiansOpNode;105;-1472,-1152;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;107;-1504,-1072;Inherit;False;Constant;_Vector1;Vector 1;10;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;103;-2080,-1216;Inherit;False;Property;_Eye_L_Rotation;Eye_L_Rotation;7;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;127;-960,-976;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;52;-1184,384;Inherit;False;Property;_Eye_R_Pupil_Scale;Eye_R_Pupil_Scale;0;1;[Header];Create;True;1;Pupil scale;0;0;False;0;False;0.3,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;53;-1216,-1184;Inherit;False;Property;_Eye_L_Pupil_Scale;Eye_L_Pupil_Scale;1;0;Create;True;0;0;0;False;0;False;0.3,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;110;-1168,992;Inherit;False;Property;_Eye_R_Iris_Scale;Eye_R_Iris_Scale;2;1;[Header];Create;True;1;Iris scale;0;0;False;0;False;0.1,0.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;125;-1168,-576;Inherit;False;Property;_Eye_L_Iris_Scale;Eye_L_Iris_Scale;3;0;Create;True;0;0;0;False;0;False;0.1,0.1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;92;592,-64;Inherit;True;Rectangle;-1;;25;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;89;576,-1632;Inherit;True;Rectangle;-1;;26;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;128;-832,1136;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;129;-832,1232;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;133;-880,-320;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;132;-880,-416;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;140;981.7549,385.3118;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;138;560.2293,378.5695;Inherit;True;Bacteria;-1;;27;c4b8e21d0aca1b04d843e80ebaf2ba67;0;2;2;FLOAT2;15,15;False;5;FLOAT;565.86;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;156;366.8108,903.8784;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;20,20;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;168;633.5215,1412.77;Inherit;True;Stripes;-1;;45;8e73a71cdf24db740864b4c3f3357e7f;0;4;5;FLOAT;39.5;False;4;FLOAT;0;False;3;FLOAT;0.4;False;12;FLOAT;149.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;165;635.4885,1143.63;Inherit;True;Spiral;-1;;47;310c5f1537fa4c44699ebaf10a65d8a2;1,2,1;5;7;FLOAT2;4,4;False;8;FLOAT2;3,3;False;9;FLOAT;1;False;10;FLOAT;2.61;False;11;FLOAT;0.11;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;-96,384;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;173;1628.042,797.3171;Inherit;True;Whirl;-1;;51;7d75aee9e4d352a4299928ac98404afc;2,26,1,25,0;6;27;FLOAT2;0,0;False;1;FLOAT2;1,1;False;7;FLOAT2;0.75,0.75;False;16;FLOAT;4;False;21;FLOAT;1.54;False;10;FLOAT;21.95;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;185;1158.899,867.1359;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;190;1276.498,870.3358;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;187;1426.897,871.9362;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;177;1297.156,1052.618;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.75,0.75;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;174;682.6068,2272.749;Inherit;False;Zig Zag;-1;;54;8cd734fbcae021148a58931ed7d68679;2,24,0,17,1;4;19;FLOAT2;0,0;False;22;FLOAT2;1,1;False;15;FLOAT;0.5;False;16;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;172;688.207,1900.75;Inherit;True;0;0;1;0;1;False;1;False;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;7.41;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1746.96,-54.01892;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;Eyeballs;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.RangedFloatNode;191;1424,224;Inherit;False;Property;_Metalic;Metalic;14;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;192;1429.6,304;Inherit;False;Property;_Smoothness;Smoothness;15;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;189;990.8974,876.736;Inherit;False;1;0;FLOAT;4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;181;1088.235,1157.23;Inherit;False;1;0;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;148;551.8611,600.3161;Inherit;True;Dots Pattern;-1;;57;7d8d5e315fd9002418fb41741d3a59cb;1,22,0;5;21;FLOAT2;0,0;False;3;FLOAT2;20,20;False;2;FLOAT;0.5;False;4;FLOAT;1;False;5;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;179;1024.235,1029.229;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;155;670.8108,907.0784;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;171;632.9862,1674.033;Inherit;True;Truchet;-1;;59;600b4e63537aa56498ba8983340930ed;0;5;25;FLOAT2;8,8;False;6;FLOAT;3;False;33;FLOAT;0.74;False;34;FLOAT;0.1;False;19;FLOAT;163;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;109;-640,992;Inherit;True;Ellipse;-1;;60;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;139;814.6943,477.3484;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
WireConnection;50;0;97;0
WireConnection;50;1;96;0
WireConnection;96;0;92;0
WireConnection;96;1;114;0
WireConnection;47;2;58;0
WireConnection;47;7;52;1
WireConnection;47;9;52;2
WireConnection;87;0;99;0
WireConnection;87;1;85;0
WireConnection;87;2;47;0
WireConnection;44;2;71;0
WireConnection;44;7;53;1
WireConnection;44;9;53;2
WireConnection;97;0;89;0
WireConnection;97;1;126;0
WireConnection;102;0;101;0
WireConnection;102;1;100;0
WireConnection;102;2;44;0
WireConnection;111;1;112;0
WireConnection;111;2;109;0
WireConnection;122;2;127;0
WireConnection;122;7;125;1
WireConnection;122;9;125;2
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
WireConnection;128;0;52;1
WireConnection;128;1;110;1
WireConnection;129;0;52;2
WireConnection;129;1;110;2
WireConnection;133;0;53;2
WireConnection;133;1;125;2
WireConnection;132;0;53;1
WireConnection;132;1;125;1
WireConnection;140;0;139;0
WireConnection;114;0;87;0
WireConnection;114;1;111;0
WireConnection;173;27;177;0
WireConnection;173;21;187;0
WireConnection;185;0;189;0
WireConnection;190;0;185;0
WireConnection;187;0;190;0
WireConnection;177;0;179;0
WireConnection;177;2;181;0
WireConnection;0;0;50;0
WireConnection;0;3;191;0
WireConnection;0;4;192;0
WireConnection;155;0;156;0
WireConnection;109;2;116;0
WireConnection;109;7;110;1
WireConnection;109;9;110;2
WireConnection;139;0;138;0
ASEEND*/
//CHKSM=FFF9108FF9ADC0BC0CCF4D2823A573032845053A