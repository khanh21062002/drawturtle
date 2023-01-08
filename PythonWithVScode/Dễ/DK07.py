import math
a, b, c = [int(x) for x in input().split()]
delta = b**2 - 4*a*c
if a==0:
    if b==0:
        if c==0:
            print("INF")
        else:
            print("NO")
    elif b>0:
        print(round(-c/b, 2))
    elif b<0:
        if c==0:
            b = abs(b)
            print(round(-c/b,2))
        elif c!=0:
            print(round(-c/b,2))
else:
    if delta >0:
        x1 = ((-b - math.sqrt(delta)) / (2 * a))
        x2 = ((-b + math.sqrt(delta)) / (2 * a))
        if x1>x2:
            print(x2,x1)
        else:
            print(x1,x2)
    elif delta <0:
        print("NO")
    else:
        print(round(-b/(2*a),2))