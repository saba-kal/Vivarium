//Maya ASCII 2020 scene
//Name: Boulder3_v2.ma
//Last modified: Wed, Apr 28, 2021 08:35:37 PM
//Codeset: 1252
requires maya "2020";
requires "mtoa" "4.0.4.1";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2020";
fileInfo "version" "2020";
fileInfo "cutIdentifier" "202009141615-87c40af620";
fileInfo "osv" "Microsoft Windows 10 Technical Preview  (Build 19042)\n";
fileInfo "UUID" "0AA6CD5B-4494-9089-852D-EA951FCE9321";
fileInfo "license" "student";
createNode transform -s -n "persp";
	rename -uid "7A027972-4EB3-47D5-C964-87BAD165F182";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -214.34509884075393 101.13849675759739 -42.361186439976976 ;
	setAttr ".r" -type "double3" -9.9383527301158026 991.39999999992301 -3.2544731191852442e-14 ;
createNode camera -s -n "perspShape" -p "persp";
	rename -uid "237D31DB-41E1-1643-8F00-38AB160F5A9E";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".coi" 274.36736413312769;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" 55.824430465698242 53.785853385925293 -48.963996857404709 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
	setAttr ".ai_translator" -type "string" "perspective";
createNode transform -s -n "top";
	rename -uid "21ECBBBF-4E56-DC3D-237B-559F7073FE77";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 1000.1 0 ;
	setAttr ".r" -type "double3" -90 0 0 ;
createNode camera -s -n "topShape" -p "top";
	rename -uid "8E8FC2B2-49FF-7B53-D0E8-C2BF5786FE3B";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "front";
	rename -uid "25B3C4B0-4D3F-13F3-BB26-018A6856017A";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 1000.1 ;
createNode camera -s -n "frontShape" -p "front";
	rename -uid "9BDBA7FF-4C70-ED16-1278-EFBD1FD1B8B0";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -s -n "side";
	rename -uid "4DDE9A7B-46CD-5006-6E58-08B1429842E8";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1000.1 0 0 ;
	setAttr ".r" -type "double3" 0 90 0 ;
createNode camera -s -n "sideShape" -p "side";
	rename -uid "CC5E90D7-4C02-C463-09A5-5E8D74943EF1";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 1000.1;
	setAttr ".ow" 30;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -n "Boulder3:bottom";
	rename -uid "694C5954-4516-5B95-776A-9BBAC831A6E9";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 29.309324727836835 -100010.01634093198 -49.086478442168449 ;
	setAttr ".r" -type "double3" 89.999999999999986 0 0 ;
createNode camera -n "Boulder3:bottomShape" -p "Boulder3:bottom";
	rename -uid "5FB22072-4D82-D5E4-22CC-33A59B5ABBF0";
	setAttr -k off ".v";
	setAttr ".rnd" no;
	setAttr ".ncp" 10;
	setAttr ".fcp" 1000000;
	setAttr ".fd" 500;
	setAttr ".coi" 100010.18177205352;
	setAttr ".ow" 240.31119321471132;
	setAttr ".imn" -type "string" "bottom1";
	setAttr ".den" -type "string" "bottom1_depth";
	setAttr ".man" -type "string" "bottom1_mask";
	setAttr ".tp" -type "double3" 29.309324727836831 0.16543112157103224 -49.086478442190653 ;
	setAttr ".hc" -type "string" "viewSet -bo %camera";
	setAttr ".o" yes;
	setAttr ".ai_translator" -type "string" "orthographic";
createNode transform -n "Boulder3:MediumBoulder";
	rename -uid "882AE435-45FC-EC36-D8EE-6D974D435CD1";
	setAttr ".rp" -type "double3" 49.656830040179742 0 -49.086487347014632 ;
	setAttr ".sp" -type "double3" 49.656830040179742 -2.486899575160351e-14 -49.086487347014632 ;
createNode mesh -n "Boulder3:MediumBoulderShape" -p "Boulder3:MediumBoulder";
	rename -uid "07D7D117-413D-9210-67AA-C2AF82A56F67";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.49894982945458688 0.50828933462316039 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr -s 185 ".uvst[0].uvsp[0:184]" -type "float2" 0.89451092 0.67324197
		 0.85383475 0.76732117 0.80261517 0.73932886 0.81938839 0.70923221 0.77051073 0.85362327
		 0.67182267 0.91152012 0.64719939 0.8566637 0.73064536 0.81109059 0.55765271 0.93910885
		 0.43577069 0.93430793 0.45208463 0.87940198 0.55399245 0.88022053 0.7259267 0.15500253
		 0.63764209 0.10324574 0.23118067 0.27481106 0.13467991 0.28444758 0.34588766 0.10411558
		 0.43364716 0.092876792 0.47484827 0.2234156 0.34143031 0.22785451 0.56930363 0.13226113
		 0.22003505 0.19596848 0.72442305 0.24036141 0.60807824 0.22580351 0.87222302 0.26944178
		 0.18141592 0.36424896 0.18249547 0.4962512 0.086306632 0.52169299 0.35012329 0.36618498
		 0.47939891 0.36188391 0.48355603 0.48514074 0.35428047 0.48944178 0.60846466 0.36096546
		 0.77118641 0.35854241 0.8177157 0.47401753 0.61250317 0.48087889 0.95776516 0.51661527
		 0.23788416 0.67976528 0.26243877 0.77729672 0.17383313 0.75298744 0.3926872 0.59771371
		 0.48757976 0.60576403 0.4945547 0.81420201 0.38067293 0.80421835 0.59157372 0.59984273
		 0.75709045 0.62636548 0.72174168 0.77105635 0.62733215 0.80302078 0.88007343 0.33681193
		 0.73465836 0.67072344 0.71368122 0.71924436 0.78837419 0.24047691 0.67087138 0.73195434
		 0.11876884 0.4177469 0.15317172 0.51893908 0.6001116 0.67569947 0.65647376 0.59300989
		 0.83480972 0.60620624 0.61778408 0.77893114 0.91654474 0.45976382 0.54839277 0.7797547
		 0.54603249 0.69342566 0.1623874 0.61509055 0.47842884 0.79572415 0.4010461 0.76442182
		 0.42441231 0.6592648 0.20174509 0.7134912 0.60070676 0.59408575 0.36376905 0.90036422
		 0.84115487 0.75304073 0.28360647 0.79923809 0.6018284 0.57396621 0.52056575 0.60917795
		 0.39414316 0.86716759 0.7662766 0.26043311 0.64107263 0.1408653 0.47248322 0.6050902
		 0.55051821 0.53664422 0.87302196 0.46789262 0.81501031 0.32175419 0.57407922 0.51983899
		 0.5068996 0.54754043 0.86556137 0.6130017 0.45380342 0.5508731 0.5372752 0.46830249
		 0.88213933 0.52213705 0.21059906 0.25338873 0.57933205 0.46370193 0.48393238 0.48377812
		 0.42637628 0.51497799 0.54212028 0.36504546 0.58169723 0.39673781 0.47207889 0.39505425
		 0.43097717 0.41402042 0.51752239 0.3271991 0.58642626 0.34707385 0.76335096 0.14946046
		 0.25085634 0.21843114 0.44193268 0.34472844 0.35124502 0.40931737 0.51472211 0.22879454
		 0.34128201 0.15997958 0.59917092 0.22701058 0.61955696 0.25315207 0.43835279 0.12735003
		 0.39110422 0.25559282 0.46274543 0.21969122 0.25012615 0.37676847 0.31093287 0.28917566
		 0.61740261 0.1346719 0.53771669 0.12063736 0.22813448 0.53236043 0.24177474 0.45938507
		 0.3579883 0.50735569 0.30290455 0.64827055 0.22170413 0.59738547 0.3716917 0.59278977
		 0.34359926 0.74518138 0.69961721 0.33809176 0.73831815 0.36671436 0.66867578 0.42393634
		 0.81402743 0.47914714 0.75343406 0.51775855 0.67017448 0.50437301 0.76546943 0.61727387
		 0.6337651 0.5858466 0.60006654 0.64043123 0.53159833 0.65273583 0.45791453 0.63646221
		 0.39978397 0.58010447 0.38611716 0.51273286 0.41239893 0.40616021 0.45923918 0.3577854
		 0.52872467 0.34356466 0.58569443 0.36759135 0.6454547 0.44485828 0.63752329 0.50793272
		 0.62637967 0.37669402 0.61444479 0.39466524 0.59379512 0.4219805 0.53383464 0.41809961
		 0.47339612 0.43490076 0.41999751 0.45905909 0.39270931 0.45130992 0.36453801 0.44547129
		 0.67394125 0.45945266 0.64570272 0.47226045 0.57329166 0.48538589 0.54126281 0.49703082
		 0.49673918 0.51226538 0.44093543 0.52640963 0.38816783 0.54221475 0.3578763 0.54550821
		 0.56818002 0.51958448 0.54702282 0.5346179 0.53880954 0.49970371 0.56763196 0.48946646
		 0.50735992 0.54479545 0.49835992 0.51337457 0.45877129 0.54780447 0.44784719 0.5261637
		 0.25901318 0.87114215 0.36297721 0.91246313 0.49053061 0.90821952 0.6173979 0.89506984
		 0.74748224 0.86582112 0.78825271 0.81303507 0.27375126 0.14707531 0.54685622 0.0834589
		 0.46554089 0.12357293 0.32227057 0.12816438 0.68919605 0.1290375 0.17430538 0.25957462
		 0.80611277 0.19787359 0.089959383 0.38192296 0.091719627 0.39177385 0.074935585 0.50421864
		 0.95389569 0.44937256 0.081354976 0.61906892 0.088869274 0.63625574 0.16607842 0.77827483
		 0.92332613 0.63729268 0.20631349 0.79455703 0.28254503 0.87768996 0.16555661 0.20329671;
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 161 ".vt[0:160]"  19.19471741 8.85986328 -19.25533295 37.94061661 6.2370472 -9.246521
		 61.34630585 7.092599392 -10.067913055 80.11898804 8.85986328 -14.8061018 25.61847687 43.78727722 -10.074184418
		 37.94061661 43.96568298 -0.87381738 61.34630585 44.23103333 -1.89944446 87.14677429 43.96568298 -10.074184418
		 35.18952179 70.31216431 -23.33769035 46.39286804 72.21131897 -4.10086823 59.089229584 74.17922974 -3.6792829
		 78.7642746 70.49196625 -23.33769035 37.84915924 88.29777527 -25.90463257 40.53433609 85.074249268 -22.92128181
		 58.80606079 89.73988342 -27.53638268 70.27893066 90.22852325 -28.98074341 38.12607193 100.35955048 -40.085277557
		 45.90835571 105.080368042 -37.49303436 60.99002838 107.57170868 -36.94470978 78.052993774 102.39910889 -40.89009094
		 34.99047852 93.0909729 -54.077392578 45.87602997 97.91939545 -54.4129982 60.99002838 100.41166687 -54.25325012
		 78.052993774 93.89260864 -52.72465134 35.95143127 75.52300262 -65.62658691 42.71504974 70.23457336 -73.21806335
		 56.81967545 79.22950745 -77.51934052 65.67147827 77.23399353 -73.34909058 35.49890137 60.85282135 -71.99591064
		 46.39286804 58.49072266 -77.45359039 59.089229584 61.15090942 -80.35003662 76.5552063 58.58008194 -72.41252136
		 28.70343971 35.30104828 -81.77876282 41.91359329 34.86301422 -92.15661621 61.34630585 35.12837219 -97.054176331
		 91.041847229 34.83041382 -84.19084167 22.13109589 4.0041217804 -77.97653198 37.94061661 2.14318466 -88.92636871
		 61.34630585 2.63136482 -88.10496521 80.11898804 3.63982773 -83.36679077 9.19682312 2.69297791 -62.19511032
		 38.30438232 0.43852237 -59.90989685 60.99002838 0.93174744 -59.61616135 96.41235352 2.69297981 -62.19512939
		 8.41460037 2.14315414 -32.48149872 40.53433609 0.21887971 -38.49546051 58.80606079 0.74675941 -38.71382904
		 88.46482849 6.2370472 -35.90167999 103.06098175 38.49186707 -59.7952919 99.12709045 49.84027863 -35.39101791
		 81.20272827 69.77029419 -56.53087997 84.29660034 69.86306763 -39.8172226 15.15812683 39.094047546 -59.7952919
		 17.88611221 47.52301788 -35.39101791 26.63034248 63.1903038 -54.20483398 26.63640785 72.38780212 -39.8172226
		 7.56864548 18.016782761 -32.71546173 8.43382645 18.80796051 -60.97591019 21.86690712 42.24232483 -19.83428383
		 13.88720703 31.28268051 -39.92347717 20.30304527 40.80049896 -65.49629211 97.8421936 18.016782761 -36.13574219
		 99.10766602 18.80796051 -60.97591019 91.35377502 40.35145569 -19.83428383 104.080215454 34.3563652 -49.086479187
		 94.32213593 39.1013298 -69.82707977 82.300354 6.069438934 -22.41162872 61.34630585 0.64207458 -20.78922081
		 37.94061661 0.11198425 -20.1935215 17.013317108 6.069438934 -22.41162491 96.41235352 2.14315414 -49.086479187
		 60.99002838 0.74675941 -49.086479187 38.30438232 0.21887971 -49.086479187 9.19682312 2.14315414 -39.92347717
		 88.59587097 2.047510147 -69.84793854 61.34630585 0.64207458 -71.47032166 37.94061661 0.11198425 -72.06602478
		 16.61865234 2.04750824 -69.84793854 77.80981445 8.79121208 -85.239151 61.14265442 9.21470451 -90.70426178
		 38.14857483 8.8650856 -91.58036804 27.54669189 8.79121017 -86.090065002 79.044815063 17.88732529 -12.93374634
		 61.14265442 18.62945557 -7.46862459 38.14857483 18.016782761 -6.59250975 23.76059914 19.2152977 -12.93374634
		 73.23571777 0.11198425 -35.90167999 49.65683365 0.21887971 -38.49546051 26.077991486 0.11198425 -35.90167999
		 78.99944305 0.68199539 -62.49353409 49.65683365 0.43852237 -59.90989685 20.31426811 0.68199539 -62.49353409
		 73.36058044 2.047512054 -85.82157898 49.65683365 2.14318466 -88.92636871 29.038316727 2.047512054 -86.002166748
		 73.34205627 31.24880791 -88.9251709 49.65683365 31.28268433 -90.37840271 29.089509964 31.24880791 -87.13612366
		 73.34205627 40.35145569 -4.60617828 49.65683365 40.38535309 -0.87381738 31.67557716 38.67521667 -4.60617828
		 73.36058044 6.069438934 -12.35132217 49.65683365 6.2370472 -9.246521 25.95312881 6.069438934 -12.35132217
		 14.79679871 17.93233109 -22.8958931 8.43382645 18.016782761 -39.92347717 14.79679871 17.93233109 -69.36366272
		 90.81239319 17.93232727 -22.8958931 99.10766602 18.016782761 -49.086479187 90.81239319 8.81689262 -69.36366272
		 73.30973816 -1.9073486e-06 -22.46883774 49.65683365 0.11198425 -20.1935215 26.0039653778 -1.9073486e-06 -22.46883774
		 78.99943542 0.11198425 -49.086479187 49.65683365 0.21887971 -49.086479187 20.31426811 0.11198425 -49.086479187
		 73.30973816 -1.9073486e-06 -69.79072571 49.65683365 0.11198425 -72.06602478 26.0039653778 -1.9073486e-06 -69.79072571
		 72.93024445 8.81689453 -88.31593323 49.65683365 8.8650856 -89.80215454 28.91172409 8.81689453 -88.67710876
		 72.93024445 17.93232727 -9.85694885 49.65683365 18.016782761 -6.59250975 26.38345909 17.93232727 -9.85694885
		 36.28191757 77.69937897 -24.39200592 43.98659897 77.49449158 -11.83095932 58.97292328 80.57043457 -14.86254501
		 71.56933594 78.5983429 -25.65545464 80.78266144 80.48471069 -40.21413803 80.78266144 80.046981812 -54.96755981
		 69.67692566 73.2611084 -75.83074951 58.15705872 64.83758545 -75.003112793 44.88227844 63.3142662 -75.71393585
		 35.68476868 66.8782959 -69.37984467 23.6636219 73.89021301 -53.33233643 27.11829758 82.65591431 -39.6398468
		 31.58109474 61.8854332 -64.13645172 32.77655792 69.22312164 -62.29063416 34.49168777 79.75047302 -59.6424408
		 44.11145782 87.5461731 -67.85412598 58.66199112 88.58702087 -67.2412262 71.1411972 84.59317017 -64.23796082
		 74.58303833 76.25886536 -66.61414337 78.76919556 68.45730591 -68.27037048 26.63295364 67.15197754 -48.0076141357
		 25.15166473 77.66589355 -47.43453217 36.34108353 98.68411255 -49.43087769 45.88995743 103.38967896 -47.6425972
		 60.99002838 105.78321075 -46.87950897 78.052993774 99.75733948 -48.90385437 80.78266144 80.23551941 -48.6127739
		 82.53536224 69.81025696 -49.33176041 39.92805862 100.62112427 -40.37842941 46.9750061 104.89587402 -38.031124115
		 46.95834732 103.36494446 -47.22166824 38.31173325 99.10400391 -48.84097672 60.63163757 107.15183258 -37.53461075
		 60.63163757 105.53231049 -46.53068542 76.082336426 102.46796417 -41.10719681 76.082336426 100.075813293 -48.36375046;
	setAttr -s 310 ".ed";
	setAttr ".ed[0:165]"  0 103 1 1 102 1 2 101 1 4 100 1 5 99 1 6 98 1 8 9 1
		 9 10 1 10 11 1 12 13 1 13 14 1 14 15 1 16 17 0 17 18 0 18 19 0 20 21 1 21 22 1 22 23 1
		 24 25 1 25 26 1 26 27 1 28 29 1 29 30 1 30 31 1 32 97 1 33 96 1 34 95 1 36 94 1 37 93 1
		 38 92 1 40 91 1 41 90 1 42 89 1 44 88 1 45 87 1 46 86 1 0 85 1 1 84 1 2 83 1 3 82 1
		 4 8 1 5 9 1 6 10 1 7 11 1 8 125 1 9 126 1 10 127 1 11 128 1 12 16 1 13 17 1 14 18 1
		 15 19 1 16 147 0 19 150 0 20 139 1 21 140 1 22 141 1 23 142 1 24 134 1 25 133 1 26 132 1
		 27 131 1 28 32 1 29 33 1 30 34 1 31 35 1 32 81 1 33 80 1 34 79 1 35 78 1 36 77 1
		 37 76 1 38 75 1 39 74 1 40 73 1 41 72 1 42 71 1 43 70 1 44 69 1 45 68 1 46 67 1 47 66 1
		 35 65 1 48 64 1 49 63 1 31 144 1 50 152 1 51 11 1 43 62 1 47 61 1 48 50 1 49 51 1
		 50 130 1 51 129 1 32 60 1 52 59 1 53 58 1 28 137 1 54 145 1 55 8 1 40 57 1 44 56 1
		 52 54 1 53 55 1 54 135 1 55 136 1 56 53 1 57 52 1 58 4 1 59 53 1 60 52 1 61 49 1
		 62 48 1 63 7 1 64 49 1 65 48 1 66 3 1 67 2 1 68 1 1 69 0 1 70 47 1 71 46 1 72 45 1
		 73 44 1 74 43 1 75 42 1 76 41 1 77 40 1 78 39 1 79 38 1 80 37 1 81 36 1 82 7 1 83 6 1
		 84 5 1 85 4 1 86 47 1 87 46 1 88 45 1 89 43 1 90 42 1 91 41 1 92 39 1 93 38 1 94 37 1
		 95 35 1 96 34 1 97 33 1 98 7 1 99 6 1 100 5 1 101 3 1 102 2 1 103 1 1 69 104 1 104 58 1
		 104 85 1 104 56 1 73 105 1 105 59 1 105 56 1 105 57 1 77 106 1 106 60 1 106 57 1
		 106 81 1;
	setAttr ".ed[166:309]" 66 107 1 107 63 1 107 61 1 107 82 1 70 108 1 108 64 1
		 108 62 1 108 61 1 74 109 1 109 65 1 109 78 1 109 62 1 86 110 1 110 101 1 110 66 1
		 110 67 1 87 111 1 111 102 1 111 67 1 111 68 1 88 112 1 112 103 1 112 68 1 112 69 1
		 89 113 1 113 86 1 113 70 1 113 71 1 90 114 1 114 87 1 114 71 1 114 72 1 91 115 1
		 115 88 1 115 72 1 115 73 1 92 116 1 116 89 1 116 74 1 116 75 1 93 117 1 117 90 1
		 117 75 1 117 76 1 94 118 1 118 91 1 118 76 1 118 77 1 95 119 1 119 92 1 119 78 1
		 119 79 1 96 120 1 120 93 1 120 79 1 120 80 1 97 121 1 121 94 1 121 80 1 121 81 1
		 101 122 1 122 98 1 122 82 1 122 83 1 102 123 1 123 99 1 123 83 1 123 84 1 103 124 1
		 124 100 1 124 84 1 124 85 1 125 12 1 126 13 1 127 14 1 128 15 1 129 19 1 130 23 1
		 131 31 1 132 30 1 133 29 1 134 28 1 135 20 1 136 16 1 125 126 1 126 127 1 127 128 1
		 128 129 1 129 151 1 130 143 1 131 132 1 132 133 1 133 134 1 134 138 1 135 146 1 136 125 1
		 137 54 1 138 135 1 139 24 1 140 25 1 141 26 1 142 27 1 143 131 1 144 50 1 137 138 1
		 138 139 1 139 140 1 140 141 1 141 142 1 142 143 1 143 144 1 145 55 1 146 136 1 147 20 1
		 148 21 1 149 22 1 150 23 1 151 130 1 152 51 1 145 146 1 146 147 1 147 148 0 148 149 0
		 149 150 0 150 151 1 151 152 1 16 153 0 17 154 0 153 154 0 148 155 0 154 155 1 147 156 0
		 156 155 0 153 156 0 18 157 0 154 157 0 149 158 0 157 158 1 155 158 0 19 159 0 157 159 0
		 150 160 0 159 160 0 158 160 0;
	setAttr -s 151 -ch 620 ".fc[0:150]" -type "polyFaces" 
		f 4 0 234 237 -37
		mu 0 4 0 1 2 3
		f 4 1 230 233 -38
		mu 0 4 4 5 6 7
		f 4 2 226 229 -39
		mu 0 4 8 9 10 11
		f 5 3 150 41 -7 -41
		mu 0 5 49 50 52 55 56
		f 5 4 149 42 -8 -42
		mu 0 5 52 58 60 61 55
		f 5 5 148 43 -9 -43
		mu 0 5 60 63 64 65 61
		f 4 250 239 -10 -239
		mu 0 4 125 126 67 71
		f 4 251 240 -11 -240
		mu 0 4 126 127 72 67
		f 4 252 241 -12 -241
		mu 0 4 127 128 76 72
		f 4 9 49 -13 -49
		mu 0 4 71 67 77 80
		f 4 10 50 -14 -50
		mu 0 4 67 72 81 77
		f 4 11 51 -15 -51
		mu 0 4 72 76 83 81
		f 4 287 280 -16 -280
		mu 0 4 147 148 84 87
		f 4 288 281 -17 -281
		mu 0 4 148 149 88 84
		f 4 289 282 -18 -282
		mu 0 4 149 150 89 88
		f 4 272 265 -19 -265
		mu 0 4 139 140 90 91
		f 4 273 266 -20 -266
		mu 0 4 140 141 92 90
		f 4 274 267 -21 -267
		mu 0 4 141 142 93 92
		f 4 18 59 258 -59
		mu 0 4 91 90 133 134
		f 4 19 60 257 -60
		mu 0 4 90 92 132 133
		f 4 20 61 256 -61
		mu 0 4 92 93 131 132
		f 5 21 63 -148 -25 -63
		mu 0 5 95 94 100 102 103
		f 5 22 64 -147 -26 -64
		mu 0 5 94 98 105 106 100
		f 5 23 65 -146 -27 -65
		mu 0 5 98 99 107 108 105
		f 4 24 222 225 -67
		mu 0 4 103 102 109 75
		f 4 25 218 221 -68
		mu 0 4 100 106 104 110
		f 4 26 214 217 -69
		mu 0 4 105 108 97 101
		f 4 27 210 213 -71
		mu 0 4 184 167 14 15
		f 4 28 206 209 -72
		mu 0 4 16 169 18 19
		f 4 29 202 205 -73
		mu 0 4 20 171 22 23
		f 4 30 198 201 -75
		mu 0 4 174 25 26 27
		f 4 31 194 197 -76
		mu 0 4 28 29 30 31
		f 4 32 190 193 -77
		mu 0 4 32 33 34 35
		f 4 33 186 189 -79
		mu 0 4 178 37 38 39
		f 4 34 182 185 -80
		mu 0 4 40 41 42 43
		f 4 35 178 181 -81
		mu 0 4 44 45 46 47
		f 4 -125 174 177 -89
		mu 0 4 176 175 53 54
		f 4 -121 170 173 -90
		mu 0 4 180 179 62 66
		f 4 -117 166 169 -40
		mu 0 4 68 183 70 73
		f 6 -116 -83 -66 85 269 -91
		mu 0 6 111 112 107 99 144 113
		f 6 -115 -84 90 86 284 -92
		mu 0 6 114 115 111 113 152 116
		f 5 -114 -85 91 87 -44
		mu 0 5 64 117 114 116 65
		f 4 275 268 -62 -268
		mu 0 4 142 143 131 93
		f 4 290 283 243 -283
		mu 0 4 150 151 130 89
		f 4 253 242 -52 -242
		mu 0 4 128 129 83 76
		f 4 70 162 165 131
		mu 0 4 12 173 74 75
		f 4 74 158 161 -101
		mu 0 4 24 177 78 79
		f 4 78 154 157 -102
		mu 0 4 36 181 82 85
		f 6 94 110 102 -263 -98 62
		mu 0 6 103 118 119 120 137 95
		f 6 95 109 103 -278 -99 -103
		mu 0 6 119 121 122 123 145 120
		f 5 96 108 40 -100 -104
		mu 0 5 122 124 49 56 123
		f 4 259 271 264 58
		mu 0 4 134 138 139 91
		f 4 260 286 279 -249
		mu 0 4 135 146 147 87
		f 4 261 238 48 -250
		mu 0 4 136 125 71 80
		f 4 -156 156 135 -109
		mu 0 4 124 82 3 49
		f 4 -157 -155 119 36
		mu 0 4 3 82 181 0
		f 4 -158 155 -97 -107
		mu 0 4 85 82 124 122
		f 4 -160 160 106 -110
		mu 0 4 121 78 85 122
		f 4 -161 -159 123 101
		mu 0 4 85 78 177 36
		f 4 -162 159 -96 -108
		mu 0 4 79 78 121 119
		f 4 -164 164 107 -111
		mu 0 4 118 74 79 119
		f 4 -165 -163 127 100
		mu 0 4 79 74 173 24
		f 4 -166 163 -95 66
		mu 0 4 75 74 118 103
		f 4 -168 168 111 84
		mu 0 4 117 70 66 114
		f 4 -169 -167 -82 89
		mu 0 4 66 70 183 180
		f 4 -170 167 113 -133
		mu 0 4 73 70 117 64
		f 4 -172 172 112 83
		mu 0 4 115 62 54 111
		f 4 -173 -171 -78 88
		mu 0 4 54 62 179 176
		f 4 -174 171 114 -112
		mu 0 4 66 62 115 114
		f 4 -176 176 -70 82
		mu 0 4 112 53 86 107
		f 4 -177 -175 -74 -129
		mu 0 4 86 53 175 172
		f 4 -178 175 115 -113
		mu 0 4 54 53 112 111
		f 4 -180 180 116 -152
		mu 0 4 165 46 69 166
		f 4 -181 -179 136 81
		mu 0 4 69 46 45 57
		f 4 -182 179 -3 -118
		mu 0 4 47 46 165 164
		f 4 -184 184 117 -153
		mu 0 4 163 42 47 164
		f 4 -185 -183 137 80
		mu 0 4 47 42 41 44
		f 4 -186 183 -2 -119
		mu 0 4 43 42 163 162
		f 4 -188 188 118 -154
		mu 0 4 161 38 43 162
		f 4 -189 -187 138 79
		mu 0 4 43 38 37 40
		f 4 -190 187 -1 -120
		mu 0 4 39 38 161 182
		f 4 -192 192 120 -137
		mu 0 4 45 34 59 57
		f 4 -193 -191 139 77
		mu 0 4 59 34 33 48
		f 4 -194 191 -36 -122
		mu 0 4 35 34 45 44
		f 4 -196 196 121 -138
		mu 0 4 41 30 35 44
		f 4 -197 -195 140 76
		mu 0 4 35 30 29 32
		f 4 -198 195 -35 -123
		mu 0 4 31 30 41 40
		f 4 -200 200 122 -139
		mu 0 4 37 26 31 40
		f 4 -201 -199 141 75
		mu 0 4 31 26 25 28
		f 4 -202 199 -34 -124
		mu 0 4 27 26 37 178
		f 4 -204 204 124 -140
		mu 0 4 33 22 51 48
		f 4 -205 -203 142 73
		mu 0 4 51 22 171 96
		f 4 -206 203 -33 -126
		mu 0 4 23 22 33 32
		f 4 -208 208 125 -141
		mu 0 4 29 18 23 32
		f 4 -209 -207 143 72
		mu 0 4 23 18 169 20
		f 4 -210 207 -32 -127
		mu 0 4 19 18 29 28
		f 4 -212 212 126 -142
		mu 0 4 25 14 19 28
		f 4 -213 -211 144 71
		mu 0 4 19 14 167 16
		f 4 -214 211 -31 -128
		mu 0 4 15 14 25 174
		f 4 -216 216 128 -143
		mu 0 4 21 97 86 172
		f 4 -217 -215 145 69
		mu 0 4 86 97 108 107
		f 4 -218 215 -30 -130
		mu 0 4 101 97 21 170
		f 4 -220 220 129 -144
		mu 0 4 17 104 101 170
		f 4 -221 -219 146 68
		mu 0 4 101 104 106 105
		f 4 -222 219 -29 -131
		mu 0 4 110 104 17 168
		f 4 -224 224 130 -145
		mu 0 4 13 109 110 168
		f 4 -225 -223 147 67
		mu 0 4 110 109 102 100
		f 4 -226 223 -28 -132
		mu 0 4 75 109 13 12
		f 4 -228 228 132 -149
		mu 0 4 63 10 73 64
		f 4 -229 -227 151 39
		mu 0 4 73 10 9 68
		f 4 -230 227 -6 -134
		mu 0 4 11 10 63 60
		f 4 -232 232 133 -150
		mu 0 4 58 6 11 60
		f 4 -233 -231 152 38
		mu 0 4 11 6 5 8
		f 4 -234 231 -5 -135
		mu 0 4 7 6 58 52
		f 4 -236 236 134 -151
		mu 0 4 50 2 7 52
		f 4 -237 -235 153 37
		mu 0 4 7 2 1 4
		f 4 -238 235 -4 -136
		mu 0 4 3 2 50 49
		f 4 6 45 -251 -45
		mu 0 4 56 55 126 125
		f 4 7 46 -252 -46
		mu 0 4 55 61 127 126
		f 4 8 47 -253 -47
		mu 0 4 61 65 128 127
		f 4 -88 93 -254 -48
		mu 0 4 65 116 129 128
		f 4 291 -87 92 -284
		mu 0 4 151 152 113 130
		f 4 276 -86 -245 -269
		mu 0 4 143 144 99 131
		f 4 -257 244 -24 -246
		mu 0 4 132 131 99 98
		f 4 -258 245 -23 -247
		mu 0 4 133 132 98 94
		f 4 -259 246 -22 -248
		mu 0 4 134 133 94 95
		f 4 97 270 -260 247
		mu 0 4 95 137 138 134
		f 4 98 285 -261 -105
		mu 0 4 120 145 146 135
		f 4 99 44 -262 -106
		mu 0 4 123 56 125 136
		f 4 -271 262 104 -264
		mu 0 4 138 137 120 135
		f 4 -272 263 248 54
		mu 0 4 139 138 135 87
		f 4 15 55 -273 -55
		mu 0 4 87 84 140 139
		f 4 16 56 -274 -56
		mu 0 4 84 88 141 140
		f 4 17 57 -275 -57
		mu 0 4 88 89 142 141
		f 4 255 -276 -58 -244
		mu 0 4 130 143 142 89
		f 4 -270 -277 -256 -93
		mu 0 4 113 144 143 130
		f 4 -286 277 105 -279
		mu 0 4 146 145 123 136
		f 4 -287 278 249 52
		mu 0 4 147 146 136 80
		f 4 294 296 -299 -300
		mu 0 4 153 154 155 156
		f 4 301 303 -305 -297
		mu 0 4 154 157 158 155
		f 4 306 308 -310 -304
		mu 0 4 157 159 160 158
		f 4 254 -291 -54 -243
		mu 0 4 129 151 150 83
		f 4 -285 -292 -255 -94
		mu 0 4 116 152 151 129
		f 4 12 293 -295 -293
		mu 0 4 80 77 154 153
		f 4 -288 297 298 -296
		mu 0 4 148 147 156 155
		f 4 -53 292 299 -298
		mu 0 4 147 80 153 156
		f 4 13 300 -302 -294
		mu 0 4 77 81 157 154
		f 4 -289 295 304 -303
		mu 0 4 149 148 155 158
		f 4 14 305 -307 -301
		mu 0 4 81 83 159 157
		f 4 53 307 -309 -306
		mu 0 4 83 150 160 159
		f 4 -290 302 309 -308
		mu 0 4 150 149 158 160;
	setAttr ".cd" -type "dataPolyComponent" Index_Data Edge 0 ;
	setAttr ".cvd" -type "dataPolyComponent" Index_Data Vertex 0 ;
	setAttr ".pd[0]" -type "dataPolyComponent" Index_Data UV 24 
		2 0 
		3 0 
		6 0 
		7 0 
		10 0 
		11 0 
		53 0 
		54 0 
		62 0 
		66 0 
		70 0 
		73 0 
		74 0 
		75 0 
		78 0 
		79 0 
		82 0 
		85 0 
		86 0 
		97 0 
		101 0 
		104 0 
		109 0 
		110 0 ;
	setAttr ".hfd" -type "dataPolyComponent" Index_Data Face 0 ;
	setAttr ".dr" 3;
	setAttr ".dsm" 2;
	setAttr ".ai_translator" -type "string" "polymesh";
createNode lightLinker -s -n "lightLinker1";
	rename -uid "5D4BD775-4DBC-B009-5D1F-CBAA3EC2A755";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode shapeEditorManager -n "shapeEditorManager";
	rename -uid "AAC72D89-4B34-F065-B69D-838803C2E767";
createNode poseInterpolatorManager -n "poseInterpolatorManager";
	rename -uid "9885EF25-4D52-F001-3D04-1CA9DE440887";
createNode displayLayerManager -n "layerManager";
	rename -uid "8F2860AF-41DB-7FF9-962A-C99233AF62CD";
createNode displayLayer -n "defaultLayer";
	rename -uid "153A3499-47E4-7B47-269B-13BE9CA6019F";
createNode renderLayerManager -n "renderLayerManager";
	rename -uid "88DBBCF3-4381-3F52-E4B9-59B148883662";
createNode renderLayer -n "defaultRenderLayer";
	rename -uid "605F86CE-48F0-FAF2-DE2D-2C92FE6AF30B";
	setAttr ".g" yes;
createNode script -n "Boulder3:uiConfigurationScriptNode";
	rename -uid "5DE78859-4842-D3E1-3BDA-84B2C3496D8D";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $nodeEditorPanelVisible = stringArrayContains(\"nodeEditorPanel1\", `getPanel -vis`);\n\tint    $nodeEditorWorkspaceControlOpen = (`workspaceControl -exists nodeEditorPanel1Window` && `workspaceControl -q -visible nodeEditorPanel1Window`);\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\n\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n"
		+ "            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n"
		+ "            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n"
		+ "            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n"
		+ "            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n"
		+ "            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n"
		+ "            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 1\n            -height 1\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n"
		+ "            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -holdOuts 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 1\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -depthOfFieldPreview 1\n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n"
		+ "            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -controllers 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 0\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n"
		+ "            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            -captureSequenceNumber -1\n            -width 580\n            -height 499\n            -sceneRenderFilter 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"ToggledOutliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"ToggledOutliner\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -autoExpandAnimatedShapes 1\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n"
		+ "            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -isSet 0\n            -isSetMember 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            -renderFilterIndex 0\n            -selectionOrder \"chronological\" \n            -expandAttribute 0\n"
		+ "            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAssignedMaterials 0\n            -showTimeEditor 1\n            -showReferenceNodes 0\n            -showReferenceMembers 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -organizeByClip 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -autoExpandAnimatedShapes 1\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showParentContainers 0\n"
		+ "            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n"
		+ "            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            -ignoreOutlinerColor 0\n            -renderFilterVisible 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n"
		+ "                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -autoExpandAnimatedShapes 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n"
		+ "                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayValues 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showPlayRangeShades \"on\" \n                -lockPlayRangeShades \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n"
		+ "                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -valueLinesToggle 1\n                -highlightAffectedCurves 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAssignedMaterials 0\n                -showTimeEditor 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n"
		+ "                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -organizeByClip 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -autoExpandAnimatedShapes 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showParentContainers 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n"
		+ "                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                -ignoreOutlinerColor 0\n                -renderFilterVisible 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayValues 0\n                -snapTime \"integer\" \n"
		+ "                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"timeEditorPanel\" (localizedPanelLabel(\"Time Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Time Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n"
		+ "            clipEditor -e \n                -displayValues 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayValues 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -initialized 0\n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n"
		+ "                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif ($nodeEditorPanelVisible || $nodeEditorWorkspaceControlOpen) {\n\t\tif (\"\" == $panelName) {\n\t\t\tif ($useSceneConfig) {\n\t\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n                -copyConnectionsOnPaste 0\n                -connectionStyle \"bezier\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n"
		+ "                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -editorMode \"default\" \n                -hasWatchpoint 0\n                $editorName;\n\t\t\t}\n\t\t} else {\n\t\t\t$label = `panel -q -label $panelName`;\n\t\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -consistentNameSize 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -connectNodeOnCreation 0\n                -connectOnDrop 0\n"
		+ "                -copyConnectionsOnPaste 0\n                -connectionStyle \"bezier\" \n                -defaultPinnedState 0\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -crosshairOnEdgeDragging 0\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                -editorMode \"default\" \n                -hasWatchpoint 0\n                $editorName;\n\t\t\tif (!$useSceneConfig) {\n\t\t\t\tpanel -e -l $label $panelName;\n\t\t\t}\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"shapePanel\" (localizedPanelLabel(\"Shape Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tshapePanel -edit -l (localizedPanelLabel(\"Shape Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"posePanel\" (localizedPanelLabel(\"Pose Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tposePanel -edit -l (localizedPanelLabel(\"Pose Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"profilerPanel\" (localizedPanelLabel(\"Profiler Tool\")) `;\n\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Profiler Tool\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"contentBrowserPanel\" (localizedPanelLabel(\"Content Browser\")) `;\n"
		+ "\tif (\"\" != $panelName) {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Content Browser\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-userCreated false\n\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 0\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 580\\n    -height 499\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -holdOuts 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 1\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -depthOfFieldPreview 1\\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -controllers 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 0\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    -captureSequenceNumber -1\\n    -width 580\\n    -height 499\\n    -sceneRenderFilter 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "Boulder3:sceneConfigurationScriptNode";
	rename -uid "D6CE8A31-4615-EFA8-06C4-1A9F1898836F";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode file -n "file1";
	rename -uid "C18BF2E6-4C98-C30D-520A-60B6FAA7E1D7";
	setAttr ".ftn" -type "string" "C:/Users/ethan/Documents/maya/projects/Pebble_BoulderUVColor.png";
	setAttr ".cs" -type "string" "sRGB";
createNode place2dTexture -n "place2dTexture1";
	rename -uid "22E484D5-4EAF-764D-7C95-26A49AAE7DC9";
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
	setAttr -s 5 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderUtilityList1;
select -ne :defaultRenderingList1;
select -ne :defaultTextureList1;
select -ne :lambert1;
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :initialMaterialInfo;
select -ne :defaultRenderGlobals;
	addAttr -ci true -h true -sn "dss" -ln "defaultSurfaceShader" -dt "string";
	setAttr ".ren" -type "string" "arnold";
	setAttr ".dss" -type "string" "lambert1";
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr ":defaultColorMgtGlobals.cme" "file1.cme";
connectAttr ":defaultColorMgtGlobals.cfe" "file1.cmcf";
connectAttr ":defaultColorMgtGlobals.cfp" "file1.cmcp";
connectAttr ":defaultColorMgtGlobals.wsn" "file1.ws";
connectAttr "place2dTexture1.c" "file1.c";
connectAttr "place2dTexture1.tf" "file1.tf";
connectAttr "place2dTexture1.rf" "file1.rf";
connectAttr "place2dTexture1.mu" "file1.mu";
connectAttr "place2dTexture1.mv" "file1.mv";
connectAttr "place2dTexture1.s" "file1.s";
connectAttr "place2dTexture1.wu" "file1.wu";
connectAttr "place2dTexture1.wv" "file1.wv";
connectAttr "place2dTexture1.re" "file1.re";
connectAttr "place2dTexture1.of" "file1.of";
connectAttr "place2dTexture1.r" "file1.ro";
connectAttr "place2dTexture1.n" "file1.n";
connectAttr "place2dTexture1.vt1" "file1.vt1";
connectAttr "place2dTexture1.vt2" "file1.vt2";
connectAttr "place2dTexture1.vt3" "file1.vt3";
connectAttr "place2dTexture1.vc1" "file1.vc1";
connectAttr "place2dTexture1.o" "file1.uv";
connectAttr "place2dTexture1.ofs" "file1.fs";
connectAttr "place2dTexture1.msg" ":defaultRenderUtilityList1.u" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "file1.msg" ":defaultTextureList1.tx" -na;
connectAttr "file1.oc" ":lambert1.c";
connectAttr "Boulder3:MediumBoulderShape.iog" ":initialShadingGroup.dsm" -na;
connectAttr "file1.msg" ":initialMaterialInfo.t" -na;
// End of Boulder3_v2.ma
