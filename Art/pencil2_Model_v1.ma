//Maya ASCII 2018 scene
//Name: pencil2_Model_v1.ma
//Last modified: Sat, Jan 30, 2021 01:27:55 AM
//Codeset: 936
requires maya "2018";
currentUnit -l meter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2018";
fileInfo "version" "2018";
fileInfo "cutIdentifier" "201706261615-f9658c4cfc";
fileInfo "osv" "Microsoft Windows 8 Home Premium Edition, 64-bit  (Build 9200)\n";
fileInfo "license" "student";
createNode transform -s -n "persp";
	rename -uid "150B6EE9-46FB-B835-C8B2-558FCA247148";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -3.9403347733186211 -2.3804088086409791 -0.25226823159667222 ;
	setAttr ".r" -type "double3" 507.26164726968591 80.999999999991942 0 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "D2F5AD77-4A5A-968C-9F47-99ACC9EAC2F7";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 4.4725395492250719;
	setAttr ".ow" 0.1;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
	setAttr ".ai_translator" -type "string" "perspective";
createNode transform -s -n "top";
	rename -uid "05E6A653-483D-B2BD-A182-1FBC53B3BEC1";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 10.001000000000001 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "BE4D4BAC-4E2B-DE32-AE80-B7987E2EC9F0";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 10.001000000000001;
	setAttr ".ow" 0.3;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "front";
	rename -uid "4B9DA580-4322-B820-0C2F-648FEF6D9944";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 10.001000000000001 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "5B0BE5BC-4259-75F5-24F2-0AA92DE4131E";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 10.001000000000001;
	setAttr ".ow" 0.3;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "side";
	rename -uid "B11DE44B-4A9A-4592-E0E8-C2B7F0F98A99";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 10.001000000000001 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "4DA8303D-481E-CC66-AFBB-A2B9F4B2468F";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 10.001000000000001;
	setAttr ".ow" 0.3;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -n "pCylinder3";
	rename -uid "B467D100-48C5-581D-2002-058D45735D18";
	setAttr ".rp" -type "double3" 0 1.0878689913340623e-08 -0.016730261477055849 ;
	setAttr ".sp" -type "double3" 0 1.0878689913340623e-08 -0.016730261477055849 ;
createNode mesh -n "pCylinder3Shape" -p "pCylinder3";
	rename -uid "1C649AC9-4FE0-26E8-21E3-34BAEE777B9E";
	setAttr -k off ".v";
	setAttr ".iog[0].og[0].gcl" -type "componentList" 1 "f[0:137]";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 166 ".uvst[0].uvsp[0:165]" -type "float2" 0.57812506 0.020933539
		 0.42187503 0.020933509 0.34375 0.15624997 0.421875 0.29156646 0.578125 0.29156649
		 0.65625 0.15625 0.375 0.3125 0.41666666 0.3125 0.45833331 0.3125 0.49999997 0.3125
		 0.54166663 0.3125 0.58333331 0.3125 0.625 0.3125 0.375 0.68843985 0.41666666 0.68843985
		 0.45833331 0.68843985 0.49999997 0.68843985 0.54166663 0.68843985 0.58333331 0.68843985
		 0.625 0.68843985 0.57812506 0.70843351 0.42187503 0.70843351 0.34375 0.84375 0.421875
		 0.97906649 0.578125 0.97906649 0.65625 0.84375 0.5 0.15000001 0.5 0.83923614 0.578125
		 0.97906649 0.421875 0.97906649 0.34375 0.84375 0.42187503 0.70843351 0.57812506 0.70843351
		 0.65625 0.84375 0.56103516 0.94881493 0.43896484 0.94881493 0.37792969 0.84309894
		 0.43896487 0.73738295 0.56103516 0.73738295 0.62207031 0.84309894 0.36328125 0.84375
		 0.43164063 0.96215189 0.56835938 0.96215194 0.63671875 0.84375 0.56835943 0.72534811
		 0.43164065 0.72534806 0.56347668 0.73380536 0.43652347 0.7338053 0.37304688 0.84375
		 0.43652344 0.95369464 0.56347656 0.9536947 0.62695313 0.84375 0.34375 0.84375 0.421875
		 0.97906649 0.578125 0.97906649 0.65625 0.84375 0.57812506 0.70843351 0.421875 0.70843351
		 0.34375 0.84375 0.421875 0.97906649 0.578125 0.97906649 0.65625 0.84375 0.57812506
		 0.70843351 0.42187503 0.70843351 0.45833331 0.6759553 0.41666666 0.6759553 0.625
		 0.6759553 0.375 0.6759553 0.58333331 0.6759553 0.54166663 0.6759553 0.49999997 0.6759553
		 0.45833331 0.32331851 0.41666666 0.32331851 0.625 0.32331851 0.375 0.32331851 0.58333337
		 0.32331851 0.54166663 0.32331851 0.49999997 0.32331851 0.5 0.93187761 0.5 0.9672693
		 0.4609375 0.90880406 0.5390625 0.90880406 0.421875 0.88677216 0.39257813 0.90524918
		 0.421875 0.84114587 0.421875 0.79656118 0.39257813 0.78120911 0.4609375 0.77348757
		 0.50000006 0.75145561 0.50000006 0.71918905 0.5390625 0.77348757 0.578125 0.79656118
		 0.60742188 0.78120911 0.578125 0.84114581 0.578125 0.88677216 0.60742188 0.90524918
		 0.3828125 0.91140819 0.3828125 0.91140825 0.36328125 0.84375 0.43164063 0.96215194
		 0.5 0.97906649 0.5 0.97906649 0.56835938 0.96215189 0.6171875 0.91140825 0.6171875
		 0.91140825 0.63671875 0.84375 0.6171875 0.77609181 0.6171875 0.77609175 0.56835943
		 0.72534806 0.50000006 0.70843351 0.50000006 0.70843351 0.43164063 0.72534811 0.3828125
		 0.77609175 0.3828125 0.77609175 0.50000006 0.70843351 0.50000006 0.70843351 0.56835943
		 0.72534806 0.43164063 0.72534811 0.3828125 0.77609175 0.3828125 0.77609175 0.36328125
		 0.84375 0.3828125 0.91140819 0.3828125 0.91140825 0.43164063 0.96215194 0.5 0.97906649
		 0.5 0.97906649 0.56835938 0.96215189 0.6171875 0.91140825 0.61718756 0.91140825 0.63671875
		 0.84375 0.61718756 0.77609181 0.61718762 0.77609175 0.375 0.3125 0.41666666 0.3125
		 0.41666666 0.68843985 0.375 0.68843985 0.45833331 0.3125 0.45833331 0.68843985 0.49999997
		 0.3125 0.49999997 0.68843985 0.54166663 0.3125 0.54166663 0.68843985 0.58333331 0.3125
		 0.58333331 0.68843985 0.625 0.3125 0.625 0.68843985 0.42187503 0.020933509 0.57812506
		 0.020933539 0.5 0.15000001 0.34375 0.15624997 0.421875 0.29156646 0.578125 0.29156649
		 0.65625 0.15625 0.578125 0.97906649 0.421875 0.97906649 0.5 0.83749998 0.34375 0.84375
		 0.42187503 0.70843351 0.57812506 0.70843351 0.65625 0.84375 0.42187503 0.020933509
		 0.57812506 0.020933539 0.34375 0.15624997 0.421875 0.29156646 0.578125 0.29156649
		 0.65625 0.15625;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 136 ".vt[0:135]"  0.14258924 0.24697167 0.67007178 -0.14258912 0.24697173 0.67007178
		 -0.28517833 4.2494886e-08 0.67007178 -0.1425892 -0.24697168 0.67007178 0.14258915 -0.2469717 0.67007178
		 0.28517833 -9.7330124e-18 0.67007178 0.14258924 0.24697167 0.75773919 -0.14258912 0.24697173 0.75773919
		 -0.28517833 4.2494886e-08 0.75773919 -0.1425892 -0.24697168 0.75773919 0.14258915 -0.2469717 0.75773919
		 0.28517833 9.7330505e-18 0.75773919 0 -9.7330124e-18 0.67007178 0.13350026 0.2312291 0.75773919
		 -0.13350016 0.23122917 0.75773919 -0.26700038 4.0479595e-08 0.75773919 -0.13350022 -0.23122913 0.75773919
		 0.13350019 -0.23122916 0.75773919 0.26700038 6.9343309e-10 0.75773919 0.10429706 0.18064773 1.050577283
		 -0.10429701 0.18064779 1.050577283 2.7196725e-09 1.822125e-09 1.05175662 -0.20859405 3.2478322e-08 1.050577283
		 -0.10429705 -0.18064773 1.050577283 0.10429703 -0.18064778 1.050577283 0.20859405 -4.4127106e-09 1.050577283
		 -0.23362535 3.6557832e-08 1.0083701611 -0.11681263 0.20232554 1.0083701611 0.11681271 0.20232548 1.0083701611
		 0.23362535 -4.4127106e-09 1.0083701611 0.11681267 -0.20232552 1.0083701611 -0.11681268 -0.20232549 1.0083701611
		 0.1335002 -0.23122917 0.76127321 -0.13350022 -0.23122916 0.76127321 -0.26700041 4.0479595e-08 0.76127321
		 -0.13350017 0.23122919 0.76127321 0.13350028 0.23122913 0.76127321 0.26700041 6.9343309e-10 0.76127321
		 -0.2687678 4.0675538e-08 0.75773919 -0.13438387 0.2327598 0.75773919 0.13438396 0.23275974 0.75773919
		 0.2687678 6.2601169e-10 0.75773919 0.1343839 -0.23275977 0.75773919 -0.13438393 -0.23275976 0.75773919
		 -0.28277829 4.2228802e-08 0.75773919 -0.14138909 0.24489319 0.75773919 0.14138919 0.24489315 0.75773919
		 0.28277829 9.1554805e-11 0.75773919 0.14138915 -0.24489319 0.75773919 -0.14138918 -0.24489316 0.75773919
		 -0.28517833 4.2494886e-08 0.7548278 -0.14258912 0.24697173 0.7548278 0.14258924 0.24697167 0.7548278
		 0.28517833 9.086603e-18 0.7548278 0.14258915 -0.2469717 0.7548278 -0.1425892 -0.24697168 0.7548278
		 -0.28517833 4.2494886e-08 0.67259461 -0.14258912 0.24697173 0.67259461 0.14258924 0.24697167 0.67259461
		 0.28517833 -9.1728325e-18 0.67259461 0.14258915 -0.2469717 0.67259461 -0.1425892 -0.24697168 0.67259461
		 3.263607e-08 0.15415275 1.05175662 4.4874596e-08 0.21196005 1.050577283 -0.066750079 0.11561459 1.05175662
		 0.066750132 0.11561456 1.05175662 -0.13350019 0.077076405 1.05175662 -0.18356276 0.10598005 1.050577283
		 -0.13350019 2.175738e-08 1.05175662 -0.13350022 -0.077076361 1.05175662 -0.1835628 -0.10598001 1.050577283
		 -0.066750109 -0.11561456 1.05175662 -7.2524604e-09 -0.15415275 1.05175662 -9.9721333e-09 -0.21196005 1.050577283
		 0.066750094 -0.11561458 1.05175662 0.13350019 -0.077076383 1.05175662 0.18356277 -0.10598002 1.050577283
		 0.13350019 -4.0795087e-09 1.05175662 0.13350022 0.077076368 1.05175662 0.1835628 0.10598001 1.050577283
		 -0.20025027 0.11561461 1.04703927 -0.20025027 0.11561461 1.0083701611 -0.23362534 3.5355743e-08 1.04703927
		 -0.11681263 0.20232552 1.04703927 4.8954107e-08 0.23122913 1.04703927 4.8954107e-08 0.23122913 1.0083701611
		 0.11681271 0.20232548 1.04703927 0.20025033 0.11561455 1.04703927 0.20025033 0.11561455 1.0083701611
		 0.23362535 -5.439345e-09 1.04703927 0.2002503 -0.11561458 1.04703927 0.2002503 -0.11561458 1.0083701611
		 0.11681268 -0.20232549 1.04703927 -1.087869e-08 -0.23122913 1.04703927 -1.087869e-08 -0.23122913 1.0083701611
		 -0.11681268 -0.20232549 1.04703927 -0.20025033 -0.11561454 1.04703927 -0.20025033 -0.11561454 1.0083701611
		 -1.087869e-08 -0.23122917 0.90179753 -1.087869e-08 -0.23122917 0.76127321 0.11681268 -0.20232554 0.90179753
		 -0.11681268 -0.20232549 0.90179753 -0.20025033 -0.11561455 0.90179753 -0.20025033 -0.11561455 0.76127321
		 -0.23362535 3.263607e-08 0.90179753 -0.2002503 0.11561461 0.90179753 -0.2002503 0.11561462 0.76127321
		 -0.11681266 0.20232554 0.90179753 4.8954107e-08 0.23122916 0.90179753 4.8954107e-08 0.23122917 0.76127321
		 0.11681274 0.20232549 0.90179753 0.20025034 0.11561456 0.90179753 0.20025034 0.11561456 0.76127321
		 0.23362535 -5.439345e-09 0.90179753 0.20025033 -0.11561459 0.90179753 0.20025033 -0.11561459 0.76127321
		 0.13530818 0.23436052 -0.72771889 -0.13530809 0.23436056 -0.72771889 -0.27061623 4.0324963e-08 -0.72771889
		 -0.13530815 -0.23436055 -0.72771889 0.13530812 -0.23436056 -0.72771889 0.27061623 -1.9007366e-16 -0.72771889
		 0.13530818 0.23436052 0.69132859 -0.13530809 0.23436056 0.69132859 -0.27061623 4.0324963e-08 0.69132859
		 -0.13530815 -0.23436055 0.69132859 0.13530812 -0.23436056 0.69132859 0.27061623 1.9007366e-16 0.69132859
		 0 1.9007366e-16 0.69132859 0.0096228831 0.016667323 -1.085217118 -0.0096228514 0.016667323 -1.085217118
		 1.5552024e-08 7.4914253e-09 -1.085217118 -0.019245718 1.0359263e-08 -1.085217118
		 -0.0096228514 -0.016667306 -1.085217118 0.0096228831 -0.016667306 -1.085217118 0.019245751 7.4914253e-09 -1.085217118;
	setAttr -s 270 ".ed";
	setAttr ".ed[0:165]"  0 1 1 1 2 1 2 3 1 3 4 1 4 5 1 5 0 1 6 7 1 7 8 1 8 9 1
		 9 10 1 10 11 1 11 6 1 0 58 1 1 57 1 2 56 1 3 61 1 4 60 1 5 59 1 12 0 1 12 1 1 12 2 1
		 12 3 1 12 4 1 12 5 1 6 46 1 7 45 1 13 14 1 8 44 1 14 15 1 9 49 1 15 16 1 10 48 1
		 16 17 1 11 47 1 17 18 1 18 13 1 13 36 1 14 35 1 19 63 1 20 64 1 19 65 1 15 34 1 20 67 1
		 22 68 1 16 33 1 22 70 1 23 71 1 17 32 1 23 73 1 24 74 1 18 37 1 24 76 1 25 77 1 25 79 1
		 26 82 1 27 83 1 26 81 1 28 86 1 27 85 1 29 89 1 28 88 1 30 92 1 29 91 1 31 95 1 30 94 1
		 31 97 1 32 100 1 33 101 1 32 99 1 34 104 1 33 103 1 35 107 1 34 106 1 36 110 1 35 109 1
		 37 113 1 36 112 1 37 115 1 38 15 1 39 14 1 38 39 1 40 13 1 39 40 1 41 18 1 40 41 1
		 42 17 1 41 42 1 43 16 1 42 43 1 43 38 1 44 38 1 45 39 1 44 45 1 46 40 1 45 46 1 47 41 1
		 46 47 1 48 42 1 47 48 1 49 43 1 48 49 1 49 44 1 50 8 1 51 7 1 50 51 1 52 6 1 51 52 1
		 53 11 1 52 53 1 54 10 1 53 54 1 55 9 1 54 55 1 55 50 1 56 50 1 57 51 1 56 57 1 58 52 1
		 57 58 1 59 53 1 58 59 1 60 54 1 59 60 1 61 55 1 60 61 1 61 56 1 63 20 1 64 21 1 65 21 1
		 63 62 1 64 62 1 65 62 1 67 22 1 68 21 1 67 66 1 68 66 1 64 66 1 70 23 1 71 21 1 70 69 1
		 71 69 1 68 69 1 73 24 1 74 21 1 73 72 1 74 72 1 71 72 1 76 25 1 77 21 1 76 75 1 77 75 1
		 74 75 1 79 19 1 79 78 1 65 78 1 77 78 1 81 27 1 82 22 1 83 20 1 81 80 1 82 80 1 67 80 1
		 83 80 1 85 28 1 86 19 1 85 84 1;
	setAttr ".ed[166:269]" 83 84 1 63 84 1 86 84 1 88 29 1 89 25 1 88 87 1 86 87 1
		 79 87 1 89 87 1 91 30 1 92 24 1 91 90 1 89 90 1 76 90 1 92 90 1 94 31 1 95 23 1 94 93 1
		 92 93 1 73 93 1 95 93 1 97 26 1 97 96 1 95 96 1 70 96 1 82 96 1 99 33 1 100 30 1
		 101 31 1 99 98 1 100 98 1 94 98 1 101 98 1 103 34 1 104 26 1 103 102 1 101 102 1
		 97 102 1 104 102 1 106 35 1 107 27 1 106 105 1 104 105 1 81 105 1 107 105 1 109 36 1
		 110 28 1 109 108 1 107 108 1 85 108 1 110 108 1 112 37 1 113 29 1 112 111 1 110 111 1
		 88 111 1 113 111 1 115 32 1 115 114 1 113 114 1 91 114 1 100 114 1 116 117 1 117 118 1
		 118 119 1 119 120 1 120 121 1 121 116 1 122 123 0 123 124 0 124 125 0 125 126 0 126 127 0
		 127 122 0 116 122 0 117 123 0 118 124 0 119 125 0 120 126 0 121 127 0 122 128 1 123 128 1
		 124 128 1 125 128 1 126 128 1 127 128 1 116 129 0 117 130 0 129 130 0 131 129 1 131 130 1
		 118 132 0 130 132 0 131 132 1 119 133 0 132 133 0 131 133 1 120 134 0 133 134 0 131 134 1
		 121 135 0 134 135 0 131 135 1 135 129 0;
	setAttr -s 138 -ch 540 ".fc[0:137]" -type "polyFaces" 
		f 4 0 13 118 -13
		mu 0 4 6 7 72 74
		f 4 1 14 116 -14
		mu 0 4 7 8 71 72
		f 4 2 15 125 -15
		mu 0 4 8 9 77 71
		f 4 3 16 124 -16
		mu 0 4 9 10 76 77
		f 4 4 17 122 -17
		mu 0 4 10 11 75 76
		f 4 5 12 120 -18
		mu 0 4 11 12 73 75
		f 3 -1 -19 19
		mu 0 3 1 0 26
		f 3 -2 -20 20
		mu 0 3 2 1 26
		f 3 -3 -21 21
		mu 0 3 3 2 26
		f 3 -4 -22 22
		mu 0 3 4 3 26
		f 3 -5 -23 23
		mu 0 3 5 4 26
		f 3 -6 -24 18
		mu 0 3 0 5 26
		f 4 -41 38 129 -132
		mu 0 4 81 34 79 78
		f 4 -40 42 134 -137
		mu 0 4 80 35 83 82
		f 4 -44 45 139 -142
		mu 0 4 84 36 86 85
		f 4 -47 48 144 -147
		mu 0 4 87 37 89 88
		f 4 -50 51 149 -152
		mu 0 4 90 38 92 91
		f 4 -53 53 153 -156
		mu 0 4 93 39 95 94
		f 4 6 25 94 -25
		mu 0 4 24 23 59 60
		f 4 7 27 92 -26
		mu 0 4 23 22 58 59
		f 4 8 29 101 -28
		mu 0 4 22 21 63 58
		f 4 9 31 100 -30
		mu 0 4 21 20 62 63
		f 4 10 33 98 -32
		mu 0 4 20 25 61 62
		f 4 11 24 96 -34
		mu 0 4 25 24 60 61
		f 5 26 37 74 211 -37
		mu 0 5 28 29 49 125 50
		f 5 28 41 72 205 -38
		mu 0 5 29 30 48 122 49
		f 5 30 44 70 199 -42
		mu 0 5 30 31 47 119 48
		f 5 32 47 68 192 -45
		mu 0 5 31 32 46 115 47
		f 5 34 50 77 223 -48
		mu 0 5 32 33 51 131 46
		f 5 35 36 76 217 -51
		mu 0 5 33 28 50 128 51
		f 4 -56 -157 159 -163
		mu 0 4 99 41 97 96
		f 4 -58 -164 165 -169
		mu 0 4 102 42 101 100
		f 4 -60 -170 171 -175
		mu 0 4 105 43 104 103
		f 4 -62 -176 177 -181
		mu 0 4 108 44 107 106
		f 4 -64 -182 183 -187
		mu 0 4 111 45 110 109
		f 4 -55 -188 188 -192
		mu 0 4 98 40 113 112
		f 4 -68 -193 195 -199
		mu 0 4 117 47 115 114
		f 4 -70 -200 201 -205
		mu 0 4 120 48 119 118
		f 4 -72 -206 207 -211
		mu 0 4 123 49 122 121
		f 4 -74 -212 213 -217
		mu 0 4 126 50 125 124
		f 4 -76 -218 219 -223
		mu 0 4 129 51 128 127
		f 4 -67 -224 224 -228
		mu 0 4 116 46 131 130
		f 4 -81 78 -29 -80
		mu 0 4 53 52 30 29
		f 4 -83 79 -27 -82
		mu 0 4 54 53 29 28
		f 4 -85 81 -36 -84
		mu 0 4 55 54 28 33
		f 4 -87 83 -35 -86
		mu 0 4 56 55 33 32
		f 4 -89 85 -33 -88
		mu 0 4 57 56 32 31
		f 4 -90 87 -31 -79
		mu 0 4 52 57 31 30
		f 4 -93 90 80 -92
		mu 0 4 59 58 52 53
		f 4 -95 91 82 -94
		mu 0 4 60 59 53 54
		f 4 -97 93 84 -96
		mu 0 4 61 60 54 55
		f 4 -99 95 86 -98
		mu 0 4 62 61 55 56
		f 4 -101 97 88 -100
		mu 0 4 63 62 56 57
		f 4 -102 99 89 -91
		mu 0 4 58 63 57 52
		f 4 -105 102 -8 -104
		mu 0 4 65 64 15 14
		f 4 -107 103 -7 -106
		mu 0 4 67 65 14 13
		f 4 -109 105 -12 -108
		mu 0 4 68 66 19 18
		f 4 -111 107 -11 -110
		mu 0 4 69 68 18 17
		f 4 -113 109 -10 -112
		mu 0 4 70 69 17 16
		f 4 -114 111 -9 -103
		mu 0 4 64 70 16 15
		f 4 -117 114 104 -116
		mu 0 4 72 71 64 65
		f 4 -119 115 106 -118
		mu 0 4 74 72 65 67
		f 4 -121 117 108 -120
		mu 0 4 75 73 66 68
		f 4 -123 119 110 -122
		mu 0 4 76 75 68 69
		f 4 -125 121 112 -124
		mu 0 4 77 76 69 70
		f 4 -126 123 113 -115
		mu 0 4 71 77 70 64
		f 4 126 39 130 -130
		mu 0 4 79 35 80 78
		f 4 127 -129 131 -131
		mu 0 4 80 27 81 78
		f 4 132 43 135 -135
		mu 0 4 83 36 84 82
		f 4 133 -128 136 -136
		mu 0 4 84 27 80 82
		f 4 137 46 140 -140
		mu 0 4 86 37 87 85
		f 4 138 -134 141 -141
		mu 0 4 87 27 84 85
		f 4 142 49 145 -145
		mu 0 4 89 38 90 88
		f 4 143 -139 146 -146
		mu 0 4 90 27 87 88
		f 4 147 52 150 -150
		mu 0 4 92 39 93 91
		f 4 148 -144 151 -151
		mu 0 4 93 27 90 91
		f 4 152 40 154 -154
		mu 0 4 95 34 81 94
		f 4 128 -149 155 -155
		mu 0 4 81 27 93 94
		f 4 -57 54 160 -160
		mu 0 4 97 40 98 96
		f 4 157 -133 161 -161
		mu 0 4 98 36 83 96
		f 4 -43 -159 162 -162
		mu 0 4 83 35 99 96
		f 4 -59 55 166 -166
		mu 0 4 101 41 99 100
		f 4 158 -127 167 -167
		mu 0 4 99 35 79 100
		f 4 -39 -165 168 -168
		mu 0 4 79 34 102 100
		f 4 -61 57 172 -172
		mu 0 4 104 42 102 103
		f 4 164 -153 173 -173
		mu 0 4 102 34 95 103
		f 4 -54 -171 174 -174
		mu 0 4 95 39 105 103
		f 4 -63 59 178 -178
		mu 0 4 107 43 105 106
		f 4 170 -148 179 -179
		mu 0 4 105 39 92 106
		f 4 -52 -177 180 -180
		mu 0 4 92 38 108 106
		f 4 -65 61 184 -184
		mu 0 4 110 44 108 109
		f 4 176 -143 185 -185
		mu 0 4 108 38 89 109
		f 4 -49 -183 186 -186
		mu 0 4 89 37 111 109
		f 4 -66 63 189 -189
		mu 0 4 113 45 111 112
		f 4 182 -138 190 -190
		mu 0 4 111 37 86 112
		f 4 -46 -158 191 -191
		mu 0 4 86 36 98 112
		f 4 -69 66 196 -196
		mu 0 4 115 46 116 114
		f 4 193 64 197 -197
		mu 0 4 116 44 110 114
		f 4 181 -195 198 -198
		mu 0 4 110 45 117 114
		f 4 -71 67 202 -202
		mu 0 4 119 47 117 118
		f 4 194 65 203 -203
		mu 0 4 117 45 113 118
		f 4 187 -201 204 -204
		mu 0 4 113 40 120 118
		f 4 -73 69 208 -208
		mu 0 4 122 48 120 121
		f 4 200 56 209 -209
		mu 0 4 120 40 97 121
		f 4 156 -207 210 -210
		mu 0 4 97 41 123 121
		f 4 -75 71 214 -214
		mu 0 4 125 49 123 124
		f 4 206 58 215 -215
		mu 0 4 123 41 101 124
		f 4 163 -213 216 -216
		mu 0 4 101 42 126 124
		f 4 -77 73 220 -220
		mu 0 4 128 50 126 127
		f 4 212 60 221 -221
		mu 0 4 126 42 104 127
		f 4 169 -219 222 -222
		mu 0 4 104 43 129 127
		f 4 -78 75 225 -225
		mu 0 4 131 51 129 130
		f 4 218 62 226 -226
		mu 0 4 129 43 107 130
		f 4 175 -194 227 -227
		mu 0 4 107 44 116 130
		f 4 228 241 -235 -241
		mu 0 4 132 133 134 135
		f 4 229 242 -236 -242
		mu 0 4 133 136 137 134
		f 4 230 243 -237 -243
		mu 0 4 136 138 139 137
		f 4 231 244 -238 -244
		mu 0 4 138 140 141 139
		f 4 232 245 -239 -245
		mu 0 4 140 142 143 141
		f 4 233 240 -240 -246
		mu 0 4 142 144 145 143
		f 3 -255 -256 256
		mu 0 3 146 147 148
		f 3 -259 -257 259
		mu 0 3 149 146 148
		f 3 -262 -260 262
		mu 0 3 150 149 148
		f 3 -265 -263 265
		mu 0 3 151 150 148
		f 3 -268 -266 268
		mu 0 3 152 151 148
		f 3 -270 -269 255
		mu 0 3 147 152 148
		f 3 234 247 -247
		mu 0 3 153 154 155
		f 3 235 248 -248
		mu 0 3 154 156 155
		f 3 236 249 -249
		mu 0 3 156 157 155
		f 3 237 250 -250
		mu 0 3 157 158 155
		f 3 238 251 -251
		mu 0 3 158 159 155
		f 3 239 246 -252
		mu 0 3 159 153 155
		f 4 -229 252 254 -254
		mu 0 4 160 161 147 146
		f 4 -230 253 258 -258
		mu 0 4 162 160 146 149
		f 4 -231 257 261 -261
		mu 0 4 163 162 149 150
		f 4 -232 260 264 -264
		mu 0 4 164 163 150 151
		f 4 -233 263 267 -267
		mu 0 4 165 164 151 152
		f 4 -234 266 269 -253
		mu 0 4 161 165 152 147;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode lightLinker -s -n "lightLinker1";
	rename -uid "C7CC3F4A-45AE-FAF7-B70D-09B9D3D09A02";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "50558C4E-4AF6-B4DC-10C5-FAAD83412403";
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "A1FB4F48-4216-1026-F126-1F8FD9E2854C";
createNode displayLayerManager -n "layerManager";
	rename -uid "88F95CEA-44AC-FE25-480B-D39DD0E067F8";
createNode displayLayer -n "defaultLayer";
	rename -uid "F96FFE12-4920-C064-7E05-A69E0E91BB27";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "5F7F23D1-48DC-CC5C-9AE5-0B980A4CE8CE";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "2C898B83-4BAD-46CA-FE4B-259E3071ACBD";
	setAttr ".g" yes;
createNode groupId -n "groupId1";
	rename -uid "6A531A2A-4DA8-3FCC-3520-C0B5EC96872F";
	setAttr ".ihi" 0;
createNode script -n "uiConfigurationScriptNode";
	rename -uid "28F3AC73-470D-3EA3-D000-B1A4D1A109B0";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n"
		+ "            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n"
		+ "            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n"
		+ "            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n"
		+ "            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n"
		+ "            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n"
		+ "            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n"
		+ "            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n"
		+ "            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 32768\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n"
		+ "            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1115\n            -height 692\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n"
		+ "            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n"
		+ "            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n"
		+ "\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n"
		+ "            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"0\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n"
		+ "                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n"
		+ "                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 1\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -showCurveNames 0\n"
		+ "                -showActiveCurveNames 0\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                -valueLinesToggle 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n"
		+ "                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 1\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n"
		+ "                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n"
		+ "                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n"
		+ "                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -displayValues 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n"
		+ "                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" != $panelName) {\n"
		+ "\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n"
		+ "                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -highlightConnections 0\n                -copyConnectionsOnPaste 0\n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -activeTab -1\n                -editorMode \"default\" \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1115\\n    -height 692\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 32768\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 1115\\n    -height 692\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	rename -uid "0A93D988-4F4E-5AB4-965D-D2A00B00A655";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
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
connectAttr "groupId1.id" "pCylinder3Shape.iog.og[0].gid";
connectAttr ":initialShadingGroup.mwc" "pCylinder3Shape.iog.og[0].gco";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "pCylinder3Shape.iog.og[0]" ":initialShadingGroup.dsm" -na;
connectAttr "groupId1.msg" ":initialShadingGroup.gn" -na;
// End of pencil2_Model_v1.ma
