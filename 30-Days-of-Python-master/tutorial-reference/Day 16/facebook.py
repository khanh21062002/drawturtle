from selenium import webdriver
import time
from getpass import getpass
from webdriver_manager.chrome import ChromeDriverManager 
from selenium.webdriver.support.ui import WebDriverWait

username = "0822131126"
passwd = "haduykhanh21062002"

#driver = webdriver.Chrome(ChromeDriverManager().install)
driver = webdriver.Chrome("chromedriver")
driver.get('https://www.facebook.com')

txtUsername = driver.find_element_by_id('email')
txtUsername.send_keys(username)

txtPasswd = driver.find_element_by_id('pass')
txtPasswd.send_keys(passwd)

time.sleep(1.5)
btnLogin = driver.find_element_by_css_selector("button[type='submit']")
btnLogin.click()