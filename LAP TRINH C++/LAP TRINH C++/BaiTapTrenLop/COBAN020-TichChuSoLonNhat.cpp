#include<bits/stdc++.h>

using namespace std;

long long TichChuSo(long long n){
    int product = 1;
    while (n > 0)
    {
        int temp = n % 10;
        product = product * temp;
        n = n / 10;
    }
    return product;
}

int main()
{
    int t;
    cin >> t;
    while (t--)
    {
        long long n;
        cin >> n;
        long long TichLonNhat = 0;
        if( n <= 9)
        {
            TichLonNhat = 9;
        }
        if( n > 9)
        {
            for(int i = 10 ; i <=n ; i++)
            {
                long long tichchuso = TichChuSo(i);
                if(tichchuso > TichLonNhat){
                  TichLonNhat = tichchuso;
                }
            }
        }
        cout << TichLonNhat << endl;
    }
    return 0;
}