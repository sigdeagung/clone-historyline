#include <SoftwareSerial.h>
SoftwareSerial Bluetooth(10, 9); // RX, TX
String data = "";
int LED = 12; 


void setup() {
  //Bluetooth module baud rate which I set using AT commands
  Bluetooth.begin(38400);
  //Serial baud rate which I use to communicate with the Serial Monitor in the Arduino IDE
  Serial.begin(9600);
  Serial.println("Waiting for command...");

  pinMode(LED,OUTPUT);
}

void loop() {

 if(Bluetooth.available() > 0) {
     data = Bluetooth.readStringUntil('\n');

     if (data == "0,1") {
         digitalWrite(LED,1);
         Serial.println("LED ON!"); 
 }
}