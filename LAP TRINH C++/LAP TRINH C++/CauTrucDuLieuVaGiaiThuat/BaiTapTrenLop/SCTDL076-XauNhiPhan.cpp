#include <bits/stdc++.h>
#define ll long long
#define faster() ios_base::sync_with_stdio(false),cin.tie(0),cout.tie(0);
const int MOD=1e9+7;

using namespace std;

string cvt1(ll n){
    string s="";
    while(n){
        if(n%2) s = "1" + s;
        else s = "0" + s;
        n/=2;
    }
    return s;
}
string addBinary(string binary1, string binary2) {
    while (binary1.length() < binary2.length()) {
        binary1 = "0" + binary1;
    }

    while (binary2.length() < binary1.length()) {
        binary2 = "0" + binary2;
    }

    int carry = 0;
    string result = "";

    for (int i = binary1.length() - 1; i >= 0; i--) {
        int sum = (binary1[i] - '0') + (binary2[i] - '0') + carry;
        if (sum == 0) {
            result = "0" + result;
            carry = 0;
        } else if (sum == 1) {
            result = "1" + result;
            carry = 0;
        } else if (sum == 2) {
            result = "0" + result;
            carry = 1;
        } else if (sum == 3) {
            result = "1" + result;
            carry = 1;
        }
    }

    return result;
}
int main()
{
    faster();
    int t;
	cin >> t;
	while(t--){
        int n, k;
        cin >>n >>k;
        string s;
        cin >>s;
        string tmp = cvt1(k+1);
        cout <<addBinary(tmp,s);
        cout <<'\n';
	}
   return 0;
}