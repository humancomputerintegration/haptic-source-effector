#include <Servo.h>

Servo myservo;
String mes1;
int targetValueS;
int currentValueS;
//int sensorValueS;
int errorMarginS = 20;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  while (!Serial);
  myservo.attach(7);
  currentValueS=1500;
  targetValueS=1500;
}

void loop() {
  if (Serial.available()) {
    mes1 = Serial.readStringUntil('\n'); 
    targetValueS = mes1.toInt();
    delay(1);
  }

//  sensorValueS = analogRead(A0);
//  sensorValueS = map(sensorValueS, 222, 555, 900, 2100);
  
  if (currentValueS <= targetValueS - errorMarginS) {
    currentValueS += 25;
  }
  else if (currentValueS >= targetValueS + errorMarginS) {
    currentValueS -= 25;
  }
  
  myservo.write(currentValueS);
  
  Serial.println(currentValueS);
  delay(100);
}
