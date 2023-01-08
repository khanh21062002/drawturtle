import math
a, b = [int(x) for x in input().split()]
if a!=0:
    print(round(-b/a, 2))
else:
    if b==0:
        print("INF")
    else:
        print("NO")
