#define L1_1 3
#define L1_2 4
#define L2_1 5
#define L2_2 6

String mes2, mes3;

int targetValueL1, targetValueL2;
int errorMarginL = 10;

void setup() {
  Serial.begin(115200);
  while (!Serial);
  pinMode(L1_1, OUTPUT);
  pinMode(L1_2, OUTPUT);
  targetValueL1=targetValueL2=500;
}

void loop() {
  if (Serial.available()) {
    mes2 = Serial.readStringUntil(',');
    mes3 = Serial.readStringUntil('\n');
    
    targetValueL1 = mes2.toInt();
    targetValueL2 = mes3.toInt();

    delay(1);
  }

  // Logic for L1
  int sensorValueL1 = analogRead(A2);
  controlMotor(sensorValueL1, targetValueL1, 1);

  // Logic for L2
  int sensorValueL2 = analogRead(A1);  // Assuming the sensor for M2 is on A11
  controlMotor(sensorValueL2, targetValueL2, 2);

  Serial.print("M1: ");
  Serial.print(sensorValueL1);
  Serial.print(", M2: ");
  Serial.println(sensorValueL2);
  delay(1);
}

void controlMotor(int sensorValue, int targetValue, int motorNum) {
  if (abs(sensorValue - targetValue) < errorMarginL) {
    if (motorNum == 1) {
      analogWrite(L1_1, 0);
      digitalWrite(L1_1, LOW);
      analogWrite(L1_2, 0);
    } else {
      analogWrite(L2_1, 0);
      digitalWrite(L2_1, LOW);
      analogWrite(L2_2, 0);
    }
  } 
  else if (sensorValue <= targetValue - 4*errorMarginL) {
    if (motorNum == 1) {
      analogWrite(L1_1, 0);
      digitalWrite(L1_1, LOW);
      analogWrite(L1_2, 200);
    } else {
      analogWrite(L2_1, 0);
      digitalWrite(L2_1, LOW);
      analogWrite(L2_2, 200);
    }
  } 
  else if (sensorValue >= targetValue + 4*errorMarginL) {
    if (motorNum == 1) {
      analogWrite(L1_2, 0);
      digitalWrite(L1_2, LOW);
      analogWrite(L1_1, 200);
    } else {
      analogWrite(L2_2, 0);
      digitalWrite(L2_2, LOW);
      analogWrite(L2_1, 200);
    }
  }
}
