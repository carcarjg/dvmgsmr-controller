/*
 */
#include <Keypad.h>
#include "Wire.h"               // Library for I2C communication
#include "LiquidCrystal_I2C.h"  // Library for LCD

LiquidCrystal_I2C lcd = LiquidCrystal_I2C(0x27, 20, 4);

///Inbound

//Inbound cmds to set the text for lines 0,1,2,and 3
static String IOPDEF = "at#att";
static String IOPLine0 = "at$l00";
static String IOPLine1 = "at$l01";
static String IOPLine2 = "at$l02";
static String IOPLine3 = "at$l03";

//Inbound cmd to flash Emrg button led
static String IOPFlEmrg = "at$fem";
//Inbound cmd to flash Disp button led
static String IOPFlDisp = "at$fdp";
//Inbound cmd to steady Emrg button led
static String IOPSdyEmrg = "at$sem";
//Inbound cmd to steady Disp button led
static String IOPSdyDisp = "at$sdp";
//Inbound cmd to turn off Emrg button led
static String IOPOffEmrg = "at$oem";
//Inbound cmd to turn off Disp button led
static String IOPOffDisp = "at$odp";

//Inbound cmd to power off CH
static String IOPpwrOff = "at!pwr";
//Inbound cmd to power on CH
static String IOPpwrOn = "at$pwr";

//Inbound cmd to run DSD behavior
static String IOPdsdOn = "at$dsd";
//Inbound cmd to stop DSD behavior
static String IOPdsdOff = "at!dsd";

//Inbound cmd to RequestStatus
static String IOPReqStatus = "at?ack";
//Inbound cmd to RequestSelfcheck
static String IOPReqSlfChk = "at?slf";

//ControllerReady
static String IOPControllerReady = "at&RDY";

///Outbound
//Outbound cmd to ack
static String OOPack = "at&ack";
//Outbound cmd to nack
static String OOPnack = "at!ack";

const int ROW_NUM = 4;     //four rows
const int COLUMN_NUM = 4;  //four columns

char keys[ROW_NUM][COLUMN_NUM] = {
  { '1', '2', '3', 'A' },
  { '4', '5', '6', 'B' },
  { '7', '8', '9', 'C' },
  { '*', '0', '#', 'D' }
};

byte pin_rows[ROW_NUM] = { 37, 38, 39, 40 };       //connect to the row pinouts of the keypad
byte pin_column[COLUMN_NUM] = { 33, 34, 35, 36 };  //connect to the column pinouts of the keypad

Keypad keypad = Keypad(makeKeymap(keys), pin_rows, pin_column, ROW_NUM, COLUMN_NUM);

// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin 13 as an output.
  pinMode(32, OUTPUT);
  pinMode(30, OUTPUT);
  pinMode(31, INPUT);
  pinMode(29, INPUT);
  pinMode(28, INPUT);
  pinMode(27, INPUT);
  pinMode(26, INPUT);
  pinMode(25, INPUT);
  pinMode(24, INPUT);
  pinMode(23, INPUT);
  pinMode(22, INPUT);
  Serial.begin(115200);
  lcd.init();
  lcd.setCursor(0, 0);  //param1 = X   param2 = Y
  int i = 0;
  do {
    lcd.setCursor(i, 0);  //param1 = X   param2 = Y
    lcd.write(255);
    i++;
  } while (i < 20);
  lcd.setCursor(0, 1);  //param1 = X   param2 = Y
  i = 0;
  do {
    lcd.setCursor(i, 1);  //param1 = X   param2 = Y
   lcd.write(255);
    i++;
  } while (i < 20);
  lcd.setCursor(0, 2);  //param1 = X   param2 = Y
  i = 0;
  do {
    lcd.setCursor(i, 2);  //param1 = X   param2 = Y
   lcd.write(255);
    i++;
  } while (i < 20);
  lcd.setCursor(0, 3);  //param1 = X   param2 = Y
  i = 0;
  do {
    lcd.setCursor(i, 3);  //param1 = X   param2 = Y
    lcd.write(255);
    i++;
  } while (i < 20);
  lcd.setCursor(0, 0);
  i = 0;
  digitalWrite(32, LOW);
  digitalWrite(30, LOW);
  lcd.backlight();
  delay(3000);
  lcd.clear();
  //Wait for controller
  String tmpcmd;
  do {
    if (Serial.available()> 0) {
      String readfromserial;
      readfromserial = Serial.readStringUntil('\n');
      tmpcmd = readfromserial.substring(0, 6);
    }
    lcd.setCursor(2, 0);
    lcd.print("DVM GSM-R SR0.1B");
    lcd.setCursor(5, 2);
    lcd.print("**WARNING**");
    lcd.setCursor(4, 3);
    lcd.print("!!LOST MRU!!");
    if(i>250){i=0;}else{i++;}
  } while (tmpcmd != IOPControllerReady);
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("GSM-R DVM");
}

// the loop function runs over and over again forever
void loop() {
  //Look for CMD from Controller
  if (Serial.available() > 0) {
    String readfromserial;
    readfromserial = Serial.readStringUntil('\n');
    String cmdfromserial = readfromserial.substring(0, 6);
    //look for at$CMD
    //I wanted to use a switch... but stupid C++ said no
    if (cmdfromserial == IOPLine0) {
    } else if (cmdfromserial == IOPLine1) {
    } else if (cmdfromserial == IOPLine2) {
    } else if (cmdfromserial == IOPLine3) {
    } else if (cmdfromserial == IOPFlEmrg) {
    } else if (cmdfromserial == IOPFlDisp) {
    } else if (cmdfromserial == IOPSdyEmrg) {
    } else if (cmdfromserial == IOPSdyDisp) {
    } else if (cmdfromserial == IOPOffEmrg) {
    } else if (cmdfromserial == IOPOffDisp) {
    } else if (cmdfromserial == IOPpwrOff) {
    } else if (cmdfromserial == IOPpwrOn) {
    } else if (cmdfromserial == IOPdsdOn) {
    } else if (cmdfromserial == IOPdsdOff) {
    } else if (cmdfromserial == IOPReqStatus) {
    } else if (cmdfromserial == IOPReqSlfChk) {
    }
  }
  char keypress = keypad.getKey();
  digitalWrite(32, HIGH);
  digitalWrite(30, HIGH);
}
