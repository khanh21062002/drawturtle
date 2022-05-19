import torch
a = torch.rand(2,2)
b = torch.rand(2,2)
x =torch.tensor([2.5,0.1])
z =a + b
m = a - b
n = a / b
o = a*b
z = torch.add(a,b)
m = torch.sub(a,b)
n = torch.div(a,b)
o = torch.mul(a,b)
y = torch.rand(5,3)
print(y)
print(y[1,1].item())