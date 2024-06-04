import sys
from magpy import magstim
from time import sleep
# from pythonosc import osc_server
# from pythonosc import udp_client
# from pythonosc import dispatcher
from typing import List

class TMS:
    def __init__(self, fake = False):
        if fake:
            port = ''
        else:
            port = '/dev/tty.usbserial-14320'
        self.myMagstim = magstim.Rapid(serialConnection=port, superRapid=1, unlockCode='7cef-ae8082c7-0e') #magstim initialization
        self.myMagstim.connect() #connect to magstim device via serial
        self.myMagstim.rTMSMode(enable=True) #activate rTMS

    def config(self, intensity, frequency = 1, period = 0.2): #this must be executed before any stim functions; this takes roughly 250 ms to execute
        self.myMagstim.setPower(intensity, receipt=False, delay=False)
        self.myMagstim.setFrequency(frequency, receipt=False)
        self.myMagstim.setDuration(period)
        self.myMagstim.validateSequence()
        self.myMagstim.arm(receipt=False, delay=True)
    # def config(intensity, frequency, period):
    #     myMagstim.setPower(intensity, receipt=False, delay=False)
    #     myMagstim.setFrequency(frequency, receipt=False)
    #     myMagstim.setDuration(period)
    #     myMagstim.validateSequence()

    # def armStim():
    #     myMagstim.arm(receipt=False, delay=True)
    #     armed = True

    # def disarmStim():
    #     myMagstim.disarm(receipt=False)

    # def stimQuick(): #output stim with minimal latency; this requires the config() function to be executed beforehand; need to evaluate latency later
    #     myMagstim.ignoreCoilSafetySwitch()
    #     myMagstim.fire(receipt=False)

    def pulse(self, intensity): #0.05 per pulse
        self.myMagstim.ignoreCoilSafetySwitch()
        self.myMagstim.fire(receipt=False)
        # self.sleep(period)
        # self.myMagstim.disarm(receipt=False)
        # self.myMagstim.arm(receipt=False, delay=True)


# if __name__ == "__main__": #the library owner says this is necessary to avoid collapse in Windows
#     # use the following for mac/linux (currently Aki's pc)
#     # myMagstim = magstim.Rapid(serialConnection='/dev/ttyS8', superRapid=1, unlockCode='7cef-ae8082c7-0e') #magstim initialization
#     # use the following for Windows (currently lab's pc)
#     myMagstim = magstim.Rapid(serialConnection='/dev/tty.usbserial-14320', superRapid=1, unlockCode='7cef-ae8082c7-0e') #magstim initialization

#     myMagstim.connect() #connect to magstim device via serial
#     myMagstim.rTMSMode(enable=True) #activate rTMS
#     TMS.config(30, 20, 0.2)
#     TMS.armStim()
#     while True:
#         user_input = input("c = config; a = arm; s = stim: ")
#         if user_input == 's':
#             TMS.stimQuick()
#             sleep(0.15)
#             TMS.disarmStim()
#             TMS.armStim()
