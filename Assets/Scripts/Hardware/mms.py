import sys
from magpy import magstim
from time import sleep
from pythonosc import osc_server
from pythonosc import udp_client
from pythonosc import dispatcher
from typing import List

ip = "127.0.0.1" #OSC IP address; here, just for local communication

# MMS OSC port
serverPort = 6969
clientPort = 6161

def armStim(address, *args):
    # if not myMagstim.isArmed():
    myMagstim.arm(receipt=False, delay=True)
    print("Magstim is armed")
    # else:
    #     print(myMagstim.isArmed())
    armed = True
    client.send_message("/armed", 1)

def disarmStim(address, *args):
    #if myMagstim.isArmed():
    myMagstim.disarm(receipt=False)
    print("Magstim is disarmed")
    # else:
    #     print("Magstim is already disarmed")
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
    #sleep(duration)
    #myMagstim.disarm(receipt=False)

def stimUntilStop(address, *args: List[any]): #output stim until it stopped externally; this induces latency before and after
    trigger = args[0]
    if trigger == 1:
        myMagstim.ignoreCoilSafetySwitch()
        myMagstim.fire(receipt=False)
        #myMagstim.ignoreCoilSafetySwitch()
    else:
        myMagstim.disarm(receipt=False)
        #myMagstim.arm(receipt=False, delay=False)

def getInfo(address, *args):
    print(myMagstim.getParameters())

def quit(address, *args):
    myMagstim.disconnect()
    sys.exit()
    #client.send_message("/connection", 0)

def config(intensity, frequency, period): #this must be executed before any stim functions; this takes roughly 250 ms to execute
    myMagstim.setPower(intensity, receipt=False, delay=False)
    myMagstim.setFrequency(frequency, receipt=False)
    myMagstim.setDuration(period, receipt=False)
    myMagstim.validateSequence()

if __name__ == "__main__": #the library owner says this is necessary to avoid collapse in Windows
    # use the following for mac/linux (currently Aki's pc)
    # myMagstim = magstim.Rapid(serialConnection='/dev/ttyS8', superRapid=1, unlockCode='7cef-ae8082c7-0e') #magstim initialization
    # use the following for Windows (currently lab's pc)
    myMagstim = magstim.Rapid(serialConnection='COM14', superRapid=1, unlockCode='7cef-ae8082c7-0e') #magstim initialization

    myMagstim.connect() #connect to magstim device via serial
    myMagstim.rTMSMode(enable=True) #activate rTMS

    # myMagstim.arm(receipt=False, delay=True)
    # armed = True

    # r = Rehamove("COM14")

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

# import sys
# from magpy import magstim
# from time import sleep
# from pythonosc import osc_server
# from pythonosc import udp_client
# from pythonosc import dispatcher
# from typing import List

# ip = "127.0.0.1" #OSC IP address; here, just for local communication

# # MMS OSC port
# serverPort = 6969
# clientPort = 6161

# def armStim(address, *args):
#     # if not myMagstim.isArmed():
#     myMagstim.arm(receipt=False, delay=True)
#     print("Magstim is armed")
#     # else:
#     #     print(myMagstim.isArmed())
#     armed = True
#     client.send_message("/armed", 1)

# def disarmStim(address, *args):
#     #if myMagstim.isArmed():
#     myMagstim.disarm(receipt=False)
#     print("Magstim is disarmed")
#     # else:
#     #     print("Magstim is already disarmed")
#     armed = False
#     client.send_message("/armed", 0)

# def set(address, *args: List[any]):
#     power = args[0]
#     frequency = args[1]
#     duration = args[2]
#     myMagstim.setPower(power, receipt=False, delay=False)
#     myMagstim.setFrequency(frequency, receipt=False)
#     myMagstim.setDuration(duration, receipt=False)
#     myMagstim.validateSequence()

# def stimQuick(address, *args: List[any]): #output stim with minimal latency; this requires the config() function to be executed beforehand; need to evaluate latency later
#     duration = args[0]
#     myMagstim.ignoreCoilSafetySwitch()
#     myMagstim.fire(receipt=False)
#     sleep(duration)
#     myMagstim.disarm(receipt=False)

# def stimUntilStop(address, *args: List[any]): #output stim until it stopped externally; this induces latency before and after
#     trigger = args[0]
#     if trigger == 1:
#         myMagstim.ignoreCoilSafetySwitch()
#         myMagstim.fire(receipt=False)
#         #myMagstim.ignoreCoilSafetySwitch()
#     else:
#         myMagstim.disarm(receipt=False)
#         #myMagstim.arm(receipt=False, delay=False)

# def getInfo(address, *args):
#     print(myMagstim.getParameters())

# def quit(address, *args):
#     myMagstim.disconnect()
#     sys.exit()
#     #client.send_message("/connection", 0)

# def config(intensity, frequency, period): #this must be executed before any stim functions; this takes roughly 250 ms to execute
#     myMagstim.setPower(intensity, receipt=False, delay=False)
#     myMagstim.setFrequency(frequency, receipt=False)
#     myMagstim.setDuration(period, receipt=False)
#     myMagstim.validateSequence()

# if __name__ == "__main__": #the library owner says this is necessary to avoid collapse in Windows
#     # use the following for mac/linux (currently Aki's pc)
#     # myMagstim = magstim.Rapid(serialConnection='/dev/ttyS8', superRapid=1, unlockCode='7cef-ae8082c7-0e') #magstim initialization
#     # use the following for Windows (currently lab's pc)
#     myMagstim = magstim.Rapid(serialConnection='COM20', superRapid=1, unlockCode='7cef-ae8082c7-0e') #magstim initialization

#     myMagstim.connect() #connect to magstim device via serial
#     myMagstim.rTMSMode(enable=True) #activate rTMS

#     # myMagstim.arm(receipt=False, delay=True)
#     # armed = True

#     # r = Rehamove("COM14")

#     dispatcher = dispatcher.Dispatcher()
#     dispatcher.map("/arm", armStim)
#     dispatcher.map("/disarm", disarmStim)
#     dispatcher.map("/set", set)
#     dispatcher.map("/stim", stimUntilStop)
#     dispatcher.map("/periodicStim", stimQuick)
#     dispatcher.map("/getinfo", getInfo)
#     dispatcher.map("/disconnect", quit)

#     #start OSC server and keep running
#     server = osc_server.ThreadingOSCUDPServer((ip, serverPort), dispatcher)
#     print("Serving on {}".format(server.server_address))

#     client = udp_client.SimpleUDPClient(ip, clientPort)
#     print("connected to OSC server at "+ip+":"+str(clientPort))
#     client.send_message("/armed", 1)

#     server.serve_forever()
