def my_print(txt):
    print(txt)

msg_template = """hello {name} , 
Thank you for joining {website}. We are very happy to have you with us .
"""#.format(name= "robert", website= 'cfe.sh')

def format_msg(my_name= "robert", my_website= "cfe.sh"):
    my_msg=msg_template.format( name=my_name, website=my_website)
    #print(my_msg)
    return my_msg
