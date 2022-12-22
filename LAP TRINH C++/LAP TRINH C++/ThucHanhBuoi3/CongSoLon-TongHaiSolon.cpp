#include<bits/stdc++.h>
using namespace std;

typedef string solon;
void input(solon &num)
{
    cin >> num;
}

void output(solon num)
{
    cout << num;
}

void equal_length(solon &a, solon &b)
{
    while (a.size() < b.size())
        a = '0' + a;
    while (b.size() < a.size())
        b = '0' + b;
}

solon add(solon a, solon b)
{
    equal_length(a, b);
    
    int carry = 0;
    solon res;
    for (int i = a.size() - 1; i >= 0; --i)
    {
        
        int d = (a[i] - '0') + (b[i] - '0') + carry;
        
        carry = d / 10; 
        res = (char)(d % 10 + '0') + res; 
    }
    
    if (carry)
        res = '1' + res;
        
    return res;
}

int main(){
    int t;
    cin >> t;
    while (t--)
    {
        string a,b;
        input(a);
        input(b);
        output(add(a,b));
        cout << endl;
    }
    
}