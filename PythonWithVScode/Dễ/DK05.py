import math
n = float(input())
if abs(n)<=10**12:
    b = int(math.sqrt(n))
    if b**2 == n:
        print("YES")
    else:
        print("NO")
else:
    print("NO")

