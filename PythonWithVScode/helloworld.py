#Initial list
res = []

# Input lengths
lengths = int(input())

# Add element
for i in range(lengths):
    # Input elements
    n = int(input())
    res.append(n)


def evenNum(res):
    answer = []
    for v in res:
      if v % 2==0:
        answer.append(v)
    return answer
print(evenNum(res)) 
