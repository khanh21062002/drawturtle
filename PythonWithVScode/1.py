print('Máy tính bỏ túi đơn giản.')

a, b = input('Nhập vào giá trị a, b cần tính: ').split()
a = float(a); b = float(b)

print('Tổng của', a, '+', b, '=', round(a+b,2))
print('Hiệu của', a, '-', b, '=', round(a-b,2))
print('Tích của', a, '*', b, '=', round(a*b,2))

if b != 0:
    print('Thương của', a, '/', b, '=', round(a/b,2))
else:
    print('Thương của', a, '/', b, '= Math Error')