from selenium import webdriver
from selenium.webdriver.support.ui import WebDriverWait

#  tài khoản Github
username = "khanh21062002"
password = "987654321A@a"

# khởi chạy trình điều khiển Chrome
driver = webdriver.Chrome("chromedriver")
# truy cập trang đăng nhập github
driver.get("https://github.com/login")
# tìm tên người dùng / email và gửi chính tên người dùng đó vào chỗ nhập
driver.find_element_by_id("login_field").send_keys(username)
# tìm chỗ nhập mật khẩu và chèn cả mật khẩu
driver.find_element_by_id("password").send_keys(password)
# bấm nút đăng nhập
driver.find_element_by_name("commit").click()
# đợi trạng thái sẵn sàng hoàn tất
WebDriverWait(driver=driver, timeout=10).until(
    lambda x: x.execute_script("return document.readyState === 'complete'")
)
error_message = "Incorrect username or password."
# nếu xảy ra lỗi 
errors = driver.find_elements_by_class_name("flash-error")
# print the errors optionally
# for e in errors:
#     print(e.text)
# nếu tìm thấy thông báo lỗi đó trong các lỗi, thì đăng nhập không thành công
if any(error_message in e.text for e in errors):
    print("[!] Login failed")
else:
    print("[+] Login successful")
# đóng driver
# driver.close()