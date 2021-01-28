//Maya ASCII 2018 scene
//Name: TreasureChest_Model.ma
//Last modified: Thu, Jan 21, 2021 10:46:33 PM
//Codeset: 1252
requires maya "2018";
requires "stereoCamera" "10.0";
requires "stereoCamera" "10.0";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2018";
fileInfo "version" "2018";
fileInfo "cutIdentifier" "201706261615-f9658c4cfc";
fileInfo "osv" "Microsoft Windows 8 Home Premium Edition, 64-bit  (Build 9200)\n";
fileInfo "license" "student";
createNode transform -s -n "persp";
	rename -uid "50202479-4A12-33DC-0F7C-EC8AF25A9FC9";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 39.930048687045918 25.219063964360799 60.899048292461401 ;
	setAttr ".r" -type "double3" -17.738352732009421 769.39999999984832 2.4436698605058533e-15 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "CC5016B6-486D-DE9B-639F-BCA538C549D5";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 78.607196351495531;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
	setAttr ".ai_translator" -type "string" "perspective";
createNode transform -s -n "top";
	rename -uid "8CE7DA9D-48BB-8A85-571E-CCAA5DDF6CFE";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 1000.1 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "3C929736-458F-F032-0BE6-C294AD367E5D";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 39.62566844919786;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "front";
	rename -uid "70434253-4632-9A03-D8F2-E4BFF96C66A0";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 1000.1 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "488724C2-4C90-285E-349D-B4921F67F18C";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 39.62566844919786;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "side";
	rename -uid "C675BD32-475D-14C9-CC26-70A12D57035D";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1000.1 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "1DAE3572-4DDC-22FA-3594-17AD9832E610";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 39.62566844919786;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -n "pCube1";
	rename -uid "F5961030-441B-06C3-85FA-699381CF36FE";
	setAttr ".t" -type "double3" 0.28699774705393466 5.011089327882174 22.767480230142411 ;
	setAttr ".s" -type "double3" 11.193170859514737 7.3155173834236384 20.763492080175194 ;
	setAttr ".rp" -type "double3" 0 0 -22.661056767328382 ;
	setAttr ".sp" -type "double3" 0 0 -1.0913894772529602 ;
	setAttr ".spt" -type "double3" 0 0 -21.569667290075422 ;
createNode mesh -n "pCubeShape1" -p "pCube1";
	rename -uid "0ADDE908-4DFA-B02D-D0E7-99B28F09F2BB";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.37976419925689697 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 69 ".pt";
	setAttr ".pt[8]" -type "float3" 0 0 -5.9604645e-08 ;
	setAttr ".pt[9]" -type "float3" 0 0 -5.9604645e-08 ;
	setAttr ".pt[10]" -type "float3" 0 0 -5.9604645e-08 ;
	setAttr ".pt[11]" -type "float3" 0 0 -5.9604645e-08 ;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube2";
	rename -uid "7499D879-4116-8C83-5D79-F79C328D2747";
	setAttr ".t" -type "double3" 0.19518180046051903 9.0349531361895306 0 ;
	setAttr ".s" -type "double3" 13.529520637952762 1.2531218485875926 22.046018931292988 ;
createNode mesh -n "pCubeShape2" -p "pCube2";
	rename -uid "83346123-457F-0BA9-67F8-3CA1FF2ADD6B";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCylinder2";
	rename -uid "7B19347A-44FF-2946-4C39-E2BAC60912B5";
	setAttr ".t" -type "double3" 0.2135790665935664 9.4773406571001786 -0.048190631029642716 ;
	setAttr ".r" -type "double3" 270 0 0 ;
	setAttr ".s" -type "double3" 5.6046480758720021 9.9194199693603728 5.7526696897838505 ;
createNode mesh -n "pCylinderShape2" -p "pCylinder2";
	rename -uid "A4D45867-4444-DCC9-9B81-AD837FAAF9F2";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 72 ".pt[26:97]" -type "float3"  0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 0 0 -4.7683716e-07 
		0;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode mesh -n "polySurfaceShape1" -p "pCylinder2";
	rename -uid "18480272-404C-6276-FC14-B7BAD6D88373";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 52 ".uvst[0].uvsp[0:51]" -type "float2" 0.63531649 0.078125
		 0.578125 0.020933539 0.5 0 0.421875 0.020933539 0.36468354 0.078125 0.34375 0.15625
		 0.36468354 0.234375 0.421875 0.29156646 0.5 0.3125 0.578125 0.29156646 0.63531649
		 0.234375 0.65625 0.15625 0.375 0.3125 0.39583334 0.3125 0.41666669 0.3125 0.43750003
		 0.3125 0.45833337 0.3125 0.47916672 0.3125 0.50000006 0.3125 0.52083337 0.3125 0.54166669
		 0.3125 0.5625 0.3125 0.58333331 0.3125 0.60416663 0.3125 0.62499994 0.3125 0.375
		 0.68843985 0.39583334 0.68843985 0.41666669 0.68843985 0.43750003 0.68843985 0.45833337
		 0.68843985 0.47916672 0.68843985 0.50000006 0.68843985 0.52083337 0.68843985 0.54166669
		 0.68843985 0.5625 0.68843985 0.58333331 0.68843985 0.60416663 0.68843985 0.62499994
		 0.68843985 0.63531649 0.765625 0.578125 0.70843351 0.5 0.6875 0.421875 0.70843351
		 0.36468354 0.765625 0.34375 0.84375 0.36468354 0.921875 0.421875 0.97906649 0.5 1
		 0.578125 0.97906649 0.63531649 0.921875 0.65625 0.84375 0.5 0.15000001 0.5 0.83749998;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 26 ".vt[0:25]"  0.86602539 -1 -0.5 0.5 -1 -0.86602539 0 -1 -1
		 -0.5 -1 -0.86602539 -0.86602539 -1 -0.5 -1 -1 0 -0.86602539 -1 0.5 -0.5 -1 0.86602539
		 0 -1 1 0.5 -1 0.86602539 0.86602539 -1 0.5 1 -1 0 0.86602539 1 -0.5 0.5 1 -0.86602539
		 0 1 -1 -0.5 1 -0.86602539 -0.86602539 1 -0.5 -1 1 0 -0.86602539 1 0.5 -0.5 1 0.86602539
		 0 1 1 0.5 1 0.86602539 0.86602539 1 0.5 1 1 0 0 -1 0 0 1 0;
	setAttr -s 60 ".ed[0:59]"  0 1 0 1 2 0 2 3 0 3 4 0 4 5 0 5 6 0 6 7 0
		 7 8 0 8 9 0 9 10 0 10 11 0 11 0 0 12 13 0 13 14 0 14 15 0 15 16 0 16 17 0 17 18 0
		 18 19 0 19 20 0 20 21 0 21 22 0 22 23 0 23 12 0 0 12 0 1 13 0 2 14 0 3 15 0 4 16 0
		 5 17 0 6 18 0 7 19 0 8 20 0 9 21 0 10 22 0 11 23 0 24 0 1 24 1 1 24 2 1 24 3 1 24 4 1
		 24 5 1 24 6 1 24 7 1 24 8 1 24 9 1 24 10 1 24 11 1 12 25 1 13 25 1 14 25 1 15 25 1
		 16 25 1 17 25 1 18 25 1 19 25 1 20 25 1 21 25 1 22 25 1 23 25 1;
	setAttr -s 36 -ch 120 ".fc[0:35]" -type "polyFaces" 
		f 4 0 25 -13 -25
		mu 0 4 12 13 26 25
		f 4 1 26 -14 -26
		mu 0 4 13 14 27 26
		f 4 2 27 -15 -27
		mu 0 4 14 15 28 27
		f 4 3 28 -16 -28
		mu 0 4 15 16 29 28
		f 4 4 29 -17 -29
		mu 0 4 16 17 30 29
		f 4 5 30 -18 -30
		mu 0 4 17 18 31 30
		f 4 6 31 -19 -31
		mu 0 4 18 19 32 31
		f 4 7 32 -20 -32
		mu 0 4 19 20 33 32
		f 4 8 33 -21 -33
		mu 0 4 20 21 34 33
		f 4 9 34 -22 -34
		mu 0 4 21 22 35 34
		f 4 10 35 -23 -35
		mu 0 4 22 23 36 35
		f 4 11 24 -24 -36
		mu 0 4 23 24 37 36
		f 3 -1 -37 37
		mu 0 3 1 0 50
		f 3 -2 -38 38
		mu 0 3 2 1 50
		f 3 -3 -39 39
		mu 0 3 3 2 50
		f 3 -4 -40 40
		mu 0 3 4 3 50
		f 3 -5 -41 41
		mu 0 3 5 4 50
		f 3 -6 -42 42
		mu 0 3 6 5 50
		f 3 -7 -43 43
		mu 0 3 7 6 50
		f 3 -8 -44 44
		mu 0 3 8 7 50
		f 3 -9 -45 45
		mu 0 3 9 8 50
		f 3 -10 -46 46
		mu 0 3 10 9 50
		f 3 -11 -47 47
		mu 0 3 11 10 50
		f 3 -12 -48 36
		mu 0 3 0 11 50
		f 3 12 49 -49
		mu 0 3 48 47 51
		f 3 13 50 -50
		mu 0 3 47 46 51
		f 3 14 51 -51
		mu 0 3 46 45 51
		f 3 15 52 -52
		mu 0 3 45 44 51
		f 3 16 53 -53
		mu 0 3 44 43 51
		f 3 17 54 -54
		mu 0 3 43 42 51
		f 3 18 55 -55
		mu 0 3 42 41 51
		f 3 19 56 -56
		mu 0 3 41 40 51
		f 3 20 57 -57
		mu 0 3 40 39 51
		f 3 21 58 -58
		mu 0 3 39 38 51
		f 3 22 59 -59
		mu 0 3 38 49 51
		f 3 23 48 -60
		mu 0 3 49 48 51;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube3";
	rename -uid "453898DC-45E2-ADD0-EA70-70B763911A74";
	setAttr ".t" -type "double3" 0.19518180046051903 1.1120896177292519 0 ;
	setAttr ".s" -type "double3" 13.529520637952762 1.6325799086660597 22.046018931292988 ;
createNode mesh -n "pCubeShape3" -p "pCube3";
	rename -uid "909EE855-4CE2-3A0D-238F-728D6E1120D9";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode mesh -n "polySurfaceShape4" -p "pCube3";
	rename -uid "5F12E157-4D5D-B233-8551-B1B4C0BB8F9A";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 14 ".uvst[0].uvsp[0:13]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 8 ".vt[0:7]"  -0.5 -0.5 0.5 0.5 -0.5 0.5 -0.5 0.5 0.5 0.5 0.5 0.5
		 -0.5 0.5 -0.5 0.5 0.5 -0.5 -0.5 -0.5 -0.5 0.5 -0.5 -0.5;
	setAttr -s 12 ".ed[0:11]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0;
	setAttr -s 6 -ch 24 ".fc[0:5]" -type "polyFaces" 
		f 4 0 5 -2 -5
		mu 0 4 0 1 3 2
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".dr" 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube4";
	rename -uid "37CE800E-474E-5E29-9B84-50A4BB30277B";
	setAttr ".t" -type "double3" 0.19518180046051903 7.8252393559936433 0.13613472305044638 ;
	setAttr ".s" -type "double3" 13.529520637952762 1.2531218485875926 22.046018931292988 ;
createNode mesh -n "pCubeShape4" -p "pCube4";
	rename -uid "D1E042AA-415B-C97E-A933-13877EB54E98";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.5 0.5 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode mesh -n "polySurfaceShape3" -p "pCube4";
	rename -uid "5B588016-4E65-E209-A71C-80996B3D343A";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 14 ".uvst[0].uvsp[0:13]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 8 ".vt[0:7]"  -0.5 -0.5 0.5 0.5 -0.5 0.5 -0.5 0.5 0.5 0.5 0.5 0.5
		 -0.5 0.5 -0.5 0.5 0.5 -0.5 -0.5 -0.5 -0.5 0.5 -0.5 -0.5;
	setAttr -s 12 ".ed[0:11]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0;
	setAttr -s 6 -ch 24 ".fc[0:5]" -type "polyFaces" 
		f 4 0 5 -2 -5
		mu 0 4 0 1 3 2
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".dr" 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube5";
	rename -uid "41836696-41D2-EF24-35A7-87A25E247603";
	setAttr ".t" -type "double3" 7.2849844275211773 7.0851383220943962 0 ;
	setAttr ".s" -type "double3" 0.80436443429052418 2.2465315369033751 1.9441642808978465 ;
createNode mesh -n "pCubeShape5" -p "pCube5";
	rename -uid "8BEA506C-4632-87FD-FF2C-96B0E3B1A182";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.49999998509883881 0.43137508630752563 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pTorus1";
	rename -uid "739F5A34-4302-C49F-0316-3AA946DC7D19";
	setAttr ".t" -type "double3" 7.0991712127937516 8.1783537648257649 0 ;
	setAttr -av ".tx";
	setAttr -av ".ty";
	setAttr ".r" -type "double3" 1.2944088476350331 -1.4529531078193607 98.930008024399172 ;
	setAttr -av ".rx";
	setAttr -av ".ry";
	setAttr -av ".rz";
	setAttr ".s" -type "double3" 0.65182945360557165 0.71903680304394335 0.67094470684406826 ;
	setAttr -av ".sx";
	setAttr -av ".sy";
	setAttr -av ".sz";
createNode mesh -n "pTorusShape1" -p "pTorus1";
	rename -uid "D8412521-45D1-B51B-892F-C3B8B3A6FED6";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pTorus2";
	rename -uid "36134373-410C-65A5-9A8A-03989969B5D1";
	setAttr ".t" -type "double3" 0 6.5159158676447246 -11.326993877432145 ;
	setAttr ".r" -type "double3" 90 0 90 ;
	setAttr ".s" -type "double3" 0.43627228015049313 0.23399602791128871 0.43627228015049313 ;
createNode mesh -n "pTorusShape2" -p "pTorus2";
	rename -uid "7BB9EC21-4DC9-6D20-9403-E3925218F462";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube6";
	rename -uid "77686D7B-495E-D0E9-A1D7-38BA544B6F78";
	setAttr ".t" -type "double3" 0 7.8497935242880308 -11.249102241226963 ;
	setAttr ".s" -type "double3" 3.3319117658417787 1 0.51652501644667903 ;
createNode mesh -n "pCubeShape6" -p "pCube6";
	rename -uid "197DBB54-4C91-CC47-8C83-E8870959716E";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pTorus3";
	rename -uid "3F4B8494-4463-ABA0-98E7-929851FC8426";
	setAttr ".t" -type "double3" 0 6.5159158676447246 10.946583922882544 ;
	setAttr ".r" -type "double3" 90 0 90 ;
	setAttr ".s" -type "double3" 0.43627228015049313 0.23399602791128871 0.43627228015049313 ;
createNode mesh -n "pTorusShape3" -p "pTorus3";
	rename -uid "B8595F41-47E6-B084-D8D7-988AF3605CAF";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 81 ".uvst[0].uvsp[0:80]" -type "float2" 0 1 0.125 1 0.25
		 1 0.375 1 0.5 1 0.625 1 0.75 1 0.875 1 1 1 0 0.875 0.125 0.875 0.25 0.875 0.375 0.875
		 0.5 0.875 0.625 0.875 0.75 0.875 0.875 0.875 1 0.875 0 0.75 0.125 0.75 0.25 0.75
		 0.375 0.75 0.5 0.75 0.625 0.75 0.75 0.75 0.875 0.75 1 0.75 0 0.625 0.125 0.625 0.25
		 0.625 0.375 0.625 0.5 0.625 0.625 0.625 0.75 0.625 0.875 0.625 1 0.625 0 0.5 0.125
		 0.5 0.25 0.5 0.375 0.5 0.5 0.5 0.625 0.5 0.75 0.5 0.875 0.5 1 0.5 0 0.375 0.125 0.375
		 0.25 0.375 0.375 0.375 0.5 0.375 0.625 0.375 0.75 0.375 0.875 0.375 1 0.375 0 0.25
		 0.125 0.25 0.25 0.25 0.375 0.25 0.5 0.25 0.625 0.25 0.75 0.25 0.875 0.25 1 0.25 0
		 0.125 0.125 0.125 0.25 0.125 0.375 0.125 0.5 0.125 0.625 0.125 0.75 0.125 0.875 0.125
		 1 0.125 0 0 0.125 0 0.25 0 0.375 0 0.5 0 0.625 0 0.75 0 0.875 0 1 0;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 64 ".vt[0:63]"  2.20467854 0 -2.20467854 0 0 -3.11788607
		 -2.20467854 0 -2.20467854 -3.11788607 0 0 -2.20467854 0 2.20467854 0 0 3.1178863
		 2.20467877 0 2.20467877 3.11788654 0 0 2.30823183 0.35355338 -2.30823183 0 0.35355338 -3.26433277
		 -2.30823183 0.35355338 -2.30823183 -3.26433277 0.35355338 0 -2.30823183 0.35355338 2.30823183
		 0 0.35355338 3.26433301 2.30823207 0.35355338 2.30823207 3.26433325 0.35355338 0
		 2.55823183 0.49999997 -2.55823183 0 0.49999997 -3.61788607 -2.55823183 0.49999997 -2.55823183
		 -3.61788607 0.49999997 0 -2.55823183 0.49999997 2.55823183 0 0.49999997 3.6178863
		 2.55823207 0.49999997 2.55823207 3.61788654 0.49999997 0 2.80823183 0.35355335 -2.80823183
		 0 0.35355335 -3.97143936 -2.80823183 0.35355335 -2.80823183 -3.97143936 0.35355335 0
		 -2.80823183 0.35355335 2.80823183 0 0.35355335 3.9714396 2.80823207 0.35355335 2.80823207
		 3.97143984 0.35355335 0 2.91178513 0 -2.91178513 0 0 -4.11788607 -2.91178513 0 -2.91178513
		 -4.11788607 0 0 -2.91178513 0 2.91178513 0 0 4.11788607 2.91178536 0 2.91178536 4.11788654 0 0
		 2.80823183 -0.35355335 -2.80823183 0 -0.35355335 -3.97143936 -2.80823183 -0.35355335 -2.80823183
		 -3.97143936 -0.35355335 0 -2.80823183 -0.35355335 2.80823183 0 -0.35355335 3.9714396
		 2.80823207 -0.35355335 2.80823207 3.97143984 -0.35355335 0 2.55823183 -0.49999994 -2.55823183
		 0 -0.49999994 -3.61788607 -2.55823183 -0.49999994 -2.55823183 -3.61788607 -0.49999994 0
		 -2.55823183 -0.49999994 2.55823183 0 -0.49999994 3.6178863 2.55823207 -0.49999994 2.55823207
		 3.61788654 -0.49999994 0 2.30823183 -0.35355335 -2.30823183 0 -0.35355335 -3.26433277
		 -2.30823183 -0.35355335 -2.30823183 -3.26433277 -0.35355335 0 -2.30823183 -0.35355335 2.30823183
		 0 -0.35355335 3.26433301 2.30823207 -0.35355335 2.30823207 3.26433325 -0.35355335 0;
	setAttr -s 128 ".ed[0:127]"  0 1 0 1 2 0 2 3 0 3 4 0 4 5 0 5 6 0 6 7 0
		 7 0 0 8 9 0 9 10 0 10 11 0 11 12 0 12 13 0 13 14 0 14 15 0 15 8 0 16 17 0 17 18 0
		 18 19 0 19 20 0 20 21 0 21 22 0 22 23 0 23 16 0 24 25 0 25 26 0 26 27 0 27 28 0 28 29 0
		 29 30 0 30 31 0 31 24 0 32 33 0 33 34 0 34 35 0 35 36 0 36 37 0 37 38 0 38 39 0 39 32 0
		 40 41 0 41 42 0 42 43 0 43 44 0 44 45 0 45 46 0 46 47 0 47 40 0 48 49 0 49 50 0 50 51 0
		 51 52 0 52 53 0 53 54 0 54 55 0 55 48 0 56 57 0 57 58 0 58 59 0 59 60 0 60 61 0 61 62 0
		 62 63 0 63 56 0 0 8 0 1 9 0 2 10 0 3 11 0 4 12 0 5 13 0 6 14 0 7 15 0 8 16 0 9 17 0
		 10 18 0 11 19 0 12 20 0 13 21 0 14 22 0 15 23 0 16 24 0 17 25 0 18 26 0 19 27 0 20 28 0
		 21 29 0 22 30 0 23 31 0 24 32 0 25 33 0 26 34 0 27 35 0 28 36 0 29 37 0 30 38 0 31 39 0
		 32 40 0 33 41 0 34 42 0 35 43 0 36 44 0 37 45 0 38 46 0 39 47 0 40 48 0 41 49 0 42 50 0
		 43 51 0 44 52 0 45 53 0 46 54 0 47 55 0 48 56 0 49 57 0 50 58 0 51 59 0 52 60 0 53 61 0
		 54 62 0 55 63 0 56 0 0 57 1 0 58 2 0 59 3 0 60 4 0 61 5 0 62 6 0 63 7 0;
	setAttr -s 64 -ch 256 ".fc[0:63]" -type "polyFaces" 
		f 4 -1 64 8 -66
		mu 0 4 1 0 9 10
		f 4 -2 65 9 -67
		mu 0 4 2 1 10 11
		f 4 -3 66 10 -68
		mu 0 4 3 2 11 12
		f 4 -4 67 11 -69
		mu 0 4 4 3 12 13
		f 4 -5 68 12 -70
		mu 0 4 5 4 13 14
		f 4 -6 69 13 -71
		mu 0 4 6 5 14 15
		f 4 -7 70 14 -72
		mu 0 4 7 6 15 16
		f 4 -8 71 15 -65
		mu 0 4 8 7 16 17
		f 4 -9 72 16 -74
		mu 0 4 10 9 18 19
		f 4 -10 73 17 -75
		mu 0 4 11 10 19 20
		f 4 -11 74 18 -76
		mu 0 4 12 11 20 21
		f 4 -12 75 19 -77
		mu 0 4 13 12 21 22
		f 4 -13 76 20 -78
		mu 0 4 14 13 22 23
		f 4 -14 77 21 -79
		mu 0 4 15 14 23 24
		f 4 -15 78 22 -80
		mu 0 4 16 15 24 25
		f 4 -16 79 23 -73
		mu 0 4 17 16 25 26
		f 4 -17 80 24 -82
		mu 0 4 19 18 27 28
		f 4 -18 81 25 -83
		mu 0 4 20 19 28 29
		f 4 -19 82 26 -84
		mu 0 4 21 20 29 30
		f 4 -20 83 27 -85
		mu 0 4 22 21 30 31
		f 4 -21 84 28 -86
		mu 0 4 23 22 31 32
		f 4 -22 85 29 -87
		mu 0 4 24 23 32 33
		f 4 -23 86 30 -88
		mu 0 4 25 24 33 34
		f 4 -24 87 31 -81
		mu 0 4 26 25 34 35
		f 4 -25 88 32 -90
		mu 0 4 28 27 36 37
		f 4 -26 89 33 -91
		mu 0 4 29 28 37 38
		f 4 -27 90 34 -92
		mu 0 4 30 29 38 39
		f 4 -28 91 35 -93
		mu 0 4 31 30 39 40
		f 4 -29 92 36 -94
		mu 0 4 32 31 40 41
		f 4 -30 93 37 -95
		mu 0 4 33 32 41 42
		f 4 -31 94 38 -96
		mu 0 4 34 33 42 43
		f 4 -32 95 39 -89
		mu 0 4 35 34 43 44
		f 4 -33 96 40 -98
		mu 0 4 37 36 45 46
		f 4 -34 97 41 -99
		mu 0 4 38 37 46 47
		f 4 -35 98 42 -100
		mu 0 4 39 38 47 48
		f 4 -36 99 43 -101
		mu 0 4 40 39 48 49
		f 4 -37 100 44 -102
		mu 0 4 41 40 49 50
		f 4 -38 101 45 -103
		mu 0 4 42 41 50 51
		f 4 -39 102 46 -104
		mu 0 4 43 42 51 52
		f 4 -40 103 47 -97
		mu 0 4 44 43 52 53
		f 4 -41 104 48 -106
		mu 0 4 46 45 54 55
		f 4 -42 105 49 -107
		mu 0 4 47 46 55 56
		f 4 -43 106 50 -108
		mu 0 4 48 47 56 57
		f 4 -44 107 51 -109
		mu 0 4 49 48 57 58
		f 4 -45 108 52 -110
		mu 0 4 50 49 58 59
		f 4 -46 109 53 -111
		mu 0 4 51 50 59 60
		f 4 -47 110 54 -112
		mu 0 4 52 51 60 61
		f 4 -48 111 55 -105
		mu 0 4 53 52 61 62
		f 4 -49 112 56 -114
		mu 0 4 55 54 63 64
		f 4 -50 113 57 -115
		mu 0 4 56 55 64 65
		f 4 -51 114 58 -116
		mu 0 4 57 56 65 66
		f 4 -52 115 59 -117
		mu 0 4 58 57 66 67
		f 4 -53 116 60 -118
		mu 0 4 59 58 67 68
		f 4 -54 117 61 -119
		mu 0 4 60 59 68 69
		f 4 -55 118 62 -120
		mu 0 4 61 60 69 70
		f 4 -56 119 63 -113
		mu 0 4 62 61 70 71
		f 4 -57 120 0 -122
		mu 0 4 64 63 72 73
		f 4 -58 121 1 -123
		mu 0 4 65 64 73 74
		f 4 -59 122 2 -124
		mu 0 4 66 65 74 75
		f 4 -60 123 3 -125
		mu 0 4 67 66 75 76
		f 4 -61 124 4 -126
		mu 0 4 68 67 76 77
		f 4 -62 125 5 -127
		mu 0 4 69 68 77 78
		f 4 -63 126 6 -128
		mu 0 4 70 69 78 79
		f 4 -64 127 7 -121
		mu 0 4 71 70 79 80;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCube7";
	rename -uid "26A5F10D-4555-4788-BFF0-FCB29BB77FE9";
	setAttr ".t" -type "double3" 0 7.8497935242880308 11.024475559087726 ;
	setAttr ".s" -type "double3" 3.3319117658417787 1 0.51652501644667903 ;
createNode mesh -n "pCubeShape7" -p "pCube7";
	rename -uid "2BB191AE-4AAD-03F1-964C-A0B63A4B6C6A";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode mesh -n "polySurfaceShape5" -p "pCube7";
	rename -uid "25BF179C-4190-9DD8-5859-8085560EDBA3";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 14 ".uvst[0].uvsp[0:13]" -type "float2" 0.375 0 0.625 0 0.375
		 0.25 0.625 0.25 0.375 0.5 0.625 0.5 0.375 0.75 0.625 0.75 0.375 1 0.625 1 0.875 0
		 0.875 0.25 0.125 0 0.125 0.25;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 8 ".vt[0:7]"  -0.5 -0.5 0.5 0.5 -0.5 0.5 -0.5 0.5 0.5 0.5 0.5 0.5
		 -0.5 0.5 -0.5 0.5 0.5 -0.5 -0.5 -0.5 -0.5 0.5 -0.5 -0.5;
	setAttr -s 12 ".ed[0:11]"  0 1 0 2 3 0 4 5 0 6 7 0 0 2 0 1 3 0 2 4 0
		 3 5 0 4 6 0 5 7 0 6 0 0 7 1 0;
	setAttr -s 6 -ch 24 ".fc[0:5]" -type "polyFaces" 
		f 4 0 5 -2 -5
		mu 0 4 0 1 3 2
		f 4 1 7 -3 -7
		mu 0 4 2 3 5 4
		f 4 2 9 -4 -9
		mu 0 4 4 5 7 6
		f 4 3 11 -1 -11
		mu 0 4 6 7 9 8
		f 4 -12 -10 -8 -6
		mu 0 4 1 10 11 3
		f 4 10 4 6 8
		mu 0 4 12 0 2 13;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCylinder3";
	rename -uid "C6BCE735-4AD0-29E9-CCEC-BE9D9260EB09";
	setAttr ".t" -type "double3" -6.5060509841100442 8.3469917586821083 3.7142242187918839 ;
	setAttr ".r" -type "double3" 90 0 0 ;
	setAttr ".s" -type "double3" 0.48988195827704406 1.3976359699972838 0.65021388352770848 ;
createNode mesh -n "pCylinderShape3" -p "pCylinder3";
	rename -uid "1861E3C0-4FA1-93AB-CF04-11A1E6284662";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode transform -n "pCylinder4";
	rename -uid "9F750A78-455B-85C0-8EF9-B0AD244C7EC3";
	setAttr ".t" -type "double3" -6.5060509841100442 8.3469917586821083 -3.9531514201334446 ;
	setAttr ".r" -type "double3" 90 0 0 ;
	setAttr ".s" -type "double3" 0.48988195827704406 1.3976359699972838 0.65021388352770848 ;
createNode mesh -n "pCylinderShape4" -p "pCylinder4";
	rename -uid "5E1985D0-40D5-1641-BF1B-84AEA02E260B";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode mesh -n "polySurfaceShape2" -p "pCylinder4";
	rename -uid "A51E4F2A-4EE8-E856-ADF2-2085C608E225";
	setAttr -k off ".v";
	setAttr ".io" yes;
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 36 ".uvst[0].uvsp[0:35]" -type "float2" 0.61048543 0.04576458
		 0.5 1.4901161e-08 0.38951457 0.04576458 0.34375 0.15625 0.38951457 0.26673543 0.5
		 0.3125 0.61048543 0.26673543 0.65625 0.15625 0.375 0.3125 0.40625 0.3125 0.4375 0.3125
		 0.46875 0.3125 0.5 0.3125 0.53125 0.3125 0.5625 0.3125 0.59375 0.3125 0.625 0.3125
		 0.375 0.68843985 0.40625 0.68843985 0.4375 0.68843985 0.46875 0.68843985 0.5 0.68843985
		 0.53125 0.68843985 0.5625 0.68843985 0.59375 0.68843985 0.625 0.68843985 0.61048543
		 0.73326457 0.5 0.6875 0.38951457 0.73326457 0.34375 0.84375 0.38951457 0.95423543
		 0.5 1 0.61048543 0.95423543 0.65625 0.84375 0.5 0.15000001 0.5 0.83749998;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 18 ".vt[0:17]"  0.70710671 -1 -0.70710671 0 -1 -0.99999988
		 -0.70710671 -1 -0.70710671 -0.99999988 -1 0 -0.70710671 -1 0.70710671 0 -1 0.99999994
		 0.70710677 -1 0.70710677 1 -1 0 0.70710671 1 -0.70710671 0 1 -0.99999988 -0.70710671 1 -0.70710671
		 -0.99999988 1 0 -0.70710671 1 0.70710671 0 1 0.99999994 0.70710677 1 0.70710677 1 1 0
		 0 -1 0 0 1 0;
	setAttr -s 40 ".ed[0:39]"  0 1 0 1 2 0 2 3 0 3 4 0 4 5 0 5 6 0 6 7 0
		 7 0 0 8 9 0 9 10 0 10 11 0 11 12 0 12 13 0 13 14 0 14 15 0 15 8 0 0 8 0 1 9 0 2 10 0
		 3 11 0 4 12 0 5 13 0 6 14 0 7 15 0 16 0 1 16 1 1 16 2 1 16 3 1 16 4 1 16 5 1 16 6 1
		 16 7 1 8 17 1 9 17 1 10 17 1 11 17 1 12 17 1 13 17 1 14 17 1 15 17 1;
	setAttr -s 24 -ch 80 ".fc[0:23]" -type "polyFaces" 
		f 4 0 17 -9 -17
		mu 0 4 8 9 18 17
		f 4 1 18 -10 -18
		mu 0 4 9 10 19 18
		f 4 2 19 -11 -19
		mu 0 4 10 11 20 19
		f 4 3 20 -12 -20
		mu 0 4 11 12 21 20
		f 4 4 21 -13 -21
		mu 0 4 12 13 22 21
		f 4 5 22 -14 -22
		mu 0 4 13 14 23 22
		f 4 6 23 -15 -23
		mu 0 4 14 15 24 23
		f 4 7 16 -16 -24
		mu 0 4 15 16 25 24
		f 3 -1 -25 25
		mu 0 3 1 0 34
		f 3 -2 -26 26
		mu 0 3 2 1 34
		f 3 -3 -27 27
		mu 0 3 3 2 34
		f 3 -4 -28 28
		mu 0 3 4 3 34
		f 3 -5 -29 29
		mu 0 3 5 4 34
		f 3 -6 -30 30
		mu 0 3 6 5 34
		f 3 -7 -31 31
		mu 0 3 7 6 34
		f 3 -8 -32 24
		mu 0 3 0 7 34
		f 3 8 33 -33
		mu 0 3 32 31 35
		f 3 9 34 -34
		mu 0 3 31 30 35
		f 3 10 35 -35
		mu 0 3 30 29 35
		f 3 11 36 -36
		mu 0 3 29 28 35
		f 3 12 37 -37
		mu 0 3 28 27 35
		f 3 13 38 -38
		mu 0 3 27 26 35
		f 3 14 39 -39
		mu 0 3 26 33 35
		f 3 15 32 -40
		mu 0 3 33 32 35;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode lightLinker -s -n "lightLinker1";
	rename -uid "8F8FD2D2-41B1-2F31-B0BA-8789A2D56F20";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "6EC9AF4E-4603-2346-A16D-C7B68A695545";
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "194735A1-4D37-F6FA-85B5-77A80584E8F9";
createNode displayLayerManager -n "layerManager";
	rename -uid "17A937DC-4703-DB2A-22D6-7AA1F98A000E";
createNode displayLayer -n "defaultLayer";
	rename -uid "09F02267-4E6D-7BA3-DE24-6C8FE8B52C42";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "1B9E17BC-499D-9BB4-74F7-DE918300A2FA";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "1A89FC64-439A-1D9B-DC5A-709075C43B40";
	setAttr ".g" yes;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "803E8042-4729-C262-D309-45AD0EA41A05";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n"
		+ "            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n"
		+ "            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n"
		+ "            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 374\n            -height 251\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n"
		+ "            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n"
		+ "            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 374\n            -height 251\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n"
		+ "            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n"
		+ "            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 374\n            -height 251\n"
		+ "            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 1\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n"
		+ "            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 755\n            -height 546\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n"
		+ "            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n"
		+ "            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n"
		+ "            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n"
		+ "                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 1\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n"
		+ "                -showActiveCurveNames 0\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                -valueLinesToggle 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n"
		+ "                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n"
		+ "                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n"
		+ "                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n"
		+ "                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n"
		+ "                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"Stereo\" (localizedPanelLabel(\"Stereo\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Stereo\")) -mbv $menusOkayInPanels  $panelName;\nstring $editorName = ($panelName+\"Editor\");\n            stereoCameraView -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -holdOuts 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n"
		+ "                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -depthOfFieldPreview 1\n                -maxConstantTransparency 1\n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 4 4 \n                -bumpResolution 4 4 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 0\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n"
		+ "                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -controllers 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n"
		+ "                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                -captureSequenceNumber -1\n                -width 0\n                -height 0\n                -sceneRenderFilter 0\n                -displayMode \"centerEye\" \n                -viewColor 0 0 0 1 \n                -useCustomBackground 1\n                $editorName;\n            stereoCameraView -e -viewSelected 0 $editorName;\n            stereoCameraView -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n"
		+ "                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -highlightConnections 0\n                -copyConnectionsOnPaste 0\n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n"
		+ "                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 1\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 755\\n    -height 546\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 1\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 755\\n    -height 546\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "01FC8AA7-4FA2-C14F-DFFE-F28909D685A8";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode polyCube -n "polyCube1";
	rename -uid "5724CE71-4ABF-206A-3B3D-66A6335F339C";
	setAttr ".cuv" 4;
createNode polyCube -n "polyCube2";
	rename -uid "D968B381-4D55-8DFF-7B14-9C91EB1505F4";
	setAttr ".cuv" 4;
createNode polySplitRing -n "polySplitRing1";
	rename -uid "3D676305-4C07-7703-6C9B-95809157E8B8";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[24:35]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.098937275861096907 3.8279117619788909 -0.10047146071013557 1;
	setAttr ".wt" 0.037837471812963486;
	setAttr ".re" 33;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing2";
	rename -uid "5BD39522-434C-F254-C595-8FA028563C55";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[60:61]" "e[63]" "e[65]" "e[67]" "e[69]" "e[71]" "e[73]" "e[75]" "e[77]" "e[79]" "e[81]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.098937275861096907 3.8279117619788909 -0.10047146071013557 1;
	setAttr ".wt" 0.24042455852031708;
	setAttr ".re" 60;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing3";
	rename -uid "F0276EF9-4B9F-2A0B-3355-A38CDD5CEBFC";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[84:85]" "e[87]" "e[89]" "e[91]" "e[93]" "e[95]" "e[97]" "e[99]" "e[101]" "e[103]" "e[105]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.098937275861096907 3.8279117619788909 -0.10047146071013557 1;
	setAttr ".wt" 0.10078243166208267;
	setAttr ".re" 84;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing4";
	rename -uid "70EC165A-408B-F3E0-488F-7E8CED66562B";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[108:109]" "e[111]" "e[113]" "e[115]" "e[117]" "e[119]" "e[121]" "e[123]" "e[125]" "e[127]" "e[129]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.098937275861096907 3.8279117619788909 -0.10047146071013557 1;
	setAttr ".wt" 0.46890616416931152;
	setAttr ".re" 108;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing5";
	rename -uid "819F018E-4188-81F2-A655-479B1DF0E0DB";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[132:133]" "e[135]" "e[137]" "e[139]" "e[141]" "e[143]" "e[145]" "e[147]" "e[149]" "e[151]" "e[153]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.098937275861096907 3.8279117619788909 -0.10047146071013557 1;
	setAttr ".wt" 0.22292369604110718;
	setAttr ".re" 132;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing6";
	rename -uid "FED39C9D-47F6-0239-AED2-AAA916851A04";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[156:157]" "e[159]" "e[161]" "e[163]" "e[165]" "e[167]" "e[169]" "e[171]" "e[173]" "e[175]" "e[177]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.098937275861096907 3.8279117619788909 -0.10047146071013557 1;
	setAttr ".wt" 0.85002344846725464;
	setAttr ".dr" no;
	setAttr ".re" 156;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	rename -uid "5FC0CD7D-4165-BC74-D84D-B9A988F00EEE";
	setAttr ".ics" -type "componentList" 1 "f[0:11]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.10920227817362083 20.863404632117433 -0.10047146071013557 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.10920228 20.863405 9.7112341 ;
	setAttr ".rs" 40949;
	setAttr ".lt" -type "double3" -1.0547118733938987e-15 -1.9604222954931627e-15 0.5 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.6563408662362384 15.110734942333581 9.3253846867777295 ;
	setAttr ".cbx" -type "double3" 6.8747454225834801 26.616074321901291 10.09708355594117 ;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	rename -uid "DC930017-492B-2BF1-92E1-D5ADC35E853C";
	setAttr ".ics" -type "componentList" 1 "f[72:83]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.10920227817362083 20.863404632117433 -0.10047146071013557 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.10920248 20.863405 -3.9730704 ;
	setAttr ".rs" 61374;
	setAttr ".lt" -type "double3" 0 0 0.5 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.6563404629784424 15.110734942333579 -4.7664988007331202 ;
	setAttr ".cbx" -type "double3" 6.8747454225834801 26.616074321901287 -3.1796418148355756 ;
createNode polyExtrudeFace -n "polyExtrudeFace3";
	rename -uid "3190A2F1-49C7-999D-ED63-D0B50717A5BE";
	setAttr ".ics" -type "componentList" 1 "f[48:59]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.10920227817362083 20.863404632117433 -0.10047146071013557 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.10920268 20.863405 3.8563297 ;
	setAttr ".rs" 54779;
	setAttr ".lt" -type "double3" 1.1657341758564144e-15 -5.5777539149553731e-18 0.5 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.6563400597206464 15.110734942333579 3.1052254781887378 ;
	setAttr ".cbx" -type "double3" 6.8747454225834801 26.616074321901287 4.6074338788757538 ;
createNode polyExtrudeFace -n "polyExtrudeFace4";
	rename -uid "1F8F5CDB-412E-58D0-DF6E-42A166A6C248";
	setAttr ".ics" -type "componentList" 1 "f[96:107]";
	setAttr ".ix" -type "matrix" 6.7655431444098593 0 0 0 0 -2.2643120748736098e-15 -10.197555016651304 0
		 0 5.7526696897838541 -1.2773492685322583e-15 0 0.10920227817362083 20.863404632117433 -0.10047146071013557 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.10920288 20.863405 -9.8832264 ;
	setAttr ".rs" 65361;
	setAttr ".lt" -type "double3" 0 0 0.5 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.6563396564628512 15.110734942333576 -10.298026477361441 ;
	setAttr ".cbx" -type "double3" 6.8747454225834801 26.616074321901287 -9.46842591053605 ;
createNode polySplitRing -n "polySplitRing7";
	rename -uid "9E767A3A-479D-07B7-4F78-30927AAEF089";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[6:7]" "e[10:11]";
	setAttr ".ix" -type "matrix" 13.473211357172762 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0 5.011089327882174 60.891592903944904 1;
	setAttr ".wt" 0.284303218126297;
	setAttr ".re" 7;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyTweak -n "polyTweak1";
	rename -uid "B0F0647B-4B3E-89C5-B7EC-36AA2863543E";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[0:7]" -type "float3"  0 0 -1.091389537 0 0 -1.091389537
		 0 0 -1.091389537 0 0 -1.091389537 0 0 -1.091389537 0 0 -1.091389537 0 0 -1.091389537
		 0 0 -1.091389537;
createNode polySplitRing -n "polySplitRing8";
	rename -uid "5CDAFDE8-4CC8-641B-CFC3-648ADA5FCB1D";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[10:13]";
	setAttr ".ix" -type "matrix" 13.473211357172762 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0 5.011089327882174 60.891592903944904 1;
	setAttr ".wt" 0.10349801182746887;
	setAttr ".re" 12;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing9";
	rename -uid "04972D9B-4B6B-62F0-4254-6A88EFAD3EF0";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[10:11]" "e[20:21]";
	setAttr ".ix" -type "matrix" 13.473211357172762 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0 5.011089327882174 60.891592903944904 1;
	setAttr ".wt" 0.45129916071891785;
	setAttr ".re" 20;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing10";
	rename -uid "F0AAEC56-461F-1BAB-815E-E5A3A4C2F4F4";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[10:11]" "e[28:29]";
	setAttr ".ix" -type "matrix" 13.473211357172762 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0 5.011089327882174 60.891592903944904 1;
	setAttr ".wt" 0.20636852085590363;
	setAttr ".re" 28;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing11";
	rename -uid "ADF80843-4FE9-502A-13B0-218EFDC8201F";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[6:7]" "e[15]" "e[17]";
	setAttr ".ix" -type "matrix" 13.473211357172762 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0 5.011089327882174 60.891592903944904 1;
	setAttr ".wt" 0.15455614030361176;
	setAttr ".re" 7;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing12";
	rename -uid "4A1CB30E-489B-7397-C79A-36BD5EF7F83D";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[10:11]" "e[36:37]";
	setAttr ".ix" -type "matrix" 13.473211357172762 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0 5.011089327882174 60.891592903944904 1;
	setAttr ".wt" 0.83489948511123657;
	setAttr ".dr" no;
	setAttr ".re" 36;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyExtrudeFace -n "polyExtrudeFace5";
	rename -uid "70D01DF8-40DC-D430-60CB-2896A976F68D";
	setAttr ".ics" -type "componentList" 4 "f[1]" "f[3:9]" "f[14:17]" "f[26:29]";
	setAttr ".ix" -type "matrix" 13.473211357172762 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0 5.011089327882174 60.891592903944904 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 5.0110893 38.230534 ;
	setAttr ".rs" 60950;
	setAttr ".lt" -type "double3" 0 3.1123909543847933e-16 1.4016962742398658 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.7366056785863808 1.3533306361703548 27.848788858928351 ;
	setAttr ".cbx" -type "double3" 6.7366056785863808 8.6688480195939928 48.612280939103549 ;
createNode polyCube -n "polyCube3";
	rename -uid "4752488F-4DA1-6E2B-F67C-A98DE871BBCC";
	setAttr ".cuv" 4;
createNode polyTorus -n "polyTorus1";
	rename -uid "C0307047-47E6-CDB1-7140-B38A3E9D81C9";
	setAttr ".sa" 8;
	setAttr ".sh" 8;
createNode animCurveTU -n "pTorus1_scaleX";
	rename -uid "01959BC1-4E52-0DC2-2F64-7E9D97D20A16";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 1;
createNode animCurveTU -n "pTorus1_scaleY";
	rename -uid "7AB611F9-4E1A-F6C3-AB6B-71A6B94B0FF6";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 1;
createNode animCurveTU -n "pTorus1_scaleZ";
	rename -uid "881D0B18-4621-FD42-D502-D5B309A692F0";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 1;
createNode animCurveTU -n "pTorus1_visibility";
	rename -uid "D2712FAD-433A-DCFC-A97A-AAB69A0C0CFB";
	setAttr ".tan" 9;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 1;
	setAttr ".kot[0]"  5;
createNode animCurveTL -n "pTorus1_translateX";
	rename -uid "B3CC4590-4552-3407-2830-88B4114C14AA";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 0;
createNode animCurveTL -n "pTorus1_translateY";
	rename -uid "5646F385-4291-035E-1B45-A49A1A57A18C";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 8.3699229485781252;
createNode animCurveTL -n "pTorus1_translateZ";
	rename -uid "576781A7-450C-CCEF-A08B-BA911A4B5D32";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 0;
createNode animCurveTA -n "pTorus1_rotateX";
	rename -uid "09234FBC-4EA0-FFC4-03D1-6EACE56FDA9F";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 0;
createNode animCurveTA -n "pTorus1_rotateY";
	rename -uid "6304F97D-425B-2B9E-5974-008A5DB6473B";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 0;
createNode animCurveTA -n "pTorus1_rotateZ";
	rename -uid "83ED1129-4384-0FD7-3CA1-A7889B978360";
	setAttr ".tan" 18;
	setAttr ".wgt" no;
	setAttr ".ktv[0]"  1 0;
createNode polySplitRing -n "polySplitRing13";
	rename -uid "3F2CF439-4BE8-C2AB-9E5D-2F950ABE10FA";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[6:7]" "e[10:11]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.35190483927726746;
	setAttr ".re" 7;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing14";
	rename -uid "007DB3C0-4C50-3E98-D6F7-18A424893C24";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[10:13]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.40162432193756104;
	setAttr ".dr" no;
	setAttr ".re" 12;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing15";
	rename -uid "B1503A7B-44EC-ABCB-5A3C-D2B2143A9382";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[4:5]" "e[8:9]" "e[16]" "e[19]" "e[24]" "e[27]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.72954094409942627;
	setAttr ".dr" no;
	setAttr ".re" 27;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing16";
	rename -uid "1119FCCD-48A5-F6A4-41D1-A88C84A1F30D";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 7 "e[4:5]" "e[19]" "e[27]" "e[35]" "e[37]" "e[39]" "e[41]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.65314584970474243;
	setAttr ".dr" no;
	setAttr ".re" 27;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing17";
	rename -uid "3170733B-41A1-F242-65EE-6EAD459D3687";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 7 "e[12:13]" "e[23]" "e[25]" "e[30]" "e[38]" "e[46]" "e[54]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.7572091817855835;
	setAttr ".dr" no;
	setAttr ".re" 12;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing18";
	rename -uid "D9D49983-4641-89E3-16F9-C59EB23A7840";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 7 "e[12:13]" "e[38]" "e[54]" "e[67]" "e[69]" "e[71]" "e[73]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.17249748110771179;
	setAttr ".re" 12;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing19";
	rename -uid "2DDCA115-42FA-5B64-2C64-B39FF44B8274";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[4:5]" "e[19]" "e[27]" "e[51]" "e[53]" "e[55]" "e[57]" "e[68]" "e[72]" "e[84]" "e[88]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.74161636829376221;
	setAttr ".dr" no;
	setAttr ".re" 5;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyExtrudeFace -n "polyExtrudeFace6";
	rename -uid "64042F68-4CB1-8751-1FFA-AC810CE6EB97";
	setAttr ".ics" -type "componentList" 1 "f[57]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.38104194639156486 5.011089327882174 22.435504060780367 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.85847062 5.0110893 -5.1596894 ;
	setAttr ".rs" 37646;
	setAttr ".lt" -type "double3" 1.7937895246507235e-17 -2.0722848767617926e-17 -0.16921490165201902 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -5.7977890299351671 0.65248239656978591 -10.607299984236185 ;
	setAttr ".cbx" -type "double3" -5.2155434833658036 9.3696962591945621 -10.607299984236185 ;
createNode polyExtrudeFace -n "polyExtrudeFace7";
	rename -uid "9323BB9B-4C9C-517A-C726-0CB79B1BA867";
	setAttr ".ics" -type "componentList" 4 "f[22]" "f[36]" "f[44]" "f[55]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0.85847062 5.0110893 -5.1596894 ;
	setAttr ".rs" 48137;
	setAttr ".lt" -type "double3" 1.7937895246507235e-17 -2.0722848767617926e-17 -0.16921490165201902 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" 7.5147303099005178 6.7557467007985785 -0.21812674283528619 ;
	setAttr ".cbx" -type "double3" 7.5147303099005178 7.6008092920243318 0.28792129268065697 ;
createNode polyTweak -n "polyTweak2";
	rename -uid "724DA1B2-437A-774E-9064-A0B294281EF4";
	setAttr ".uopa" yes;
	setAttr -s 12 ".tk";
	setAttr ".tk[32]" -type "float3" 0 5.5511151e-17 -0.020003494 ;
	setAttr ".tk[33]" -type "float3" 0 5.5511151e-17 -0.020003494 ;
	setAttr ".tk[34]" -type "float3" 0 0 -0.020003494 ;
	setAttr ".tk[35]" -type "float3" 0 0 -0.020003494 ;
	setAttr ".tk[36]" -type "float3" 0 -5.5511151e-17 -0.020003494 ;
	setAttr ".tk[37]" -type "float3" 0 -5.5511151e-17 -0.020003494 ;
	setAttr ".tk[38]" -type "float3" 0 0 -0.020003494 ;
	setAttr ".tk[39]" -type "float3" 0 0 -0.020003494 ;
	setAttr ".tk[52]" -type "float3" 0 0 -0.020003494 ;
	setAttr ".tk[57]" -type "float3" 0 0 -0.020003494 ;
createNode polyTorus -n "polyTorus2";
	rename -uid "519837A9-4CB0-F1BE-359E-F1A5982B4CC0";
	setAttr ".r" 3.6178866334592668;
	setAttr ".sa" 8;
	setAttr ".sh" 8;
createNode polyCube -n "polyCube4";
	rename -uid "725DFAE8-4902-2F49-2F75-B6ACF6281C38";
	setAttr ".cuv" 4;
createNode polyCylinder -n "polyCylinder1";
	rename -uid "2B863090-4B1C-988A-A991-E385C262172B";
	setAttr ".sa" 8;
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode polySplitRing -n "polySplitRing20";
	rename -uid "07DD55FF-483F-3FEE-40B6-839B5456D4D3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[16:23]";
	setAttr ".ix" -type "matrix" 0.48988195827704406 0 0 0 0 3.1033752678705979e-16 1.3976359699972838 0
		 0 -0.65021388352770848 1.4437648488468035e-16 0 -6.5060509841100442 8.3469917586821083 -3.9531514201334446 1;
	setAttr ".wt" 0.088732145726680756;
	setAttr ".re" 18;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing21";
	rename -uid "E25C749A-45F2-4940-AC40-ABACCC3B690C";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 7 "e[40:41]" "e[43]" "e[45]" "e[47]" "e[49]" "e[51]" "e[53]";
	setAttr ".ix" -type "matrix" 0.48988195827704406 0 0 0 0 3.1033752678705979e-16 1.3976359699972838 0
		 0 -0.65021388352770848 1.4437648488468035e-16 0 -6.5060509841100442 8.3469917586821083 -3.9531514201334446 1;
	setAttr ".wt" 0.91917097568511963;
	setAttr ".dr" no;
	setAttr ".re" 40;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing22";
	rename -uid "912253B5-4E02-0101-79D1-19B499CFAE44";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[16:23]";
	setAttr ".ix" -type "matrix" 0.48988195827704406 0 0 0 0 3.1033752678705979e-16 1.3976359699972838 0
		 0 -0.65021388352770848 1.4437648488468035e-16 0 -6.5060509841100442 8.3469917586821083 3.7142242187918839 1;
	setAttr ".wt" 0.063888087868690491;
	setAttr ".re" 18;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing23";
	rename -uid "FF248BB8-4C33-A90E-82F0-9FAA42F568F5";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 7 "e[40:41]" "e[43]" "e[45]" "e[47]" "e[49]" "e[51]" "e[53]";
	setAttr ".ix" -type "matrix" 0.48988195827704406 0 0 0 0 3.1033752678705979e-16 1.3976359699972838 0
		 0 -0.65021388352770848 1.4437648488468035e-16 0 -6.5060509841100442 8.3469917586821083 3.7142242187918839 1;
	setAttr ".wt" 0.91404682397842407;
	setAttr ".dr" no;
	setAttr ".re" 40;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing24";
	rename -uid "5C5EAFB5-48A4-558C-0C0F-6E9B068F0425";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[4:5]" "e[8:9]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 9.1799536426170665 0 1;
	setAttr ".wt" 0.8638572096824646;
	setAttr ".dr" no;
	setAttr ".re" 4;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing25";
	rename -uid "D0460D5E-4CCC-22EC-6A65-698204C82DD9";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[4:5]" "e[13]" "e[15]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 9.1799536426170665 0 1;
	setAttr ".wt" 0.16993537545204163;
	setAttr ".re" 4;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing26";
	rename -uid "ADE5D71E-46B7-B4B7-8483-1B944F6E1A4A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[6:7]" "e[10:11]" "e[14]" "e[18]" "e[22]" "e[26]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 9.1799536426170665 0 1;
	setAttr ".wt" 0.011852089315652847;
	setAttr ".re" 14;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing27";
	rename -uid "155A614B-457C-498E-73E8-858285415D17";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[10:11]" "e[18]" "e[26]" "e[28:29]" "e[39]" "e[41]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 9.1799536426170665 0 1;
	setAttr ".wt" 0.99137014150619507;
	setAttr ".dr" no;
	setAttr ".re" 28;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing28";
	rename -uid "26CFD7A4-4FB6-2657-6401-13BCB6E9B7DF";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[340]" "e[343]" "e[348]" "e[353]" "e[358]" "e[363]" "e[368]" "e[373]" "e[378]" "e[383]" "e[388]" "e[393]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.8515666127204895;
	setAttr ".dr" no;
	setAttr ".re" 353;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing29";
	rename -uid "D7411F00-4C29-ABEF-E41A-4F81EE27CD92";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[340]" "e[343]" "e[348]" "e[353]" "e[358]" "e[363]" "e[368]" "e[373]" "e[378]" "e[383]" "e[388]" "e[393]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.3179747462272644;
	setAttr ".re" 353;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing30";
	rename -uid "670D953A-4168-5053-8C96-769EEE0A907B";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[120:121]" "e[123]" "e[125]" "e[127]" "e[129]" "e[131]" "e[133]" "e[135]" "e[137]" "e[139]" "e[141]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.95911306142807007;
	setAttr ".dr" no;
	setAttr ".re" 125;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing31";
	rename -uid "8BA27CA0-44C0-A73C-B7EE-26838B2EF217";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[120:121]" "e[123]" "e[125]" "e[127]" "e[129]" "e[131]" "e[133]" "e[135]" "e[137]" "e[139]" "e[141]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.046340979635715485;
	setAttr ".re" 123;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing32";
	rename -uid "01C379C2-4FC2-10FC-A43F-FEB5FE9A5FC1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[220]" "e[223]" "e[228]" "e[233]" "e[238]" "e[243]" "e[248]" "e[253]" "e[258]" "e[263]" "e[268]" "e[273]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.87941575050354004;
	setAttr ".dr" no;
	setAttr ".re" 228;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing33";
	rename -uid "9980E88C-4CC7-1E3F-9FCB-7D879F0B7461";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[220]" "e[223]" "e[228]" "e[233]" "e[238]" "e[243]" "e[248]" "e[253]" "e[258]" "e[263]" "e[268]" "e[273]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.16317294538021088;
	setAttr ".re" 228;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing34";
	rename -uid "88CAFE4A-4F89-0E4D-4A43-82BAE03771B0";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[84:85]" "e[87]" "e[89]" "e[91]" "e[93]" "e[95]" "e[97]" "e[99]" "e[101]" "e[103]" "e[105]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.96749389171600342;
	setAttr ".dr" no;
	setAttr ".re" 89;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing35";
	rename -uid "7855DE2D-4C16-477C-B23E-7A8D9760FD5E";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[84:85]" "e[87]" "e[89]" "e[91]" "e[93]" "e[95]" "e[97]" "e[99]" "e[101]" "e[103]" "e[105]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.029466858133673668;
	setAttr ".re" 89;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing36";
	rename -uid "BBEEA1A2-4A99-E6C4-738D-0C86460A86FD";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[280]" "e[283]" "e[288]" "e[293]" "e[298]" "e[303]" "e[308]" "e[313]" "e[318]" "e[323]" "e[328]" "e[333]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.84763902425765991;
	setAttr ".dr" no;
	setAttr ".re" 293;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing37";
	rename -uid "D34B194E-482F-D6C4-C303-61A9FF0DDDFA";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[280]" "e[283]" "e[288]" "e[293]" "e[298]" "e[303]" "e[308]" "e[313]" "e[318]" "e[323]" "e[328]" "e[333]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.14099796116352081;
	setAttr ".re" 293;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing38";
	rename -uid "7B4DC760-46C9-F0F0-7A0B-06A9A2FD0B26";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[48:49]" "e[51]" "e[53]" "e[55]" "e[57]" "e[59]" "e[61]" "e[63]" "e[65]" "e[67]" "e[69]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.96336382627487183;
	setAttr ".dr" no;
	setAttr ".re" 53;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing39";
	rename -uid "F9A35F47-4B21-2F5B-5CEB-1F9B02723E27";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[160]" "e[163]" "e[167]" "e[172]" "e[177]" "e[182]" "e[187]" "e[192]" "e[197]" "e[202]" "e[207]" "e[212]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.15159124135971069;
	setAttr ".re" 187;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing40";
	rename -uid "6E2892AE-4ADA-50D3-E437-B3A0ACEF4606";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[660:661]" "e[663]" "e[665]" "e[667]" "e[669]" "e[671]" "e[673]" "e[675]" "e[677]" "e[679]" "e[681]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.72071307897567749;
	setAttr ".dr" no;
	setAttr ".re" 660;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing41";
	rename -uid "4803EB39-4198-00B6-A4BE-5281B262446A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[48:49]" "e[51]" "e[53]" "e[55]" "e[57]" "e[59]" "e[61]" "e[63]" "e[65]" "e[67]" "e[69]";
	setAttr ".ix" -type "matrix" 5.4599828791765761 0 0 0 0 -2.2025536881820902e-15 -9.9194199693603728 0
		 0 5.7526696897838505 -1.2773492685322575e-15 0 0.29915489257219718 9.7763774722247732 -0.15217651994303516 1;
	setAttr ".wt" 0.030388787388801575;
	setAttr ".re" 53;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing42";
	rename -uid "24884745-4A07-24A7-6D57-56A519CF09DD";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[6:7]" "e[10:11]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 7.8252393559936433 0 1;
	setAttr ".wt" 0.013384453020989895;
	setAttr ".re" 7;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing43";
	rename -uid "862C7BC8-49F8-13BB-373B-1AA471DBD7D2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[10:13]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 7.8252393559936433 0 1;
	setAttr ".wt" 0.98810052871704102;
	setAttr ".dr" no;
	setAttr ".re" 12;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing44";
	rename -uid "EC076FC1-4FF1-8C6A-88CD-56BCE9D34F7D";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[4:5]" "e[8:9]" "e[16]" "e[19]" "e[24]" "e[27]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 7.8252393559936433 0 1;
	setAttr ".wt" 0.16005368530750275;
	setAttr ".re" 9;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing45";
	rename -uid "D2250EC5-4FC1-9489-233E-BD9CD9325BEF";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 5 "e[4:5]" "e[19]" "e[27:29]" "e[31]" "e[33]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.2531218485875926 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 7.8252393559936433 0 1;
	setAttr ".wt" 0.77395695447921753;
	setAttr ".dr" no;
	setAttr ".re" 28;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing46";
	rename -uid "EF277DBE-4A8D-BB58-EA8B-B89FE1FF6E41";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[108]" "e[111]" "e[114]" "e[120]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.85778146982192993;
	setAttr ".dr" no;
	setAttr ".re" 108;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing47";
	rename -uid "0AC9069F-4E29-2285-4030-09A5395CDC0A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[108]" "e[111]" "e[135]" "e[137]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.20258663594722748;
	setAttr ".re" 108;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing48";
	rename -uid "FB4BA52E-4298-7935-C22C-AABE4446E93F";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[26:27]" "e[39]" "e[41]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.97284334897994995;
	setAttr ".dr" no;
	setAttr ".re" 26;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing49";
	rename -uid "4176E260-4B61-50E0-8638-BCBA926CBC9A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[26:27]" "e[151]" "e[153]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.036187857389450073;
	setAttr ".re" 26;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing50";
	rename -uid "32457E5A-4CB5-76EB-3085-F3BC81732CD8";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[88]" "e[91]" "e[94]" "e[100]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.88246333599090576;
	setAttr ".dr" no;
	setAttr ".re" 88;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing51";
	rename -uid "1793DA52-4A5C-455A-B615-2DAAC1B90B18";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[88]" "e[91]" "e[167]" "e[169]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.13034656643867493;
	setAttr ".re" 88;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing52";
	rename -uid "9674F017-44BB-6109-B0D9-C6968BAC6614";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[14:15]" "e[21]" "e[23]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.96404790878295898;
	setAttr ".dr" no;
	setAttr ".re" 14;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing53";
	rename -uid "46E09896-43C8-C11F-A735-7A8EE8C4D53A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[14:15]" "e[183]" "e[185]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.029499216005206108;
	setAttr ".re" 14;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing54";
	rename -uid "043F6F1F-4E40-AD53-F07C-C18E316F1362";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[68]" "e[71]" "e[74]" "e[80]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.87198340892791748;
	setAttr ".dr" no;
	setAttr ".re" 68;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing55";
	rename -uid "256082A7-455C-685F-361F-08A6659A57BA";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[68]" "e[71]" "e[199]" "e[201]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.15462367236614227;
	setAttr ".re" 68;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing56";
	rename -uid "2A669563-48D6-633A-2CE7-F584DB5FB39F";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[9]" "e[11]" "e[32:33]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.96030580997467041;
	setAttr ".dr" no;
	setAttr ".re" 32;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing57";
	rename -uid "D6BD9D37-4058-66B9-227C-079217BAF856";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[32:33]" "e[215]" "e[217]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.040657907724380493;
	setAttr ".re" 32;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing58";
	rename -uid "CE635D62-4CF7-7D76-0C58-1C8448DA8684";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[48]" "e[51]" "e[56]" "e[59]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.80807507038116455;
	setAttr ".dr" no;
	setAttr ".re" 48;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing59";
	rename -uid "D1802CBE-489F-D83E-5B05-0396457582F5";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 4 "e[48]" "e[51]" "e[231]" "e[233]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.22531075775623322;
	setAttr ".re" 48;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing60";
	rename -uid "BDA2EB7B-4829-3ACF-9656-84B19AA33714";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 50 "e[4:7]" "e[10]" "e[13]" "e[17]" "e[19]" "e[22]" "e[25]" "e[29]" "e[31]" "e[35]" "e[37]" "e[40]" "e[43]" "e[60:63]" "e[75:76]" "e[82:83]" "e[95:96]" "e[102:103]" "e[115:116]" "e[122:123]" "e[126]" "e[130]" "e[136]" "e[139]" "e[144]" "e[147]" "e[152]" "e[155]" "e[160]" "e[163]" "e[168]" "e[171]" "e[176]" "e[179]" "e[184]" "e[187]" "e[192]" "e[195]" "e[200]" "e[203]" "e[208]" "e[211]" "e[216]" "e[219]" "e[224]" "e[227]" "e[232]" "e[235]" "e[240]" "e[243]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.96845078468322754;
	setAttr ".dr" no;
	setAttr ".re" 219;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing61";
	rename -uid "86561E1E-4A8A-EBF3-E92D-AB8D3B015185";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 57 "e[4:5]" "e[13]" "e[19]" "e[25]" "e[31]" "e[37]" "e[43]" "e[60:62]" "e[82:83]" "e[102:103]" "e[122]" "e[139]" "e[147]" "e[155]" "e[163]" "e[171]" "e[179]" "e[187]" "e[195]" "e[203]" "e[211]" "e[219]" "e[227]" "e[235]" "e[243]" "e[263]" "e[265]" "e[267]" "e[269]" "e[271]" "e[273]" "e[275]" "e[277]" "e[279]" "e[281]" "e[283]" "e[285]" "e[287]" "e[289]" "e[291]" "e[293]" "e[295]" "e[297]" "e[299]" "e[301]" "e[303]" "e[305]" "e[307]" "e[309]" "e[311]" "e[313]" "e[315]" "e[317]" "e[319]" "e[321]" "e[323]" "e[325]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 47.482493265823123 1;
	setAttr ".wt" 0.031919609755277634;
	setAttr ".re" 219;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing62";
	rename -uid "6FE3B6DC-4B5C-0FDE-E1E0-F0887204C3B6";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 2 "e[6:7]" "e[10:11]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.6325799086660597 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 1.1120896177292519 0 1;
	setAttr ".wt" 0.01146400161087513;
	setAttr ".re" 7;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing63";
	rename -uid "8CEBBC73-4451-084E-A3D6-D9BA6ADC9D83";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[10:13]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.6325799086660597 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 1.1120896177292519 0 1;
	setAttr ".wt" 0.99214655160903931;
	setAttr ".dr" no;
	setAttr ".re" 12;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing64";
	rename -uid "DEA6922B-4582-BE09-C645-C5A1034323CC";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[4:5]" "e[8:9]" "e[16]" "e[19]" "e[24]" "e[27]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.6325799086660597 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 1.1120896177292519 0 1;
	setAttr ".wt" 0.89105838537216187;
	setAttr ".dr" no;
	setAttr ".re" 27;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing65";
	rename -uid "298C007D-4334-9653-C4A8-35A836C713EF";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 7 "e[4:5]" "e[19]" "e[27]" "e[35]" "e[37]" "e[39]" "e[41]";
	setAttr ".ix" -type "matrix" 13.529520637952762 0 0 0 0 1.6325799086660597 0 0 0 0 22.046018931292988 0
		 0.19518180046051903 1.1120896177292519 0 1;
	setAttr ".wt" 0.10404646396636963;
	setAttr ".re" 27;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing66";
	rename -uid "E57491B0-407E-0069-0872-17AC73AA93A2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 15 "e[0:3]" "e[14]" "e[18]" "e[22]" "e[26]" "e[34]" "e[42]" "e[50]" "e[58]" "e[62]" "e[70]" "e[76]" "e[84]" "e[91]" "e[103]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.089647844433784485;
	setAttr ".re" 103;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing67";
	rename -uid "EFA67144-4EF9-1B3B-D044-CC84075F210A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 17 "e[14]" "e[22]" "e[34]" "e[50]" "e[62]" "e[76]" "e[91]" "e[136:137]" "e[139]" "e[141]" "e[143]" "e[145]" "e[147]" "e[155]" "e[165]" "e[167]" "e[169]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.88858777284622192;
	setAttr ".dr" no;
	setAttr ".re" 136;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing68";
	rename -uid "9B71A290-4C89-6B15-F13D-DD927FAFBB56";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 12 "e[10:11]" "e[20:21]" "e[40]" "e[43]" "e[56]" "e[59]" "e[101]" "e[105]" "e[140]" "e[166]" "e[176]" "e[202]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.14279079437255859;
	setAttr ".re" 59;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing69";
	rename -uid "631A1AB1-415C-0F30-7A6C-728E0DDDA78C";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 13 "e[6:7]" "e[15]" "e[17]" "e[32]" "e[36]" "e[48]" "e[52]" "e[93]" "e[112]" "e[148]" "e[158]" "e[184]" "e[194]";
	setAttr ".ix" -type "matrix" 1.2157062966014889 0 0 0 0 2.2465315369033751 0 0 0 0 1.9441642808978465 0
		 6.9068771615997733 7.0851383220943962 0 1;
	setAttr ".wt" 0.9255642294883728;
	setAttr ".dr" no;
	setAttr ".re" 48;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing70";
	rename -uid "E86F3E27-4FC8-DE6F-AB19-CFB76BF9FCBB";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[0:3]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 -11.249102241226963 1;
	setAttr ".wt" 0.96204245090484619;
	setAttr ".dr" no;
	setAttr ".re" 2;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing71";
	rename -uid "684B1170-4EEB-B206-D96F-A8BC61309CB3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[0:3]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 -11.249102241226963 1;
	setAttr ".wt" 0.039635520428419113;
	setAttr ".re" 2;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing72";
	rename -uid "DC63F968-40B8-4122-B01E-ED9409C98447";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[4:5]" "e[8:9]" "e[14]" "e[18]" "e[22]" "e[26]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 -11.249102241226963 1;
	setAttr ".wt" 0.089660003781318665;
	setAttr ".re" 22;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing73";
	rename -uid "7CD8920C-4A87-D37D-608E-52961E63168B";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[4:5]" "e[18]" "e[26]" "e[28:29]" "e[39]" "e[41]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 -11.249102241226963 1;
	setAttr ".wt" 0.86338073015213013;
	setAttr ".dr" no;
	setAttr ".re" 28;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing74";
	rename -uid "03BD4C04-4BC6-CEA1-2889-0B8838C68E3A";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[0:3]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 11.024475559087726 1;
	setAttr ".wt" 0.048753809183835983;
	setAttr ".re" 1;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing75";
	rename -uid "92D95A34-43B9-0E1D-6959-C199B784CD21";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[12:13]" "e[15]" "e[17]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 11.024475559087726 1;
	setAttr ".wt" 0.95960032939910889;
	setAttr ".dr" no;
	setAttr ".re" 12;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing76";
	rename -uid "98D64D64-45B8-D83D-BC2B-A684E86AB9E5";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 6 "e[4:5]" "e[8:9]" "e[16]" "e[19]" "e[24]" "e[27]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 11.024475559087726 1;
	setAttr ".wt" 0.87097686529159546;
	setAttr ".dr" no;
	setAttr ".re" 27;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing77";
	rename -uid "5191CF4F-4C1E-81B6-20C8-D1BCB0018963";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 7 "e[4:5]" "e[19]" "e[27]" "e[33]" "e[35]" "e[37]" "e[39]";
	setAttr ".ix" -type "matrix" 3.3319117658417787 0 0 0 0 1 0 0 0 0 0.51652501644667903 0
		 0 7.8497935242880308 11.024475559087726 1;
	setAttr ".wt" 0.13218456506729126;
	setAttr ".re" 27;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing78";
	rename -uid "3BD82C5E-4FE3-45F0-5AE4-398B4CB58ECB";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 61 "e[0:3]" "e[8]" "e[12]" "e[16]" "e[18]" "e[20]" "e[24]" "e[28]" "e[30]" "e[34]" "e[36]" "e[38]" "e[42]" "e[46]" "e[50]" "e[54]" "e[58]" "e[66]" "e[70]" "e[78]" "e[81]" "e[86]" "e[90]" "e[98]" "e[101]" "e[106]" "e[110]" "e[118]" "e[121]" "e[134]" "e[138]" "e[142]" "e[146]" "e[150]" "e[154]" "e[158]" "e[162]" "e[166]" "e[170]" "e[174]" "e[178]" "e[182]" "e[186]" "e[190]" "e[194]" "e[198]" "e[202]" "e[206]" "e[210]" "e[214]" "e[218]" "e[222]" "e[226]" "e[230]" "e[234]" "e[238]" "e[242]" "e[260]" "e[324]" "e[384]" "e[448]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 -38.33293538514333 1;
	setAttr ".wt" 0.97219693660736084;
	setAttr ".dr" no;
	setAttr ".re" 448;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polySplitRing -n "polySplitRing79";
	rename -uid "9D8BA030-4D00-5091-4F4D-F495A2D96E8E";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 61 "e[0:3]" "e[12]" "e[18]" "e[24]" "e[30]" "e[36]" "e[42]" "e[46]" "e[54]" "e[58]" "e[78]" "e[81]" "e[98]" "e[101]" "e[110]" "e[118]" "e[121]" "e[138]" "e[146]" "e[154]" "e[162]" "e[170]" "e[178]" "e[186]" "e[194]" "e[202]" "e[210]" "e[218]" "e[226]" "e[234]" "e[242]" "e[324]" "e[448]" "e[553]" "e[555]" "e[561]" "e[563]" "e[565]" "e[567]" "e[569]" "e[571]" "e[573]" "e[575]" "e[577]" "e[579]" "e[581]" "e[583]" "e[585]" "e[587]" "e[589]" "e[591]" "e[593]" "e[595]" "e[597]" "e[599]" "e[601]" "e[603]" "e[605]" "e[607]" "e[609]" "e[611]";
	setAttr ".ix" -type "matrix" 11.193170859514737 0 0 0 0 7.3155173834236384 0 0 0 0 20.763492080175194 0
		 0.28699774705393466 5.011089327882174 -38.33293538514333 1;
	setAttr ".wt" 0.023018887266516685;
	setAttr ".re" 448;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
	setAttr ".fprt" yes;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 4 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :initialShadingGroup;
	setAttr -s 13 ".dsm";
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultRenderGlobals;
	setAttr ".ren" -type "string" "arnold";
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "polySplitRing79.out" "pCubeShape1.i";
connectAttr "polySplitRing27.out" "pCubeShape2.i";
connectAttr "polySplitRing41.out" "pCylinderShape2.i";
connectAttr "polySplitRing65.out" "pCubeShape3.i";
connectAttr "polySplitRing45.out" "pCubeShape4.i";
connectAttr "polySplitRing69.out" "pCubeShape5.i";
connectAttr "pTorus1_scaleX.o" "pTorus1.sx";
connectAttr "pTorus1_scaleY.o" "pTorus1.sy";
connectAttr "pTorus1_scaleZ.o" "pTorus1.sz";
connectAttr "pTorus1_visibility.o" "pTorus1.v";
connectAttr "pTorus1_translateX.o" "pTorus1.tx";
connectAttr "pTorus1_translateY.o" "pTorus1.ty";
connectAttr "pTorus1_translateZ.o" "pTorus1.tz";
connectAttr "pTorus1_rotateX.o" "pTorus1.rx";
connectAttr "pTorus1_rotateY.o" "pTorus1.ry";
connectAttr "pTorus1_rotateZ.o" "pTorus1.rz";
connectAttr "polyTorus1.out" "pTorusShape1.i";
connectAttr "polyTorus2.out" "pTorusShape2.i";
connectAttr "polySplitRing73.out" "pCubeShape6.i";
connectAttr "polySplitRing77.out" "pCubeShape7.i";
connectAttr "polySplitRing23.out" "pCylinderShape3.i";
connectAttr "polySplitRing21.out" "pCylinderShape4.i";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "polySurfaceShape1.o" "polySplitRing1.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing1.mp";
connectAttr "polySplitRing1.out" "polySplitRing2.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing2.mp";
connectAttr "polySplitRing2.out" "polySplitRing3.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing3.mp";
connectAttr "polySplitRing3.out" "polySplitRing4.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing4.mp";
connectAttr "polySplitRing4.out" "polySplitRing5.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing5.mp";
connectAttr "polySplitRing5.out" "polySplitRing6.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing6.mp";
connectAttr "polySplitRing6.out" "polyExtrudeFace1.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace1.mp";
connectAttr "polyExtrudeFace1.out" "polyExtrudeFace2.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace2.out" "polyExtrudeFace3.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace3.mp";
connectAttr "polyExtrudeFace3.out" "polyExtrudeFace4.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace4.mp";
connectAttr "polyTweak1.out" "polySplitRing7.ip";
connectAttr "pCubeShape1.wm" "polySplitRing7.mp";
connectAttr "polyCube1.out" "polyTweak1.ip";
connectAttr "polySplitRing7.out" "polySplitRing8.ip";
connectAttr "pCubeShape1.wm" "polySplitRing8.mp";
connectAttr "polySplitRing8.out" "polySplitRing9.ip";
connectAttr "pCubeShape1.wm" "polySplitRing9.mp";
connectAttr "polySplitRing9.out" "polySplitRing10.ip";
connectAttr "pCubeShape1.wm" "polySplitRing10.mp";
connectAttr "polySplitRing10.out" "polySplitRing11.ip";
connectAttr "pCubeShape1.wm" "polySplitRing11.mp";
connectAttr "polySplitRing11.out" "polySplitRing12.ip";
connectAttr "pCubeShape1.wm" "polySplitRing12.mp";
connectAttr "polySplitRing12.out" "polyExtrudeFace5.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace5.mp";
connectAttr "polyCube3.out" "polySplitRing13.ip";
connectAttr "pCubeShape5.wm" "polySplitRing13.mp";
connectAttr "polySplitRing13.out" "polySplitRing14.ip";
connectAttr "pCubeShape5.wm" "polySplitRing14.mp";
connectAttr "polySplitRing14.out" "polySplitRing15.ip";
connectAttr "pCubeShape5.wm" "polySplitRing15.mp";
connectAttr "polySplitRing15.out" "polySplitRing16.ip";
connectAttr "pCubeShape5.wm" "polySplitRing16.mp";
connectAttr "polySplitRing16.out" "polySplitRing17.ip";
connectAttr "pCubeShape5.wm" "polySplitRing17.mp";
connectAttr "polySplitRing17.out" "polySplitRing18.ip";
connectAttr "pCubeShape5.wm" "polySplitRing18.mp";
connectAttr "polySplitRing18.out" "polySplitRing19.ip";
connectAttr "pCubeShape5.wm" "polySplitRing19.mp";
connectAttr "polyExtrudeFace5.out" "polyExtrudeFace6.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace6.mp";
connectAttr "polyTweak2.out" "polyExtrudeFace7.ip";
connectAttr "pCubeShape5.wm" "polyExtrudeFace7.mp";
connectAttr "polySplitRing19.out" "polyTweak2.ip";
connectAttr "polySurfaceShape2.o" "polySplitRing20.ip";
connectAttr "pCylinderShape4.wm" "polySplitRing20.mp";
connectAttr "polySplitRing20.out" "polySplitRing21.ip";
connectAttr "pCylinderShape4.wm" "polySplitRing21.mp";
connectAttr "polyCylinder1.out" "polySplitRing22.ip";
connectAttr "pCylinderShape3.wm" "polySplitRing22.mp";
connectAttr "polySplitRing22.out" "polySplitRing23.ip";
connectAttr "pCylinderShape3.wm" "polySplitRing23.mp";
connectAttr "polyCube2.out" "polySplitRing24.ip";
connectAttr "pCubeShape2.wm" "polySplitRing24.mp";
connectAttr "polySplitRing24.out" "polySplitRing25.ip";
connectAttr "pCubeShape2.wm" "polySplitRing25.mp";
connectAttr "polySplitRing25.out" "polySplitRing26.ip";
connectAttr "pCubeShape2.wm" "polySplitRing26.mp";
connectAttr "polySplitRing26.out" "polySplitRing27.ip";
connectAttr "pCubeShape2.wm" "polySplitRing27.mp";
connectAttr "polyExtrudeFace4.out" "polySplitRing28.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing28.mp";
connectAttr "polySplitRing28.out" "polySplitRing29.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing29.mp";
connectAttr "polySplitRing29.out" "polySplitRing30.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing30.mp";
connectAttr "polySplitRing30.out" "polySplitRing31.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing31.mp";
connectAttr "polySplitRing31.out" "polySplitRing32.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing32.mp";
connectAttr "polySplitRing32.out" "polySplitRing33.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing33.mp";
connectAttr "polySplitRing33.out" "polySplitRing34.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing34.mp";
connectAttr "polySplitRing34.out" "polySplitRing35.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing35.mp";
connectAttr "polySplitRing35.out" "polySplitRing36.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing36.mp";
connectAttr "polySplitRing36.out" "polySplitRing37.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing37.mp";
connectAttr "polySplitRing37.out" "polySplitRing38.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing38.mp";
connectAttr "polySplitRing38.out" "polySplitRing39.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing39.mp";
connectAttr "polySplitRing39.out" "polySplitRing40.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing40.mp";
connectAttr "polySplitRing40.out" "polySplitRing41.ip";
connectAttr "pCylinderShape2.wm" "polySplitRing41.mp";
connectAttr "polySurfaceShape3.o" "polySplitRing42.ip";
connectAttr "pCubeShape4.wm" "polySplitRing42.mp";
connectAttr "polySplitRing42.out" "polySplitRing43.ip";
connectAttr "pCubeShape4.wm" "polySplitRing43.mp";
connectAttr "polySplitRing43.out" "polySplitRing44.ip";
connectAttr "pCubeShape4.wm" "polySplitRing44.mp";
connectAttr "polySplitRing44.out" "polySplitRing45.ip";
connectAttr "pCubeShape4.wm" "polySplitRing45.mp";
connectAttr "polyExtrudeFace6.out" "polySplitRing46.ip";
connectAttr "pCubeShape1.wm" "polySplitRing46.mp";
connectAttr "polySplitRing46.out" "polySplitRing47.ip";
connectAttr "pCubeShape1.wm" "polySplitRing47.mp";
connectAttr "polySplitRing47.out" "polySplitRing48.ip";
connectAttr "pCubeShape1.wm" "polySplitRing48.mp";
connectAttr "polySplitRing48.out" "polySplitRing49.ip";
connectAttr "pCubeShape1.wm" "polySplitRing49.mp";
connectAttr "polySplitRing49.out" "polySplitRing50.ip";
connectAttr "pCubeShape1.wm" "polySplitRing50.mp";
connectAttr "polySplitRing50.out" "polySplitRing51.ip";
connectAttr "pCubeShape1.wm" "polySplitRing51.mp";
connectAttr "polySplitRing51.out" "polySplitRing52.ip";
connectAttr "pCubeShape1.wm" "polySplitRing52.mp";
connectAttr "polySplitRing52.out" "polySplitRing53.ip";
connectAttr "pCubeShape1.wm" "polySplitRing53.mp";
connectAttr "polySplitRing53.out" "polySplitRing54.ip";
connectAttr "pCubeShape1.wm" "polySplitRing54.mp";
connectAttr "polySplitRing54.out" "polySplitRing55.ip";
connectAttr "pCubeShape1.wm" "polySplitRing55.mp";
connectAttr "polySplitRing55.out" "polySplitRing56.ip";
connectAttr "pCubeShape1.wm" "polySplitRing56.mp";
connectAttr "polySplitRing56.out" "polySplitRing57.ip";
connectAttr "pCubeShape1.wm" "polySplitRing57.mp";
connectAttr "polySplitRing57.out" "polySplitRing58.ip";
connectAttr "pCubeShape1.wm" "polySplitRing58.mp";
connectAttr "polySplitRing58.out" "polySplitRing59.ip";
connectAttr "pCubeShape1.wm" "polySplitRing59.mp";
connectAttr "polySplitRing59.out" "polySplitRing60.ip";
connectAttr "pCubeShape1.wm" "polySplitRing60.mp";
connectAttr "polySplitRing60.out" "polySplitRing61.ip";
connectAttr "pCubeShape1.wm" "polySplitRing61.mp";
connectAttr "polySurfaceShape4.o" "polySplitRing62.ip";
connectAttr "pCubeShape3.wm" "polySplitRing62.mp";
connectAttr "polySplitRing62.out" "polySplitRing63.ip";
connectAttr "pCubeShape3.wm" "polySplitRing63.mp";
connectAttr "polySplitRing63.out" "polySplitRing64.ip";
connectAttr "pCubeShape3.wm" "polySplitRing64.mp";
connectAttr "polySplitRing64.out" "polySplitRing65.ip";
connectAttr "pCubeShape3.wm" "polySplitRing65.mp";
connectAttr "polyExtrudeFace7.out" "polySplitRing66.ip";
connectAttr "pCubeShape5.wm" "polySplitRing66.mp";
connectAttr "polySplitRing66.out" "polySplitRing67.ip";
connectAttr "pCubeShape5.wm" "polySplitRing67.mp";
connectAttr "polySplitRing67.out" "polySplitRing68.ip";
connectAttr "pCubeShape5.wm" "polySplitRing68.mp";
connectAttr "polySplitRing68.out" "polySplitRing69.ip";
connectAttr "pCubeShape5.wm" "polySplitRing69.mp";
connectAttr "polyCube4.out" "polySplitRing70.ip";
connectAttr "pCubeShape6.wm" "polySplitRing70.mp";
connectAttr "polySplitRing70.out" "polySplitRing71.ip";
connectAttr "pCubeShape6.wm" "polySplitRing71.mp";
connectAttr "polySplitRing71.out" "polySplitRing72.ip";
connectAttr "pCubeShape6.wm" "polySplitRing72.mp";
connectAttr "polySplitRing72.out" "polySplitRing73.ip";
connectAttr "pCubeShape6.wm" "polySplitRing73.mp";
connectAttr "polySurfaceShape5.o" "polySplitRing74.ip";
connectAttr "pCubeShape7.wm" "polySplitRing74.mp";
connectAttr "polySplitRing74.out" "polySplitRing75.ip";
connectAttr "pCubeShape7.wm" "polySplitRing75.mp";
connectAttr "polySplitRing75.out" "polySplitRing76.ip";
connectAttr "pCubeShape7.wm" "polySplitRing76.mp";
connectAttr "polySplitRing76.out" "polySplitRing77.ip";
connectAttr "pCubeShape7.wm" "polySplitRing77.mp";
connectAttr "polySplitRing61.out" "polySplitRing78.ip";
connectAttr "pCubeShape1.wm" "polySplitRing78.mp";
connectAttr "polySplitRing78.out" "polySplitRing79.ip";
connectAttr "pCubeShape1.wm" "polySplitRing79.mp";
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "pCubeShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape2.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape2.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape3.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape4.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape5.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pTorusShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pTorusShape2.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape6.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pTorusShape3.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape7.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape3.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape4.iog" ":initialShadingGroup.dsm" -na;
// End of TreasureChest_Model.ma
