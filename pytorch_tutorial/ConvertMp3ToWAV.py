import subprocess
import os

def mp3_wav_convert(i_path, o_path):
    command = 'D:\\Nghiencuukhoahoc\\Day 1\\0.Original\\vov\\ffmpeg.exe'
    for file in os.listdir(i_path):
        if file.endswith('mp3'):
            input = i_path + '\\' + file
            output = o_path + '\\' + file.replace('mp3', 'wav')
            subprocess.run('{} {} -ar 16k {}'.format(command, input, output), shell=True)

i_path = 'D:\\Nghiencuukhoahoc\\Day 1\\0.Original\\waw\\Processing\\Turn2'
o_path = 'D:\\Nghiencuukhoahoc\\Day 1\\0.Original\\waw\\1-Raw1\\Turn2'

if(os.path.isdir(o_path) == False):
    os.mkdir(o_path)
os.chdir(o_path)
mp3_wav_convert(i_path, o_path)
