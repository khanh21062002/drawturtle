a, b = [int(x) for x in input().split()]
if a >= -1000 and b <= 1000 and a<=b:
    for i in range(a,b+1):
      print(i)