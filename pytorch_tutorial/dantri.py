import requests
from bs4 import BeautifulSoup
import os

def downloadFile(url, filename):
    if os.path.isfile(filename) is not True:
        raw = requests.get(url, stream=True)
        with open(filename, 'wb') as fd:
            for chunk in raw.iter_content(chunk_size=1024):
                fd.write(chunk)

def createText(content, filename):
    with open(filename, 'a', encoding='utf-8') as file:
        file.writelines(content + '\n\n')

# PT:Chủ đề, SRC:Nguồn, B:Người sưu tầm
# MUC: Các mục nhỏ trên dantri như 'the-gioi', 'kinh-doanh', 'bat-dong-san', 'the-thao', 'the-gioi/quan-su', 'the-thao/bong-da-chau-au'
def dantri_crawl(start, end, MUC, PT, SRC, B):
    cnt = start
    base = 'https://dantri.com.vn'
    url = 'https://dantri.com.vn/' + MUC
    # XỬ LÝ TỪNG TRANG TRONG MỤC ĐÃ CHỌN
    while(cnt <= end):
        # Lấy source code của trang
        pageUrl = url + '/trang-' + str(cnt) + '.htm'
        print(pageUrl)
        r = requests.get(pageUrl)
        soup = BeautifulSoup(r.content, 'html.parser')

        # XỬ LÝ CÁC BÀI VIẾT TÌM THẤY TRONG TRANG
        for BaiViet in soup.findAll('a', {'class': 'news-item__avatar'}):
            LinkBaiViet = base + BaiViet.get('href')
            print(LinkBaiViet)
            rbv = requests.get(LinkBaiViet)
            soupBaiViet = BeautifulSoup(rbv.content, 'html.parser')

            # THỜI GIAN XUẤT BẢN CỦA BÀI VIẾT
            date = soupBaiViet.find('span', {'class': 'dt-news__time'})
            if date is None:
                continue
            date = date.get_text().split('/')
            yyyy = date[2][0:4] # NĂM
            mm = date[1][0:3] # THÁNG
            dd = date[0][-2] + date[0][-1] # NGÀY
            hh = date[2][-5] + date[2][-4] # GIỜ
            mnmn = date[2][-2] + date[2][-1] # PHÚT

            # MỘT PHẦN TÊN CỦA CÁC FILE
            filename = PT + SRC + yyyy + mm + dd + hh + mnmn

            # PHẦN TEXT HEADER
            header1 = soupBaiViet.find('h1')
            header2 = soupBaiViet.find('h2')
            THIS_IS_THE_MOST_STUPID_WAY_TO_CLEAN_TEXT = soupBaiViet.find('div', {'class': 'dt-news__content'})
            if header1 is None or header2 is None or THIS_IS_THE_MOST_STUPID_WAY_TO_CLEAN_TEXT is None :
                continue
            header1_text = header1.get_text().strip()
            header2_text = header2.get_text().strip()
            while THIS_IS_THE_MOST_STUPID_WAY_TO_CLEAN_TEXT.figure is not None:
                THIS_IS_THE_MOST_STUPID_WAY_TO_CLEAN_TEXT.figure.decompose()

            # AUDIO VÀ FILE TEXT TƯƠNG ỨNG
            sequence = LinkBaiViet.split('-')[-1].split('.')[0]
            R = ['0', '0', '3'] # VÙNG
            S = ['0', '1', '1'] # GIỚI TÍNH
            A = '0' # KHOẢNG ĐỘ TUỔI

            for i in range(1, 2):
                AudioName = filename + R[i] + S[i] + A + B + '.mp3' # Tên file audio
                AudioLink = 'https://acdn.dantri.com.vn/{}/{}/{}/{}/full_{}.mp3'.format(int(yyyy), int(mm), int(dd), sequence, i + 1)
                downloadFile(AudioLink, AudioName)
                textName = filename + R[i] + S[i] + A + B + '.txt'
                if os.path.isfile(textName) is not True:
                    createText(header1_text, textName)
                    createText(header2_text, textName)
                    for p in THIS_IS_THE_MOST_STUPID_WAY_TO_CLEAN_TEXT.findAll('p'):
                        createText(p.get_text().strip(), textName)
        cnt = cnt + 1

path = 'D:\\Nghiencuukhoahoc\\Day 1\\0.Original\\dan-tri-kinh-doanh'
if(os.path.isdir(path) == False):
    os.mkdir(path)
os.chdir(path)
dantri_crawl(1, 10,'lao-dong-viec-lam', '00', '005', '1') # Trang 1 - Trang 2, the-gioi, tin tức, Dân Trí, Dương

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