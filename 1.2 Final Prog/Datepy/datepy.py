import csv
import re
import os


def function(args):
	f = open(args)
	for line in f:
		data_line = line.rstrip().split(',')
		data = list(data_line)

		date = data[0].split('T')
		ymd = date[0].split('-')
		nextdate = date[1].split(':')
		nextdate2 = nextdate[2].split('.')
		mlsecAtzh = nextdate2[1]
		result = [int(d) for d in re.findall(r'-?\d+', mlsecAtzh)]

		year = int(ymd[0])
		month = int(ymd[1])
		day = int(ymd[2])
		hour = int(nextdate[0])
		minu = int(nextdate[1])
		sec = int(nextdate2[0])
		milsec = int(result[0])
		tzhour = int(result[1])
		tzmin = int(nextdate[3])

		dateTotal = year
		dateTotal = (dateTotal << 4) | month
		dateTotal = (dateTotal << 5) | day
		dateTotal = (dateTotal << 5) | hour
		dateTotal = (dateTotal << 6) | minu
		dateTotal = (dateTotal << 6) | sec
		dateTotal = (dateTotal << 10) | milsec

		if(tzhour < 0):
			tzhour = tzhour * -1 
			dateTotal = (dateTotal << 1) | 0
			dateTotal = (dateTotal << 5) | (tzhour)
			dateTotal = (dateTotal << 6) | tzmin
		else:
			dateTotal = (dateTotal << 1) | 1
			dateTotal = (dateTotal << 5) | tzhour
			dateTotal = (dateTotal << 6) | tzmin

		mintemp = int(data[1])
		maxtemp = int(data[2])
		pre = int(data[3])

		tempTotal = pre
		tempTotal = (tempTotal << 7) | maxtemp 
		tempTotal = (tempTotal << 7) | mintemp 

		dataTotal = [[dateTotal, tempTotal]]

		file = open('Output.csv', 'a',newline='')
		with file: 
			writer = csv.writer(file)
			writer.writerows(dataTotal)

file_open=str(os.getcwd()+'\\DateTime.csv')
function(file_open)

