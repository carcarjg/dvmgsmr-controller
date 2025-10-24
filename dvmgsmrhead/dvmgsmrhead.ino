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
const int PT1 = 41;

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
//Display Error MSG + Clear Screen
const String IOPErrorAndClear = "at!eac";

//Set GSM-R Country
const String IOPCountrySet = "at%ctr";

//SetHeadcode
const String IOPHeadcode = "at$hdc";
const String IOPHcb0 = "at$hc0";
const String IOPHcb1 = "at$hc1";
const String IOPHcb2 = "at$hc2";
const String IOPHcb3 = "at$hc3";
const String IOPHcb4 = "at$hc4";
const String IOPHcb5 = "at$hc5";
const String IOPHcb6 = "at$hc6";
const String IOPHcb7 = "at$hc7";

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
//Reboot
const String IOPReboot = "at@rbt";
//Volume Bar 0
const String IOPVolBar0 = "at$vo0";
//Volume Bar 1
const String IOPVolBar1 = "at$vo1";
//Volume Bar 2
const String IOPVolBar2 = "at$vo2";
//Volume Bar 3
const String IOPVolBar3 = "at$vo3";
//Volume Bar 4
const String IOPVolBar4 = "at$vo4";
//Volume Bar 5
const String IOPVolBar5 = "at$vo5";

///Outbound
//Outbound cmd to ack
const String OOPack = "at&ack";
//Outbound cmd to nack
const String OOPnack = "at!ack";
//Outbound Head ready CMD
const String OOPHeadReady = "at&hok";

//Outbound cmd PTT Key
const String OOPpttk = "at#pt1";
//Outbound cmd PTT DeKey
const String OOPpttdk = "at!pt1";

//Outbound cmd Button1
const String OOPb1 = "at#b01";
//Outbound cmd Button2
const String OOPb2 = "at#b02";
//Outbound cmd Button3
const String OOPb3 = "at#b03";
//Outbound cmd Button4
const String OOPb4 = "at#b04";
//Outbound cmd Button5
const String OOPb5 = "at#b05";
//Outbound cmd Button6
const String OOPb6 = "at#b06";
//Outbound cmd Button7
const String OOPb7 = "at#b07";
//Outbound cmd Button8
const String OOPb8 = "at#b08";
//Outbound cmd Button9
const String OOPb9 = "at#b09";
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

//Outbound cmd KeyPad 1
const String OOPkp1 = "at#kp1";
//Outbound cmd KeyPad 2
const String OOPkp2 = "at#kp2";
//Outbound cmd KeyPad 3
const String OOPkp3 = "at#kp3";
//Outbound cmd KeyPad 4
const String OOPkp4 = "at#kp4";
//Outbound cmd KeyPad 5
const String OOPkp5 = "at#kp5";
//Outbound cmd KeyPad 6
const String OOPkp6 = "at#kp6";
//Outbound cmd KeyPad 7
const String OOPkp7 = "at#kp7";
//Outbound cmd KeyPad 8
const String OOPkp8 = "at#kp8";
//Outbound cmd KeyPad 9
const String OOPkp9 = "at#kp9";
//Outbound cmd KeyPad 0
const String OOPkp0 = "at#kp0";
//Outbound cmd KeyPad Check
const String OOPkpCheck = "at#chk";
//Outbound cmd KeyPad Cross
const String OOPkpCross = "at#crs";

unsigned long previousMillisRX = 0;
unsigned long previousMillisSTAT = 0;
unsigned long previousMillisEmrg = 0;
unsigned long previousMillisEmrgBeep = 0;
unsigned long previousMillisErrMSG = 0;
unsigned long previousMillisVolMSG = 0;

unsigned long lastctrlOKMillis = 0;
int missedStatusChecks;
const int ROW_NUM = 4;     //four rows
const int COLUMN_NUM = 4;  //four columns
bool rxcallenable;
bool rxemrgcallenable;
bool rxcallphase;
bool rxemrgcallphase;
bool errormsgshow;
bool volmsgshow;
bool ACTIVEPTT = false;
int country = 0;

char keys[ROW_NUM][COLUMN_NUM] = {
  { '1', '2', '3', 'A' },
  { '4', '5', '6', 'B' },
  { '7', '8', '9', 'C' },
  { '*', '0', '#', 'D' }
};

byte pin_rows[ROW_NUM] = { 37, 38, 39, 40 };       //connect to the row pinouts of the keypad
byte pin_column[COLUMN_NUM] = { 33, 34, 35, 36 };  //connect to the column pinouts of the keypad

Keypad keypad = Keypad(makeKeymap(keys), pin_rows, pin_column, ROW_NUM, COLUMN_NUM);

void setup() {
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
  pinMode(PT1, INPUT);
  pinMode(11, OUTPUT);
  digitalWrite(48, HIGH);
  pinMode(48, OUTPUT);
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
  unsigned long currentMillis = millis();
  previousMillisEmrgBeep = currentMillis;
  do {
    currentMillis = millis();
    if (Serial.available() > 0) {
      String readfromserial;
      readfromserial = Serial.readStringUntil('\n');
      tmpcmd = readfromserial.substring(0, 6);
    }
    lcd.setCursor(2, 0);
    lcd.print("DVM GSM-R SR0.1B");
    lcd.setCursor(3, 1);
    lcd.print("UIC RICS: 5670");
    lcd.setCursor(5, 2);
    lcd.print("**WARNING**");
    lcd.setCursor(4, 3);
    lcd.print("!!LOST MRU!!");
    digitalWrite(32, LOW);
    digitalWrite(30, LOW);

    if (currentMillis - previousMillisEmrgBeep >= 700) {
      previousMillisEmrgBeep = currentMillis;
      digitalWrite(32, HIGH);
      digitalWrite(30, HIGH);
    }
    if (i > 30) {
      i = 0;
    } else if (i = 10) {
      Serial.println(OOPHeadReady);
      i++;
    } else {
      i++;
    }

  } while (tmpcmd != IOPControllerReady);
  Serial.println(OOPack);
  Serial.println(OOPHeadReady);
  lcd.clear();
  digitalWrite(32, HIGH);
  digitalWrite(30, HIGH);
  lcd.setCursor(0, 0);
  lcd.print("GSM-R DVM");
  tone(11, 932.3275230361799, 100);
  delay(200);
  tone(11, 932.3275230361799, 100);
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
      tone(11, 523, 180);
      delay(180);
      tone(11, 784, 400);
    }
  }

  if (currentMillis - previousMillisErrMSG >= 2000) {
    previousMillisErrMSG = currentMillis;
    if (errormsgshow == true) {
      lcd.setCursor(0, 2);
      lcd.print("                   ");
      errormsgshow = false;
    }
  }

  if (currentMillis - previousMillisVolMSG >= 5000) {
    previousMillisVolMSG = currentMillis;
    if (volmsgshow == true) {
      lcd.setCursor(0, 1);
      lcd.print("                   ");
      lcd.setCursor(0, 2);
      lcd.print("                   ");
      volmsgshow = false;
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

  if (digitalRead(PT1) == LOW && ACTIVEPTT == true){
    delay(400);
    ACTIVEPTT = false;
    Serial.println(OOPpttdk);
  } else if(digitalRead(PT1) == HIGH && ACTIVEPTT == false){
    delay(200);
    ACTIVEPTT = true;
    Serial.println(OOPpttk);
    tone(11, 750, 100);
  } else if (digitalRead(EmrgButPb1) == LOW) {
    delay(400);
    Serial.println(OOPb1);
    delay(400);
    tone(11, 1976, 100);
  } else if (digitalRead(DispButPb2) == LOW) {
    delay(400);
    Serial.println(OOPb2);
    delay(400);
    tone(11, 1976, 100);
  } else if (digitalRead(Pb3) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    Serial.println(OOPb3);
    delay(200);
    tone(11, 1976, 100);
  } else if (digitalRead(Pb4) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    Serial.println(OOPb4);
    delay(200);
    tone(11, 1976, 100);
  } else if (digitalRead(Pb5) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    Serial.println(OOPb5);
    delay(200);
    tone(11, 1976, 100);
  } else if (digitalRead(Pb6) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    Serial.println(OOPb6);
    delay(200);
    tone(11, 1976, 100);
  } else if (digitalRead(Pb7) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    Serial.println(OOPb7);
    delay(200);
    tone(11, 1976, 100);
  } else if (digitalRead(Pb8) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    Serial.println(OOPb8);
    delay(200);
    tone(11, 1976, 100);
  } else if (digitalRead(Pb9) == HIGH) {
    delay(200);
    lcd.setCursor(0, 1);
    Serial.println(OOPb9);
    delay(200);
    tone(11, 1976, 100);
  }
  char keypress = keypad.getKey();
  switch (keypress) {
    case '1':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp1);
      delay(200);
      break;
    case '2':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp2);
      delay(200);
      break;
    case '3':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp3);
      delay(200);
      break;
    case '4':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp4);
      delay(200);
      break;
    case '5':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp5);
      delay(200);
      break;
    case '6':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp6);
      delay(200);
      break;
    case '7':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp7);
      delay(200);
      break;
    case '8':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp8);
      delay(200);
      break;
    case '9':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp9);
      delay(200);
      break;
    case 'A':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPb15);
      delay(200);
      break;
    case 'B':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPb14);
      delay(200);
      break;
    case 'C':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPb13);
      delay(200);
      break;
    case 'D':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPb12);
      delay(200);
      break;
    case '0':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkp0);
      delay(200);
      break;
    case '*':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkpCheck);
      delay(200);
      break;
    case '#':
      delay(200);
      tone(11, 1976, 100);
      Serial.println(OOPkpCross);
      delay(200);
      break;
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
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 0);
      lcd.print(text);
    } else if (cmdfromserial == IOPControllerReady) {
      Serial.println(OOPack);
      Serial.println(OOPHeadReady);
    } else if (cmdfromserial == IOPReqStatus) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPLine1) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 1);
      lcd.print(text);
    } else if (cmdfromserial == IOPLine2) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPLine3) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 3);
      lcd.print(text);
    } else if (cmdfromserial == IOPFlEmrg) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPFlDisp) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPSdyEmrg) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPSdyDisp) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPOffEmrg) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPOffDisp) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPpwrOff) {
      Serial.println(OOPack);
      lcd.noBacklight();
    } else if (cmdfromserial == IOPpwrOn) {
      Serial.println(OOPack);
      lcd.backlight();
    } else if (cmdfromserial == IOPdsdOn) {
      Serial.println(OOPack);
      rxemrgcallenable = true;
    } else if (cmdfromserial == IOPdsdOff) {
      Serial.println(OOPack);
      lcd.setCursor(0, 1);
      lcd.print("                    ");
      lcd.setCursor(0, 2);
      lcd.print("                    ");
      rxemrgcallenable = false;
      rxemrgcallphase = true;
    } else if (cmdfromserial == IOPReqSlfChk) {
      Serial.println(OOPack);
    } else if (cmdfromserial == IOPHeadcode) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 1);
      lcd.print("Registering       ");
      delay(2000);
      lcd.setCursor(0, 2);
      lcd.print("                  ");
      lcd.setCursor(0, 1);
      lcd.print("                   ");
      tone(11, 932.3275230361799, 100);
      delay(200);
      tone(11, 932.3275230361799, 100);
      lcd.setCursor(14, 0);
      lcd.print(text);
    } else if (cmdfromserial == IOPChan) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 3);
      lcd.print(text);
      lcd.setCursor(0, 2);
      lcd.print("Registering");
      delay(2000);
      lcd.setCursor(0, 2);
      lcd.print("             ");
      tone(11, 932.3275230361799, 100);
      delay(200);
      tone(11, 932.3275230361799, 100);
    } else if (cmdfromserial == IOPRxRID) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPRxCall) {
      Serial.println(OOPack);
      rxcallenable = true;
      rxcallphase = false;
    } else if (cmdfromserial == IOPNoRXCall) {
      Serial.println(OOPack);
      rxcallenable = false;
      lcd.setCursor(0, 2);
      lcd.print("             ");
      lcd.setCursor(0, 1);
      lcd.print("             ");
    } else if (cmdfromserial == IOPTxMOde) {
      Serial.println(OOPack);
      lcd.setCursor(0, 1);
      lcd.print("Outgoing Call");
    } else if (cmdfromserial == IOPNoTXMOde) {
      Serial.println(OOPack);
      lcd.setCursor(0, 1);
      lcd.print("             ");
    } else if (cmdfromserial == IOPReboot) {
      Serial.println(OOPack);
      setup();
    } else if (cmdfromserial == IOPHcb0) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPHcb1) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(1, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPHcb2) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(2, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPHcb3) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(3, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPHcb4) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(4, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPHcb5) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(5, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPHcb6) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(6, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPHcb7) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(7, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPErrorAndClear) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 2);
      lcd.print("                    ");
      lcd.setCursor(0, 1);
      lcd.print("                    ");
      errormsgshow = true;
      previousMillisErrMSG = currentMillis;
      tone(11, 523, 200);
      tone(11, 523, 400);
      lcd.setCursor(0, 2);
      lcd.print(text);
    } else if (cmdfromserial == IOPCountrySet) {
      Serial.println(OOPack);
      String text = readfromserial.substring(6, readfromserial.length());
      lcd.setCursor(0, 0);
      //These are pulled from
      //https://ec.europa.eu/eurostat/statistics-explained/index.php?title=Glossary:Country_codes
      //They are in order as I was reading from top to bottom.. save for the first three as I was trying to remember some
      //off the top of my head
      //I do realize there are a few on this list that will prob piss some people off
      //Get mad at the EU.. not me. I just read the list and put them on here
      //once again... the order is not a ranking... its just the list from top to bottom on the website.
      //K Thx...
      if (text == "1") {
        lcd.print("GSM-R US ");
      } else if (text == "2") {
        lcd.print("GSM-R GB ");
      } else if (text == "3") {
        lcd.print("GSM-R DB ");
      } else if (text == "4") {
        lcd.print("GSM-R BE ");
      } else if (text == "5") {
        lcd.print("GSM-R BG ");
      } else if (text == "6") {
        lcd.print("GSM-R CZ ");
      } else if (text == "7") {
        lcd.print("GSM-R DK ");
      } else if (text == "8") {
        lcd.print("GSM-R EE ");
      } else if (text == "9") {
        lcd.print("GSM-R IE ");
      } else if (text == "0") {
        lcd.print("GSM-R EL ");
      } else if (text == "10") {
        lcd.print("GSM-R ES ");
      } else if (text == "11") {
        lcd.print("GSM-R FR ");
      } else if (text == "12") {
        lcd.print("GSM-R HR ");
      } else if (text == "13") {
        lcd.print("GSM-R IT ");
      } else if (text == "14") {
        lcd.print("GSM-R CY ");
      } else if (text == "15") {
        lcd.print("GSM-R LV ");
      } else if (text == "16") {
        lcd.print("GSM-R LT ");
      } else if (text == "17") {
        lcd.print("GSM-R LU ");
      } else if (text == "18") {
        lcd.print("GSM-R HU ");
      } else if (text == "19") {
        lcd.print("GSM-R MT ");
      } else if (text == "20") {
        lcd.print("GSM-R NL ");
      } else if (text == "21") {
        lcd.print("GSM-R AT ");
      } else if (text == "22") {
        lcd.print("GSM-R PL ");
      } else if (text == "23") {
        lcd.print("GSM-R PT ");
      } else if (text == "24") {
        lcd.print("GSM-R RO ");
      } else if (text == "25") {
        lcd.print("GSM-R SI ");
      } else if (text == "26") {
        lcd.print("GSM-R SK ");
      } else if (text == "27") {
        lcd.print("GSM-R FI ");
      } else if (text == "28") {
        lcd.print("GSM-R SE ");
      } else if (text == "29") {
        lcd.print("GSM-R IS ");
      } else if (text == "30") {
        lcd.print("GSM-R LI ");
      } else if (text == "31") {
        lcd.print("GSM-R NO ");
      } else if (text == "32") {
        lcd.print("GSM-R CH ");
      } else if (text == "33") {
        lcd.print("GSM-R BA ");
      } else if (text == "34") {
        lcd.print("GSM-R ME ");
      } else if (text == "35") {
        lcd.print("GSM-R MD ");
      } else if (text == "36") {
        lcd.print("GSM-R MK ");
      } else if (text == "37") {
        lcd.print("GSM-R GE ");
      } else if (text == "38") {
        lcd.print("GSM-R AL ");
      } else if (text == "39") {
        lcd.print("GSM-R RS ");
      } else if (text == "40") {
        lcd.print("GSM-R TR ");
      } else if (text == "41") {
        lcd.print("GSM-R TR ");
      } else if (text == "42") {
        lcd.print("GSM-R UA ");
      } else if (text == "43") {
        lcd.print("GSM-R XK ");
      } else if (text == "44") {
        lcd.print("GSM-R AM ");
      } else if (text == "45") {
        lcd.print("GSM-R BY ");
      } else if (text == "46") {
        lcd.print("GSM-R AZ ");
      } else if (text == "47") {
        lcd.print("GSM-R DZ ");
      } else if (text == "48") {
        lcd.print("GSM-R EG ");
      } else if (text == "49") {
        lcd.print("GSM-R IL ");
      } else if (text == "50") {
        lcd.print("GSM-R JO ");
      } else if (text == "51") {
        lcd.print("GSM-R LB ");
      } else if (text == "52") {
        lcd.print("GSM-R LY ");
      } else if (text == "53") {
        lcd.print("GSM-R MA ");
      } else if (text == "54") {
        lcd.print("GSM-R PS ");
      } else if (text == "55") {
        lcd.print("GSM-R SY ");
      } else if (text == "56") {
        lcd.print("GSM-R TN ");
      } else if (text == "57") {
        lcd.print("GSM-R AR ");
      } else if (text == "58") {
        lcd.print("GSM-R AU ");
      } else if (text == "59") {
        lcd.print("GSM-R BR ");
      } else if (text == "60") {
        lcd.print("GSM-R CA ");
      } else if (text == "61") {
        lcd.print("GSM-R CN ");
      } else if (text == "62") {
        lcd.print("GSM-R HK ");
      } else if (text == "63") {
        lcd.print("GSM-R IN ");
      } else if (text == "64") {
        lcd.print("GSM-R JP ");
      } else if (text == "65") {
        lcd.print("GSM-R MX ");
      } else if (text == "66") {
        lcd.print("GSM-R NG ");
      } else if (text == "67") {
        lcd.print("GSM-R NZ ");
      } else if (text == "68") {
        lcd.print("GSM-R RU ");
      } else if (text == "69") {
        lcd.print("GSM-R SG ");
      } else if (text == "70") {
        lcd.print("GSM-R ZA ");
      } else if (text == "71") {
        lcd.print("GSM-R KR ");
      } else if (text == "72") {
        lcd.print("GSM-R TW ");
      } else if (text == "99") {
        lcd.print("GSM-R DVM");
      } else {
        lcd.print("GSM-R DVM");
      }
    } else if (cmdfromserial == IOPVolBar0) {
      lcd.setCursor(0, 1);
      lcd.print("Speaker volume     ");
      lcd.setCursor(0, 2);
      lcd.print("<     >");
      volmsgshow = true;
      previousMillisVolMSG = currentMillis;
    } else if (cmdfromserial == IOPVolBar1) {
      lcd.setCursor(0, 1);
      lcd.print("Speaker volume     ");
      lcd.setCursor(0, 2);
      lcd.print("<     >");
      lcd.setCursor(1, 2);
      lcd.write(255);
      volmsgshow = true;
      previousMillisVolMSG = currentMillis;
    } else if (cmdfromserial == IOPVolBar2) {
      lcd.setCursor(0, 1);
      lcd.print("Speaker volume     ");
      lcd.setCursor(0, 2);
      lcd.print("<     >");
      lcd.setCursor(1, 2);
      lcd.write(255);
      lcd.setCursor(2, 2);
      lcd.write(255);
      volmsgshow = true;
      previousMillisVolMSG = currentMillis;
    } else if (cmdfromserial == IOPVolBar3) {
      lcd.setCursor(0, 1);
      lcd.print("Speaker volume     ");
      lcd.setCursor(0, 2);
      lcd.print("<     >");
      lcd.setCursor(1, 2);
      lcd.write(255);
      lcd.setCursor(2, 2);
      lcd.write(255);
      lcd.setCursor(3, 2);
      lcd.write(255);
      volmsgshow = true;
      previousMillisVolMSG = currentMillis;
    } else if (cmdfromserial == IOPVolBar4) {
      lcd.setCursor(0, 1);
      lcd.print("Speaker volume     ");
      lcd.setCursor(0, 2);
      lcd.print("<     >");
      lcd.setCursor(1, 2);
      lcd.write(255);
      lcd.setCursor(2, 2);
      lcd.write(255);
      lcd.setCursor(3, 2);
      lcd.write(255);
      lcd.setCursor(4, 2);
      lcd.write(255);
      volmsgshow = true;
      previousMillisVolMSG = currentMillis;
    } else if (cmdfromserial == IOPVolBar5) {
      lcd.setCursor(0, 1);
      lcd.print("Speaker volume     ");
      lcd.setCursor(0, 2);
      lcd.print("<     >");
      lcd.setCursor(1, 2);
      lcd.write(255);
      lcd.setCursor(2, 2);
      lcd.write(255);
      lcd.setCursor(3, 2);
      lcd.write(255);
      lcd.setCursor(4, 2);
      lcd.write(255);
      lcd.setCursor(5, 2);
      lcd.write(255);
      volmsgshow = true;
      previousMillisVolMSG = currentMillis;
    }
  }
}