import requests
from bs4 import BeautifulSoup
import os
from time import sleep
def downloadFile(url, filename):
    if os.path.isfile(filename) is not True:
        raw = requests.get(url, stream=True)
        with open(filename, 'wb') as fd:
            for chunk in raw.iter_content(chunk_size=1024):
                fd.write(chunk)

def vov6_crawl(start, end, MUC, PT, SRC, R, S, A, B):
    cnt = start
    base = 'http://vov6.vov.vn'
    url = 'http://vov6.vov.vn/' + MUC + '.aspx'
    h = {'user-agent': 'my-app/0.0.1'}

    save_file_path = path + '\\vov6_save_file.txt'
    file = open(save_file_path, 'a', encoding='utf-8')
    file.close()

    # Chuyển trang
    while(cnt <= end):
        pageUrl = url.replace('.aspx', '-p{}.aspx'.format(cnt))
        print('\n----------{}'.format(pageUrl))
        r = requests.get(pageUrl, headers=h) # Lấy source code của page
        soup = BeautifulSoup(r.content, 'html.parser') # Chuyển source code của page thành kiểu dữ liệu BeautifulSoup

        for BaiViet in soup.findAll(['h1', 'h4'], {'class': 'title'}):
            try:
                LinkBaiViet = base + BaiViet.find('a').get('href') # Lấy link bài viết
                with open(save_file_path, 'r', encoding='utf-8') as save_file:
                    save_data = save_file.readlines()
                if (LinkBaiViet + '\n') in save_data: # KIỂM TRA XEM LINK BÀI VIẾT CÓ BỊ TRÙNG KO
                    continue
                
                rbv = requests.get(LinkBaiViet, headers=h) # Lấy source code của bài viết
                soupBaiViet = BeautifulSoup(rbv.content, 'html.parser') # Chuyển source code của bài viết thành kiểu dữ liệu BeautifulSoup

                # THỜI GIAN XUẤT BẢN CỦA BÀI VIẾT
                date = soupBaiViet.find('span', {'class': 'time'})
                if date is None:
                    continue
                date = date.get_text().split('/')
                yyyy = date[-1]
                mm = date[-2]
                dd = date[-3].split(' ')[-1]
                hh = date[-3].split(' ')[-2].split(':')[-2]
                mnmn = date[-3].split(' ')[-2].split(':')[-1]

                print(LinkBaiViet)

                # FILE NAME
                filename = '{}{}{:04}{:02}{:02}{:02}{:02}{}{}{}{}'.format(PT, SRC, int(yyyy), int(mm), int(dd), int(hh), int(mnmn), R, S, A, B)

                #AUDIO
                audioName = filename + '.mp3' # Đặt tên file audio
                LinkAudio = soupBaiViet.find('source').get('src') # Link file audio
                downloadFile(LinkAudio, audioName) # download audio
                sleep(1)
                print(audioName)
                with open(save_file_path, 'a', encoding='utf-8') as save_file: #LƯU LẠI LINK BÀI VIẾT VÀO FILE
                    save_file.write(LinkBaiViet + '\n')
            except Exception as e:
                print(e)
        cnt = cnt + 1

path = 'D:\\Nghiencuukhoahoc\\Day 1\\0.Original\\vov' #ÉC ÉC ÉC ÉC ÉC ÉC ÉC ÉC ÉC ÉC ÉC ÉC
if(os.path.isdir(path) == False):
    os.mkdir(path)
os.chdir(path)

#                           MỤC              PT    SRC                   B
vov6_crawl(1, 3, 'doc-truyen-dem-khuya-c11', '02', '000', 'R', 'S', 'A', '4')

'''
PT: CHỦ ĐỀ:
00	Tin tức: từ các nguồn thời sự, tin trên các đài phát thanh, truyền hình, trang tin							
01	Phim, kịch: có các đoạn hội thoại							
02	Đọc truyện, kể chuyện, sách nói							
03	Phỏng vấn:							
04	Talk show: Các đối đáp, trên các chương trình talkshow							
05	Đời sống: những phần bình của các đoạn về cuộc sống, du lịch, ..							
06	Thể thao: Tin thể thao, bình luận thể thao							
07	An ninh cuộc sống: trộm, cướp, ..							
08	Giao thông: tin tức, an toàn giao thông							
09	Sức khỏe: tư vấn sức khỏe, …	

SRC: NGUỒN:
000	VOV: thuộc Đài tiếng nói Việt Nam (không phân biệt VOV1, 2,…)						
001	VTV: thuộc Đài truyền hình Việt Nam (không phân biệt VTV1, 2, …)						
002	VTC: thuộc Đài TH Kỹ thuật số VTC (không phân biệt VTC1, 2, …)						
003	HCMTV: thuộc đài phát thanh và truyền hình TP. Hồ Chí Minh						
004	HanoiTV: thuộc Đài phát thanh và truyền hình Hà Nội						
005	Dân trí: từ trang Dân trí			

B: NGƯỜI SƯU TẦM:
0	Hưng
1	Dương
2	Đông
3	Lộc
4	Hoàng									
'''