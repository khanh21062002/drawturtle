# month = int(input())
# year = int(input())
month, year = [int(x) for x in input().split()]

def nam_nhuan(year):
       if (year % 400==0) or ((year % 4 == 0) and (year % 100 != 0)):
          return True
       else:
          return False
# def thang_2(month,year):
#     if month == 2:
#         if nam_nhuan(year) is True:
#             return True
#         else:
#             return False
#     else:
#         return False
if ( month >= 1 and month <=12) and (year >0 and year <= 100000):
    if nam_nhuan(year) is True :
        if month == 4 or month == 6 or month ==9 or month ==11:
            print("30")
        if month ==1 or month == 3 or month ==5 or month ==7 or month ==8 or month == 10 or month ==12 :
            print("31")
        if month ==2:
            print("29")
    else:
        if month == 4 or month == 6 or month ==9 or month ==11:
            print("30")
        if month ==1 or month == 3 or month ==5 or month ==7 or month ==8 or month == 10 or month ==12 :
            print("31")
        if month ==2:
            print("28")
else:
    print("INVALID")






