EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 1 1
Title ""
Date ""
Rev ""
Comp ""
Comment1 ""
Comment2 ""
Comment3 ""
Comment4 ""
$EndDescr
$Comp
L alarmclocksystem:HX711LoadCellBoard U?
U 1 1 5FA04B25
P 7600 4700
F 0 "U?" H 7600 4850 50  0001 C CNN
F 1 "HX711LoadCellBoard" H 7600 4673 50  0000 C CNN
F 2 "" H 7600 4850 50  0001 C CNN
F 3 "" H 7600 4850 50  0001 C CNN
	1    7600 4700
	1    0    0    -1  
$EndComp
$Comp
L alarmclocksystem:LoadCell U?
U 1 1 5FA05267
P 7300 3400
F 0 "U?" H 7478 2951 50  0001 L CNN
F 1 "LoadCell" H 7478 2860 50  0000 L CNN
F 2 "" H 7300 3400 50  0001 C CNN
F 3 "" H 7300 3400 50  0001 C CNN
	1    7300 3400
	1    0    0    -1  
$EndComp
Wire Wire Line
	6850 3900 6750 3900
$Comp
L alarmclocksystem:LoadCell U?
U 1 1 5FA06464
P 5650 4400
F 0 "U?" H 5492 4425 50  0001 C CNN
F 1 "LoadCell" H 5492 4334 50  0000 C CNN
F 2 "" H 5650 4400 50  0001 C CNN
F 3 "" H 5650 4400 50  0001 C CNN
	1    5650 4400
	-1   0    0    1   
$EndComp
Wire Wire Line
	6100 3900 6150 3900
Wire Wire Line
	7050 4900 6450 4900
Wire Wire Line
	7050 5000 6300 5000
Wire Wire Line
	7050 5100 6750 5100
Wire Wire Line
	6750 3900 6750 5100
Wire Wire Line
	7050 5200 6150 5200
Wire Wire Line
	6150 3900 6150 5200
$Comp
L alarmclocksystem:MSP430-Alarmclock U?
U 1 1 5FA0B0E4
P 6600 1200
F 0 "U?" H 6658 1165 50  0001 C CNN
F 1 "MSP430-Alarmclock" H 6550 1150 50  0000 C CNN
F 2 "" H 6600 1200 50  0001 C CNN
F 3 "" H 6600 1200 50  0001 C CNN
	1    6600 1200
	1    0    0    -1  
$EndComp
$Comp
L power:GND #PWR?
U 1 1 5FA08032
P 6400 2800
F 0 "#PWR?" H 6400 2550 50  0001 C CNN
F 1 "GND" H 6405 2627 50  0000 C CNN
F 2 "" H 6400 2800 50  0001 C CNN
F 3 "" H 6400 2800 50  0001 C CNN
	1    6400 2800
	1    0    0    -1  
$EndComp
$Comp
L power:VCC #PWR?
U 1 1 5FA0877C
P 6000 2650
F 0 "#PWR?" H 6000 2500 50  0001 C CNN
F 1 "VCC" H 6015 2823 50  0000 C CNN
F 2 "" H 6000 2650 50  0001 C CNN
F 3 "" H 6000 2650 50  0001 C CNN
	1    6000 2650
	1    0    0    -1  
$EndComp
Wire Wire Line
	6300 2500 6300 2850
Wire Wire Line
	6300 2850 6000 2850
Wire Wire Line
	6000 2850 6000 2650
Wire Wire Line
	6400 2500 6400 2800
$Comp
L power:GND #PWR?
U 1 1 5FA0D3DC
P 8450 5550
F 0 "#PWR?" H 8450 5300 50  0001 C CNN
F 1 "GND" H 8455 5377 50  0000 C CNN
F 2 "" H 8450 5550 50  0001 C CNN
F 3 "" H 8450 5550 50  0001 C CNN
	1    8450 5550
	1    0    0    -1  
$EndComp
$Comp
L power:VCC #PWR?
U 1 1 5FA0D9BE
P 8350 4800
F 0 "#PWR?" H 8350 4650 50  0001 C CNN
F 1 "VCC" H 8365 4973 50  0000 C CNN
F 2 "" H 8350 4800 50  0001 C CNN
F 3 "" H 8350 4800 50  0001 C CNN
	1    8350 4800
	1    0    0    -1  
$EndComp
Wire Wire Line
	8150 5350 8350 5350
Wire Wire Line
	8350 5350 8350 4800
Wire Wire Line
	8150 4900 8450 4900
Wire Wire Line
	8450 4900 8450 5550
Wire Wire Line
	8150 5200 9200 5200
$Comp
L power:GND #PWR?
U 1 1 5FA0C4CF
P 10300 3350
F 0 "#PWR?" H 10300 3100 50  0001 C CNN
F 1 "GND" H 10305 3177 50  0000 C CNN
F 2 "" H 10300 3350 50  0001 C CNN
F 3 "" H 10300 3350 50  0001 C CNN
	1    10300 3350
	1    0    0    -1  
$EndComp
$Comp
L power:VCC #PWR?
U 1 1 5FA0BEB9
P 10050 3500
F 0 "#PWR?" H 10050 3350 50  0001 C CNN
F 1 "VCC" H 10065 3673 50  0000 C CNN
F 2 "" H 10050 3500 50  0001 C CNN
F 3 "" H 10050 3500 50  0001 C CNN
	1    10050 3500
	1    0    0    -1  
$EndComp
Wire Wire Line
	10050 3650 10050 3500
Wire Wire Line
	6100 3600 6450 3600
Wire Wire Line
	6100 4200 6300 4200
Wire Wire Line
	6450 4900 6450 3600
Connection ~ 6450 3600
Wire Wire Line
	6450 3600 6850 3600
Wire Wire Line
	6300 5000 6300 4200
Connection ~ 6300 4200
Wire Wire Line
	6300 4200 6850 4200
Wire Wire Line
	8150 5050 9300 5050
$Comp
L alarmclocksystem:MicBoard U?
U 1 1 5FA05211
P 8700 2300
F 0 "U?" H 8928 1988 50  0001 L CNN
F 1 "MicBoard" H 8600 2250 50  0000 L CNN
F 2 "" H 8700 2400 50  0001 C CNN
F 3 "" H 8700 2400 50  0001 C CNN
	1    8700 2300
	1    0    0    -1  
$EndComp
$Comp
L alarmclocksystem:PassiveBuzzerBoard U?
U 1 1 5FC17ED4
P 10150 2450
F 0 "U?" H 10428 2138 50  0001 L CNN
F 1 "PassiveBuzzerBoard" H 9850 2400 50  0000 L CNN
F 2 "" H 10150 2450 50  0001 C CNN
F 3 "" H 10150 2450 50  0001 C CNN
	1    10150 2450
	1    0    0    -1  
$EndComp
Wire Wire Line
	10050 3650 10200 3650
Wire Wire Line
	10200 3650 10200 3050
Wire Wire Line
	10300 3050 10300 3350
$Comp
L power:GND #PWR?
U 1 1 5FC1B375
P 8850 3750
F 0 "#PWR?" H 8850 3500 50  0001 C CNN
F 1 "GND" H 8855 3577 50  0000 C CNN
F 2 "" H 8850 3750 50  0001 C CNN
F 3 "" H 8850 3750 50  0001 C CNN
	1    8850 3750
	1    0    0    -1  
$EndComp
$Comp
L power:VCC #PWR?
U 1 1 5FC1B37B
P 8600 3900
F 0 "#PWR?" H 8600 3750 50  0001 C CNN
F 1 "VCC" H 8615 4073 50  0000 C CNN
F 2 "" H 8600 3900 50  0001 C CNN
F 3 "" H 8600 3900 50  0001 C CNN
	1    8600 3900
	1    0    0    -1  
$EndComp
Wire Wire Line
	8600 4050 8600 3900
Wire Wire Line
	8600 4050 8750 4050
Wire Wire Line
	8750 4050 8750 3450
Wire Wire Line
	8850 3450 8850 3750
Text GLabel 7150 2100 2    50   Output ~ 0
CLK
Text GLabel 9300 5050 2    50   Input ~ 0
CLK
Text GLabel 9200 5200 2    50   Output ~ 0
LOAD
Text GLabel 7150 2200 2    50   Input ~ 0
LOAD
Wire Wire Line
	8650 3450 8650 3550
Wire Wire Line
	8650 3550 8500 3550
Wire Wire Line
	10100 3050 10100 3150
Wire Wire Line
	10100 3150 9900 3150
$Comp
L Switch:SW_Push SW?
U 1 1 5FC236A1
P 7850 1850
F 0 "SW?" V 7804 1998 50  0001 L CNN
F 1 "SW_Push" V 7850 1998 50  0000 L CNN
F 2 "" H 7850 2050 50  0001 C CNN
F 3 "~" H 7850 2050 50  0001 C CNN
	1    7850 1850
	0    1    1    0   
$EndComp
Wire Wire Line
	7850 1650 7150 1650
$Comp
L power:GND #PWR?
U 1 1 5FC24D72
P 7850 2300
F 0 "#PWR?" H 7850 2050 50  0001 C CNN
F 1 "GND" H 7855 2127 50  0000 C CNN
F 2 "" H 7850 2300 50  0001 C CNN
F 3 "" H 7850 2300 50  0001 C CNN
	1    7850 2300
	1    0    0    -1  
$EndComp
Wire Wire Line
	7850 2300 7850 2050
Text GLabel 7150 1750 2    50   Output ~ 0
BUZZ
Text GLabel 7150 1850 2    50   Input ~ 0
MIC
Text GLabel 8500 3550 0    50   Output ~ 0
MIC
Text GLabel 9900 3150 0    50   Input ~ 0
BUZZ
$EndSCHEMATC
