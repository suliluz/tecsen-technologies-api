#include <Wire.h>
#include <Servo.h>

bool reportStatusMode = false;
bool liveConnection = false;

//Define the instruction type
const unsigned char ANALOG_PIN = 0x29;
const unsigned char DIGITAL_PIN_INPUT = 0x28;
const unsigned char DIGITAL_PIN_OUTPUT = 0x2B;
const unsigned char DIGITAL_PWM = 0x5F;
const unsigned char SERVO_PIN = 0x2F;
const unsigned char UART = 0x41;
const unsigned char UART_INITIALIZE = 0x61;
const unsigned char HANDSHAKE = 0x3F; 

//Messages
const unsigned char ANALOG_MSG = 0x40;
const unsigned char DIGITAL_MSG = 0x21;
const unsigned char ERROR_MSG = 0x58;

//Intitialize fixed values
const int dpn = NUM_DIGITAL_PINS - NUM_ANALOG_INPUTS;
const unsigned char feedback[] = {0x7E, 0x72, 0x69};
int digitalPins[dpn];

//Define serial state
bool UART_online = false;

//Some variable storing
unsigned char storage[3];
unsigned char clientReport[3];

void Handshake() {
	clientReport[0] = 0x7E;
	clientReport[1] = (unsigned char) NUM_DIGITAL_PINS;
	clientReport[2] = (unsigned char) NUM_ANALOG_INPUTS;
	
	Serial.write(clientReport, 3);
	
	memset(clientReport, 0, sizeof clientReport);
}

void DigitalInputReport() {
	for (int i = 0; i < dpn; ++i) {
		if(digitalPins[i] == digitalRead(i)) {
			digitalPins[i] = digitalRead(i);
		} else {
			int value = digitalRead(i);
			digitalPins[i] = value;
			
			clientReport[0] = DIGITAL_MSG;
			clientReport[1] = (unsigned char) i;
			clientReport[2] = (unsigned char) value;		
		
			Serial.write(clientReport, 3);
			memset(clientReport, 0, sizeof clientReport);
		}
	}
}

void StartSequence() {
  pinMode(LED_BUILTIN, OUTPUT);
  
  for(int n = 0; n < 10; ++n) {
    digitalWrite(LED_BUILTIN, HIGH);   
    delay(100);                       
    digitalWrite(LED_BUILTIN, LOW);    
    delay(100); 
  }    
  
  for(int i = 0; i < dpn; ++i) {
	  pinMode(i, INPUT);
	  digitalWrite(i, HIGH);
	  digitalPins[i] = digitalRead(i);
  }
}

void DigitalPin(int pin, int value, unsigned char type) {
  
  switch(type) {
	  case DIGITAL_PIN_INPUT:
		pinMode(pin, INPUT);
		break;
	  case DIGITAL_PIN_OUTPUT:
		pinMode(pin, OUTPUT);
		if (value == 1) digitalWrite(pin, HIGH);
		else if (value == 0) digitalWrite(pin, LOW);
		else {
		  clientReport[0] = ERROR_MSG;
		  clientReport[1] = DIGITAL_PIN_OUTPUT;
		  clientReport[2] = 0x21;
		  Serial.write(clientReport, 3);
		}
		break;
	  case DIGITAL_PWM:
		pinMode(pin, OUTPUT);
		analogWrite(pin, value);
  }
  
  memset(clientReport, 0, sizeof clientReport);
}

void AnalogRead(int pin, bool report) {
  int output = map(analogRead(pin), 0, 1023, 0, 255);
  

  clientReport[0] = ANALOG_MSG;
  clientReport[1] = (unsigned char) pin;
  clientReport[2] = (unsigned char) output;
  
  memset(clientReport, 0, sizeof clientReport);
}

void ServoPin(int pin, int value) {
	Servo servo;
	servo.attach(pin);
	servo.write(value);
	servo.detach();
}

void setup() {
  Serial.begin(9600);
  StartSequence();
}

void Listen() {
  if(Serial.available() < 3) {
    return;
  }
    
  for(int n = 0; n < 3; n++) {
    storage[n] = Serial.read();
  }

  if(sizeof(storage) == 3) {
    unsigned char clientReport[3];

    if(reportStatusMode == true) {
      Serial.write(feedback, 3);
      delay(100);
    }
    
    int pin = (int) storage[1];
    int state = (int) storage[2];
    
    switch(storage[0]) {
		
      case ANALOG_PIN:
        
        if(state == 0) {
          AnalogRead(pin, false);
        } else if (state == 1) {
          AnalogRead(pin, true);
        }
        
        memset(storage, 0, sizeof storage);
        break;
      case DIGITAL_PIN_INPUT:
	  case DIGITAL_PIN_OUTPUT:
	  case DIGITAL_PWM:
	  
		DigitalPin(pin, state, storage[0]);
		
        memset(storage, 0, sizeof storage);
        break;
      case SERVO_PIN:
		
		ServoPin(pin, state);
		
        memset(storage, 0, sizeof storage);
        break;
      case HANDSHAKE:
        
        if(pin == 0 && state == 0) {
          reportStatusMode = false;
          liveConnection = false;
        }
        else if(pin == 0 && state == 1) {
          reportStatusMode = false;
          liveConnection = true;
        } 
        else if(pin == 1 && state == 1) {
          liveConnection = true;
          reportStatusMode = true;
        } 
        
		    Handshake();  
        memset(storage, 0, sizeof storage);
        break;
      default:  
        clientReport[0] = ERROR_MSG;
        clientReport[1] = 0x2D;
        clientReport[2] = 0x2D;
        
        Serial.write(clientReport, 3);
        
        memset(clientReport, 0, sizeof clientReport);
        memset(storage, 0, sizeof storage);
    }
  
  } 
}

void loop() {
  Listen();
  DigitalInputReport();
}
