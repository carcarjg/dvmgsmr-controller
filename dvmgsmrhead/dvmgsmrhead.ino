/*
 */
#include <Keypad.h>
#include "Wire.h"               // Library for I2C communication
#include "LiquidCrystal_I2C.h"  // Library for LCD

///Define buttons
const int EmrgButPb1 = 29;
const int DispButPb2 = 31;
const int Pb3 = 28;
const int Pb4 = 27;
const int Pb5 = 26;
const int Pb6 = 25;
const int Pb7 = 24;
const int Pb8 = 23;
const int Pb9 = 22;

String headcode;
String channel;

LiquidCrystal_I2C lcd = LiquidCrystal_I2C(0x27, 20, 4);


///Inbound

//Inbound cmds to set the text for lines 0,1,2,and 3
const String IOPDEF = "at#att";
const String IOPLine0 = "at$l00";
const String IOPLine1 = "at$l01";
const String IOPLine2 = "at$l02";
const String IOPLine3 = "at$l03";

//Inbound cmd to flash Emrg button led
const String IOPFlEmrg = "at$fem";
//Inbound cmd to flash Disp button led
const String IOPFlDisp = "at$fdp";
//Inbound cmd to steady Emrg button led
const String IOPSdyEmrg = "at$sem";
//Inbound cmd to steady Disp button led
const String IOPSdyDisp = "at$sdp";
//Inbound cmd to turn off Emrg button led
const String IOPOffEmrg = "at$oem";
//Inbound cmd to turn off Disp button led
const String IOPOffDisp = "at$odp";

//Inbound cmd to power off CH
const String IOPpwrOff = "at!pwr";
//Inbound cmd to power on CH
const String IOPpwrOn = "at$pwr";

//Inbound cmd to run DSD behavior
const String IOPdsdOn = "at$dsd";
//Inbound cmd to stop DSD behavior
const String IOPdsdOff = "at!dsd";

//Inbound cmd to RequestStatus
const String IOPReqStatus = "at?ack";
//Inbound cmd to RequestSelfcheck
const String IOPReqSlfChk = "at?slf";

//ControllerReady
const String IOPControllerReady = "at&cok";

//SetHeadcode
const String IOPHeadcode = "at$hdc";
//SetChannel
const String IOPChan = "at$cha";
//SetRXRID
const String IOPRxRID = "at$rid";
//SetRXMode
const String IOPRxCall = "at$rxm";
//ClearRXMode
const String IOPNoRXCall = "at!rxm";
//SetTxMode
const String IOPTxMOde = "at$txm";
//ClearTxMode
const String IOPNoTXMOde = "at!txm";

///Outbound
//Outbound cmd to ack
const String OOPack = "at&ack";
//Outbound cmd to nack
const String OOPnack = "at!ack";
//Outbound Head ready CMD
const String OOPHeadReady = "at&hok";

//Outbound cmd Button1
const String OOPb1 = "at#b1";
//Outbound cmd Button2
const String OOPb2 = "at#b2";
//Outbound cmd Button3
const String OOPb3 = "at#b3";
//Outbound cmd Button4
const String OOPb4 = "at#b4";
//Outbound cmd Button5
const String OOPb5 = "at#b5";
//Outbound cmd Button6
const String OOPb6 = "at#b6";
//Outbound cmd Button7
const String OOPb7 = "at#b7";
//Outbound cmd Button8
const String OOPb8 = "at#b8";
//Outbound cmd Button9
const String OOPb9 = "at#b9";
//Outbound cmd Button10
const String OOPb10 = "at#b10";
//Outbound cmd Button11
const String OOPb11 = "at#b11";
//Outbound cmd Button12
const String OOPb12 = "at#b12";
//Outbound cmd Button13
const String OOPb13 = "at#b13";
//Outbound cmd Button14
const String OOPb14 = "at#b14";
//Outbound cmd Button15
const String OOPb15 = "at#b15";
//Outbound cmd Button16
const String OOPb16 = "at#b16";

unsigned long previousMillisRX = 0;
unsigned long previousMillisSTAT = 0;
unsigned long previousMillisEmrg = 0;
unsigned long previousMillisEmrgBeep = 0;

unsigned long lastctrlOKMillis = 0;
int missedStatusChecks;
const int ROW_NUM = 4;     //four rows
const int COLUMN_NUM = 4;  //four columns
bool rxcallenable;
bool rxemrgcallenable;
bool rxcallphase;
bool rxemrgcallphase;

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
  pinMode(EmrgButPb1, INPUT);
  pinMode(DispButPb2, INPUT);
  pinMode(Pb3, INPUT);
  pinMode(Pb4, INPUT);
  pinMode(Pb5, INPUT);
  pinMode(Pb6, INPUT);
  pinMode(Pb7, INPUT);
  pinMode(Pb8, INPUT);
  pinMode(Pb9, INPUT);
  pinMode(11, OUTPUT);
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
    if (Serial.available() > 0) {
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
    if (i > 30) {
      i = 0;
    } else if (i = 10) {
      Serial.println(OOPHeadReady);
    } else {
      i++;
    }

  } while (tmpcmd != IOPControllerReady);
  lcd.clear();
  digitalWrite(32, HIGH);
  digitalWrite(30, HIGH);
  lcd.setCursor(0, 0);
  lcd.print("GSM-R DVM");
  tone(11, 1000, 120);
  delay(200);
  tone(11, 1000, 120);
}
// the loop function runs over and over again forever
void loop() {
  unsigned long currentMillis = millis();


  if (currentMillis - previousMillisEmrg >= 500) {
    previousMillisEmrg = currentMillis;
    if (rxemrgcallenable == true) {
      if (rxemrgcallphase == true) {
        lcd.setCursor(0, 1);
        lcd.print("Incoming Call");
        lcd.setCursor(0, 2);
        lcd.print("STOP EMERGENCY");
        rxemrgcallphase = false;
      } else {
        lcd.setCursor(0, 1);
        lcd.print("         Call");
        lcd.setCursor(0, 2);
        lcd.print("     EMERGENCY");
        rxemrgcallphase = true;
      }
    }
  }

  if (currentMillis - previousMillisEmrgBeep >= 200) {
    previousMillisEmrgBeep = currentMillis;
    if (rxemrgcallenable == true) {
      tone(11, 1000, 120);
    }
  }



  //RXCallBlinky
  if (currentMillis - previousMillisRX >= 500) {
    previousMillisRX = currentMillis;
    if (rxcallenable == true) {
      if (rxcallphase == true) {
        lcd.setCursor(0, 1);
        lcd.print("Incoming Call");
        rxcallphase = false;
      } else {
        lcd.setCursor(0, 1);
        lcd.print("         Call");
        rxcallphase = true;
      }
    }
  }

  /*if (digitalRead(EmrgButPb1) == HIGH) {
      Serial.println(OOPb1);
      lcd.setCursor(0, 1);
      lcd.print("Button1");
    } else if (digitalRead(DispButPb2) == HIGH) {
      lcd.setCursor(0, 1);
      lcd.print("Button1");
      Serial.println(OOPb2);
    } */
  if (digitalRead(Pb3) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    lcd.print("Button1");
    Serial.println(OOPb3);
    delay(200);
    tone(11, 1050, 100);
  } else if (digitalRead(Pb4) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    lcd.print("Button2");
    Serial.println(OOPb4);
    delay(200);
    tone(11, 1050, 100);
  } else if (digitalRead(Pb5) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    lcd.print("Button3");
    Serial.println(OOPb5);
    delay(200);
    tone(11, 1050, 100);
  } else if (digitalRead(Pb6) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    lcd.print("Button4");
    Serial.println(OOPb6);
    delay(200);
    tone(11, 1050, 100);
  } else if (digitalRead(Pb7) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    lcd.print("Button5");
    Serial.println(OOPb7);
    delay(200);
    tone(11, 1050, 100);
  } else if (digitalRead(Pb8) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    lcd.print("Button6");
    Serial.println(OOPb8);
    delay(200);
    tone(11, 1050, 100);
  } else if (digitalRead(Pb9) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    lcd.print("Button7");
    Serial.println(OOPb9);
    delay(200);
    tone(11, 1050, 100);
  }
  //Look for CMD from Controller
  if (Serial.available() > 0) {
    String readfromserial;
    readfromserial = Serial.readStringUntil('\n');
    String cmdfromserial = readfromserial.substring(0, 6);
    //lcd.setCursor(0, 1);
    //lcd.print(cmdfromserial);
    //look for at$CMD
    //I wanted to use a switch... but stupid C++ said no
    if (cmdfromserial == IOPLine0) {
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 0);
      lcd.print(text);
    } else if (cmdfromserial == IOPLine1) {
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 1);
      lcd.print(text);
    } else if (cmdfromserial == IOPLine2) {
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPLine3) {
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 3);
      lcd.print(text);
    } else if (cmdfromserial == IOPFlEmrg) {
    } else if (cmdfromserial == IOPFlDisp) {
    } else if (cmdfromserial == IOPSdyEmrg) {
    } else if (cmdfromserial == IOPSdyDisp) {
    } else if (cmdfromserial == IOPOffEmrg) {
    } else if (cmdfromserial == IOPOffDisp) {
    } else if (cmdfromserial == IOPpwrOff) {
      lcd.noBacklight();
    } else if (cmdfromserial == IOPpwrOn) {
      lcd.backlight();
    } else if (cmdfromserial == IOPdsdOn) {
      rxemrgcallenable = true;
    } else if (cmdfromserial == IOPdsdOff) {
      lcd.setCursor(0, 1);
      lcd.print("                    ");
      lcd.setCursor(0, 2);
      lcd.print("                    ");
      rxemrgcallenable = false;
      rxemrgcallphase = true;
    } else if (cmdfromserial == IOPReqStatus) {
      lastctrlOKMillis = millis();
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPReqSlfChk) {
    } else if (cmdfromserial == IOPHeadcode) {
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(14, 0);
      lcd.print(text);
      lcd.setCursor(0, 2);
      lcd.print("Registering");
      delay(2000);
      lcd.setCursor(0, 2);
      lcd.print("             ");
      tone(11, 1000, 120);
      delay(200);
      tone(11, 1000, 120);
    } else if (cmdfromserial == IOPChan) {
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 3);
      lcd.print(text);
      lcd.setCursor(0, 2);
      lcd.print("Registering");
      delay(2000);
      lcd.setCursor(0, 2);
      lcd.print("             ");
      tone(11, 1000, 120);
      delay(200);
      tone(11, 1000, 120);
    } else if (cmdfromserial == IOPRxRID) {
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPRxCall) {
      rxcallenable = true;
      rxcallphase = false;
    } else if (cmdfromserial == IOPNoRXCall) {
      rxcallenable = false;
      lcd.setCursor(0, 2);
      lcd.print("             ");
      lcd.setCursor(0, 1);
      lcd.print("             ");
    } else if (cmdfromserial == IOPTxMOde) {
      lcd.setCursor(0, 1);
      lcd.print("Outgoing Call");
    } else if (cmdfromserial == IOPNoTXMOde) {
      lcd.setCursor(0, 1);
      lcd.print("             ");
    }
  }
  char keypress = keypad.getKey();
  switch (keypress) {
    case '1':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '2':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '3':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '4':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '5':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '6':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '7':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '8':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case '9':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case 'A':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case 'B':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case 'C':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
    case 'D':
      delay(200);
      tone(11, 1050, 100);
      delay(200);
      break;
  }
}