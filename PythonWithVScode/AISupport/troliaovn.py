import speech_recognition as sr
import pyttsx3
import datetime
import webbrowser as wb 
import os 

friday = pyttsx3.init()
voice = friday.getProperty('voices')
friday.setProperty('voices', voice[1].id)

def speak(audio):
    print("F.R.I.D.A.Y: " + audio)
    friday.say(audio)
    friday.runAndWait()
def time():
    Time = datetime.datetime.now().strftime("%H:%M:%S%p")
    speak(Time)
def welcome():
    hour=datetime.datetime.now().hour
    if hour >=6 and hour < 12:
        speak("chào buổi sáng, sếp")
    elif hour >= 12 and hour <18:
        speak("chào buổi chiều , sếp")
    elif hour >=18 and hour < 24:
        speak("chào buổi tối, sếp")
    speak("bạn cần tôi giúp gì không ?")
def command():
    c=sr.Recognizer()
    with sr.Microphone() as source:
        c.pause_threshold=2
        audio=c.listen(source)
    try: 
        query = c.recognize_google( audio ,language='vi')
        print("Khánh robert: " + query)
    except sr.UnknownValueError:
        print("vui lòng nhập lại dữ liệu vào: ")
        query=str(input("câu lệnh của bạn là: "))
    return query
if __name__ == '__main__':
    welcome()
    while True:
        query=command().lower()
        if "google" in query:
            speak("bạn muốn tra cái gì?")
            search = command().lower()
            url=f"https://www.google.com/search?q={search}"
            wb.get().open(url)
            speak(f"đây là kết quả tìm kiếm {search} của bạn trên google")
        if "youtube" in query:
            speak("bạn muốn tra cái gì?")
            search = command().lower()
            url=f"https://www.youtube.com/search?q={search}"
            wb.get().open(url)
            speak(f"đây là kết quả tìm kiếm {search} của bạn trên youtube")
        if "Facebook" in query:
            url="https://www.facebook.com/"
            wb.get().open(url)
            speak("here is Facebook")
        if "video"in query:
            meme=r"C:\Users\haduykhanh\Videos\Captures\friday.mp4"
            os.startfile(meme)
        if "time"in query:
            time()
        if "bye" in query:
            speak("goodbye, sir")
            quit()
        
