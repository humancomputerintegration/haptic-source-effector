# Haptic Source-Effector: Full-Body Haptics via Non-Invasive Brain Stimulation

This is the repository for hardware and codes used for the "Haptic Source-Effector: Full-Body Haptics via Non-Invasive Brain Stimulation" paper (ACM CHI2024).

For more information, please refer to our paper: http://lab.plopes.org/#source-effector

## citing
When using or building upon this device in an academic publication, please consider citing as follows:

Yudai Tanaka, Jacob Serfaty, and Pedro Lopes. 2024. Haptic Source-Effector: Full-Body Haptics via Non-Invasive Brain Stimulation. In Proceedings of the 2024 CHI Conference on Human Factors in Computing Systems (CHI'24), May 11-16, 2024, Hawaii, USA. ACM, NewYork, NY, USA, 15 pages. [https://dl.acm.org/doi/abs/10.1145/3544548.3581382](https://dl.acm.org/doi/full/10.1145/3613904.3642483)

## contact
For any questions about this repository, please contact yudaitanaka@uchicago.edu and/or jserfaty@uchicago.edu


## Hardware
Our device is designed to be directly affixed to Meta Quest 2 and its Elite Strap.

coilMount.stl, servoBoard.stl, and servoToHeadset(needx2).stl are for 3D-printed parts we use to incorporate the coil and the servo motor into our device.

We also use custom-cut 3-mm aluminum plates as the device's mechanical joints. The railJoints.ai has the cut patterns.

partsList.xlsx covers all the other components you need to build our device.


## Codes/motorControl
Codes to control the motors on the source-effector device.

this is on Unity2020.3.21f1. you might need the same version.

this application may only work on Mac computers.

PREP  
direct to "Assets/Scenes/SampleScene". that's the scene.

this one controls the motors via Serial and receives commands from the Windows VR PC (connected to Quest) over OSC,
hence you have to configure the two things respectively.

1: go to the stimTest script attached to tmsMotors game object. check the serial ports for the servo and the linear (their usb-c cables are labeled) and change "serialportServo" (line64) and "serialportLinear" (line78) accordingly.

2: if you don't do VR, you can skip this part. go to the OSC game object and make sure "In Port" is the same four digits as the ones on "Out Port" of the OSC script attached to the Controller GameObject in the Unity VR application. ask Jacob if you have any questions re: the Windows PC.

RUN  
after the above configuration is done, make sure that you can receive Serial messages from both the servo and linear via Arduino serial monitor. the Serial doesn't work even the ports are configured unless you open the Serial monitors beforehand.  

now you can control the motors on stimTest at inspector.

Calibration/Registration:  
make sure the calibration toggle is ON. in this state, you can move the motors by moving the three sliders on the inspector. you can register 6 different sets of the motor values as target positions: ORIGIN; Left Foot; Right Foot; Left Hand; Right Hand; and Jaw.

first, you adjust the motor positions via the sliders once you found a good position, you can click the Calib State tab and choose a target that you want to register these motor values for. if the registration is successful, these motor values will be shown next to the labels of the targets.

Move to the targets:  
now, toggle the calibration bool to OFF. in this state, you can move the motors to the registered target positions. to do so, you can simply click the State tab and select the target.
if you want to update the target positions, go back to the calibration state by toggling the bool and repeat the calibration procedure.


## Codes/Arduino
Firmware for the microcontrollers to control the motors. Upload "coilPositionControlX.ino" and "coilPositionControlYZ.ino" to each Seeeduino XIAO.


## Codes/TMS
Codes to control the TMS stimulator. Run tms.py while the Magstim device is connected to your PC/laptop's serial port.


## Codes/VR
Codes to run our interactive VR game.

this is on Unity2020.3.8f1. you might need the same version.

running this VR game requires 3 total applications: 
1. this VR scene
2. the tms.py application from Codes/TMS on the computer attached to the TMS coil
3. the Codes/motorControl application on a Mac computer attached to the motor mechanism for our device.

PREP  
direct to "Assets/Scenes/Testing". that's the main game scene. alternatively, direct to "Assets/Scenes/Calibration" for the same calibration process as in the Codes/MotorControl Unity project.

this VR scene requires SteamVR hand and foot tracking using HTC Vive trackers and our hardware device which performs tracking with a Meta Quest 2. pair the Meta Quest 2 to the PC using SteamVR with Quest Link (ideally AirLink with the AirLink).

to unify base station tracking for the HTC Vive trackers and Meta Quest 2 inside-out tracking, use the program [OpenVR Space Calibrator](https://github.com/pushrax/OpenVR-SpaceCalibrator). we recommend performing the calibration for OpenVR Space Calibrator by only pairing one HTC Vive tracker, holding it next to the Meta Quest 2 headset, initiating fast calibration, and looking around in every direction. the remaining HTC Vive trackers can be paired after calibration is complete.

when pairing the HTC Vive trackers, attach 4 trackers to the body: one on each hand and one on each foot. in SteamVR, assign the left hand tracker to "Left Shoulder", the right hand tracker to "Right Hand", the left foot tracker to "Left Foot", and the right foot tracker to "Right Foot".

before running:
1. connect all computers to the same WiFi network
2. make sure the "Out IP" on the OSC script attached to the Controller GameObject in Unity is set to the IP address of the computer that controls the TMS coil's movements
3. skip this step if the TMS coil is attached to the same PC that is running the VR application (which we recommend). make sure the "Out IP" on the TMS Controller GameObject (which is a child object of the Controller GameObject) in Unity is set to the IP address of the computer that is connected to the TMS coil.

RUN  
after the above configuration is done, first run Codes/motorControl on a separate Mac computer as described above. then, on the PC connected to the TMS coil, run tms.py. finally, once tms.py is properly initiated, run the Unity VR scene.

if the Unity VR scene is stopped, an emergency stop signal will be sent to the tms.py script that prevents further TMS stimulations. 

to restart the VR application after it is stopped, make sure to stop and restart tms.py. if it is still running, you do not need to restart the Codes/motorControl application.

PLAYING THE GAME  
shoot blasts by placing one hand outwards towards the enemy, grabbing the wrist of that hand with the opposite hand, and waiting for the blast to charge up and fire. 

while charging attacks, the enemy may fire its own attacks at the hands and feet.

ammo is listed on the back of each hand. when ammo reaches 0, an ammo box may fall. step on the box with the foot closest to the box to break it and receive ammo.

after defeating 3 enemies, a button will spawn. press the button with the right hand to finish the game.

