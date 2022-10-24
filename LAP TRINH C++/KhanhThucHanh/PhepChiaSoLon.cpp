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

long long big_mod(solon a, solon b)
{
    long long res = 0;
    for (int i = 0; i < a.size(); ++i){
        for(int j = 0; j < b.size(); ++j){
            res = (res * 10 + (a[i] - '0')) % (b[j] - '0');
        }
    }
    return res;
}

solon qoutient(solon A, solon B)
{
    long long hold = 0, s = 0;
    solon res;

    for (int i = 0; i < A.size(); ++i)
    {
        for(int j = 0; j < B.size(); ++j){
            hold = hold * 10 + (A[i] - '0');
            s = hold / (B[j] - '0');
            hold %= (B[j] - '0');
            res = res + (char)(s + 48); // Thêm kết quả chia vào bên phải kết quả cuối.
        }
    }
    
    // Xóa các chữ 0 vô nghĩa ở bên trái kết quả cuối.
    while (res.size() > 1 && res.front() == '0')
        res.erase(res.begin());

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
        output(qoutient(a,b));
        cout << ' ' << big_mod(a,b) << endl;
    }
    return 0;
}