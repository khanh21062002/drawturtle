import requests
from datetime import datetime

from formatting import format_msg

def send(name, website = None):
    if website != None:
        msg = format_msg(my_name=name,my_website=website)
    else:
        msg = format_msg(my_name= name)
    #send the message
    r =requests.get("http://httpbin.org/json")
    if r.status_code == 200:
        return r.json()
    else:
        return "there was an error"
    