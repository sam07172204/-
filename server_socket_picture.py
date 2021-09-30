import time
import threading
import socket
import sys
import os
import struct
import datetime
from datetime import datetime, timedelta

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind(('140.116.39.241', 5555))

s.listen(5)
#s.settimeout(2)
print('Server socket accept')
save_path = ''
SIZE = 1024
while True:
    sock, addr = s.accept()
    #sock.settimeout(2)
    receive = sock.recv(SIZE)#讀取CLIENT傳送之資料
    img_name_length = struct.unpack('i',receive[0:4])[0]#圖片長度
    img_name = struct.unpack((str(img_name_length) + 's'),receive[4:4+img_name_length])[0].decode('utf-8')#圖片名稱
    data_length = struct.unpack('i',receive[4+img_name_length:8+img_name_length])[0]#圖片資訊
    print("Server got:\r\n	img_name_length: {0}\r\n	img_name: {1}\r\n	data_length: {2}".format(img_name_length, img_name, data_length))
    start_rec_time = time.time()

    if data_length > 0:
      save_path = 'C:/Users/sam/Desktop/saved_images/'+ img_name
      data = bytearray()
      count = len(receive[8+img_name_length:])#將圖片資訊組合
      data = receive[8+img_name_length:]
      while (count < data_length):
          data += sock.recv(SIZE)
          count = len(data)
      f = open(save_path, 'wb')
      f.write(data)#將圖片寫入資料夾
      f.close()
s.close()
