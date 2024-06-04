# Haptic Source-Effector: Full-Body Haptics via Non-Invasive Brain Stimulation

This is the repository for hardware and codes used for the "Haptic Source-Effector: Full-Body Haptics via Non-Invasive Brain Stimulation" paper (ACM CHI2024).

For more information, please refer to our paper: http://lab.plopes.org/#source-effector

## citing
When using or building upon this device in an academic publication, please consider citing as follows:

Yudai Tanaka, Jacob Serfaty, and Pedro Lopes. 2024. Haptic Source-Effector: Full-Body Haptics via Non-Invasive Brain Stimulation. In Proceedings of the 2024 CHI Conference on Human Factors in Computing Systems (CHI'24), May 11-16, 2024, Hawaii, USA. ACM, NewYork, NY, USA, 15 pages. [https://dl.acm.org/doi/abs/10.1145/3544548.3581382](https://dl.acm.org/doi/full/10.1145/3613904.3642483)

## contact
For any questions about this repository, please contact yudaitanaka@uchicago.edu and/or jserfaty@uchicago.edu


## Hardware
Our device is designed to directly affixed to Meta Quest 2 and its Elite Strap.

coilMount.stl, servoBoard.stl, and servoToHeadset(needx2).stl are for 3D-printed parts we use to incorporate the coil and the servo motor into our device.

We also use custom-cut 3-mm aluminum plates as the device's mechanical joints. The railJoints.ai has the cut patterns.

partsList.xlsx covers all the other components you need to build our device.


## Codes/motorControl
Codes to control the motors on the source-effector device.

this is on Unity2020.3.21f1. you might need the same version.

PREP  
direct to "Assets/Scenes/SampleScene". that's the scene.

this one controls the motors via Serial and receives commands from the Windows VR pc (connected to Quest) over OSC,
hence you have to configure the two things respectively.

1: go to the stimTest script attached to tmsMotors game object. check the serial ports for the servo and the linear (their usb-c cables are labelled) and change "serialportServo" (line64) and "serialportLinear" (line78) accordingly.

2: if you don't do VR, you can skip this part. go to the OSC game object and In Port is the same four digits as the ones on Out Port of the OSC game object in the Windows. ask Jacob if you have any questions re: the windows pc.

RUN  
after the above configuration is done, make sure that you can receive Serial messages from both the servo and linear via Arduino serial monitor. the Serial doesn't work even the ports are configured unless you open the Serial monitors beforehand.  

now you can control the motors on stimTest at inspector.

Calibration/Registration:  
make sure the calibration toggle is ON. in this state, you can move the motors by moving the three sliders on the inspector. you can register 6 different sets of the motor values as target positions: ORIGIN; Left Foot; Right Foot; Left Hand; Right Hand; and Jaw.

first, you adjust the motor positions via the sliders once you found a good position, you can click the Calib State tab and choose a target that you want to register these motor values for. if the registration is successful, these motor values will be shown next to the labels of the targets.

Move to the targets:  
now, toggle the calibration bool to OFF. in this state, you can move the motors to the registered target positions. to do so, you can simply click the State tab and select the target.
if you want to update the target positions, go back to calibration state by toggling the bool and repeat the calibration procedure.


## Codes/Arduino
Firmware for the microcontrollers to control the motors. Upload "coilPositionControlX.ino" and "coilPositionControlYZ.ino" to each Seeeduino XIAO.


## Codes/TMS
Codes to control the TMS stimulator. Run tms.py while the Magstim device is connected to your PC/laptop's serial port.


## Codes/VR
Codes to run our interactive VR game.
