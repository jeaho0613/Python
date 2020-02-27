import requests
from bs4 import BeautifulSoup

source = requests.get('https://www.naver.com/').text
soup = BeautifulSoup(source, 'html.parser')
# 현재는 span.ah_k 태그를 받아 올 수가 없다..
hotKeys = soup.select('span.ah_k') 
print(hotKeys)