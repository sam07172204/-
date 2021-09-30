import socket
import os
import datetime
from datetime import datetime, timedelta
import time
import struct

host = '192.168.31.84'
port = 5555

lastminute_new = datetime.now()

while True:
	tmp_photo = os.listdir('C:/Users/SAM/Desktop/456/')#讀圖片資料夾
	#time.sleep(0.05)
	if (len(tmp_photo) > 0):
		print('len(tmp_photo): {0}'.format(len(tmp_photo)))#印出資料夾裡面有幾個檔案

		time.sleep(1)
		for i in range(len(tmp_photo)):
			photo_dir = 'C:/Users/SAM/Desktop/456/' + str(tmp_photo[i])
			img_name = str(tmp_photo[i])
			img_name_length = len(img_name)
			print('Client send img_name: {0}'.format(img_name))#檔名
			f = open(photo_dir, 'rb')
			data = f.read()#讀圖片資訊
			data_length = f.tell()#位置
			f.close()
			os.remove(photo_dir)#讀完後刪除
			pack_img_name_length = struct.pack('i', img_name_length)#將圖片長度變成二進位
			form_1 = str(img_name_length) + 's'
			pack_img_name = struct.pack(form_1, img_name.encode('utf-8'))#將圖片名稱變成二進位
			pack_data_length = struct.pack('i', data_length)#將圖片資訊變成二進位
			info = pack_img_name_length + pack_img_name + pack_data_length
			fhead = info + data
			print('Client send fhead')
			s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
			s.connect((host, port))
			s.send(fhead)
			s.close()
