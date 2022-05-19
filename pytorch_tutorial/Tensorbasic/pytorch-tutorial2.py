from re import X
import torch
import numpy as np
a = torch.ones(5)
print(a)
b = a.numpy()
print(b)
x = np.ones(5)
print(x)
y = torch.from_numpy(x)
print(y)
