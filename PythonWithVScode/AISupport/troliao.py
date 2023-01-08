import speech_recognition
import pyttsx3
from datetime import date
from datetime import datetime
import webbrowser as wb 
import os 

robot_ear = speech_recognition.Recognizer()
robot_mouth = pyttsx3.init()
robot_voice = robot_mouth.getProperty('voices')
robot_mouth.setProperty('voices', robot_voice[1].id)
robot_brain = ""
while True:
 with speech_recognition.Microphone() as mic:
    print("Alice: I'm Listening")
    audio = robot_ear.listen(mic)
 
 print("Alice: ...")

 try:
   you = robot_ear.recognize_google(audio)
 except:
    you = ""

 print("You: "+ you) 

 if you =="":
    robot_brain = "I can't hear you, try again "
 elif "your name" in you:
    robot_brain =" My name is Alice "
 elif "hello" in you :
    robot_brain = " Hello robert"
 elif "today" in you :
    today = date.today()
 # Textual month, day and year	
    robot_brain = today.strftime("%B %d, %Y")
 elif "time" in you:
    now = datetime.now()
    robot_brain = now.strftime("%H:%M:%S")
 elif "Google" in you :
    robot_brain = "what should I search boss ?"
    url= "https://www.google.com/search?q="
    wb.get().open(url)
    robot_brain= "here is Google"
 elif "YouTube" in you :
    url= "https://www.youtube.com/"
    wb.get().open(url)
    robot_brain= "here is You tube"
 elif "Facebook" in you:
    url= "https://www.facebook.com/"
    wb.get().open(url)
    robot_brain= "here is Facebook"
 elif "open video"in you:
    meme=r"" 
    os.startfile(meme)
 elif "how are you" in you :
    robot_brain = "I'm fine, thanks and you"
 elif "bye" in you:
    robot_brain = "Bye robert"
    print("Alice: " + robot_brain)
    robot_mouth.say(robot_brain)
    robot_mouth.runAndWait()
    break 
 else:
    robot_brain= "no problem =))"


 print("Alice: " + robot_brain)
 robot_mouth.say(robot_brain)
 robot_mouth.runAndWait()

