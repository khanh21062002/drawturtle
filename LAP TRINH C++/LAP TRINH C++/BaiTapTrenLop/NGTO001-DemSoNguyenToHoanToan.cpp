#include <bits/stdc++.h>

using namespace std;
bool check1(int n){
    while(n!=0){
        int k = n%10;
        if(k!=2 && k!=3 && k!=5 && k!=7) return false; 
        n/=10;
    }
    return true;
}
int main(){
    int N = 1e6;
  bool check[N + 1];
  for (int i = 2; i <= N; i++) {
    check[i] = true;
  }
  check[0] = false;
  check[1] = false;
  for (int i = 2; i*i <= N; i++) {
    if (check[i] == true) {
      for (int j = i * i; j <= N; j += i) {
        check[j] = false;
      }
    }
  }
    int t; cin >>t;
   	while(t--){
        int n, cnt=0;
        cin >>n;
        for(int i = 1; i <= n; ++i){
            if(check[i]== true && check1(i)==true) cnt++;
        }
        cout <<cnt <<endl;

    }
}