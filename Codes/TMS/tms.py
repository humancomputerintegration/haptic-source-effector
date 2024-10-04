import sys
from magpy import magstim
from time import sleep
from pythonosc import osc_server
from pythonosc import udp_client
from pythonosc import dispatcher
from typing import List

ip = "127.0.0.1" #OSC IP address; here, just for local communication

# TMS OSC port
serverPort = 6969
clientPort = 6161

#TMS unlock code
unlockCode = '7cef-ae8082c7-0e'

def armStim(address, *args):
    myMagstim.arm(receipt=False, delay=True)
    print("Magstim is armed")
    armed = True
    client.send_message("/armed", 1)

def disarmStim(address, *args):
    myMagstim.disarm(receipt=False)
    print("Magstim is disarmed")
    armed = False
    client.send_message("/armed", 0)

def set(address, *args: List[any]):
    power = args[0]
    frequency = args[1]
    duration = args[2]
    myMagstim.setPower(power, receipt=False, delay=False)
    myMagstim.setFrequency(frequency, receipt=False)
    myMagstim.setDuration(duration, receipt=False)
    myMagstim.validateSequence()

def stimQuick(address, *args: List[any]): #output stim with minimal latency; this requires the config() function to be executed beforehand; need to evaluate latency later
    duration = args[0]
    myMagstim.ignoreCoilSafetySwitch()
    myMagstim.fire(receipt=False)

def stimUntilStop(address, *args: List[any]): #output stim until it stopped externally; this induces latency before and after
    trigger = args[0]
    if trigger == 1:
        myMagstim.ignoreCoilSafetySwitch()
        myMagstim.fire(receipt=False)
    else:
        myMagstim.disarm(receipt=False)

def getInfo(address, *args):
    print(myMagstim.getParameters())

def quit(address, *args):
    myMagstim.disconnect()
    sys.exit()

def config(intensity, frequency, period): #this must be executed before any stim functions; this takes roughly 250 ms to execute
    myMagstim.setPower(intensity, receipt=False, delay=False)
    myMagstim.setFrequency(frequency, receipt=False)
    myMagstim.setDuration(period, receipt=False)
    myMagstim.validateSequence()

if __name__ == "__main__": 
    # use the following for mac/linux 
    # myMagstim = magstim.Rapid(serialConnection='/dev/ttyS8', superRapid=1, unlockCode=unlockCode) #magstim initialization
    # use the following for Windows 
    myMagstim = magstim.Rapid(serialConnection='COM8', superRapid=1, unlockCode=unlockCode) #magstim initialization

    myMagstim.connect() #connect to magstim device via serial
    myMagstim.rTMSMode(enable=True) #activate rTMS

    dispatcher = dispatcher.Dispatcher()
    dispatcher.map("/arm", armStim)
    dispatcher.map("/disarm", disarmStim)
    dispatcher.map("/set", set)
    dispatcher.map("/stim", stimUntilStop)
    dispatcher.map("/periodicStim", stimQuick)
    dispatcher.map("/getinfo", getInfo)
    dispatcher.map("/disconnect", quit)

    #start OSC server and keep running
    server = osc_server.ThreadingOSCUDPServer((ip, serverPort), dispatcher)
    print("Serving on {}".format(server.server_address))

    client = udp_client.SimpleUDPClient(ip, clientPort)
    print("connected to OSC server at "+ip+":"+str(clientPort))
    client.send_message("/armed", 1)

    server.serve_forever()
